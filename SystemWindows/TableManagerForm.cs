using Common;
using DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemWindows;

namespace SystemWindows
{
    public partial class TableManagerForm : Form
    {

        public TableManagerForm(string tableMode)
        {
            InitializeComponent();
            TableMode = tableMode;
        }
        public TableManagerForm(string tableMode, string tableName,string templateName, bool isCopy = false) :this(tableMode)
        {
            TableName = tableName;
            TemplateName = templateName;
            IsCopy = isCopy;
        }
        public string TableMode;
        public string TableName;
        public string TemplateName;
        private bool IsCopy = false;
        DatabaseHelper.DBHelper helper = DatabaseHelper.DBHelper.GetDBHelper();// (DatabaseHelper.EnumDatabaseType.Access);
        private void TableManagerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSize = true;
            this.cbox_Mode.Items.AddRange(TableField.FieldTypeList.Keys.Select(k=>k.ToString()).ToArray());
            this.cbox_Mode.SelectedIndex = 0;
            if (TableName != null)
            {
                TableField[] fields = helper.GetTableFields(TableName).GetTableFields(false);
                {
                    foreach(TableField field in fields)
                    {
                        Type colType = typeof(string);
                        switch(field.Type)
                        {
                            case "int":
                                colType = typeof(int);
                                break;
                            case "float":
                                colType = typeof(float);
                                break;
                            case "smalldatetime":
                                colType = typeof(DateTime);
                                break;
                            case "image":
                                colType = typeof(object);
                                break;
                        }
                        DataGridViewColumn col = new DataGridViewColumn();

                        col.Name = field.Name;
                        col.ValueType = colType;
                        col.Tag = field;
                        dataGridView1.Columns.Add(col);
                    }
                }
            }

            dataGridView1.SelectionChanged += (obj, ee) =>
              {
                  if (dataGridView1.SelectedColumns.Count > 0)
                  {
                      cb_NoNull.Checked = false;
                      tb_Caption.Text = dataGridView1.SelectedColumns[0].HeaderText;
                      var kv = TableField.FieldTypeList.Where(t => t.Value.ToString() == dataGridView1.SelectedColumns[0].ValueType.ToString()).FirstOrDefault();
                      cbox_Mode.Text =(default(KeyValuePair<string, string>).Equals(kv) ? TableField.FieldTypeList.FirstOrDefault().Key: kv.Key).ToString();
                      if(dataGridView1.SelectedColumns[0].Tag !=null)
                      {
                          TableField tf = dataGridView1.SelectedColumns[0].Tag as TableField;
                          cb_NoNull.Checked = tf.NoNull;
                          cb_IsSearched.Checked = tf.IsSearched;
                          kv = TableField.FieldTypeList.Where(t => t.Value.ToString() == tf.Type).FirstOrDefault();
                          cbox_Mode.Text = (default(KeyValuePair<string, string>).Equals(kv) ? TableField.FieldTypeList.FirstOrDefault().Key : kv.Key).ToString();
                      }
                  }
              };
            //this.tb_Caption.Validating += (obj, ee) =>
            //{
            //        const string IllegalChars = "\"',.，。”‘;；\\、!！@#￥%……&*（）(?)/|<>-_+=";
            //        string content = tb_Caption.Text;
            //        if (IllegalChars.Any(c => content.Contains(c)))
            //        {
            //            SystemSounds.Beep.Play();
            //            MessageBox.Show("您输入的内容含有非法字符，如:\r\n" + IllegalChars);
            //            ee.Cancel = true;
            //        }
            //};
        }

        //1、添加表字段
        //alter table 表名 add  字段名 类型(值)
        //示例：alter table   user  add name varchar(40);

        //2、删除表字段
        //alter table 表名 drop  字段名
        //示例： alter table user drop name; 

        //3、字段名更名
        //alter table 表名 rename 老字段名 to 新字段名
        //示例：alter table  user  rename oldname to newname;

        //4、更改字段类型
        //alter table 表名 alter 字段 类型;
        //示例：alter table user alter name varchar(50);


        List<TableField> AddCol = new List<TableField>();
        List<TableField> DelCol = new List<TableField>();
        private void bt_Save_Click(object sender, EventArgs e)
        {
            if (tb_Caption.Text == "" )
            {
                MessageBox.Show("请输入字段名");
                return;
            }
            TableField tf = new TableField()
            {
                Name = tb_Caption.Text,
                Type = TableField.FieldTypeList[(TableFieldType)Enum.Parse(typeof(TableFieldType),cbox_Mode.Text)],
                IsSearched = cb_IsSearched.Checked,
                NoNull = cb_NoNull.Checked
            };
            
            if(AddCol.Any(c=>c.Name== tf.Name))
            {
                AddCol = AddCol.Where(c => c.Name != tf.Name).ToList();
                //MessageBox.Show("此列已存在！");
                //tb_Caption.Text = "";
                //tb_Caption.Text = "";
                //cbox_Mode.SelectedIndex = 0;
                //cb_NoNull.Checked = false;
                //cb_IsSearched.Checked = false;
                //return;
            }
            AddCol.Add(tf);
            DataGridViewColumn col = new DataGridViewColumn();

            Type colType = typeof(string);
            TableFieldType fieldType = (TableFieldType)Enum.Parse(typeof(TableFieldType), cbox_Mode.Text);
            switch (fieldType)
            {
                case TableFieldType.整型:
                    colType = typeof(int);
                    break;
                case  TableFieldType.小数:
                    colType = typeof(float);
                    break;
                case  TableFieldType.日期时间:
                    colType = typeof(DateTime);
                    break;
                case  TableFieldType.文档:
                    colType = typeof(object);
                    break;
                case TableFieldType.布尔:
                    colType = typeof(bool);
                    break;
                default:
                    colType = typeof(string);
                    break;
            }

            
            col.HeaderText = tb_Caption.Text;
            col.Name = tb_Caption.Text;
            col.ValueType = colType;
            col.Tag = tf;
            if (dataGridView1.Columns.Contains(tb_Caption.Text))
            {
                TableField field = new TableField()
                {
                    Name = dataGridView1.SelectedColumns[0].Name,
                    Type = dataGridView1.SelectedColumns[0].ValueType.ToString(),
                    NoNull = false,
                };
                DelCol.Add(field);
                int index = dataGridView1.Columns[tb_Caption.Text].Index;
                dataGridView1.Columns.RemoveAt(index);
                dataGridView1.Columns.Insert(index, col);
            }
            else
                dataGridView1.Columns.Add(col);
            tb_Caption.Text = "";
            tb_Caption.Text= "";
            cbox_Mode.SelectedIndex = 0;
            cb_NoNull .Checked= false;
        }
        private TableField[] GetTableFields()
        {
            List<TableField> fields = new List<TableField>();
            foreach(DataGridViewColumn col in dataGridView1.Columns)
            {
                fields.Add(col.Tag as TableField);
            }
            return fields.ToArray();
        }
        private void TableManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (dataGridView1.Columns.Count == 0)
            {
                this.DialogResult = DialogResult.Cancel;
                return;
            }
            if (MessageBox.Show("是否保存？", "保存", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (helper.ExistTable(TableName))
                {
                    helper.DeleteTable(TableName);
                }
                helper.CreateTable(TableMode, TableName, "", GetTableFields(),false);
                this.DialogResult = DialogResult.OK;
            }
            else
                this.DialogResult = DialogResult.Cancel;
            return;
            EditNameForm tnf = new EditNameForm(TableName) { LabelTableName = "报表名称", TemplateName = TemplateName  };
            while (true)
            {
                if (tnf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (helper.ExistTable(tnf.TableName))
                    {
                        if (IsCopy)
                        {
                            MessageBox.Show("复制的新表不能与原表重名，请重新命名");
                            continue;
                        }
                        else
                        {
                            helper.DeleteTable(tnf.TableName);
                        }
                        //helper.DelColumns(tnf.TextBoxValue, DelCol.ToArray());
                        //helper.AddColumns(tnf.TextBoxValue, AddCol.ToArray());
                    }
                    if (Directory.Exists(Application.StartupPath + "\\RDLC"))
                    {
                        if (Directory.GetFiles(Application.StartupPath + "\\RDLC").Select(f => Path.GetFileNameWithoutExtension(f)).Contains(tnf.TemplateName))
                        {
                            helper.CreateTable(TableMode, tnf.TableName, tnf.TemplateName, GetTableFields());
                            this.DialogResult = DialogResult.OK;
                            return;
                        }
                        else
                            if (MessageBox.Show("没有找到此报表的模板，将无法使用报表导出功能，请确认！", "确认", MessageBoxButtons.YesNo) == DialogResult.No)
                            continue;
                    }
                    helper.CreateTable(TableMode, tnf.TableName, GetTableFields());
                    this.DialogResult = DialogResult.OK;
                    return;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }
            }
        }

        private void bt_Del_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedColumns.Count > 0)
            {
                TableField tf = new TableField()
                {
                    Name = dataGridView1.SelectedColumns[0].Name,
                    Type = dataGridView1.SelectedColumns[0].ValueType.ToString(),
                    NoNull = false,
                };
                var addField = this.AddCol.Where(t => t.Name == tf.Name).FirstOrDefault();
                if(addField!=null)
                {
                    AddCol.Remove(addField);
                }
                this.DelCol.Add(tf);
                dataGridView1.Columns.Remove(dataGridView1.SelectedColumns[0].Name);
            }
            else
            {
                MessageBox.Show("请选择要删除的列！");
                return;
            }
        }

        private void bt_Right_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedColumns.Count>0)
            {
                foreach (DataGridViewColumn col in dataGridView1.SelectedColumns)
                {
                    var colNew = (DataGridViewColumn)col.Clone();
                    colNew.ValueType = col.ValueType;
                    if (col.Index + 2 > dataGridView1.Columns.Count - 1)
                        continue;
                    dataGridView1.Columns.Insert(col.Index + 2, colNew);
                    dataGridView1.Columns.Remove(col);
                    dataGridView1.ClearSelection();
                    colNew.Selected = true;
                    //if (col.Index < dataGridView1.ColumnCount)
                    //{
                    //    dataGridView1.Columns.Remove(col);
                    //    dataGridView1.Columns.Insert(col.Index + 2, col);
                    //}
                    //dataGridView1.Columns[""].SetOrdinal(1);
                    //if (col.DisplayIndex < dataGridView1.ColumnCount-1)
                    //col.DisplayIndex++;
                }
            }
        }

        private void bt_Left_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedColumns.Count > 0)
            {
                foreach (DataGridViewColumn col in dataGridView1.SelectedColumns)
                {
                    var colNew = (DataGridViewColumn)col.Clone();
                    colNew.ValueType = col.ValueType;
                    dataGridView1.Columns.Insert(col.Index - 1, colNew);
                    dataGridView1.Columns.Remove(col);
                    dataGridView1.ClearSelection();
                    colNew.Selected = true;
                }
                
            }
        }
    }
}

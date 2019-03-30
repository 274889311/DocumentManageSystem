using DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemWindows
{
    public partial class UserManagerForm : Form
    {
        public UserManagerForm()
        {
            InitializeComponent();
        }
        private void UserManagerForm_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Enabled = false;
            dataGridView1.AutoSize = true;
            dataGridView1.DataSourceChanged += (obj, ee) =>
              {
                  if (dataGridView1.Columns.Contains("ID"))
                      dataGridView1.Columns["ID"].Visible = false;
              };
            dataGridView1.SelectionChanged += (obj, ee) =>
              {
                  if (dataGridView1.SelectedRows.Count > 0)
                  {
                      foreach (DataGridViewColumn col in dataGridView1.Columns)
                      {
                          AddTextField(flowLayoutPanel1, col.Name, col.ValueType, dataGridView1.SelectedRows[0].Cells[col.Name].Value.ToString());
                      }
                  }
              };
            dataGridView1.DataSource = DatabaseHelper.DBHelper.GetDBHelper().GetDataFromTable(0, 0, "用户表", "");
        }
        private void AddTextField(FlowLayoutPanel flp, string fieldName, Type type, string fieldValue)
        {
            if (type != typeof(string) && type != typeof(bool)) return;
            var textBox = flp.Controls.Find(fieldName, true).FirstOrDefault();
            if (textBox == null)
            {
                TableLayoutPanel tlp = GetTableLayout();
                Label lb = new Label()
                {
                    Text = fieldName,
                    AutoSize = true,
                    Anchor = AnchorStyles.Right,
                    Margin = new Padding(0, 3, 0, 3)
                };
                Control valueBox;

                if (type == typeof(string))
                    valueBox = new TextBox();
                else
                    valueBox = new CheckBox();
                valueBox.Name = fieldName;
                valueBox.Anchor = AnchorStyles.Left;
                valueBox.Margin = new Padding(0, 3, 0, 3);
                tlp.Controls.Add(lb, 0, 0);
                tlp.Controls.Add(valueBox, 1, 0);
                flp.Controls.Add(tlp);
            }
            else
            {
                if (textBox is TextBox)
                    textBox.Text = fieldValue;
                else if (textBox is CheckBox)
                    ((CheckBox)textBox).Checked = fieldValue.ToLower() == "true" ? true : false;
            }
        }

        /// <summary>
        /// 新建文 本框组件
        /// </summary>
        /// <returns></returns>
        private TableLayoutPanel GetTableLayout()
        {
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Margin = new Padding(0);
            tlp.RowCount = 1;
            tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlp.ColumnCount = 2;
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlp.AutoSize = true;
            tlp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            return tlp;
        }

        private void bt_New_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            flowLayoutPanel1.Enabled = true;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                Control control = flowLayoutPanel1.Controls.Find(col.Name,true).FirstOrDefault();
                if(control!=null)
                {
                    if (control is TextBox)
                        control.Text = "";
                    else if (control is CheckBox)
                        ((CheckBox)control).Checked = false;
                }
            }

        }

        private void bt_Edit_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Enabled = true;
        }

        private void bt_Del_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("确定要删除已选的用户？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    string ID = dataGridView1.SelectedRows[0].Cells["id"].Value.ToString();
                    DatabaseHelper.DBHelper.GetDBHelper().DelDataFromTable("用户表", ID.ToString());
                    dataGridView1.SuspendLayout();
                    dataGridView1.DataSource = DatabaseHelper.DBHelper.GetDBHelper().GetDataFromTable(0, 0, "用户表", "");
                    dataGridView1.ResumeLayout(false);
                    dataGridView1.PerformLayout();
                }
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Save_Click(object sender, EventArgs e)
        {
            var fields = GetTableFields();
            DataTable dt = DBHelper.GetDBHelper().GetDataFromTable(0, 0, "用户表", "用户名=N'" + fields.Where(f => f.Name == "用户名").FirstOrDefault().Value + "'");
            if (dt.Rows.Count > 0)
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    if (MessageBox.Show("此用户已存在！确定要更新此用户的数据吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (DatabaseHelper.DBHelper.GetDBHelper().UpdateTableField("用户表", dt.Rows[0]["ID"].ToString(), fields) > 0)
                        {
                            MessageBox.Show("更新用户成功！");
                        }
                        else
                        {
                            MessageBox.Show("更新用户失败！" + DBHelper.GetDBHelper().GetErrorMesasge());
                            return;
                        }
                    }
                    else
                        return;
                }
                else
                {
                    if (DatabaseHelper.DBHelper.GetDBHelper().UpdateTableField("用户表", dt.Rows[0]["ID"].ToString(), fields) > 0)
                    {
                        MessageBox.Show("更新用户成功！");
                    }
                    else
                    {
                        MessageBox.Show("更新用户失败！" + DBHelper.GetDBHelper().GetErrorMesasge());
                        return;
                    }
                }
            }
            else
            {
                if (DatabaseHelper.DBHelper.GetDBHelper().InsertIntoTable("用户表", fields) > 0)
                {
                    MessageBox.Show("添加用户成功！");
                }
                else
                {
                    MessageBox.Show("添加用户失败！" + DBHelper.GetDBHelper().GetErrorMesasge());
                    return;
                }
            }
            flowLayoutPanel1.Enabled = false;
            dataGridView1.SuspendLayout();
            dataGridView1.DataSource = DatabaseHelper.DBHelper.GetDBHelper().GetDataFromTable(0, 0, "用户表", "");
            dataGridView1.ResumeLayout(false);
            dataGridView1.PerformLayout();
        }
        /// <summary>
        /// 获取当前表字段及其值
        /// </summary>
        /// <returns></returns>
        private TableField[] GetTableFields()
        {
            List<TableField> fields = new List<TableField>();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                Control control = flowLayoutPanel1.Controls.Find(col.Name, true).FirstOrDefault();
                if (control != null)
                {
                    fields.Add(new TableField()
                    {
                        Name = col.Name,
                        Value = (control is TextBox) ? control.Text : (control is CheckBox) ? ((CheckBox)control).Checked.ToString() : ""
                    });
                }
            }
            fields.Add(new TableField()
            {
                Name = "创建时间",
                Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            fields.Add(new TableField()
            {
                Name = "最后登录时间",
                Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            });
            return fields.ToArray();
        }
    }
}

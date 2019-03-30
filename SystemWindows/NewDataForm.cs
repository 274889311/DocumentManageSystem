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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemWindows
{
    
    public partial class NewDataForm : Form
    {
        public NewDataForm()
        {
            InitializeComponent();
            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel2.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel3.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel4.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            flowLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);

            this.pictureBox1.Click += (sender, e) =>
            {
               if (this.pictureBox1.Image != null)
                {  
                   
                   Form frm = new Form();
                  frm.AutoSize = true;
                  frm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                  frm.ShowIcon = false;
                  frm.StartPosition = FormStartPosition.CenterScreen;
               
                    PictureBox pictureBox = new PictureBox();

                    pictureBox.Image = (Image)pictureBox1.Image.Clone();
                    pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                    frm.Controls.Add(pictureBox);
                    frm.ShowDialog();

                }
                
            };
            
            this.Load += (sender, e) =>
              {
                  

                  this.bt_CreateDetail.Text = this.Text + "详细";
                  if (GoalTables.Contains(TableName) && RecordID!="")
                      this.bt_CreateDetail.Visible = true;
              };

            #region 读取报表配置
            string GoalTableString = Common.CommonConfigurationManager.GetAppConfig("评价报表");
            if (GoalTableString == "")
            {
                GoalTables = new string[] {
                                        "施工单位安全生产条件核查表",
                                        "施工单位平安工地考核评价表"
                                            };
                GoalTableString = string.Join(",", GoalTables);
                CommonConfigurationManager.UpdateAppConfig("评价报表", GoalTableString);
            }
            else
            {
                GoalTables = GoalTableString.Split(',');
            }
            #endregion
        }

        IFieldTable CurrentTable = null;
        public string TableName { get; set; }
        public string TableModeName { get; set; }
        string RecordID { get; set; }

        string[] GoalTables ;
        /// <summary>
        /// 重新布局界面
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="table"></param>
        /// <param name="enable"></param>
        public bool CreateFromFieldTable(IFieldTable table, WorkingStateEnum workingState,bool uploadFile)
        {
            RecordID = table.GetFieldValue("ID");
            if (GoalTables.Contains(TableName) && workingState == WorkingStateEnum.View)
            {
                DataDetailFrom ddf = new DataDetailFrom(this.TableModeName) { Text = bt_CreateDetail.Text, Name = this.TableName, DataID = RecordID };
                ddf.ShowDialog();
                this.DialogResult = DialogResult.OK;
                return false;
            }
            TableName = table.TableName;
            pictureBox1.Image = null;
            if (table == null) return false;
            CurrentTable = table;
            lv_Doc.Items.Clear();
            if (uploadFile)
            {
                OpenFileDialogForm ofd = new OpenFileDialogForm();
               if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach( var file in ofd.FileNames)
                    lv_Doc.Items.Add(new ListViewItem(Path.GetFileName(file)) { Name = file });
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    return false;
                }
            }
            var proList = workingState.GetType().GetField(workingState.ToString()).GetCustomAttributes(typeof(Exportable), false).FirstOrDefault();
            this.Text = proList!=null?(proList as Exportable).Name.ToString():"";

            var fields = table.GetTableFields();
            tableLayoutPanel1.SuspendLayout();
            flp_DataField.SuspendLayout();
            if (fields.Where(f => !f.IsExtend).Select(f => f.Name).Any(f => flp_DataField.Controls.Find(f, true).Length == 0))
            {
                flp_DataField.Controls.Clear();
            }
            
            foreach (TableField field in fields)
            {
                if (field.IsExtend)
                {
                    if (field.Name == "ID" && field.Value != "")
                    {
                        var files = DBHelper.GetDBHelper().GetTableRecordDocList(table.TableName, field.Value);
                        if (files.Length > 0)
                        {
                            lv_Doc.Items.AddRange(files.Select(f => new ListViewItem(Path.GetFileName(f)) { Name = f }).ToArray());
                        }
                    }
                    continue;
                }
                AddTextField(flp_DataField, field);

            }
            flp_DataField.ResumeLayout(false);
            flp_DataField.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            return true;
        }
        public void AddFile(string fileName)
        {
            lv_Doc.Items.Add(new ListViewItem(Path.GetFileName(fileName)) { Name = fileName });
        }
        private void AddTextField(FlowLayoutPanel flp, TableField field, bool bSearch = false)
        {
            var lable = flp.Controls.Find("lb_" + field.Name, true).FirstOrDefault();
            var textBox = flp.Controls.Find(field.Name, true).FirstOrDefault();
            if (textBox == null)
            {
                TableLayoutPanel tlp = GetTableLayout();
                Label lb = new Label()
                {
                    Text = field.Name,
                    Name = "lb_" + field.Name,
                    AutoSize = true,
                    MaximumSize = new Size(120, 0),
                    Anchor = AnchorStyles.Right,
                    Margin = new Padding(0, 3, 0, 3)
                };
                Control valueBox;
                if (field.Type == "smalldatetime" ||field.Type == "date")
                {
                    valueBox = new DateTimePicker()
                    {
                        Name = field.Name,
                        Anchor = AnchorStyles.Left,
                        Margin = new Padding(0, 3, 0, 3),
                        Format = DateTimePickerFormat.Custom,
                        CustomFormat = "yyyy-MM-dd",
                        Width = 84
                    };
                }
                else if (field.Type == "image")
                {
                    PictureBox pb = new PictureBox();
                    pb.Click += (sender, e) =>
                          {
                              OpenFileDialogForm ofd = new OpenFileDialogForm();
                              if (ofd.ShowDialog() == DialogResult.OK)
                              {
                                  pb.Image = Image.FromFile(ofd.FileNames.FirstOrDefault());
                              };
                          };
                    valueBox = pb;
                }
                else
                {
                    valueBox = new TextBox()
                    {
                        Name = field.Name,
                        Anchor = AnchorStyles.Left,
                        Margin = new Padding(0, 3, 0, 3),

                    };
                    CreateTextBox(flp_DataField, field, valueBox);
                }
                if (field.Value != "" && !bSearch)
                {
                    valueBox.Text = field.Value;
                    lb.ForeColor = Color.Black;
                }
                else
                {
                    if (field.NoNull)
                        lb.ForeColor = Color.Red;
                }

                
                tlp.Controls.Add(lb, 0, 0);
                tlp.Controls.Add(valueBox, 1, 0);
                flp.Controls.Add(tlp);
            }
            else if (!bSearch)
            {
                textBox.Text = field.Value;
                if (field.NoNull)
                    lable.ForeColor = Color.Red;
                else
                    lable.ForeColor = Color.Black;
            }
            
        }
        private void CreateTextBox(FlowLayoutPanel flp, TableField field,  Control valueBox)
        {
            TextBox tb = valueBox as TextBox;
            if(field.Type == "text")
            {
                tb.Multiline = true;
                tb.Width = tb.Width * 2;
                tb.Height = tb.Height * 2;
            }
            valueBox.Validating += (sender, e) =>
            {
                if (field.Type == "int" || field.Type == "float")
                {
                    const string pattern = @"^[0-9]*$";
                    string content = valueBox.Text;

                    if (!(Regex.IsMatch(content, pattern)))
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("只能输入数字!");
                        e.Cancel = true;
                    }
                }
                //else
                //{
                //    const string IllegalChars = "\"',.，。”‘;；\\、!！@#￥%……&*（）(?)/|<>_+=";
                //    string content = valueBox.Text;
                //    if (IllegalChars.Any(c => content.Contains(c)))
                //    {
                //        SystemSounds.Beep.Play();
                //        MessageBox.Show("您输入的内容{"+ content + "}含有非法字符，如:\r\n" + IllegalChars);
                //        e.Cancel = true;
                //    }
                //}
            };
            
            valueBox.KeyPress += (sender, e) =>
            {
                if (System.Convert.ToInt32(e.KeyChar) == 13)
                {
                    System.Windows.Forms.SendKeys.Send("{tab}");
                    e.Handled = true;
                }
            };
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


        private void lv_Doc_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip cms = new ContextMenuStrip();
                cms.ShowImageMargin = false;
                cms.ShowCheckMargin = false;
                cms.ShowItemToolTips = false;
                cms.Font = this.Font;

                if (lv_Doc.GetItemAt(e.X, e.Y) != null)
                {
                    ToolStripButton itemDel = new ToolStripButton();
                    itemDel.Text = "文档删除";
                    itemDel.Click += (obj, ee) =>
                    {
                        if (MessageBox.Show("您确认要删除选择的文件吗？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            ListViewItem item = lv_Doc.GetItemAt(e.X, e.Y);
                            if (item != null)
                            {
                                lv_Doc.Items.Remove(item);
                                int record = 0;
                                if (int.TryParse(RecordID, out record))
                                {
                                    SaveDocList(record);
                                }
                                else
                                {
                                    SaveDocList();
                                }
                            }
                        }
                    };
                    cms.Items.Add(itemDel);

                    ToolStripButton itemOpen = new ToolStripButton();
                    itemOpen.Text = "文档打开";
                    itemOpen.Click += (obj, ee) =>
                    {
                        string filePath = Path.GetTempPath() + lv_Doc.SelectedItems[0].Text;
                        if (!File.Exists(filePath))
                        {
                            filePath = DBHelper.GetDBHelper().GetDocFromTableRecord(
                           this.TableName,
                           RecordID,
                           lv_Doc.SelectedItems[0].Text);
                        }
                        if (filePath != "") new DocumentViewwerForm(filePath).ShowDialog(); //打开此文件。
                    };
                    cms.Items.Add(itemOpen);
                }
                else
                {
                    ToolStripButton itemUpload = new ToolStripButton();
                    itemUpload.Text = "文档上传";
                    itemUpload.Click += (obj, ee) =>
                    {
                        OpenFileDialogForm ofd = new OpenFileDialogForm();
                        if (ofd.ShowDialog() == DialogResult.OK)
                        {
                            foreach(var file in ofd.FileNames)
                            lv_Doc.Items.Add(new ListViewItem(Path.GetFileName(file)) { Name = file });
                            int record = 0;
                            if (int.TryParse(RecordID, out record))
                            {
                                SaveDocList(record);
                            }
                            else
                            {
                                SaveDocList();
                            }
                        }
                    };
                    cms.Items.Add(itemUpload);
                }
                cms.Show(lv_Doc, e.Location);
            }
        }

        private void lv_Doc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string filePath = Path.GetTempPath() + lv_Doc.SelectedItems[0].Text;
            if (!File.Exists(filePath))
            {
                filePath = DBHelper.GetDBHelper().GetDocFromTableRecord(
                    this.TableName,
                    RecordID,
                    lv_Doc.SelectedItems[0].Text);
            }
            if (filePath != "") new DocumentViewwerForm(filePath).ShowDialog(); //打开此文件。
        }

        private void lv_Doc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv_Doc.SelectedItems.Count > 0)
            {
                string filePath = Path.GetTempPath() + "\\" + lv_Doc.SelectedItems[0].Text;
                if (!File.Exists(filePath))
                {
                    filePath = DBHelper.GetDBHelper().GetDocFromTableRecord(
                        this.TableName,
                        RecordID,
                        lv_Doc.SelectedItems[0].Text);
                }
                try
                {
                    Image img = Image.FromFile(filePath);
                    pictureBox1.Image = (Image)img.Clone();
                    img.Dispose();
                    img = null;
                }
                catch
                {
                    pictureBox1.Image = Properties.Resources.timg;
                }
            }
        }

        private void SaveDocList(int recordID = 0)
        {
            if (recordID > 0)
            {
                UpdateDocList(TableName, recordID);
            }
            else
            {
                if (lv_Doc.SelectedItems.Count > 0 )
                {
                    if (int.TryParse(RecordID, out recordID))
                    {
                        UpdateDocList(TableName, recordID);
                    }
                }
            }
        }
        /// <summary>
        /// 更新文 件列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="recordID"></param>
        private void UpdateDocList(string tableName, int recordID)
        {
            var files = DBHelper.GetDBHelper().GetTableRecordDocList(tableName, recordID.ToString());
            List<string> newFiles = new List<string>();
            foreach (ListViewItem item in lv_Doc.Items)
            {
                newFiles.Add(item.Name);
            }
            //删 除旧文档
            files.Where(f => newFiles.All(nf=>!nf.Contains(f))).ToList().ForEach(f =>
            {
                DBHelper.GetDBHelper().DeleteTableRecordDoc(tableName, recordID, f);
            });
            //添加新文 档
            newFiles.Where(f => !files.Contains(Path.GetFileName(f))).ToList().ForEach(f =>
            {
                DBHelper.GetDBHelper().InsertIntoTableRecordDoc(tableName, recordID, f, UserLogin.UserName);
            });

        }
        private void bt_Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialogForm ofd = new OpenFileDialogForm();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in ofd.FileNames)
                {
                    if (!lv_Doc.Items.ContainsKey(Path.GetFileName(file)))
                    {
                        var tb = flp_DataField.Controls.Find("名称", true).FirstOrDefault();
                        if (tb != null && tb is TextBox)
                        {
                            TextBox box = tb as TextBox;
                            tb.Text = Path.GetFileName(file);
                        }
                        lv_Doc.Items.Add(new ListViewItem(Path.GetFileName(file)) { Name = file });
                        
                    }
                }
                SaveDocList();
            }
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            var dataTable = GetAllFields(flp_DataField);
            
            
            var NoInputFields = dataTable.GetTableFields().Where(f => !f.IsExtend && f.NoNull && f.Value == "").ToArray();
            if (NoInputFields.Length > 0)
            {
                MessageBox.Show("\"" + NoInputFields.FirstOrDefault().Name + "\"没有填入值");
                return ;
            }
            dataTable.UpdateFieldValue("修改人", UserLogin.UserName);
            dataTable.UpdateFieldValue("修改时间", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            int recordID = 0;
            if (CurrentTable.GetTableFields().Where(f => f.Name.ToUpper() == "ID").FirstOrDefault().Value != "")
            {
                recordID = DBHelper.GetDBHelper().UpdateTableField(dataTable);
            }
            else
            {
                recordID = DBHelper.GetDBHelper().InsertIntoTable(dataTable);
            }
            if (recordID > 0)
            {
                UpdateDocList(dataTable.TableName, recordID);
            }
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 获到当前所有文 本框 的值 ，并返回到对象中
        /// </summary>
        /// <returns></returns>
        private IFieldTable GetAllFields(FlowLayoutPanel flp)
        {
            TableField[] fieldList = CurrentTable.GetTableFields().Where(f => !f.IsExtend).ToArray();
            foreach (TableField field in fieldList)
            {
                Control tb = flp.Controls.Find(field.Name, true).FirstOrDefault() as Control;
                if (tb == null) continue;
                if (tb is ComboBox)
                    field.Value = (tb as ComboBox).SelectedValue.ToString();
                //else if(tb is PictureBox)
                //{
                //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                //    {

                //        (tb as PictureBox).Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                //        byte[] byteImage = new Byte[ms.Length];
                //        field.Value = ms.ToArray();
                //        //string strB64 = Convert.ToBase64String(byteImage);
                //    }
                //}
                else
                    field.Value = tb.Text;
                CurrentTable.UpdateFieldValue(field);
            }
            return CurrentTable;
        }

        private void bt_Upload_Click_1(object sender, EventArgs e)
        {
            OpenFileDialogForm ofd = new OpenFileDialogForm();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in ofd.FileNames)
                {
                    lv_Doc.Items.Add(new ListViewItem(Path.GetFileName(file)) { Name = file });
                }
                int record = 0;
                if (int.TryParse(RecordID, out record))
                {
                    SaveDocList(record);
                }
                else
                {
                    SaveDocList();
                }
            }
        }

        private void bt_Open_Click(object sender, EventArgs e)
        {
            if (lv_Doc.SelectedItems.Count == 0) return;
            string filePath = Path.GetTempPath() + lv_Doc.SelectedItems[0].Text;
            if (!File.Exists(filePath))
            {
                filePath = DBHelper.GetDBHelper().GetDocFromTableRecord(
               this.TableName,
               RecordID,
               lv_Doc.SelectedItems[0].Text);
            }
            if (filePath != "") new DocumentViewwerForm(filePath).ShowDialog(); //打开此文件。
        }

        private void bt_Del_Click(object sender, EventArgs e)
        {
            if (lv_Doc.SelectedItems.Count == 0) return;
            if (MessageBox.Show("您确认要删除选择的文件吗？", "确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ListViewItem item = lv_Doc.SelectedItems[0];
                if (item != null)
                {
                    lv_Doc.Items.Remove(item);
                    int record = 0;
                    if (int.TryParse(RecordID, out record))
                    {
                        SaveDocList(record);
                    }
                    else
                    {
                        SaveDocList();
                    }
                }
            }
        }

        private void bt_CreateDetail_Click(object sender, EventArgs e)
        {
            DataDetailFrom ddf = new DataDetailFrom(this.TableModeName) { Text = bt_CreateDetail.Text,Name = this.TableName, DataID = RecordID };
            ddf.ShowDialog();
        }
    }

    public class Exportable : Attribute
    {
        public Exportable(string name)
        {
            Name = name;
        }
        public string Name;
    }
}

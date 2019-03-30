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
    
    public partial class ShowDocForm : Form
    {
        public ShowDocForm()
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
                      pictureBox.Dock = System.Windows.Forms.DockStyle.Fill; 
                      pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                      frm.Controls.Add(pictureBox);
                      frm.ShowDialog();
                  }
                 
              };
        }

        IFieldTable CurrentTable = null;
        public string TableName { get; set; }
        string RecordID { get; set; }
        public bool CreateFromFieldTable(DataTable table, WorkingStateEnum workingState, bool uploadFile)
        {
            TableName = table.TableName;
            this.tableLayoutPanel1.SuspendLayout();
            groupBox1.Visible = false;
            groupBox3.Visible = false;

            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            return true;
        }
            /// <summary>
            /// 重新布局界面
            /// </summary>
            /// <param name="tableName"></param>
            /// <param name="table"></param>
            /// <param name="enable"></param>
            public bool CreateFromFieldTable(IFieldTable table, WorkingStateEnum workingState,bool uploadFile)
        {
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

                    foreach(var file in ofd.FileNames)
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
            DataTable dt = new DataTable();
            List<string> values = new List<string>();
            dt.Columns.AddRange(fields.Where(f => !f.IsExtend || f.Value!="ID").Select(f =>new DataColumn( f.Name)).ToArray());
            dt.Rows.Add(fields.Where(f => !f.IsExtend || f.Value != "ID").Select(f => f.Value).ToArray());
            foreach (TableField field in fields)
            {
                if (field.IsExtend)
                {
                    if (field.Name == "ID" && field.Value != "")
                    {
                        RecordID = field.Value;
                        var files = DBHelper.GetDBHelper().GetTableRecordDocList(table.TableName, field.Value);
                        if (files.Length > 0)
                        {
                            lv_Doc.Items.AddRange(files.Select(f => new ListViewItem(Path.GetFileName(f)) { Name = f }).ToArray());
                        }
                        else
                        {
                            MessageBox.Show("此记录没有文档可浏览！");
                            this.DialogResult = DialogResult.Cancel;
                            return false;
                        }
                        break;
                    }
                }
            }
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            return true;
        }
        public void AddFile(string fileName)
        {
            lv_Doc.Items.Add(new ListViewItem(Path.GetFileName(fileName)) { Name = fileName });
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
                            foreach (var file in ofd.FileNames)
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
                        lv_Doc.Items.Add(new ListViewItem(Path.GetFileName(file)) { Name = file });
                    }
                    //else
                    //{
                    //    MessageBox.Show("文档已添加！");
                    //}
                }
                SaveDocList();
            }
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
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
    }
}

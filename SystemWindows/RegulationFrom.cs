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
    public partial class RegulationFrom : Form
    {
        public RegulationFrom()
        {
            InitializeComponent();
            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            flp_DataField.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);

            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            this.bt_print.Click += new System.EventHandler(this.btnPrint_Click);
            this.bt_PrintPreview.Click += new System.EventHandler(this.btnPrintPreview_Click);
            this.bt_PageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
        }
        #region 打印
        private int checkPrint;
        private void btnPageSetup_Click(object sender, System.EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void btnPrintPreview_Click(object sender, System.EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            checkPrint = 0;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Print the content of RichTextBox. Store the last character printed.
            checkPrint = this.rtb_Regulation.Print(checkPrint, rtb_Regulation.TextLength, e);

            // Check for more pages
            if (checkPrint < rtb_Regulation.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }
        #endregion
        private void bt_Paste_Click(object sender, EventArgs e)
        {
            // Declares an IDataObject to hold the data returned from the clipboard.
            // Retrieves the data from the clipboard.
            IDataObject iData = Clipboard.GetDataObject();

            // Determines whether the data is in a format you can use.
            if (iData.GetDataPresent(DataFormats.Text))
            {
                // Yes it is, so display it in a text box.
                this.rtb_Regulation.Paste();
            }
        }
        private DialogResult FormResult = DialogResult.Cancel;
        public string TableName { get; set; }
        public bool EditEnable { get; set; }
        public string RecordID { get; set; }
        public Stream RegulationFileStream { get; set; }
        public IFieldTable CurrentFieldTable  { get; set; }

        private void bt_Save_Click(object sender, EventArgs e)
        {
            if (rtb_Regulation.Visible && rtb_Regulation.TextLength > 0)
            {
                var dataTable = GetAllFields(flp_DataField);

                var NoInputFields = dataTable.GetTableFields().Where(f => !f.IsExtend && f.NoNull && f.Value == "").ToArray();
                if (NoInputFields.Length > 0)
                {
                    MessageBox.Show("\"" + NoInputFields.FirstOrDefault().Name + "\"没有填入值");
                    return;
                }
                dataTable.UpdateFieldValue("修改人", UserLogin.UserName);
                dataTable.UpdateFieldValue("修改时间", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                //string filePath = Path.GetTempPath() + dataTable.GetFieldValue("制度名称") + ".doc";
                byte[] bytes = rtb_Regulation.SaveToBytes();
                int recordID = DBHelper.GetDBHelper().InsertIntoTable(dataTable);
                string fileName = dataTable.GetTableFields(false, false).Where(f => f.Name.Contains("名称")).Select(f => f.Value).FirstOrDefault();
                if(fileName ==null)
                {
                    fileName = this.TableName + recordID;
                }
                DBHelper.GetDBHelper().InsertIntoTableRecordDoc(this.TableName, recordID, bytes, fileName, UserLogin.UserName);
                MessageBox.Show("保存成功！");
            }
            FormResult = DialogResult.OK;
        }

        private void RegulationFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = FormResult;
        }
        private void RegulationFrom_Load(object sender, EventArgs e)
        {
            if(RegulationFileStream!=null)
            {
                this.rtb_Regulation.OpenWord(RegulationFileStream);
                rtb_Regulation.ReadOnly = !EditEnable;
                bt_Save.Visible = EditEnable;
                bt_Paste.Visible = EditEnable;
            }
            if (CurrentFieldTable == null)
            {
                CurrentFieldTable = DBHelper.GetDBHelper().GetTableFields(this.TableName);
            }
            CreateFromFieldTable(CurrentFieldTable, WorkingStateEnum.Creating);
        }
        private bool CreateFromFieldTable(IFieldTable table, WorkingStateEnum workingState)
        {
            TableName = table.TableName;
            if (table == null) return false;
            var proList = workingState.GetType().GetField(workingState.ToString()).GetCustomAttributes(typeof(Exportable), false).FirstOrDefault();
            this.Text = proList != null ? (proList as Exportable).Name.ToString() : "";

            var fields = table.GetTableFields();
            tableLayoutPanel1.SuspendLayout();
            flp_DataField.SuspendLayout();
            if (fields.Where(f => !f.IsExtend).Select(f => f.Name).Any(f => flp_DataField.Controls.Find(f, true).Length == 0))
            {
                flp_DataField.Controls.Clear();
            }

            foreach (TableField field in fields)
            {
                if (!field.IsExtend)
                {
                    AddTextField(flp_DataField, field);
                }
            }
            flp_DataField.ResumeLayout(false);
            flp_DataField.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            return true;
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
                if (field.Type == "smalldatetime"||field.Type=="date")
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
                valueBox.Enabled = EditEnable;
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
        private void CreateTextBox(FlowLayoutPanel flp, TableField field, Control valueBox)
        {
            TextBox tb = valueBox as TextBox;
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
                //    const string IllegalChars = "\"',.，。”‘;；\\、!！@#￥%……&*（）(?)/|<>-_+=";
                //    string content = valueBox.Text;
                //    if (IllegalChars.Any(c => content.Contains(c)))
                //    {
                //        SystemSounds.Beep.Play();
                //        MessageBox.Show("您输入的内容含有非法字符，如:\r\n" + IllegalChars);
                //        e.Cancel = true;
                //    }
                //}
            };

            valueBox.KeyPress += (sender, e) =>
            {
                if (System.Convert.ToInt32(e.KeyChar) == 13)
                {
                    System.Windows.Forms.SendKeys.Send("{tab}");
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

        private IFieldTable GetAllFields(FlowLayoutPanel flp)
        {
            TableField[] fieldList = CurrentFieldTable.GetTableFields().Where(f => !f.IsExtend).ToArray();
            foreach (TableField field in fieldList)
            {
                Control tb = flp.Controls.Find(field.Name, true).FirstOrDefault() as Control;
                if (tb == null) continue;
                field.Value = tb.Text;
                CurrentFieldTable.UpdateFieldValue(field);
            }
            return CurrentFieldTable;
        }
    }
}

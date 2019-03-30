using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseHelper;
using System.IO;
using Common;
using System.Media;
using System.Text.RegularExpressions;
using SystemWindows;

namespace DMSComponent
{
    public partial class DataEditPanel : UserControl
    {
        public DataEditPanel()
        {
            InitializeComponent();

            //DataWorkingStateChanged += DataEditPanel_DataWorkingStateChanged;
            this.dataGridView1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(dataGridView1, true, null);
            this.Load += DataEditPanel_Load;
            this.DoubleBuffered = true;
        }
        string[] RowReportTables;
        string[] GoalTables;
        string[] RegulationTables;
        private void DataEditPanel_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView1.DataSourceChanged += (obj, ee) =>
            {
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    if (col.ValueType == typeof(DateTime))
                    {
                        if (col.HeaderText == "修改时间")
                            col.DefaultCellStyle.Format = "yyyy/MM/dd HH:mm:ss";
                        else
                            col.DefaultCellStyle.Format = "yyyy/MM/dd";
                    }
                }
                if (dataGridView1.Columns.Contains("ID"))
                {
                    dataGridView1.Columns["ID"].Visible = false;
                }
                if (dataGridView1.Columns.Contains("ShowDoc"))
                    dataGridView1.Columns.Remove("ShowDoc");
                DataGridViewTextBoxColumn colDefault = new DataGridViewTextBoxColumn();
                colDefault.Name = "ShowDoc";
                colDefault.HeaderText = "      ";
                colDefault.DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Underline);
                colDefault.DefaultCellStyle.ForeColor = Color.Blue;
                int colIndex = dataGridView1.Columns.Add(colDefault);
                dataGridView1.Visible = true;
            };

            dataGridView1.CellFormatting += (obj, ee) =>
            {
                if (ee.RowIndex < 0)
                    return;
                if (ee.ColumnIndex >= 0 && dataGridView1.Columns[ee.ColumnIndex].Name == "ShowDoc")
                {
                    dataGridView1.Rows[ee.RowIndex].Cells[ee.ColumnIndex].Value = "浏览";
                }
            };
            dataGridView1.CellContentClick += (obj, ee) =>
          {
              if (ee.ColumnIndex < 0 || ee.RowIndex < 0)
                  return;
              if (dataGridView1.Columns[ee.ColumnIndex].Name == "ShowDoc")
              {
                  DataWorkingState = WorkingStateEnum.Creating;
                  IFieldTable table = GetFieldTable(dataGridView1.Rows[ee.RowIndex]);
                  ShowDocForm ndf = new ShowDocForm();
                  if (ndf.CreateFromFieldTable(table, DataWorkingState, false))
                  {
                      ndf.TableName = table.TableName;
                      if (ndf.ShowDialog() == DialogResult.OK)
                          DataSource = DBHelper.GetDBHelper().GetDataFromTable(0, 0, TableName, "");
                  }
                  this.DataWorkingState = WorkingStateEnum.Search;
              }
          };
            dataGridView1.CellMouseDoubleClick += (obj, ee) =>
              {
                  if (ee.RowIndex < 0) return;
                  if (!RowReportTables.Contains(this.TableName))
                  {
                      if (CreateFromField(dataGridView1.DataSource as DataTable, dataGridView1.Rows[ee.RowIndex], WorkingStateEnum.View, false))
                      {
                          return;
                      }
                  }
                  dataGridView1.Rows[ee.RowIndex].Selected = true;
                  IFieldTable table = GetFieldTable(dataGridView1.SelectedRows[0]);
                  CreateFromField(table, dataGridView1.Rows[ee.RowIndex], WorkingStateEnum.View, false);
              };

            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel2.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel2, true, null);
            this.flp_SearchField.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(flp_SearchField, true, null);

            #region 读取报表配置文件
            string RowReportTableString = Common.CommonConfigurationManager.GetAppConfig("单行报表");
            if(RowReportTableString == "")
            {
                RowReportTables = new string[] {
                                    "安全技术交底表" ,
                                    "安全检查记录表" ,
                                    "安全生产会议记录" ,
                                    "安全生产奖励通知单" ,
                                    "安全教育培训记录表" ,
                                    "安全生产违约处理通知单" ,
                                    "安全隐患整改回复单" ,
                                    "安全隐患整改通知单" ,
                                    "应急救援预案演练记录表" ,
                                    "三级安全教育登记卡",
                                    "生产管理制度",
                                    "设备安全检查记录表"
                                            };
                RowReportTableString = string.Join(",", RowReportTables);
                CommonConfigurationManager.UpdateAppConfig("单行报表", RowReportTableString);
            }
            else
            {
                RowReportTables = RowReportTableString.Split(',');
            }
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

            string RegulationTableString = Common.CommonConfigurationManager.GetAppConfig("制度方案报表");
            if (RegulationTableString == "")
            {
                RegulationTables = new string[] {
                                     "生产管理制度",
                                     "工程专项施工方案",
                                     "临时用电方案"
                };
                RegulationTableString = string.Join(",", RegulationTables);
                CommonConfigurationManager.UpdateAppConfig("制度方案报表", RegulationTableString);
            }
            else
            {
                RegulationTables = RegulationTableString.Split(',');
            }
            #endregion
        }
        private IFieldTable GetFieldTable(DataGridViewRow dataRow)
        {
            IFieldTable table = DBHelper.GetDBHelper().GetTableFields(this.TableName);
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (dataRow.Cells[col.Index].Value!=null)
                    table.UpdateFieldValue(new TableField() { Name = col.Name, Value = dataRow.Cells[col.Index].Value.ToString() });
            }
            return table;
        }
        private void PictureBoxButton_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("人员管理", new Font(this.Font.FontFamily, 15.0f, FontStyle.Bold), new SolidBrush(Color.LightYellow), pb.ClientRectangle, sf);
        }
        private DataTable BaseDataTable = null;
        public DataGridViewSelectedRowCollection SelectedRows { get { return dataGridView1.SelectedRows; } }
        public object DataSource
        {
            get { return dataGridView1.DataSource; }
            set
            {
                BaseDataTable = value as DataTable;
                dataGridView1.SuspendLayout();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = BaseDataTable;
                dataGridView1.ResumeLayout(false);
                dataGridView1.PerformLayout();
            }
        }
        public DataGridViewColumnCollection DataColumns { get { return dataGridView1.Columns; } }
        public string TableName { get; set; }
        public string TableModeName { get; set; }
        public string TemplateName { get; set; }
        IFieldTable CurrentTable = null;
        WorkingStateEnum _DataWorkingState = WorkingStateEnum.Editting;
        public event Action<WorkingStateEnum> DataWorkingStateChanged;
        public event Action<string> QueryEvent;
        /// <summary>
        /// 当前组件的工作状态
        /// </summary>
        public WorkingStateEnum DataWorkingState
        {
            get { return _DataWorkingState; }
            set
            {
                tableLayoutPanel1.SuspendLayout();
                _DataWorkingState = value;
                tableLayoutPanel1.ResumeLayout(false);
                tableLayoutPanel1.PerformLayout();
                if (DataWorkingStateChanged != null)
                    DataWorkingStateChanged.Invoke(_DataWorkingState);
            }
        }
        public bool CreateFromField(DataTable table, DataGridViewRow row, WorkingStateEnum workingState, bool uploadFile)
        {
            var file = Directory.GetFiles("RDLC").Where(f => this.TemplateName.Equals(Path.GetFileNameWithoutExtension(f))).FirstOrDefault();
            if (workingState == WorkingStateEnum.View && file != null)
            {
                ViewDataForm ndf = new ViewDataForm(file);
                if (ndf.CreateFromFieldTable(table, WorkingStateEnum.View, false))
                {
                    ndf.TableName = table.TableName;
                    if (ndf.ShowDialog() == DialogResult.OK)
                        DataSource = DBHelper.GetDBHelper().GetDataFromTable(0, 0, TemplateName, "");
                }
                return true;
            }
            return false;
        }
        public void CreateFromField(IFieldTable table,DataGridViewRow row, WorkingStateEnum workingState, bool uploadFile)
        {
            var file = Directory.GetFiles("RDLC").Where(f =>this.TemplateName.Equals(Path.GetFileNameWithoutExtension( f))).FirstOrDefault();

            if (workingState == WorkingStateEnum.View && file != null)
            {
                ViewDataForm ndf = new ViewDataForm(file);
                if (ndf.CreateFromFieldTable(table, WorkingStateEnum.View, false))
                {
                    ndf.TableName = table.TableName;
                    if (ndf.ShowDialog() == DialogResult.OK)
                        DataSource = DBHelper.GetDBHelper().GetDataFromTable(0, 0, TemplateName, "");
                }
            }
            
            else if (row != null && RegulationTables.Contains(this.TableName))
            {
                string fileName = "";
                foreach(DataGridViewColumn col in row.DataGridView.Columns)
                {
                    if(col.HeaderText.Contains("名称"))
                    {
                        fileName = row.Cells[col.Name].Value.ToString();
                    }
                }
                if (fileName == "")
                    fileName = this.TableName+row.Cells["ID"].Value.ToString();
                
                Byte[] bytes = DBHelper.GetDBHelper().GetDocFromTableRecordBytes(this.TableName, row.Cells["ID"].Value.ToString(), fileName);
                MemoryStream stream = new MemoryStream(bytes);
                RegulationFrom rf = new RegulationFrom() { Text = this.TableName, TableName = this.TableName, EditEnable = false, RegulationFileStream = stream, CurrentFieldTable  = table };
                rf.ShowDialog();
            }
            else
            {
                NewDataForm ndf = new NewDataForm() { TableName = this.TableName,TableModeName = this.TableModeName};
                if (ndf.CreateFromFieldTable(table, workingState, uploadFile))
                {
                    ndf.TableName = table.TableName;
                    if (ndf.ShowDialog() == DialogResult.OK)
                        DataSource = DBHelper.GetDBHelper().GetDataFromTable(0, 0, TableName, "");
                }
            }
        }
        /// <summary>
        /// 重新布局界面
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="table"></param>
        /// <param name="enable"></param>
        public void CreateFromFieldTable(IFieldTable table, WorkingStateEnum workingState)
        {
            this.DataWorkingState = workingState;
            CurrentTable = table;
            var fields = table.GetTableFields();
            tableLayoutPanel1.SuspendLayout();
            flp_SearchField.SuspendLayout();

            flp_SearchField.Controls.Clear();
            foreach (TableField field in fields)
            {
                //添加查询字段
                if (field.IsSearched)
                {
                    AddTextField(flp_SearchField, field, true);
                }
            }
            flp_SearchField.ResumeLayout(false);
            flp_SearchField.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();

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
                if (field.Type == "smalldatetime"|| field.Type =="date")
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
                    #region 下拉列表查询
                    TextBox tb = valueBox as TextBox;
                    tb.GotFocus += (sender, e) =>
                    {
                        ListBox lbx = flp.FindForm().Controls.Find("searchItemList", true).FirstOrDefault() as ListBox;
                        if (lbx == null)
                        {

                            lbx = new ListBox();
                            lbx.SuspendLayout();
                            lbx.Visible = false;
                            lbx.BackColor = Color.AliceBlue;
                            lbx.Name = "searchItemList";
                            lbx.Parent = flp.FindForm();
                            lbx.SelectionMode = SelectionMode.One;
                            lbx.DataSourceChanged += (obj, ee) =>
                              {
                                  lbx.BringToFront();
                              };
                            lbx.DisplayMemberChanged += (obj, ee) =>
                              {
                                  lbx.Height = (lbx.Items.Count + 1) * lbx.Font.Height;
                              };
                            lbx.ResumeLayout();
                            lbx.PerformLayout();
                        }
                        lbx.MouseClick += (obj, ee) =>
                            {
                                TextBox box = (lbx.Tag as TextBox);
                                box.Text = lbx.Text;
                                lbx.Dispose();
                                lbx = null;
                                DoSearch();
                            };
                        lbx.SuspendLayout();

                        DataTable gridTable = (DataTable)dataGridView1.DataSource;
                        var dtTemp = gridTable.DefaultView.ToTable();
                        dtTemp.DefaultView.RowFilter = valueBox.Name + " like '%" + valueBox.Text + "%'";
                        lbx.DataSource = dtTemp.DefaultView.ToTable(true, new string[] { valueBox.Name });
                        //lv .ValueMember = "ID";
                        lbx.DisplayMember = valueBox.Name;
                        lbx.Tag = tb;
                        var postion = flp.FindForm().PointToClient(tb.Parent.PointToScreen(tb.Location));
                        lbx.Location = new Point(postion.X, postion.Y + tb.Height);
                        lbx.TopIndex = 0;
                        lbx.ResumeLayout();
                        lbx.PerformLayout();
                        if (!lbx.Visible) lbx.Show();
                    };
                    tb.LostFocus += (sender, e) =>
                    {
                        ListBox lv = flp.FindForm().Controls.Find("searchItemList", true).FirstOrDefault() as ListBox;
                        if (lv != null && !lv.Focused)
                        {
                            lv.Dispose();
                            lv = null;
                        }
                    };
                    #endregion

                    #region 非法字符校验
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
                    #endregion

                    #region 关键字查询
                    if (bSearch)
                    {
                        valueBox.TextChanged += (sender, e) =>
                          {
                              TextBox box = sender as TextBox;
                              DataTable gridTable = (DataTable)dataGridView1.DataSource;
                              var dtTemp = gridTable.DefaultView.ToTable();
                              dtTemp.DefaultView.RowFilter = box.Name + " like '%" + box.Text + "%'";

                              var lbx = flp.FindForm().Controls.Find("searchItemList", true).FirstOrDefault();
                              if (lbx != null)
                              {
                                  ListBox listBox = lbx as ListBox;
                                  listBox.DataSource = dtTemp.DefaultView.ToTable(true, new string[] { box.Name });
                                  listBox.DisplayMember = box.Name;
                              }
                              DoSearch();
                          };

                    }
                    #endregion

                    valueBox.KeyPress += (sender, e) =>
                      {
                          if (System.Convert.ToInt32(e.KeyChar) == 13)
                          {
                              System.Windows.Forms.SendKeys.Send("{tab}");
                              e.Handled = true;
                          }

                      };
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
                field.Value = tb.Text;
                CurrentTable.UpdateFieldValue(field);
            }
            return CurrentTable;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Search_Click(object sender, EventArgs e)
        {
            DoSearch();
        }
        private void DoSearch()
        {
            string strSQL = "";
            var dataTable = GetAllFields(flp_SearchField);
            foreach (var field in dataTable.GetTableFields().Where(f => f.IsSearched).ToArray())
            {
                if (field.Value != "" && field.Type.Contains("varchar"))
                {
                    strSQL += field.Name + " like N'%" + field.Value + "%' and ";
                }
            }
            if (strSQL.Contains("and"))
                strSQL = strSQL.Remove(strSQL.LastIndexOf("and"));
            if (QueryEvent != null)
                QueryEvent.Invoke(strSQL);
        }
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                ContextMenuStrip cms = CreateDataGridViewMenu(e.RowIndex);
                dataGridView1.Rows[e.RowIndex].Selected = true;
                cms.Show(dataGridView1, dataGridView1.PointToClient(MousePosition));
            }
        }

        public void CreateNew(string modeName="")
        {
            DataWorkingState = WorkingStateEnum.Creating;
            if (RegulationTables.Contains(this.TableName))
            {
                RegulationFrom rf = new RegulationFrom() { Text = this.TableName, TableName = this.TableName, EditEnable = true, CurrentFieldTable = DBHelper.GetDBHelper().GetTableFields(this.TableName) };
                if (rf.ShowDialog() == DialogResult.OK)
                {
                    DataSource = DBHelper.GetDBHelper().GetDataFromTable(0, 0, this.TableName, "");
                }
                this.DataWorkingState = WorkingStateEnum.Search;
            }
            else
            {
                NewDataForm ndf = new NewDataForm() { TableModeName = modeName,TableName = this.TableName};
                if (ndf.CreateFromFieldTable(DBHelper.GetDBHelper().GetTableFields(this.TableName), WorkingStateEnum.Creating, false))
                {
                    if (ndf.ShowDialog() == DialogResult.OK)
                    {
                        this.DataSource = DBHelper.GetDBHelper().GetDataFromTable(0, 0, this.TableName, "");
                    };
                }
                this.DataWorkingState = WorkingStateEnum.Search;
            }


        }
        public void DeleteRow(DataGridViewRow row)
        {
            if (!DataColumns.Contains("ID")) return;
            string recordID = row.Cells["ID"].Value.ToString();
            if (MessageBox.Show("确认删除已选的记录吗？", "确认？", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if(GoalTables.Contains(this.TableName))
                {
                    DBHelper.GetDBHelper().DelDataFromTable(this.TableName+ "DataDetail", recordID);
                }
                DBHelper.GetDBHelper().DelDataFromTable(this.TableName, recordID);
                DoSearch();
            }
        }
        public void EditRow(DataGridViewRow row, WorkingStateEnum state = WorkingStateEnum.Editting)
        {
            if (state == WorkingStateEnum.View && !RowReportTables.Contains(this.TableName))
            {
                if (CreateFromField(dataGridView1.DataSource as DataTable, row, WorkingStateEnum.View, false))
                {
                    return;
                }
            }
            DataWorkingState = WorkingStateEnum.Creating;
            IFieldTable table = GetFieldTable(row);
            CreateFromField(table, row, state, false);

            this.DataWorkingState = WorkingStateEnum.Search;
        }
        private ContextMenuStrip CreateDataGridViewMenu(int rowIndex)
        {
            ContextMenuStrip cms = new ContextMenuStrip();
            cms.ShowImageMargin = false;
            cms.ShowCheckMargin = false;
            cms.ShowItemToolTips = false;
            cms.Font = this.Font;

            ToolStripButton itemNew = new ToolStripButton();
            itemNew.Text = "新建记录";
            itemNew.Click += (obj, ee) =>
            {
                CreateNew();
            };
            cms.Items.Add(itemNew);

            if (rowIndex < 0)
                return cms;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];

            ToolStripButton itemEdit = new ToolStripButton();
            itemEdit.Text = "编辑";
            itemEdit.Click += (obj, ee) =>
            {
                EditRow(row);
            };
            cms.Items.Add(itemEdit);

            ToolStripButton itemView = new ToolStripButton();
            itemView.Text = "查看";
            itemView.Click += (obj, ee) =>
            {
                EditRow(row,WorkingStateEnum.View);
            };
            cms.Items.Add(itemView);

            ToolStripButton itemDel = new ToolStripButton();
            itemDel.Text = "删除记录";
            itemDel.Click += (obj, ee) =>
            {
                DeleteRow(row);
            };
            if (!this.TableName.Contains("制度"))
                cms.Items.Add(itemDel);

            ToolStripButton itemUpload = new ToolStripButton();
            itemUpload.Text = "文档上传";
            itemUpload.Click += (obj, ee) =>
            {
                if (dataGridView1.SelectedRows.Count == 0 || this.TableName == "") return;
                IFieldTable table = GetFieldTable(row);
                CreateFromField(table,null, WorkingStateEnum.Editting, true);

            };
            if (!this.TableName.Contains("制度"))
                cms.Items.Add(itemUpload);

            return cms;
        }
        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            int row = e.Row.Index + 1;
            e.Row.HeaderCell.Value = string.Format("{0}", row);

        }
    }
   
}

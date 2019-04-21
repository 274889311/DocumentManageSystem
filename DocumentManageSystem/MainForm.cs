using DatabaseHelper;
using DMSComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemWindows;
using TemplateHelper;

namespace DocumentManageSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            new FormStateControlor(this.PicFormState);
            new FormPosition(this.tableLayoutPanel2);
            new LabelButtonControl(this.lb_UserName);

            系统配置ToolStripMenuItem1.Visible = UserLogin.UserRoler == EnumUserRoler.Admin;
            用户管理ToolStripMenuItem1.Visible = UserLogin.UserRoler == EnumUserRoler.Admin;

            tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            tableLayoutPanel2.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel2, true, null);
            lb_UserName.Text = UserLogin.RealName;
            tv_Table.DrawNode += (obj, ee) =>
              {
                  if (!ee.Node.IsVisible)
                  {//节点不可见时
                      ee.DrawDefault = true;
                      return;
                  }
                  ee.Graphics.FillRectangle(Brushes.Transparent, ee.Node.Bounds);
                  StringFormat sf = new StringFormat();
                  sf.LineAlignment = StringAlignment.Center;
                  if (ee.State == TreeNodeStates.Selected)//做判断
                  {
                      ee.Graphics.FillRectangle(Brushes.CornflowerBlue, new Rectangle(ee.Node.Bounds.Left, ee.Node.Bounds.Top, ee.Node.Bounds.Width, ee.Node.Bounds.Height));//背景色为蓝色
                      RectangleF drawRect = new RectangleF(ee.Bounds.X, ee.Bounds.Y, ee.Bounds.Width + 10, ee.Bounds.Height);
                      ee.Graphics.DrawString(ee.Node.Text, tv_Table.Font, Brushes.White, drawRect, sf);
                      //字体为白色
                  }
                  else
                  {
                      if (ee.Bounds.Y < 0) { ee.DrawDefault = true; return; }
                      RectangleF drawRect = new RectangleF(ee.Bounds.X, ee.Bounds.Y, ee.Bounds.Width + 10, ee.Bounds.Height);
                      ee.Graphics.DrawString(ee.Node.Text, tv_Table.Font, Brushes.Black, drawRect, sf);
                      //ee.DrawDefault = true;
                  }
                  //---------------------
                  //作者：spw55381155
                  //来源：CSDN
                  //原文：https://blog.csdn.net/spw55381155/article/details/79891180 
                  //版权声明：本文为博主原创文章，转载请附上博文链接！
              };
            tv_Table.AfterExpand += (obj, ee) =>
              {
                  if (ee.Node.Level == 0)
                  {
                      ee.Node.ImageIndex = 1;
                      ee.Node.SelectedImageIndex = 1;
                  }
              };
            tv_Table.AfterCollapse += (obj, ee) =>
            {
                if (ee.Node.Level == 0)
                {
                    if (ee.Node.Nodes.Count > 0)
                    {
                        ee.Node.ImageIndex = 2;
                        ee.Node.SelectedImageIndex = 2;
                    }
                    else
                    {
                        ee.Node.ImageIndex = 0;
                        ee.Node.SelectedImageIndex = 0;
                    }
                }
            };
            dataEditPanel1.DataWorkingStateChanged += (state) =>
              {
                  if (state == WorkingStateEnum.Creating)
                  {
                      tsb_New.Enabled = false;
                      tsb_Edit.Enabled = false;
                      tsb_Del.Enabled = false;
                      tsb_Cancel.Enabled = true;
                      tsb_Save.Enabled = true;
                  }
                  else
                  {
                      tsb_New.Enabled = true;
                      tsb_Edit.Enabled = this.dataEditPanel1.SelectedRows.Count > 0 ? true : false;
                      tsb_Del.Enabled = true;
                      tsb_Del.Enabled = dataEditPanel1.TableName.Contains("制度") ? false : true;
                      tsb_Cancel.Enabled = false;
                      tsb_Save.Enabled = false;

                  }
              };
            dataEditPanel1.QueryEvent += (strSQL) =>
              {
                  if (this.dataEditPanel1.TableName != "")// node.Name)
                  {
                      ReloadDataTable(this.dataEditPanel1.TableName, strSQL);
                  }
              };
            this.SizeChanged += (obj, ee) =>
              {
                  if (this.WindowState == FormWindowState.Normal)
                  {
                      int SH = Screen.PrimaryScreen.Bounds.Height;
                      int SW = Screen.PrimaryScreen.Bounds.Width;
                      this.Location = new Point(SW / 2 - this.Width / 2, SH / 2 - this.Height / 2);
                  }
              };
            LoadTableTree();
            InitDeviceCare();
            BackgroundPanel.ButtonClickEvent += (typeName) =>
              {
                  TreeNode node = tv_Table.Nodes.Find(typeName, true).FirstOrDefault();
                  if(node.Nodes.Count>0)
                  {
                      if (node.Nodes[0] != null && node.Nodes[0].Level == 1)
                      {
                          tv_Table.SelectedNode = node.Nodes[0];
                          this.dataEditPanel1.TableName = node.Nodes[0].Name;
                          this.dataEditPanel1.TableModeName = node.Text;
                          this.dataEditPanel1.TemplateName = node.Nodes[0].Tag!=null? node.Nodes[0].Tag.ToString(): node.Nodes[0].Name;
                          dataEditPanel1.CreateFromFieldTable(DBHelper.GetDBHelper().GetTableFields(node.Nodes[0].Name), WorkingStateEnum.Search);
                          ReloadDataTable(node.Nodes[0].Name);
                      }
                  }
              };
        }
        private void InitDeviceCare()
        {
            this.toolStripStatusLabel1.Text = "";
            DataTable deviceCareTable = DBHelper.GetDBHelper().GetDeviceCareTable();
            if (deviceCareTable.Rows.Count > 0)
            {
                int index = 0;
                Timer timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += (sender, e) =>
                      {
                          string message = "";
                          DateTime timeCare = DateTime.Parse(deviceCareTable.Rows[index][0].ToString());
                          if (timeCare < DateTime.Now)
                              message = "设备：\"" + deviceCareTable.Rows[index][0].ToString() + "\"离下次保养时间还有：" + (timeCare - DateTime.Now).TotalDays.ToString() + "天,请及时保养!";
                          else
                              message = "设备：\"" + deviceCareTable.Rows[index][0].ToString() + "\"已经超出保养时间：" + (DateTime.Now - timeCare).TotalDays.ToString() + "天,请及时保养!";
                          this.toolStripStatusLabel1.Text = message;
                      };
                timer.Start();
            }
            else
                this.toolStripStatusLabel1.Text = DateTime.Now.ToShortDateString();
        }

        private TreeNode CreateTree(DataRow row, DataTable dt)
        {
            TreeNode node = new TreeNode(row["类型名称"].ToString()) { Name = row["类型名称"].ToString() };
            var tableNames = DBHelper.GetDBHelper().GetTableNames(Name = row["类型名称"].ToString());
            node.Nodes.AddRange(tableNames.Select(tn=> {
                TreeNode n = new TreeNode(tn.Key);
                n.Text = tn.Key;
                n.Name = tn.Key;
                if (tn.Value != "")
                    n.Tag = tn.Value;
                else
                    n.Tag = tn.Key;
                n.ImageIndex = 3;
                n.SelectedImageIndex = 3;
                return n;
            }).ToArray());
            node.SelectedImageIndex = 0;
            node.ImageIndex = 0;
            foreach (DataRow subRow in dt.Select("ParentID="+row["ID"].ToString()))
            {
                node.Nodes.Add(CreateTree(subRow, dt));
                node.SelectedImageIndex = 2;
                node.ImageIndex = 2;
            }
            return node;
        }
        
        /// <summary>
        /// 重载目 录树
        /// </summary>
        private void LoadTableTree()
        {
            DBHelper helper = DBHelper.GetDBHelper();// (EnumDatabaseType.Access);
            tv_Table.SuspendLayout();
            tv_Table.Nodes.Clear();

            var nodeDT = helper.GetModes();
            nodeDT.DefaultView.Sort = "说明 ASC";
            foreach (DataRow row in nodeDT.DefaultView.ToTable().Rows)
            {
                if (row["ParentID"].ToString() == "")
                {
                    tv_Table.Nodes.Add(CreateTree(row, nodeDT));
                }
            }
            
            tv_Table.ResumeLayout(false);
            tv_Table.PerformLayout();
        }

        private void lb_UserName_Click(object sender, EventArgs e)
        {

            ContextMenuStrip cms = new ContextMenuStrip();
            cms.ShowImageMargin = false;
            cms.ShowCheckMargin = false;
            cms.ShowItemToolTips = false;
            cms.AutoSize = true;
            ToolStripButton itemUser = new ToolStripButton();
            itemUser.Text = "切换用户";
            itemUser.Click += (obj, ee) =>
            {
                Application.Restart();
            };
            cms.Items.Add(itemUser);

            ToolStripButton itemPassword = new ToolStripButton();
            itemPassword.Text = "修改密码";
            itemPassword.Click += (obj, ee) =>
            {
                AlterUserPasswordForm frm = new AlterUserPasswordForm();
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Application.Restart();
                }
            };
            cms.Items.Add(itemPassword);
            cms.Show(this.PointToScreen(new Point(lb_UserName.Left, lb_UserName.Bottom)));

        }

        /// <summary>
        /// 目录树菜单 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tv_Table_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenuStrip cms = new ContextMenuStrip();
                cms.ShowImageMargin = false;
                cms.ShowCheckMargin = false;
                cms.ShowItemToolTips = false;
                cms.Font = this.Font;

                TreeNode node = tv_Table.GetNodeAt(e.Location);
                if (node != null)
                {
                    tv_Table.SelectedNode = node;
                    if (node.Tag == null)
                    {
                        #region 新建报表
                        ToolStripButton itemNew = new ToolStripButton();
                        itemNew.Text = "新建报表";
                        itemNew.Click += (obj, ee) =>
                        {
                            TableManagerForm tmf = new TableManagerForm(node.Text);
                            if (tmf.ShowDialog() == DialogResult.OK)
                            {
                                LoadTableTree();
                            }
                        };
                        AddItemToMenuStrip(cms, itemNew, EnumUserRoler.Admin);
                        #endregion

                        #region 删除报表类型
                        ToolStripButton itemRemoveMode = new ToolStripButton();
                        itemRemoveMode.Text = "删除文件夹";
                        itemRemoveMode.Click += (obj, ee) =>
                        {
                            if (MessageBox.Show("确认要删除此报表类型？删除后，该报表类型下的所有表及数据将丢失", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                return;
                            DeleteNode(node);
                           
                            MessageBox.Show("删除完成");
                            LoadTableTree();
                        };
                        AddItemToMenuStrip(cms, itemRemoveMode, EnumUserRoler.Admin);
                        #endregion
                    }
                    else
                    {
                        #region 修改报表
                        ToolStripButton itemEdit = new ToolStripButton();
                        itemEdit.Text = "修改报表";
                        itemEdit.Click += (obj, ee) =>
                        {
                            TableManagerForm tmf = new TableManagerForm(node.Parent.Name, node.Name, node.Tag == null ? node.Name : node.Tag.ToString());
                            if (tmf.ShowDialog() == DialogResult.OK)
                            {
                                LoadTableTree();
                            }
                        };
                        AddItemToMenuStrip(cms, itemEdit, EnumUserRoler.Admin);
                        #endregion

                        #region 删除报表
                        ToolStripButton itemDel = new ToolStripButton();
                        itemDel.Text = "删除报表";
                        itemDel.Click += (obj, ee) =>
                        {
                            if (MessageBox.Show("确认要删除此报表？删除后，原数据将丢失", "提示", MessageBoxButtons.YesNo) != DialogResult.Yes)
                                return;
                            DBHelper.GetDBHelper().RemoveTable(node.Name);
                            MessageBox.Show("删除完成");
                            LoadTableTree();
                        };
                        AddItemToMenuStrip(cms, itemDel, EnumUserRoler.Admin);
                        #endregion

                        #region 复制报表
                        ToolStripButton itemCopy = new ToolStripButton();
                        itemCopy.Text = "复制报表";
                        itemCopy.Click += (obj, ee) =>
                        {
                            TableManagerForm tmf = new TableManagerForm(node.Parent.Name, node.Name, node.Tag == null ? node.Name : node.Tag.ToString(), true);
                            if (tmf.ShowDialog() == DialogResult.OK)
                            {
                                LoadTableTree();
                            }
                        };
                        AddItemToMenuStrip(cms, itemCopy, EnumUserRoler.Admin);
                        #endregion

                        #region 新建数据
                        ToolStripButton itemInput = new ToolStripButton();
                        itemInput.Text = "新建数据";
                        itemInput.Click += (obj, ee) =>
                        {
                            tsb_New_Click(obj, e);
                        };
                        AddItemToMenuStrip(cms, itemInput, EnumUserRoler.User);
                        #endregion
                    }
                }

                #region 新建报表类型
                ToolStripButton itemMode = new ToolStripButton();
                itemMode.Text = "新建文件夹";
                itemMode.Click += (obj, ee) =>
                {
                    TableModeForm tmf = new TableModeForm(node!=null?node.Text:"");
                    if (tmf.ShowDialog() == DialogResult.OK)
                    {
                        LoadTableTree();
                    }
                };
                AddItemToMenuStrip(cms, itemMode, EnumUserRoler.Admin);
                #endregion


                cms.Show(tv_Table.PointToScreen(e.Location));
            }
        }
        private void DeleteNode(TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                if (n.Nodes.Count > 0)
                {
                    DeleteNode(n);
                }
                else
                {
                    DBHelper.GetDBHelper().RemoveTable(n.Name);
                }
            }
            DBHelper.GetDBHelper().Excute("delete from 报表类型 where 类型名称 ='" + node.Name + "'");
        }
        /// <summary>
        /// 按照 用户权限添加项目 到弹出菜单 
        /// </summary>
        /// <param name="menuStrip"></param>
        /// <param name="toolStripItem"></param>
        /// <param name="roler"></param>
        private void AddItemToMenuStrip(ContextMenuStrip menuStrip, ToolStripItem toolStripItem, EnumUserRoler roler)
        {
            if (roler == UserLogin.UserRoler || UserLogin.UserRoler == EnumUserRoler.Admin)
                menuStrip.Items.Add(toolStripItem);
        }

        private void lb_System_Click(object sender, EventArgs e)
        {
            SystemSettingForm ssf = new SystemSettingForm();
            if (ssf.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show("配置已发生改变，需要重新启动系统，现在就重新启动吗？", "重启", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Application.Restart();
                }

            }
        }

        /// <summary>
        /// 目 录树双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tv_Table_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = tv_Table.GetNodeAt(e.Location);
            if (node != null && node.Nodes.Count == 0 && node.Parent != null)
            {
                tv_Table.SelectedNode = node;
                this.dataEditPanel1.TableName = node.Name;
                this.dataEditPanel1.TableModeName = node.Parent.Text;
                this.dataEditPanel1.TemplateName = node.Tag != null ? node.Tag.ToString() : node.Name;
                dataEditPanel1.CreateFromFieldTable(DBHelper.GetDBHelper().GetTableFields(node.Name), WorkingStateEnum.Search);
                ReloadDataTable(node.Name);
            }
        }

        private void tv_Table_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
                e.Node.ImageIndex = 0;
            else
                e.Node.ImageIndex = 2;
        }

        private void tv_Table_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level ==  0)
                e.Node.ImageIndex = 1;
            else
                e.Node.ImageIndex = 2;
        }
        /// <summary>
        /// 重新加载数据记录
        /// </summary>
        /// <param name="tableName"></param>
        private void ReloadDataTable(string tableName, string queryClause = "")
        {
            dataEditPanel1.DataSource = DBHelper.GetDBHelper().GetDataFromTable(0, 0, tableName, queryClause);

        }

        #region 快捷工具栏暂时不用了
        /// <summary>
        /// 新建数据记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsb_New_Click(object sender, EventArgs e)
        {
            //this.dataEditPanel1.cl.
            if (tv_Table.SelectedNode != null && tv_Table.SelectedNode.Nodes.Count == 0)
            {
                var dt = DBHelper.GetDBHelper().GetModes(tv_Table.SelectedNode.Name);
                if (dt.Rows.Count > 0)
                {
                    dataEditPanel1.TableName = tv_Table.SelectedNode.Name;
                    dataEditPanel1.TableModeName = tv_Table.SelectedNode.Parent.Text;
                    dataEditPanel1.TemplateName = tv_Table.SelectedNode.Tag.ToString();
                    dataEditPanel1.CreateNew(dt.Rows[0]["类型名称"].ToString());
                }
            }
        }
        /// <summary>
        /// 删 除数据记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsb_Del_Click(object sender, EventArgs e)
        {
            if (this.dataEditPanel1.SelectedRows.Count > 0)
            {
                dataEditPanel1.DeleteRow(this.dataEditPanel1.SelectedRows[0]);
            }
        }
        /// 修改数据记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsb_Edit_Click(object sender, EventArgs e)
        {
            if (this.dataEditPanel1.SelectedRows.Count > 0)
            {
                dataEditPanel1.EditRow(this.dataEditPanel1.SelectedRows[0]);
            }
            
        }
        /// <summary>
        /// 取消新建或修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsb_Cancel_Click(object sender, EventArgs e)
        {
            dataEditPanel1.DataWorkingState = WorkingStateEnum.Editting;
        }
        #endregion


        private void 用户管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SystemWindows.UserManagerForm umf = new UserManagerForm();
            umf.ShowDialog();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlterUserPasswordForm frm = new AlterUserPasswordForm();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (MessageBox.Show("修改密码后需要重新启动") == DialogResult.OK)
                    Application.Restart();
            }
        }

        private void 切换用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确认要切换用户登录系统吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)

                Application.Restart();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确认要退出系统吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Application.Exit();
        }

        private void 数据导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataEditPanel1.TableName == "") return;
            TreeNode tn = tv_Table.Nodes.Find(dataEditPanel1.TableName, true).Where(t => t.Nodes.Count == 0).FirstOrDefault();
            if (tn == null || tn.Parent == null) return; 
            ITemplateHelper templateHelper = ATemplateHelper.GetTemplateHelper(tn.Parent.Text, dataEditPanel1.TableName);
             string[] columns =DBHelper.GetDBHelper().GetTableFields(dataEditPanel1.TableName).GetTableFields(false).Select(f=>f.Name).ToArray(); 
            templateHelper.OutPut((dataEditPanel1.DataSource as DataTable).DefaultView.ToTable(true, columns));
        }

        private void 数据导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (dataEditPanel1.TableName == "")
            {
                MessageBox.Show("请在报表树中选择要导入的表");
                return;
            }
            TreeNode tn = tv_Table.Nodes.Find(dataEditPanel1.TableName, true).Where(t => t.Nodes.Count == 0).FirstOrDefault();
            if (tn == null || tn.Parent == null||tn.Tag == null)
            {
                MessageBox.Show("请在报表树中选择要导入的表");
                return;
            }
            ITemplateHelper templateHelper = ATemplateHelper.GetTemplateHelper(tn.Parent.Text, dataEditPanel1.TableName);

            string Filter = "Excel文件|*.xls|Excel文件|*.xlsx";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Excel文件";
            openFileDialog1.Filter = Filter;
            openFileDialog1.ValidateNames = true;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            if(!openFileDialog1.FileName.Contains(dataEditPanel1.TableName))
            {
                MessageBox.Show("您选择的数据导入模板不正确，请重新选择！");
                return;
            }
            DataTable dataTable = templateHelper.InPut(dataEditPanel1.TableName, openFileDialog1.FileName);
            BaseFieldTable fieldTable = new BaseFieldTable(dataTable.TableName);
            foreach (DataColumn col in dataTable.Columns)
            {
                fieldTable.AddField(new TableField() { Name = col.ColumnName, Value = "",Type =col.DataType.ToString() });
            }
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn col in dataTable.Columns)
                {
                    fieldTable.UpdateFieldValue(col.ColumnName, row[col].ToString());
                }
                DBHelper.GetDBHelper().InsertIntoTable(fieldTable);
            }
            ReloadDataTable(dataEditPanel1.TableName);
        }

        private void 系统提醒ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeviceCareWarnForm dcwf = new DeviceCareWarnForm();
            dcwf.ShowDialog();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 法律法规ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Help\法律法规.chm");
        }

        private void 使用说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Help\安全生产管理系统使用说明书.doc");
        }
    }
}

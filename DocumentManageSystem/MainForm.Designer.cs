using DMSComponent;
using PageControl;
using SystemWindows;

namespace DocumentManageSystem
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.PicFormState = new System.Windows.Forms.PictureBox();
            this.lb_UserName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataEditPanel1 = new DMSComponent.DataEditPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_New = new System.Windows.Forms.ToolStripButton();
            this.tsb_Del = new System.Windows.Forms.ToolStripButton();
            this.tsb_Edit = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.tsb_Cancel = new System.Windows.Forms.ToolStripButton();
            this.tsb_Upload = new System.Windows.Forms.ToolStripButton();
            this.tsb_View = new System.Windows.Forms.ToolStripButton();
            this.tsb_Print = new System.Windows.Forms.ToolStripButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tv_Table = new System.Windows.Forms.TreeView();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统配置ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.用户管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.更改口令ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.切换用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据维护ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统提醒ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.法律法规ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.使用说明ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicFormState)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1236, 686);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel2.BackgroundImage")));
            this.tableLayoutPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.PicFormState, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.lb_UserName, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1236, 38);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // PicFormState
            // 
            this.PicFormState.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.PicFormState.Image = ((System.Drawing.Image)(resources.GetObject("PicFormState.Image")));
            this.PicFormState.Location = new System.Drawing.Point(1105, 3);
            this.PicFormState.Name = "PicFormState";
            this.PicFormState.Size = new System.Drawing.Size(128, 32);
            this.PicFormState.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PicFormState.TabIndex = 1;
            this.PicFormState.TabStop = false;
            // 
            // lb_UserName
            // 
            this.lb_UserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_UserName.AutoSize = true;
            this.lb_UserName.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_UserName.ForeColor = System.Drawing.Color.White;
            this.lb_UserName.Location = new System.Drawing.Point(1032, 9);
            this.lb_UserName.Margin = new System.Windows.Forms.Padding(1);
            this.lb_UserName.Name = "lb_UserName";
            this.lb_UserName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lb_UserName.Size = new System.Drawing.Size(69, 19);
            this.lb_UserName.TabIndex = 5;
            this.lb_UserName.Text = "管理员";
            this.lb_UserName.Click += new System.EventHandler(this.lb_UserName_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "平安工地管理系统";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataEditPanel1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.tv_Table);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1230, 620);
            this.panel1.TabIndex = 4;
            // 
            // dataEditPanel1
            // 
            this.dataEditPanel1.BackColor = System.Drawing.Color.Transparent;
            this.dataEditPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dataEditPanel1.DataSource = null;
            this.dataEditPanel1.DataWorkingState = SystemWindows.WorkingStateEnum.Editting;
            this.dataEditPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataEditPanel1.Location = new System.Drawing.Point(205, 49);
            this.dataEditPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataEditPanel1.Name = "dataEditPanel1";
            this.dataEditPanel1.Size = new System.Drawing.Size(1025, 571);
            this.dataEditPanel1.TabIndex = 7;
            this.dataEditPanel1.TableModeName = null;
            this.dataEditPanel1.TableName = null;
            this.dataEditPanel1.TemplateName = null;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_New,
            this.tsb_Del,
            this.tsb_Edit,
            this.tsb_Save,
            this.tsb_Cancel,
            this.tsb_Upload,
            this.tsb_View,
            this.tsb_Print});
            this.toolStrip1.Location = new System.Drawing.Point(205, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1025, 25);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_New
            // 
            this.tsb_New.BackColor = System.Drawing.Color.Transparent;
            this.tsb_New.Image = ((System.Drawing.Image)(resources.GetObject("tsb_New.Image")));
            this.tsb_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_New.Name = "tsb_New";
            this.tsb_New.Size = new System.Drawing.Size(52, 22);
            this.tsb_New.Text = "新建";
            this.tsb_New.Click += new System.EventHandler(this.tsb_New_Click);
            // 
            // tsb_Del
            // 
            this.tsb_Del.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Del.Image")));
            this.tsb_Del.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Del.Name = "tsb_Del";
            this.tsb_Del.Size = new System.Drawing.Size(52, 22);
            this.tsb_Del.Text = "删除";
            this.tsb_Del.Click += new System.EventHandler(this.tsb_Del_Click);
            // 
            // tsb_Edit
            // 
            this.tsb_Edit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Edit.Image")));
            this.tsb_Edit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Edit.Name = "tsb_Edit";
            this.tsb_Edit.Size = new System.Drawing.Size(52, 22);
            this.tsb_Edit.Text = "编辑";
            this.tsb_Edit.Click += new System.EventHandler(this.tsb_Edit_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(52, 22);
            this.tsb_Save.Text = "保存";
            this.tsb_Save.Visible = false;
            // 
            // tsb_Cancel
            // 
            this.tsb_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Cancel.Image")));
            this.tsb_Cancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Cancel.Name = "tsb_Cancel";
            this.tsb_Cancel.Size = new System.Drawing.Size(52, 22);
            this.tsb_Cancel.Text = "取消";
            this.tsb_Cancel.Visible = false;
            this.tsb_Cancel.Click += new System.EventHandler(this.tsb_Cancel_Click);
            // 
            // tsb_Upload
            // 
            this.tsb_Upload.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsb_Upload.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Upload.Image")));
            this.tsb_Upload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Upload.Name = "tsb_Upload";
            this.tsb_Upload.Size = new System.Drawing.Size(52, 22);
            this.tsb_Upload.Text = "上传";
            this.tsb_Upload.Visible = false;
            // 
            // tsb_View
            // 
            this.tsb_View.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsb_View.Image = ((System.Drawing.Image)(resources.GetObject("tsb_View.Image")));
            this.tsb_View.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_View.Name = "tsb_View";
            this.tsb_View.Size = new System.Drawing.Size(52, 22);
            this.tsb_View.Text = "预览";
            this.tsb_View.Visible = false;
            // 
            // tsb_Print
            // 
            this.tsb_Print.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsb_Print.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Print.Image")));
            this.tsb_Print.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Print.Name = "tsb_Print";
            this.tsb_Print.Size = new System.Drawing.Size(52, 22);
            this.tsb_Print.Text = "打印";
            this.tsb_Print.Visible = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 596);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // tv_Table
            // 
            this.tv_Table.BackColor = System.Drawing.Color.White;
            this.tv_Table.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tv_Table.Dock = System.Windows.Forms.DockStyle.Left;
            this.tv_Table.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tv_Table.HideSelection = false;
            this.tv_Table.ImageIndex = 0;
            this.tv_Table.ImageList = this.imageListTreeView;
            this.tv_Table.Location = new System.Drawing.Point(0, 24);
            this.tv_Table.Margin = new System.Windows.Forms.Padding(0);
            this.tv_Table.Name = "tv_Table";
            this.tv_Table.SelectedImageIndex = 0;
            this.tv_Table.ShowRootLines = false;
            this.tv_Table.Size = new System.Drawing.Size(200, 596);
            this.tv_Table.TabIndex = 2;
            this.tv_Table.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tv_Table_AfterCollapse);
            this.tv_Table.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tv_Table_AfterExpand);
            this.tv_Table.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tv_Table_MouseDoubleClick);
            this.tv_Table.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tv_Table_MouseUp);
            // 
            // imageListTreeView
            // 
            this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeView.Images.SetKeyName(0, "文件夹未打开.png");
            this.imageListTreeView.Images.SetKeyName(1, "文件夹.png");
            this.imageListTreeView.Images.SetKeyName(2, "文件夹打开.png");
            this.imageListTreeView.Images.SetKeyName(3, "文件.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统配置ToolStripMenuItem1,
            this.用户管理ToolStripMenuItem1,
            this.更改口令ToolStripMenuItem1,
            this.数据维护ToolStripMenuItem,
            this.系统提醒ToolStripMenuItem1,
            this.退出ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(1230, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统配置ToolStripMenuItem1
            // 
            this.系统配置ToolStripMenuItem1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("系统配置ToolStripMenuItem1.BackgroundImage")));
            this.系统配置ToolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.系统配置ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("系统配置ToolStripMenuItem1.Image")));
            this.系统配置ToolStripMenuItem1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.系统配置ToolStripMenuItem1.Name = "系统配置ToolStripMenuItem1";
            this.系统配置ToolStripMenuItem1.Size = new System.Drawing.Size(86, 24);
            this.系统配置ToolStripMenuItem1.Text = "系统配置";
            this.系统配置ToolStripMenuItem1.Click += new System.EventHandler(this.lb_System_Click);
            // 
            // 用户管理ToolStripMenuItem1
            // 
            this.用户管理ToolStripMenuItem1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("用户管理ToolStripMenuItem1.BackgroundImage")));
            this.用户管理ToolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.用户管理ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("用户管理ToolStripMenuItem1.Image")));
            this.用户管理ToolStripMenuItem1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.用户管理ToolStripMenuItem1.Name = "用户管理ToolStripMenuItem1";
            this.用户管理ToolStripMenuItem1.Size = new System.Drawing.Size(86, 24);
            this.用户管理ToolStripMenuItem1.Text = "用户管理";
            this.用户管理ToolStripMenuItem1.Click += new System.EventHandler(this.用户管理ToolStripMenuItem1_Click);
            // 
            // 更改口令ToolStripMenuItem1
            // 
            this.更改口令ToolStripMenuItem1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("更改口令ToolStripMenuItem1.BackgroundImage")));
            this.更改口令ToolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.更改口令ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.切换用户ToolStripMenuItem,
            this.修改密码ToolStripMenuItem});
            this.更改口令ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("更改口令ToolStripMenuItem1.Image")));
            this.更改口令ToolStripMenuItem1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.更改口令ToolStripMenuItem1.Name = "更改口令ToolStripMenuItem1";
            this.更改口令ToolStripMenuItem1.Size = new System.Drawing.Size(86, 24);
            this.更改口令ToolStripMenuItem1.Text = "更改口令";
            // 
            // 切换用户ToolStripMenuItem
            // 
            this.切换用户ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("切换用户ToolStripMenuItem.BackgroundImage")));
            this.切换用户ToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.切换用户ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("切换用户ToolStripMenuItem.Image")));
            this.切换用户ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.切换用户ToolStripMenuItem.Name = "切换用户ToolStripMenuItem";
            this.切换用户ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.切换用户ToolStripMenuItem.Text = "切换用户";
            this.切换用户ToolStripMenuItem.Click += new System.EventHandler(this.切换用户ToolStripMenuItem_Click);
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("修改密码ToolStripMenuItem.BackgroundImage")));
            this.修改密码ToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.修改密码ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("修改密码ToolStripMenuItem.Image")));
            this.修改密码ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.修改密码ToolStripMenuItem.Text = "修改密码";
            this.修改密码ToolStripMenuItem.Click += new System.EventHandler(this.修改密码ToolStripMenuItem_Click);
            // 
            // 数据维护ToolStripMenuItem
            // 
            this.数据维护ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("数据维护ToolStripMenuItem.BackgroundImage")));
            this.数据维护ToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.数据维护ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据导入ToolStripMenuItem,
            this.数据导出ToolStripMenuItem});
            this.数据维护ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("数据维护ToolStripMenuItem.Image")));
            this.数据维护ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.数据维护ToolStripMenuItem.Name = "数据维护ToolStripMenuItem";
            this.数据维护ToolStripMenuItem.Size = new System.Drawing.Size(86, 24);
            this.数据维护ToolStripMenuItem.Text = "数据维护";
            // 
            // 数据导入ToolStripMenuItem
            // 
            this.数据导入ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("数据导入ToolStripMenuItem.Image")));
            this.数据导入ToolStripMenuItem.Name = "数据导入ToolStripMenuItem";
            this.数据导入ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.数据导入ToolStripMenuItem.Text = "数据导入";
            this.数据导入ToolStripMenuItem.Click += new System.EventHandler(this.数据导入ToolStripMenuItem_Click);
            // 
            // 数据导出ToolStripMenuItem
            // 
            this.数据导出ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("数据导出ToolStripMenuItem.Image")));
            this.数据导出ToolStripMenuItem.Name = "数据导出ToolStripMenuItem";
            this.数据导出ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.数据导出ToolStripMenuItem.Text = "数据导出";
            this.数据导出ToolStripMenuItem.Click += new System.EventHandler(this.数据导出ToolStripMenuItem_Click);
            // 
            // 系统提醒ToolStripMenuItem1
            // 
            this.系统提醒ToolStripMenuItem1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("系统提醒ToolStripMenuItem1.BackgroundImage")));
            this.系统提醒ToolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.系统提醒ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("系统提醒ToolStripMenuItem1.Image")));
            this.系统提醒ToolStripMenuItem1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.系统提醒ToolStripMenuItem1.Name = "系统提醒ToolStripMenuItem1";
            this.系统提醒ToolStripMenuItem1.Size = new System.Drawing.Size(86, 24);
            this.系统提醒ToolStripMenuItem1.Text = "系统提醒";
            this.系统提醒ToolStripMenuItem1.Click += new System.EventHandler(this.系统提醒ToolStripMenuItem1_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("退出ToolStripMenuItem.BackgroundImage")));
            this.退出ToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.退出ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("退出ToolStripMenuItem.Image")));
            this.退出ToolStripMenuItem.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 664);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(1236, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Stretch = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("帮助ToolStripMenuItem.BackgroundImage")));
            this.帮助ToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.法律法规ToolStripMenuItem,
            this.使用说明ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // 法律法规ToolStripMenuItem
            // 
            this.法律法规ToolStripMenuItem.Name = "法律法规ToolStripMenuItem";
            this.法律法规ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.法律法规ToolStripMenuItem.Text = "法律法规";
            this.法律法规ToolStripMenuItem.Click += new System.EventHandler(this.法律法规ToolStripMenuItem_Click);
            // 
            // 使用说明ToolStripMenuItem
            // 
            this.使用说明ToolStripMenuItem.Name = "使用说明ToolStripMenuItem";
            this.使用说明ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.使用说明ToolStripMenuItem.Text = "使用说明";
            this.使用说明ToolStripMenuItem.Click += new System.EventHandler(this.使用说明ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1236, 686);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文档管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicFormState)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.PictureBox PicFormState;
        private System.Windows.Forms.TreeView tv_Table;
        private System.Windows.Forms.Label lb_UserName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private DMSComponent.DataEditPanel dataEditPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_New;
        private System.Windows.Forms.ToolStripButton tsb_Del;
        private System.Windows.Forms.ToolStripButton tsb_Edit;
        private System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.ToolStripButton tsb_Upload;
        private System.Windows.Forms.ToolStripButton tsb_View;
        private System.Windows.Forms.ToolStripButton tsb_Print;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 数据维护ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListTreeView;
        private System.Windows.Forms.ToolStripButton tsb_Cancel;
        private System.Windows.Forms.ToolStripMenuItem 系统配置ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 用户管理ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 更改口令ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 系统提醒ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 切换用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 法律法规ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 使用说明ToolStripMenuItem;
    }
}


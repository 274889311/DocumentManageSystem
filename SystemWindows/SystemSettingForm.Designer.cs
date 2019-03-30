namespace SystemWindows
{
    partial class SystemSettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.tx_db_Password = new System.Windows.Forms.TextBox();
            this.tx_db_UserName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tlp_SQL_File = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.tx_db_FilePath = new System.Windows.Forms.TextBox();
            this.bt_OpenFile = new System.Windows.Forms.Button();
            this.tlp_SQL_Server = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_DBServer = new System.Windows.Forms.TextBox();
            this.tx_db_IP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_DBName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.bt_OK = new System.Windows.Forms.Button();
            this.bt_Cancel = new System.Windows.Forms.Button();
            this.cb_db_Type = new System.Windows.Forms.ComboBox();
            this.bt_TestConnect = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlp_SQL_File.SuspendLayout();
            this.tlp_SQL_Server.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tlp_SQL_File, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tlp_SQL_Server, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.cb_db_Type, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(29, 29);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(439, 239);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tx_db_Password, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tx_db_UserName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 135);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(187, 54);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "用户名:";
            // 
            // tx_db_Password
            // 
            this.tx_db_Password.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tx_db_Password.Location = new System.Drawing.Point(56, 30);
            this.tx_db_Password.Name = "tx_db_Password";
            this.tx_db_Password.Size = new System.Drawing.Size(128, 21);
            this.tx_db_Password.TabIndex = 5;
            this.tx_db_Password.Text = "123456";
            this.tx_db_Password.UseSystemPasswordChar = true;
            // 
            // tx_db_UserName
            // 
            this.tx_db_UserName.Location = new System.Drawing.Point(56, 3);
            this.tx_db_UserName.Name = "tx_db_UserName";
            this.tx_db_UserName.Size = new System.Drawing.Size(128, 21);
            this.tx_db_UserName.TabIndex = 4;
            this.tx_db_UserName.Text = "sa";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "密  码:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库服务器类型:";
            // 
            // tlp_SQL_File
            // 
            this.tlp_SQL_File.AutoSize = true;
            this.tlp_SQL_File.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlp_SQL_File.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tlp_SQL_File, 3);
            this.tlp_SQL_File.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_SQL_File.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_SQL_File.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_SQL_File.Controls.Add(this.label2, 0, 0);
            this.tlp_SQL_File.Controls.Add(this.tx_db_FilePath, 1, 0);
            this.tlp_SQL_File.Controls.Add(this.bt_OpenFile, 2, 0);
            this.tlp_SQL_File.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlp_SQL_File.Location = new System.Drawing.Point(0, 26);
            this.tlp_SQL_File.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_SQL_File.Name = "tlp_SQL_File";
            this.tlp_SQL_File.RowCount = 1;
            this.tlp_SQL_File.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_SQL_File.Size = new System.Drawing.Size(439, 28);
            this.tlp_SQL_File.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "数据库文件:";
            // 
            // tx_db_FilePath
            // 
            this.tx_db_FilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tx_db_FilePath.Location = new System.Drawing.Point(80, 3);
            this.tx_db_FilePath.Name = "tx_db_FilePath";
            this.tx_db_FilePath.Size = new System.Drawing.Size(299, 21);
            this.tx_db_FilePath.TabIndex = 2;
            // 
            // bt_OpenFile
            // 
            this.bt_OpenFile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_OpenFile.AutoSize = true;
            this.bt_OpenFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_OpenFile.Location = new System.Drawing.Point(385, 3);
            this.bt_OpenFile.Name = "bt_OpenFile";
            this.bt_OpenFile.Size = new System.Drawing.Size(51, 22);
            this.bt_OpenFile.TabIndex = 2;
            this.bt_OpenFile.Text = "浏  览";
            this.bt_OpenFile.UseVisualStyleBackColor = true;
            this.bt_OpenFile.Click += new System.EventHandler(this.bt_OpenFile_Click);
            // 
            // tlp_SQL_Server
            // 
            this.tlp_SQL_Server.AutoSize = true;
            this.tlp_SQL_Server.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlp_SQL_Server.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tlp_SQL_Server, 2);
            this.tlp_SQL_Server.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_SQL_Server.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_SQL_Server.Controls.Add(this.tx_db_IP, 1, 2);
            this.tlp_SQL_Server.Controls.Add(this.label3, 0, 2);
            this.tlp_SQL_Server.Controls.Add(this.tb_DBName, 1, 1);
            this.tlp_SQL_Server.Controls.Add(this.tb_DBServer, 1, 0);
            this.tlp_SQL_Server.Controls.Add(this.label7, 0, 1);
            this.tlp_SQL_Server.Controls.Add(this.label4, 0, 0);
            this.tlp_SQL_Server.Location = new System.Drawing.Point(0, 54);
            this.tlp_SQL_Server.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_SQL_Server.Name = "tlp_SQL_Server";
            this.tlp_SQL_Server.RowCount = 3;
            this.tlp_SQL_Server.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_SQL_Server.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_SQL_Server.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_SQL_Server.Size = new System.Drawing.Size(382, 81);
            this.tlp_SQL_Server.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "服务器名称:";
            // 
            // tb_DBServer
            // 
            this.tb_DBServer.Location = new System.Drawing.Point(80, 3);
            this.tb_DBServer.Name = "tb_DBServer";
            this.tb_DBServer.Size = new System.Drawing.Size(299, 21);
            this.tb_DBServer.TabIndex = 3;
            this.tb_DBServer.Text = ".\\SQLEXPRESS";
            // 
            // tx_db_IP
            // 
            this.tx_db_IP.Location = new System.Drawing.Point(80, 57);
            this.tx_db_IP.Name = "tx_db_IP";
            this.tx_db_IP.Size = new System.Drawing.Size(299, 21);
            this.tx_db_IP.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "服务器地址:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "数据库名称:";
            // 
            // tb_DBName
            // 
            this.tb_DBName.Location = new System.Drawing.Point(80, 30);
            this.tb_DBName.Name = "tb_DBName";
            this.tb_DBName.Size = new System.Drawing.Size(299, 21);
            this.tb_DBName.TabIndex = 3;
            this.tb_DBName.Text = "DMSDB";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.bt_TestConnect, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.bt_OK, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.bt_Cancel, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(68, 193);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(303, 42);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // bt_OK
            // 
            this.bt_OK.AutoSize = true;
            this.bt_OK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_OK.Location = new System.Drawing.Point(216, 8);
            this.bt_OK.Margin = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.bt_OK.Name = "bt_OK";
            this.bt_OK.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_OK.Size = new System.Drawing.Size(69, 26);
            this.bt_OK.TabIndex = 7;
            this.bt_OK.Text = "保    存";
            this.bt_OK.UseVisualStyleBackColor = true;
            this.bt_OK.Click += new System.EventHandler(this.bt_OK_Click);
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.AutoSize = true;
            this.bt_Cancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bt_Cancel.Location = new System.Drawing.Point(116, 8);
            this.bt_Cancel.Margin = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_Cancel.Size = new System.Drawing.Size(68, 26);
            this.bt_Cancel.TabIndex = 6;
            this.bt_Cancel.Text = "取    消";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            // 
            // cb_db_Type
            // 
            this.cb_db_Type.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_db_Type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cb_db_Type.FormattingEnabled = true;
            this.cb_db_Type.Items.AddRange(new object[] {
            "SQL Server",
            "SQL Server 文件"});
            this.cb_db_Type.Location = new System.Drawing.Point(116, 3);
            this.cb_db_Type.Name = "cb_db_Type";
            this.cb_db_Type.Size = new System.Drawing.Size(121, 20);
            this.cb_db_Type.TabIndex = 1;
            this.cb_db_Type.SelectedIndexChanged += new System.EventHandler(this.cb_db_Type_SelectedIndexChanged);
            // 
            // bt_TestConnect
            // 
            this.bt_TestConnect.AutoSize = true;
            this.bt_TestConnect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_TestConnect.Location = new System.Drawing.Point(16, 8);
            this.bt_TestConnect.Margin = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.bt_TestConnect.Name = "bt_TestConnect";
            this.bt_TestConnect.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bt_TestConnect.Size = new System.Drawing.Size(68, 26);
            this.bt_TestConnect.TabIndex = 6;
            this.bt_TestConnect.Text = "测试连接";
            this.bt_TestConnect.UseVisualStyleBackColor = true;
            this.bt_TestConnect.Click += new System.EventHandler(this.bt_TestConnect_Click);
            // 
            // SystemSettingForm
            // 
            this.AcceptButton = this.bt_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(508, 335);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemSettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "数据库服务配置";
            this.Load += new System.EventHandler(this.SystemSettingForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlp_SQL_File.ResumeLayout(false);
            this.tlp_SQL_File.PerformLayout();
            this.tlp_SQL_Server.ResumeLayout(false);
            this.tlp_SQL_Server.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_db_Type;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tlp_SQL_File;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tx_db_FilePath;
        private System.Windows.Forms.Button bt_OpenFile;
        private System.Windows.Forms.TableLayoutPanel tlp_SQL_Server;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tx_db_IP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tx_db_UserName;
        private System.Windows.Forms.TextBox tx_db_Password;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button bt_OK;
        private System.Windows.Forms.Button bt_Cancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_DBServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_DBName;
        private System.Windows.Forms.Button bt_TestConnect;
    }
}
namespace SystemWindows
{
    partial class RegulationFrom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegulationFrom));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flp_DataField = new System.Windows.Forms.FlowLayoutPanel();
            this.bt_print = new System.Windows.Forms.Button();
            this.bt_Save = new System.Windows.Forms.Button();
            this.bt_Paste = new System.Windows.Forms.Button();
            this.bt_PageSetup = new System.Windows.Forms.Button();
            this.bt_PrintPreview = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.rtb_Regulation = new SystemWindows.RichWordControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flp_DataField, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_print, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_Save, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_Paste, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_PageSetup, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_PrintPreview, 5, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 28);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flp_DataField
            // 
            this.flp_DataField.AutoSize = true;
            this.flp_DataField.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp_DataField.BackColor = System.Drawing.Color.Transparent;
            this.flp_DataField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_DataField.Location = new System.Drawing.Point(3, 0);
            this.flp_DataField.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.flp_DataField.MinimumSize = new System.Drawing.Size(200, 20);
            this.flp_DataField.Name = "flp_DataField";
            this.flp_DataField.Size = new System.Drawing.Size(200, 28);
            this.flp_DataField.TabIndex = 4;
            this.flp_DataField.WrapContents = false;
            // 
            // bt_print
            // 
            this.bt_print.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_print.AutoSize = true;
            this.bt_print.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_print.Location = new System.Drawing.Point(746, 3);
            this.bt_print.Name = "bt_print";
            this.bt_print.Size = new System.Drawing.Size(51, 22);
            this.bt_print.TabIndex = 3;
            this.bt_print.Text = "打  印";
            this.bt_print.UseVisualStyleBackColor = true;
            // 
            // bt_Save
            // 
            this.bt_Save.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_Save.AutoSize = true;
            this.bt_Save.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_Save.Location = new System.Drawing.Point(551, 3);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(51, 22);
            this.bt_Save.TabIndex = 3;
            this.bt_Save.Text = "保  存";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // bt_Paste
            // 
            this.bt_Paste.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_Paste.AutoSize = true;
            this.bt_Paste.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_Paste.Location = new System.Drawing.Point(494, 3);
            this.bt_Paste.Name = "bt_Paste";
            this.bt_Paste.Size = new System.Drawing.Size(51, 22);
            this.bt_Paste.TabIndex = 3;
            this.bt_Paste.Text = "粘  贴";
            this.bt_Paste.UseVisualStyleBackColor = true;
            this.bt_Paste.Click += new System.EventHandler(this.bt_Paste_Click);
            // 
            // bt_PageSetup
            // 
            this.bt_PageSetup.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_PageSetup.AutoSize = true;
            this.bt_PageSetup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_PageSetup.Location = new System.Drawing.Point(608, 3);
            this.bt_PageSetup.Name = "bt_PageSetup";
            this.bt_PageSetup.Size = new System.Drawing.Size(63, 22);
            this.bt_PageSetup.TabIndex = 3;
            this.bt_PageSetup.Text = "页面设置";
            this.bt_PageSetup.UseVisualStyleBackColor = true;
            // 
            // bt_PrintPreview
            // 
            this.bt_PrintPreview.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_PrintPreview.AutoSize = true;
            this.bt_PrintPreview.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_PrintPreview.Location = new System.Drawing.Point(677, 3);
            this.bt_PrintPreview.Name = "bt_PrintPreview";
            this.bt_PrintPreview.Size = new System.Drawing.Size(63, 22);
            this.bt_PrintPreview.TabIndex = 3;
            this.bt_PrintPreview.Text = "打印预览";
            this.bt_PrintPreview.UseVisualStyleBackColor = true;
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // rtb_Regulation
            // 
            this.rtb_Regulation.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_Regulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Regulation.Location = new System.Drawing.Point(0, 28);
            this.rtb_Regulation.Name = "rtb_Regulation";
            this.rtb_Regulation.Size = new System.Drawing.Size(800, 422);
            this.rtb_Regulation.TabIndex = 1;
            this.rtb_Regulation.Text = "";
            // 
            // RegulationFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtb_Regulation);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegulationFrom";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "制度编辑";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RegulationFrom_FormClosed);
            this.Load += new System.EventHandler(this.RegulationFrom_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bt_print;
        private System.Windows.Forms.Button bt_Save;
        private System.Windows.Forms.Button bt_Paste;
        private RichWordControl rtb_Regulation;
        private System.Windows.Forms.FlowLayoutPanel flp_DataField;
        private System.Windows.Forms.Button bt_PageSetup;
        private System.Windows.Forms.Button bt_PrintPreview;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
    }
}
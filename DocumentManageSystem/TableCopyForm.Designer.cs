namespace DocumentManageSystem
{
    partial class TableCopyForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.bt_Del = new System.Windows.Forms.Button();
            this.bt_Save = new System.Windows.Forms.Button();
            this.bt_Left = new System.Windows.Forms.Button();
            this.bt_Right = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Caption = new System.Windows.Forms.TextBox();
            this.cb_IsSearched = new System.Windows.Forms.CheckBox();
            this.cb_NoNull = new System.Windows.Forms.CheckBox();
            this.cbox_Mode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(2, 2);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect;
            this.dataGridView1.ShowCellErrors = false;
            this.dataGridView1.ShowCellToolTips = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.ShowRowErrors = false;
            this.dataGridView1.Size = new System.Drawing.Size(541, 81);
            this.dataGridView1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_Caption, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_IsSearched, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cb_NoNull, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.cbox_Mode, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 89);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(239, 110);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 3);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.bt_Del, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.bt_Save, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.bt_Left, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.bt_Right, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 50);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(233, 57);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // bt_Del
            // 
            this.bt_Del.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_Del.Location = new System.Drawing.Point(20, 29);
            this.bt_Del.Name = "bt_Del";
            this.bt_Del.Size = new System.Drawing.Size(75, 25);
            this.bt_Del.TabIndex = 3;
            this.bt_Del.Text = "删  除";
            this.bt_Del.UseVisualStyleBackColor = true;
            this.bt_Del.Click += new System.EventHandler(this.bt_Del_Click);
            // 
            // bt_Save
            // 
            this.bt_Save.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bt_Save.Location = new System.Drawing.Point(137, 29);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(75, 25);
            this.bt_Save.TabIndex = 3;
            this.bt_Save.Text = "保  存";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // bt_Left
            // 
            this.bt_Left.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bt_Left.AutoSize = true;
            this.bt_Left.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_Left.Location = new System.Drawing.Point(87, 2);
            this.bt_Left.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bt_Left.Name = "bt_Left";
            this.bt_Left.Size = new System.Drawing.Size(27, 22);
            this.bt_Left.TabIndex = 4;
            this.bt_Left.Text = "<<";
            this.bt_Left.UseVisualStyleBackColor = true;
            this.bt_Left.Click += new System.EventHandler(this.bt_Left_Click);
            // 
            // bt_Right
            // 
            this.bt_Right.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.bt_Right.AutoSize = true;
            this.bt_Right.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_Right.Location = new System.Drawing.Point(118, 2);
            this.bt_Right.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bt_Right.Name = "bt_Right";
            this.bt_Right.Size = new System.Drawing.Size(27, 22);
            this.bt_Right.TabIndex = 4;
            this.bt_Right.Text = ">>";
            this.bt_Right.UseVisualStyleBackColor = true;
            this.bt_Right.Click += new System.EventHandler(this.bt_Right_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "字段名:";
            // 
            // tb_Caption
            // 
            this.tb_Caption.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tb_Caption.Location = new System.Drawing.Point(67, 2);
            this.tb_Caption.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tb_Caption.Name = "tb_Caption";
            this.tb_Caption.Size = new System.Drawing.Size(92, 21);
            this.tb_Caption.TabIndex = 1;
            // 
            // cb_IsSearched
            // 
            this.cb_IsSearched.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_IsSearched.AutoSize = true;
            this.cb_IsSearched.Location = new System.Drawing.Point(164, 4);
            this.cb_IsSearched.Name = "cb_IsSearched";
            this.cb_IsSearched.Size = new System.Drawing.Size(72, 16);
            this.cb_IsSearched.TabIndex = 4;
            this.cb_IsSearched.Text = "是否检索";
            this.cb_IsSearched.UseVisualStyleBackColor = true;
            // 
            // cb_NoNull
            // 
            this.cb_NoNull.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cb_NoNull.AutoSize = true;
            this.cb_NoNull.Location = new System.Drawing.Point(164, 28);
            this.cb_NoNull.Name = "cb_NoNull";
            this.cb_NoNull.Size = new System.Drawing.Size(72, 16);
            this.cb_NoNull.TabIndex = 4;
            this.cb_NoNull.Text = "是否必填";
            this.cb_NoNull.UseVisualStyleBackColor = true;
            // 
            // cbox_Mode
            // 
            this.cbox_Mode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbox_Mode.FormattingEnabled = true;
            this.cbox_Mode.Location = new System.Drawing.Point(67, 25);
            this.cbox_Mode.Margin = new System.Windows.Forms.Padding(0);
            this.cbox_Mode.Name = "cbox_Mode";
            this.cbox_Mode.Size = new System.Drawing.Size(92, 20);
            this.cbox_Mode.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "字段类型:";
            // 
            // TableManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 264);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "TableManagerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "报表设计";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TableManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.TableManagerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Caption;
        private System.Windows.Forms.Button bt_Del;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbox_Mode;
        private System.Windows.Forms.CheckBox cb_IsSearched;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button bt_Save;
        private System.Windows.Forms.CheckBox cb_NoNull;
        private System.Windows.Forms.Button bt_Left;
        private System.Windows.Forms.Button bt_Right;
    }
}
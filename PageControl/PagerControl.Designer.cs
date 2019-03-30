namespace PageControl
{
    partial class PagerControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bt_first = new System.Windows.Forms.Button();
            this.bt_front = new System.Windows.Forms.Button();
            this.bt_next = new System.Windows.Forms.Button();
            this.bt_last = new System.Windows.Forms.Button();
            this.tb_input = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.bt_go = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.bt_first, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_front, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_next, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_last, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_input, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.bt_go, 6, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(628, 22);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // bt_first
            // 
            this.bt_first.AutoSize = true;
            this.bt_first.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_first.Location = new System.Drawing.Point(0, 0);
            this.bt_first.Margin = new System.Windows.Forms.Padding(0);
            this.bt_first.Name = "bt_first";
            this.bt_first.Size = new System.Drawing.Size(39, 22);
            this.bt_first.TabIndex = 0;
            this.bt_first.Text = "首页";
            this.bt_first.UseVisualStyleBackColor = true;
            // 
            // bt_front
            // 
            this.bt_front.AutoSize = true;
            this.bt_front.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_front.Location = new System.Drawing.Point(39, 0);
            this.bt_front.Margin = new System.Windows.Forms.Padding(0);
            this.bt_front.Name = "bt_front";
            this.bt_front.Size = new System.Drawing.Size(51, 22);
            this.bt_front.TabIndex = 0;
            this.bt_front.Text = "上一页";
            this.bt_front.UseVisualStyleBackColor = true;
            // 
            // bt_next
            // 
            this.bt_next.AutoSize = true;
            this.bt_next.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_next.Location = new System.Drawing.Point(470, 0);
            this.bt_next.Margin = new System.Windows.Forms.Padding(0);
            this.bt_next.Name = "bt_next";
            this.bt_next.Size = new System.Drawing.Size(51, 22);
            this.bt_next.TabIndex = 0;
            this.bt_next.Text = "下一页";
            this.bt_next.UseVisualStyleBackColor = true;
            // 
            // bt_last
            // 
            this.bt_last.AutoSize = true;
            this.bt_last.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_last.Location = new System.Drawing.Point(521, 0);
            this.bt_last.Margin = new System.Windows.Forms.Padding(0);
            this.bt_last.Name = "bt_last";
            this.bt_last.Size = new System.Drawing.Size(39, 22);
            this.bt_last.TabIndex = 0;
            this.bt_last.Text = "末页";
            this.bt_last.UseVisualStyleBackColor = true;
            // 
            // tb_input
            // 
            this.tb_input.Location = new System.Drawing.Point(560, 0);
            this.tb_input.Margin = new System.Windows.Forms.Padding(0);
            this.tb_input.Name = "tb_input";
            this.tb_input.Size = new System.Drawing.Size(41, 21);
            this.tb_input.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(90, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(380, 21);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // bt_go
            // 
            this.bt_go.AutoSize = true;
            this.bt_go.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bt_go.Location = new System.Drawing.Point(601, 0);
            this.bt_go.Margin = new System.Windows.Forms.Padding(0);
            this.bt_go.Name = "bt_go";
            this.bt_go.Size = new System.Drawing.Size(27, 22);
            this.bt_go.TabIndex = 0;
            this.bt_go.Text = "GO";
            this.bt_go.UseVisualStyleBackColor = true;
            // 
            // PagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PagerControl";
            this.Size = new System.Drawing.Size(628, 22);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button bt_first;
        private System.Windows.Forms.Button bt_front;
        private System.Windows.Forms.Button bt_next;
        private System.Windows.Forms.Button bt_last;
        private System.Windows.Forms.TextBox tb_input;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button bt_go;
    }
}

namespace SystemWindows
{
    partial class DocumentViewwerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentViewwerForm));
            this.wbDocView = new AxSHDocVw.AxWebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.wbDocView)).BeginInit();
            this.SuspendLayout();
            // 
            // wbDocView
            // 
            this.wbDocView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbDocView.Enabled = true;
            this.wbDocView.Location = new System.Drawing.Point(0, 0);
            this.wbDocView.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("wbDocView.OcxState")));
            this.wbDocView.Size = new System.Drawing.Size(800, 450);
            this.wbDocView.TabIndex = 0;
            // 
            // DocumentViewwerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.wbDocView);
            this.Name = "DocumentViewwerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文档浏览";
            this.Load += new System.EventHandler(this.DocumentViewwerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wbDocView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxSHDocVw.AxWebBrowser wbDocView;
    }
}
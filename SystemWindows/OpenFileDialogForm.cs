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

namespace SystemWindows
{
    public partial class OpenFileDialogForm : Form
    {
        public OpenFileDialogForm()
        {
            InitializeComponent();
        }
        public string[] FileNames { get; set; }
        private void bt_Brouse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.FileNames = ofd.FileNames;
                StringBuilder sb = new StringBuilder();
                this.FileNames.ToList().ForEach(f=> sb.AppendLine(f));
                this.textBox1.Text = sb.ToString();
                this.textBox1.Height = (this.textBox1.Font.Height+10) * this.textBox1.Lines.Length;
            }
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
           if( this.FileNames.All(f=> File.Exists(f)))
            this.DialogResult = DialogResult.OK;
           else
            {
                MessageBox.Show("文件不存在，请重新选择！");
            }
        }
    }
}

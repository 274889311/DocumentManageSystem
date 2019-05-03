using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemCodeCreater
{
    public partial class CodeCreaterForm : Form
    {
        public CodeCreaterForm()
        {
            InitializeComponent();
            this.bt_OK.Click += bt_OK_Click;
        }
        private void bt_OK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox1.Text.Length < "BFEBFBFF000406C4".Length)
            {
                MessageBox.Show("请输入正确的设备码！");
            }
            else
            {
                textBox2.Text = AES.AESEncrypt(textBox1.Text + "zhangdahang");
            }
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

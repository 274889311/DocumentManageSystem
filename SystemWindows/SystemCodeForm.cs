using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystemWindows
{
    public partial class SystemCodeForm : Form
    {
        public SystemCodeForm()
        {
            InitializeComponent();
        }

        private void SystemCodeForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = SystemInformationCode.GetCpuID();
        }
        public static bool CheckSystemCode()
        {
            string systemCode = CommonConfigurationManager.GetAppConfig("SystemCode");
            return AES.AESEncrypt(SystemInformationCode.GetCpuID() + "zhangdahagn").Equals(systemCode);
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            string systemCode = AES.AESEncrypt(SystemInformationCode.GetCpuID() + "zhangdahagn");
            if (systemCode.Equals(textBox2.Text))
            {
                CommonConfigurationManager.UpdateAppConfig("SystemCode", systemCode);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("您输入的注册码不正确，请重新输入！");
            }
        }
    }
}

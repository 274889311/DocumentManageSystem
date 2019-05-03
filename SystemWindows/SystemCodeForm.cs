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
            if (systemCode.Trim() == "")
                return false;
            try
            {
                return AES.AESDecrypt(systemCode).Equals(SystemInformationCode.GetCpuID() + "zhangdahang");
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            string systemCode = AES.AESEncrypt(SystemInformationCode.GetCpuID() + "zhangdahang");
            try
            {
                if (AES.AESDecrypt(textBox2.Text).Equals(SystemInformationCode.GetCpuID() + "zhangdahang"))
                {
                    CommonConfigurationManager.UpdateAppConfig("SystemCode", textBox2.Text);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("您输入的注册码不正确，请重新输入！");
                }
            }
            catch
            {
                MessageBox.Show("您输入的注册码不正确，请重新输入！");
            }
        }
    }
}

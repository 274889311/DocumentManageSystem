using Common;
using DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemWindows
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
            
        }
        public static EnumUserRoler UserRoler = EnumUserRoler.User;
        public static string UserName = "User";
        public static string UserPassword = "123456";
        public static string RealName = "User";


        Dictionary<string, string> UserDatas = new Dictionary<string, string>();
        private void bt_OK_Click(object sender, EventArgs e)
        {
            DatabaseHelper.DBHelper helper = DatabaseHelper.DBHelper.GetDBHelper();
            //if(!helper.ExistTable("用户表"))
            //{
            //    new DatabaseCreater().CreateTableUser(helper.GetConnector());
            //}
            if (helper.Login(cmb_UserName.Text, tb_Password.Text,ref RealName, ref UserRoler))
            {
                UserName = cmb_UserName.Text;
                UserPassword = tb_Password.Text;
                if (cb_Remember.Checked)
                {
                    // 登录时 如果没有Data.bin文件就创建、有就打开
                    FileStream fs = new FileStream("userData.bin", FileMode.OpenOrCreate);
                    BinaryFormatter bf = new BinaryFormatter();

                    // 保存在实体类属性中
                    if (UserDatas.Any(u => u.Key == cmb_UserName.Text))
                    {
                        UserDatas.Remove(cmb_UserName.Text);
                    }
                    UserDatas.Add(cmb_UserName.Text, tb_Password.Text);
                    //写入文件
                    bf.Serialize(fs, UserDatas);
                    //关闭
                    fs.Close();
                    fs.Dispose();
                    fs = null;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("您输入的用户名或密码不正确，请重新输入！");
                UserDatas.Remove(cmb_UserName.Text);
                FileStream fs = new FileStream("userData.bin", FileMode.OpenOrCreate);
                BinaryFormatter bf = new BinaryFormatter();
                //写入文件
                bf.Serialize(fs, UserDatas);
                //关闭
                fs.Close();
                fs.Dispose();
                fs = null;
                cmb_UserName.Text = "";
                tb_Password.Text = "";
                cmb_UserName.Focus();
                cmb_UserName.DataSource = UserDatas.Keys.ToList();
                if(cmb_UserName.Items.Count>0)
                    cmb_UserName.SelectedIndex = 0;
                
            }
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {
            SystemSettingForm.DatabaseType = EnumDatabaseType.SqlServer;
            //string connectString = Properties.Settings.Default["DocMngDBConnectionString"].ToString();
            string connectString = CommonConfigurationManager.GetAppConfig("ConnectString");
            if (connectString == null 
                || !DatabaseHelper.DBHelper.GetDBHelper(SystemSettingForm.DatabaseType).TestDBConnect(connectString))
            {
                SystemSettingForm ssf = new SystemSettingForm();
                if (ssf.ShowDialog() == DialogResult.OK)
                {
                    CommonConfigurationManager.UpdateAppConfig("ConnectString", ssf.ConnectString);
                    Application.Restart();

                }
                else
                    Application.Exit();
            }
            else
                DatabaseHelper.DBHelper.GetDBHelper(SystemSettingForm.DatabaseType).SetDBConnectString(connectString);

            if (!File.Exists("userData.bin")) return;
            cmb_UserName.SelectedIndexChanged += (obj, ee) =>
              {
                  if (UserDatas.ContainsKey(cmb_UserName.Text))
                      tb_Password.Text = UserDatas[cmb_UserName.Text];
              };
            try
            {
                //读取文件流对象
                FileStream fs = new FileStream("userData.bin", FileMode.OpenOrCreate);
                if (fs.Length > 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    //读出存在Data.bin 里的用户信息
                    UserDatas = bf.Deserialize(fs) as Dictionary<string,string>;
                    if (UserDatas != null)
                    {
                       cmb_UserName.DataSource = UserDatas.Keys.ToList();
                       if(cmb_UserName.Items.Count>0)
                            cmb_UserName.SelectedIndex = 0;
                    }
                    else
                    {
                        File.Delete("userData.bin");
                        cb_Remember.Checked = false;
                    }
                }
                else
                {
                    UserDatas = new Dictionary<string, string>();
                }
                fs.Close();
                fs.Dispose();
                fs = null;
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
        }
    }
}

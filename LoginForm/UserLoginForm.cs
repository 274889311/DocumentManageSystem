using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Com.HTRadar.RainfallDetecter.Rainfall.Forms.Forms
{
    public partial class UserLoginForm : Form
    {
        public UserLoginForm()
        {
            InitializeComponent();
        }
        List<Database.Tbl_User> UserDatas = new List<Database.Tbl_User>();
        public Database.Tbl_User LoginUser { get; set; }
        private void bt_OK_Click(object sender, EventArgs e)
        {
            var users = Database.DatabaseUtil.GetAllTbl_User();
            LoginUser = users.Where(u => u.Name == cmb_UserName.Text && u.Password == tb_Password.Text).FirstOrDefault();
            if (LoginUser !=null)
            {
                DialogResult = DialogResult.OK;
                if(cb_Remember.Checked)
                {
                    try
                    {
                        // 登录时 如果没有Data.bin文件就创建、有就打开
                        FileStream fs = new FileStream("userData.bin", FileMode.OpenOrCreate);
                        BinaryFormatter bf = new BinaryFormatter();

                        // 保存在实体类属性中
                        if (UserDatas.Any(u => u.Name == LoginUser.Name))
                        {
                            UserDatas.Remove(users.Where(u => u.Name == LoginUser.Name).FirstOrDefault());
                        }
                        UserDatas.Insert(0, LoginUser);
                        //写入文件
                        bf.Serialize(fs, UserDatas);
                        //关闭
                        fs.Close();
                    }
                    catch(Exception ex)
                    {
                        LogWriter.LogError(ex.Message);
                    }
                }
            }
            else if (!users.Any(user => user.Name == cmb_UserName.Text))
            {
                MessageBox.Show("您输入的用户名不存在，请重新输入！");
                cmb_UserName.Text = "";
                cmb_UserName.Focus();

            }
            else
            {
                MessageBox.Show("您输入的密码不正确，请重新输入！");
                tb_Password.Text = "";
                tb_Password.Focus();
            }
        }

        private void UserLoginForm_Load(object sender, EventArgs e)
        {
            cmb_UserName.DisplayMember = "Name";
            cmb_UserName.DropDownStyle = ComboBoxStyle.DropDown;
            cmb_UserName.Focus();
            bt_Cancel.Click += (obj, ee) => { DialogResult = DialogResult.Cancel; };
            if (!File.Exists("userData.bin")) return;
            try
            {
                //读取文件流对象
                FileStream fs = new FileStream("userData.bin", FileMode.OpenOrCreate);
                if (fs.Length > 0)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    //读出存在Data.bin 里的用户信息
                    UserDatas = bf.Deserialize(fs) as List<Database.Tbl_User>;
                    if (UserDatas != null)
                    {
                        cmb_UserName.DataSource = UserDatas;
                        cmb_UserName.SelectedIndex = 0;
                    }
                    else
                    {
                        File.Delete("userData.bin");
                        cb_Remember.Checked = false;
                    }
                }
                fs.Close();
            }
            catch(Exception ex)
            {
                LogWriter.LogError(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_Password.Text = UserDatas.Where(u => u.Name == cmb_UserName.Text).FirstOrDefault().Password;
            cmb_UserName.Focus();
        }

        private void bt_ClearLogin_Click(object sender, EventArgs e)
        {
            cmb_UserName.DataSource = new object[] { };
            cmb_UserName.Text = "";
            tb_Password.Text = "";
            if (File.Exists("userData.bin")) File.Delete("userData.bin"); 

        }

        private void UserLoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                DialogResult = DialogResult.Cancel;

        }
    }
}

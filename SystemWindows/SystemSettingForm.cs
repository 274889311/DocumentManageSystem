using Common;
using DatabaseHelper;
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
    public partial class SystemSettingForm : Form
    {
        public SystemSettingForm()
        {
            InitializeComponent();
        }
        public string ConnectString = "";
        public static EnumDatabaseType DatabaseType = EnumDatabaseType.SqlFile;
        private void cb_db_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConnectString = AES.GetDecryptConnectString(CommonConfigurationManager.GetAppConfig("ConnectString"));
            string[] sqlStrArray = null;
            if(ConnectString!=null && ConnectString.Contains(";"))
                sqlStrArray = ConnectString.Split(';');
            if (cb_db_Type.Text == cb_db_Type.Items[0].ToString())
            {
                tlp_SQL_File.Visible = false;
                tlp_SQL_Server.Visible = true;
                DatabaseType = EnumDatabaseType.SqlServer;
                if (sqlStrArray != null && sqlStrArray.All(str=>!str.Contains("AttachDbFilename")))
                {
                    string serverName = sqlStrArray.Where(cs => cs.Contains("Data Source")).FirstOrDefault();
                    if (serverName != null)
                    {
                        serverName = serverName.Substring(serverName.IndexOf('=') + 1);
                        tx_db_IP.Text = serverName.Trim();
                    }
                }
            }
            else
            {
                tlp_SQL_File.Visible = true;
                tlp_SQL_Server.Visible = false;
                DatabaseType = EnumDatabaseType.SqlFile;
                if (sqlStrArray != null)
                {
                    string filePath = sqlStrArray.Where(cs => cs.Contains("AttachDbFilename")).FirstOrDefault();
                    if (filePath != null)
                    {
                        filePath = filePath.Substring(filePath.IndexOf('=') + 1);
                        tx_db_FilePath.Text = filePath.Trim();
                    }
                }
            }
            if (sqlStrArray != null)
            {
                string userId = sqlStrArray.Where(cs => cs.Contains("User ID")).FirstOrDefault();
                if (userId != null)
                {
                    userId = userId.Substring(userId.IndexOf('=') + 1);
                    tx_db_UserName.Text = userId.Trim();
                }

                string password = sqlStrArray.Where(cs => cs.Contains("Password")).FirstOrDefault();
                if (password != null)
                {
                    password = password.Substring(password.IndexOf('=') + 1);
                    tx_db_Password.Text = password.Trim();
                }
            }
        }

        private void SystemSettingForm_Load(object sender, EventArgs e)
        {
            ConnectString = AES.GetDecryptConnectString(CommonConfigurationManager.GetAppConfig("ConnectString"));
            cb_db_Type.SelectedIndex = 0;
        }

        private void bt_OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Sql数据库文件(*.mdf)|*.mdf";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tx_db_FilePath.Text = ofd.FileName;
            }

        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            if(cb_db_Type.SelectedItem == cb_db_Type.Items[0])
            {
                if (this.tx_db_IP.Text.Trim() != "")
                    ConnectString = @"Data Source = " + this.tx_db_IP.Text + ",1433; Initial Catalog = " + this.tb_DBName.Text + ";";
                else
                    ConnectString = @"Server = " + this.tb_DBServer.Text + "; Database = " + this.tb_DBName.Text + "; Trusted_Connection = false;";
            }
            else
            {
                ConnectString = "Data Source = .\\SQLEXPRESS; AttachDbFilename =\"" + tx_db_FilePath.Text + "\"; Integrated Security = True; Connect Timeout = 30;";
            }
            if (tx_db_UserName.Text != "" && tx_db_Password.Text != "")
                ConnectString += "User ID = " + tx_db_UserName.Text + "; Password = " + tx_db_Password.Text + ";";

            if (DBHelper.GetDBHelper(DatabaseType).TestDBConnect(ConnectString))
            {
                CommonConfigurationManager.UpdateAppConfig("ConnectString", AES.GetEncryptConnectString(ConnectString));
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(DBHelper.GetDBHelper().GetErrorMesasge());
            }
        }

        private void bt_TestConnect_Click(object sender, EventArgs e)
        {
            if (cb_db_Type.SelectedItem == cb_db_Type.Items[0])
            {
                if (this.tx_db_IP.Text.Trim() != "")
                    ConnectString = @"Data Source = " + this.tx_db_IP.Text + ",1433; Initial Catalog = " + this.tb_DBName.Text + ";";
                else
                    ConnectString = @"Server = " + this.tb_DBServer.Text + "; Database = " + this.tb_DBName.Text + "; Trusted_Connection = false;";
            }
            else
            {
                ConnectString = "Data Source = .\\SQLEXPRESS; AttachDbFilename =\"" + tx_db_FilePath.Text + "\"; Integrated Security = True; Connect Timeout = 30;";
            }
            if (tx_db_UserName.Text != "" && tx_db_Password.Text != "")
                ConnectString += "User ID = " + tx_db_UserName.Text + "; Password = " + tx_db_Password.Text + ";";

            if (DBHelper.GetDBHelper(DatabaseType).TestDBConnect(ConnectString))
            {
                MessageBox.Show("测试连接成功！");
            }
            else
            {
                MessageBox.Show(DBHelper.GetDBHelper().GetErrorMesasge());
            }
        }
    }
}

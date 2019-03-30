using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemWindows
{
    public partial class AlterUserPasswordForm : Form
    {
        public AlterUserPasswordForm()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length >= textBox2.Text.Length)
            {
                pictureBox1.Visible = true;
                if (textBox3.Text == textBox2.Text )
                {
                    pictureBox1.Image = Properties.Resources.right;
                    if(textBox1.Text == UserLogin.UserName)
                        bt_OK.Enabled = true;

                }
                else
                {
                    pictureBox1.Image = Properties.Resources.wrong;
                    bt_OK.Enabled = false;
                }
            }
            else
                pictureBox1.Visible = false;
        }

        private void AlterUserPasswordForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            bt_OK.Enabled = false;
            textBox1.LostFocus += (obj, ee) =>
                {
                    pictureBox2.Visible = true;
                    if (textBox1.Text != UserLogin.UserPassword)
                    {
                        pictureBox2.Image = Properties.Resources.wrong;
                        
                    }
                    else
                    {
                        pictureBox2.Image = Properties.Resources.right;
                        
                    }
                };
            textBox1.GotFocus += (obj, ee) =>
                {
                    pictureBox2.Visible = false;
                };
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            DatabaseHelper.DBHelper helper = DatabaseHelper.DBHelper.GetDBHelper();
            helper.Excute("update TableUser set Password = " + textBox3.Text + " where UserName='" + UserLogin.UserName + "'");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void bt_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}

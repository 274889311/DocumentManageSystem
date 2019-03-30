using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentManageSystem
{
    public partial class TableModeForm : Form
    {
        public TableModeForm()
        {
            InitializeComponent();
        }
        public TableModeForm(string parentName):this()
        {
            ParentName = parentName;
        }
        private string ParentName = "";
        public string TableModeName
        {
            get
            {
                return textBox1.Text;
            }
        }
        private void bt_OK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请输入报表分类名称!", "提示");
                return;

            }
            else
            {
                DatabaseHelper.DBHelper helper = DatabaseHelper.DBHelper.GetDBHelper();
                helper.Excute("insert into 报表类型 values ((select ID from 报表类型 where 类型名称='"+ParentName+"'), '" + textBox1.Text + "',CAST((select count(*) from 报表类型) as varchar(20)))");
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}

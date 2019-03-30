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
    public partial class EditNameForm : Form
    {
        public EditNameForm(string tableName)
        {
            InitializeComponent();
            TableName = tableName;

            bt_Cancel.Click += (sender, e) =>
              {
                  this.DialogResult = DialogResult.Cancel;
              };
           if( Directory.Exists(Application.StartupPath + "\\RDLC"))
            {
                comboBox1.Items.AddRange(Directory.GetFiles(Application.StartupPath + "\\RDLC").Select(f => Path.GetFileNameWithoutExtension(f)).ToArray());
            }
            LabelTempleteName = "报表模板";
        }
        public string LabelTableName { get { return label1.Text.Remove(label1.Text.Length-1); } set { label1.Text = value+":";this.Text = "输入" + value; } }
        public string TableName { get { return textBox1.Text; }set { textBox1.Text = value; } }

        public string LabelTempleteName { get { return label2.Text.Remove(label2.Text.Length - 1); } set { label2.Text = value + ":"; } }

        public string TemplateName { get { return comboBox1.SelectedItem==null?"":comboBox1.SelectedItem.ToString(); }
            set {
                if (value == null) return;
                if (comboBox1.Items.Contains(value))
                {
                    comboBox1.SelectedItem = value;
                }
                else
                    comboBox1.SelectedItem = null;
            }
        }
        private void bt_OK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" )
            {
                TableName = textBox1.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请输入"+ LabelTableName + "！");
            }
        }
    }
}

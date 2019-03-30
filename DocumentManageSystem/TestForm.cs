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
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            

        }
        string[] items = new string[] {
            "asdf",
            "asdf1",
            "asdf12",
            "asdf213",
            "asdf234223",
            "asdf3455",
            "asdf54674567",

        };
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void bt_View_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange( items.Where(item => item.Contains(comboBox1.Text)).ToArray());
            //comboBox1.DropDownStyle = ComboBoxStyle.;
        }
    }
}

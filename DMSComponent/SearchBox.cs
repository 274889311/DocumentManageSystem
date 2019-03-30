using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMSComponent
{
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
            this.Load += SearchBox_Load;
        }

        private void SearchBox_Load(object sender, EventArgs e)
        {
           
        }

        public object DataSource { get { return listBox1.DataSource; } set { listBox1.DataSource = value; } }
        public string ValueMember { get { return listBox1.ValueMember; } set { listBox1.ValueMember = value; } }
        public string DisplayMember { get { return listBox1.DisplayMember; } set { listBox1.DisplayMember = value; } }
        public string TextValue {get{ return textBox1.Text; }set { textBox1.Text = value; } }
    }
}

using DatabaseHelper;
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
    public partial class DeviceCareWarnForm : Form
    {
        public DeviceCareWarnForm()
        {
            InitializeComponent();
        }

        private void DeviceCareWarnForm_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = DBHelper.GetDBHelper().GetTableNames().Select(kv=>kv.Key).ToArray();
            comboBox1.DisplayMember = "报表名称";
            comboBox1.SelectedIndexChanged += (obj, ee) =>
              {
                  IFieldTable table = DBHelper.GetDBHelper().GetTableFields(comboBox1.Text);
                  comboBox2.DataSource = table.GetTableFields(false).Where(f => f.Type == "smalldatetime" || f.Type == "date").Select(f=>f.Name).ToArray();
              };
            dataGridView1.DataSourceChanged += (obj, ee) =>
              {
                  foreach (DataGridViewColumn col in dataGridView1.Columns)
                  {
                      if (col.HeaderText != "设备表名" && col.HeaderText != "保养时间" && col.HeaderText != "保养周期")
                      {
                          col.Visible = false;
                      }
                  }
              };
            dataGridView1.DataSource = DBHelper.GetDBHelper().GetDataFromTable(0, 0, "保养提醒", "");
        }

        private void bt_Save_Click(object sender, EventArgs e)
        {
            IFieldTable table = DBHelper.GetDBHelper().GetTableFields("保养提醒");

            table.UpdateFieldValue(new TableField() { Name = "设备表名", Value = comboBox1.Text });
            table.UpdateFieldValue(new TableField() { Name = "保养时间", Value = comboBox2.Text });
            table.UpdateFieldValue(new TableField() { Name = "保养周期", Value = numericUpDown1.Value.ToString() });
            if (dataGridView1.DataSource!=null)
            {
                DataTable dt = dataGridView1.DataSource as DataTable;
                
                if (dt.Select("设备表名='"+comboBox1.Text+ "' and 保养时间='"+comboBox2.Text+"'").Length>0)
                {
                    DBHelper.GetDBHelper().UpdateTableField(table);
                    return;
                }
            }
            table.UpdateFieldValue("修改人", UserLogin.UserName);
            table.UpdateFieldValue("修改时间", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DBHelper.GetDBHelper().InsertIntoTable(table);
            dataGridView1.DataSource = DBHelper.GetDBHelper().GetDataFromTable(0, 0, "保养提醒", "");
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}

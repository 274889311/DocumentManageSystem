using DatabaseHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TemplateHelper;

namespace SystemWindows
{
    public partial class DataDetailFrom : Form
    {
        public DataDetailFrom(string tableMode)
        {
            InitializeComponent();
            this.ShowIcon = false;
            if(this.DesignMode)
                  {
                      return;
                  }
            this.Load += (sender, e) =>
              {
                  this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;  //自动调动datagridview的行高度
                  this.dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;//设置datagridview字段显示部的内容;
                  this.dataGridView1.RowHeadersVisible = false;
                  this.dataGridView1.AutoSize = true;
                  this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                  this.dataGridView1.AllowUserToAddRows = false;

                  pagerControl1.PageChangedEvent += (pageindex) =>
                  {
                      var dt = TotalDataTable.Clone();
                      for (int i = (pageindex-1) * DefaultPageSize; i< TotalDataTable.Rows.Count && i < pageindex * DefaultPageSize + DefaultPageSize; i++)
                          dt.Rows.Add(TotalDataTable.Rows[i].ItemArray);
                      //dt = DBHelper.GetDBHelper().GetDataFromTable(DefaultPageSize, pageindex, Name + DataDetailFlag, "DataID='" + DataID + "'");
                      if(dt.Columns.Contains("DataID")) dt.Columns.Remove("DataID");
                      dataGridView1.DataSource = dt;
                      if (dt.Columns.Contains("ID")) dataGridView1.Columns["ID"].Visible = false;
                      CaculateGoal(TotalDataTable.DefaultView.ToTable(false, new string[] { "得分" }));
                  };
                  if (DataID != "")
                  {
                      TotalDataTable = DBHelper.GetDBHelper().GetDataFromTable(0,0, Name + DataDetailFlag, "DataID ='" + DataID + "'");
                      if(TotalDataTable.Rows.Count == 0)
                      {
                          TotalDataTable = CreateTableFromFile();
                          //bt_Export.Visible = false;
                          //this.dataGridView1.AllowUserToAddRows = true;
                          //dt = DBHelper.GetDBHelper().GetDataFromTable(0, 0, Name + DataDetailTemplate, "");
                          //if (dt == null)
                          //{
                          //    dt = CreateTableFromFile();
                          //    if (dt != null)
                          //    {
                          //        dataGridView1.ReadOnly = true;
                          //    }
                          //}
                          //else
                          //  dataGridView1.DataSource = dt;
                          //dataGridView1.DataSource = TotalDataTable;
                          SaveData();
                          //recordCount = DBHelper.GetDBHelper().GetDataRecordCount(Name + DataDetailFlag, "DataID ='" + DataID + "'");
                      }
                      pagerControl1.SetPageCount((int)Math.Ceiling((double)TotalDataTable.Rows.Count / DefaultPageSize));
                  }
                  else
                  {
                      TotalDataTable = CreateTableFromFile();
                      SaveData();
                      pagerControl1.SetPageCount((int)Math.Ceiling((double)TotalDataTable.Rows.Count / DefaultPageSize));
                  }
                  this.dataGridView1.CellEndEdit += (obj, ee) =>
                    {
                        string columnName = dataGridView1.Columns[ee.ColumnIndex].Name;
                        TotalDataTable.Rows[(pagerControl1.CurrentPageIndex-1) * DefaultPageSize + ee.RowIndex][columnName] = dataGridView1.Rows[ee.RowIndex].Cells[columnName].Value.ToString();

                        CaculateGoal(TotalDataTable.DefaultView.ToTable(false, new string[] { "得分" }));
                    };
                  
              };
        }
        private int DefaultPageSize =8;
        DataTable TotalDataTable;
        string DataDetailTemplate = "Template";
        string DataDetailFlag = "DataDetail";
        string TableMode = "";
        public string DataID = "";
        float FullGoad { get { float goal = 0;float.TryParse(textBox1.Text, out goal);return goal; } }
        private void bt_Save_Click(object sender, EventArgs e)
        {
            SaveData(true);
        }
        private void SaveData(bool msgBox = false)
        {
            if (!DBHelper.GetDBHelper().ExistTable(Name + DataDetailFlag))
            {
                List<TableField> fields = new List<TableField>();
                fields.Add(new TableField() { Name = "DataID" });
                fields.AddRange(GetTable(TotalDataTable, Name + DataDetailFlag).GetTableFields(false, false).ToArray());
                DBHelper.GetDBHelper().CreateTable(TableMode, Name + DataDetailFlag, fields.ToArray(), false, false);
            }
            //if(DataID!="")
            //    DBHelper.GetDBHelper().DelDataFromTable(Name + DataDetailFlag, DataID, "DataID");
            //DataTable dt = DBHelper.GetDBHelper().GetDataFromTable(0, 0, Name + DataDetailFlag, "DataID='" + DataID + "'");
            foreach (DataRow row in TotalDataTable.Rows)
            {
                IFieldTable tableFields = GetFieldTable(TotalDataTable, row, Name + DataDetailFlag);
                tableFields.AddField(new TableField() { Name = "DataID", Type = TableField.FieldTypeList[TableFieldType.文本], Value = DataID });
                if (!TotalDataTable.Columns.Contains("ID"))
                    DBHelper.GetDBHelper().InsertIntoTable(tableFields, false);
                else
                    DBHelper.GetDBHelper().UpdateTableField(Name + DataDetailFlag, row["ID"].ToString(), tableFields.GetTableFields(false));
            }
            TotalDataTable = DBHelper.GetDBHelper().GetDataFromTable(0, 0, Name + DataDetailFlag, "DataID ='" + DataID + "'");
            if (msgBox && MessageBox.Show("保存完成！是否退出？", "退出？", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void CreateTable(DataGridView dgv  , string tableName)
        {
            DBHelper.GetDBHelper().CreateTable(TableMode, tableName, GetTable(dgv, tableName).GetTableFields(false,false),false,false);
            foreach (DataGridViewRow row in dgv.Rows)
            {
                IFieldTable tableFields = GetFieldTable(dgv, row,tableName);
                tableFields.TableName = tableName;
                DBHelper.GetDBHelper().InsertIntoTable(tableFields,false);
            }
        }
        private IFieldTable GetTable(DataTable dt, string tableName)
        {
            BaseFieldTable table = new BaseFieldTable(tableName);
            foreach (DataColumn col in dt.Columns)
            {
                table.AddField(new TableField() { Name = col.ColumnName, Type = TableField.FieldTypeList[TableFieldType.长文本] });
            }
            return table;
        }
        private IFieldTable GetTable(DataGridView dgv, string tableName)
        {
            BaseFieldTable table = new BaseFieldTable(tableName);
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                table.AddField(new TableField() { Name = col.HeaderText,Type = TableField.FieldTypeList[TableFieldType.长文本]});
            }
            return table;
        }
        private IFieldTable GetFieldTable(DataTable dt, DataRow dataRow, string tableName)
        {
            IFieldTable table = DBHelper.GetDBHelper().GetTableFields(tableName);
            foreach (DataColumn col in dt.Columns)
            {
                if (dataRow[col] != null)
                    table.UpdateFieldValue(new TableField() { Name = col.ColumnName, Value = dataRow[col].ToString() });
            }
            return table;
        }
        private IFieldTable GetFieldTable(DataGridView dgv, DataGridViewRow dataRow,string tableName)
        {
            IFieldTable table = DBHelper.GetDBHelper().GetTableFields(tableName);
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (dataRow.Cells[col.Index].Value != null)
                    table.UpdateFieldValue(new TableField() { Name = col.Name, Value = dataRow.Cells[col.Index].Value.ToString() });
            }
            return table;
        }
        private DataTable CreateTableFromFile()
        {
            if (!File.Exists("TableTemplate\\" + Name + DataDetailTemplate + ".txt"))
            {
                MessageBox.Show("没有找到报表模板！");
                return null;
            }
            DataTable dt = new DataTable();
            StreamReader sr = new StreamReader("TableTemplate\\" + Name + DataDetailTemplate + ".txt", Encoding.GetEncoding("GB2312"));
            while (!sr.EndOfStream)
            {

                string[] values = sr.ReadLine().Split('\t');
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.AddRange(values.Select(v => new DataColumn(v)).ToArray());
                }
                else
                    dt.Rows.Add(values);
            }
            sr.Close();
            sr = null;

            dt = OrderTable(dt);
            //if (DBHelper.GetDBHelper().ExistTable(Name + DataDetailTemplate))
            //    DBHelper.GetDBHelper().DeleteTable(Name + DataDetailTemplate);
            //dataGridView1.DataSource = dt;
            //CreateTable(dataGridView1, Name + DataDetailTemplate);
            //dt = DBHelper.GetDBHelper().GetDataFromTable(0, 0, Name + DataDetailTemplate, "");
            //dataGridView1.DataSource = dt;
            return dt;
        }
        /// <summary>
        /// 删 除空行
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable OrderTable(DataTable dt)
        {
            if (dt == null) return dt;
            List<DataRow> rows = new List<DataRow>();
            for(int i  = dt.Rows.Count-1; i>0;i--)
            {
                if(dt.Rows[i].ItemArray.Where(item=>item .ToString()!="").Count() == 1)
                {
                    int colIndex = dt.Rows[i].ItemArray.Select((item, index) => new { index, item }).Where(it => it.item != null && it.item.ToString() != "").Select(it =>it.index).FirstOrDefault();
                    dt.Rows[i - 1][colIndex] = dt.Rows[i - 1][colIndex].ToString() + "\r\n" + dt.Rows[i ][colIndex].ToString();
                    rows.Add(dt.Rows[i]);
                }
                else if(dt.Rows[i].ItemArray.Where(item => item.ToString() != "").Count() == 0)
                {
                    rows.Add(dt.Rows[i]);
                }
            }
            foreach (var row in rows)
                dt.Rows.Remove(row);
            return dt;
        }

        private void CaculateGoal(DataTable dt)
        {
            if (dt.Columns.Count == 0 || dt.Rows.Count == 0)
                return;
            List<string> goals = new List<string>();
            foreach (DataRow row in dt.DefaultView.ToTable(false,new string[] { dt.Columns[dt.Columns.Count - 1].ColumnName }).Rows)
            {
                goals.Add(row[0].ToString());
            }
            float totalGoal = goals.Select(g =>
            {
                float f = 0;
                float.TryParse(g, out f);
                return f;
            }).Sum();
            lb_TotalGoal.Text = totalGoal.ToString("f1");
            lb_FinalGoal.Text = (totalGoal / FullGoad * 100).ToString("f1");
        }

        private void bt_Export_Click(object sender, EventArgs e)
        {
            ITemplateHelper templateHelper = ATemplateHelper.GetTemplateHelper(this.TableMode, Name+DataDetailTemplate);
            string[] columns = DBHelper.GetDBHelper().GetTableFields(Name + DataDetailTemplate).GetTableFields(false).Select(f => f.Name).ToArray();
            templateHelper.OutPut(TotalDataTable.DefaultView.ToTable(true, columns));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateHelper
{
    class TemplateWordHelper:ATemplateHelper
    {
        public TemplateWordHelper(string filePath) : base(filePath) { }
        public override DataTable InPut(string tableName, string filePath)
        {
            throw new NotImplementedException();
        }

        public override void OutPut(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                ReportWord reporter = new ReportWord();
                reporter.CreateNewDocument(base.m_TemplateFilePath);
                foreach (DataColumn col in dt.Columns)
                {
                    if (!reporter.InsertValue(col.Caption, row[col].ToString()))
                    {
                        MessageBox.Show("书签：\"" + col.Caption + "\"在模板\r\n\"" + m_TemplateFilePath + "\"中没有找到，请检查导出模板是否正确！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                string fileExt = dt.Columns[0].DataType == typeof(DateTime) ? DateTime.Parse(row[0].ToString()).ToString("yyyyMMddHHmmss") : row[0].ToString();
                reporter.SaveDocument(base.m_OutputFilePath.Insert(m_OutputFilePath.LastIndexOf("."), fileExt));
            }

                    
        }
    }
}

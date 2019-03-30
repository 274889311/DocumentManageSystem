using Spire.Xls;
using Spire.Xls.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemplateHelper
{
    class TemplateSpireExcelHelper : ATemplateHelper
    {

        public TemplateSpireExcelHelper(string filePath) : base(filePath) { }
        public override void OutPut(DataTable dt)
        {
            //创建Workbook实例
            Workbook workbook = new Workbook();
            //加载Excel文件
            if (File.Exists(base.m_TemplateFilePath))
                workbook.LoadFromFile(base.m_TemplateFilePath);
            else
                workbook.Worksheets.Add("sheet1");
            //获取第1张工作表
            Worksheet sheet = workbook.Worksheets[0];

            if (File.Exists(base.m_TemplateFilePath))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (workbook.NameRanges.Contains(dt.Columns[j].Caption))
                        {

                            //定义第一个全局命名区域
                            INamedRange NamedRange1 = workbook.NameRanges[dt.Columns[j].Caption];
                            NamedRange1.RefersToRange.Value = dt.Rows[i][j].ToString();
                        }
                    }
                    string fileExt = dt.Columns[0].DataType == typeof(DateTime) ? DateTime.Parse(dt.Rows[i][0].ToString()).ToString("yyyyMMddHHmmss") : dt.Rows[i][0].ToString();
                    workbook.SaveToFile(base.m_OutputFilePath.Insert(m_OutputFilePath.LastIndexOf("."), fileExt));
                }
            }
            else
            {
                sheet.InsertDataTable(dt, true, 1, 1, -1, -1);
                workbook.SaveToFile(base.m_OutputFilePath);
                workbook.Dispose();
                if (MessageBox.Show("Excel导出成功：" + base.m_OutputFilePath + "\r\n是否要打开？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(m_OutputFilePath);
                }
            }
        }
        public override DataTable InPut(string filePath)
        {
            return new DataTable();
        }
    }
}

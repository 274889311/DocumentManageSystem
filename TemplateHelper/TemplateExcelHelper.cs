using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;

namespace TemplateHelper
{
   
    /// <summary>
    /// Excel帮助类
    /// string column = "商品编码,商品名称,刊登单号,门店名称";
    /// 导入数据
    /// var action = new Action<string, DataTable>((str, dtExcel) =>
    /// {
    /// this.dgvData.DataSource = dtExcel;
    /// });
    /// excelHelper.ImportExcelToDataTable(this, action, "Ebay侵权下线");
    /// 导出模版
    /// string message = string.Empty;
    //  excelHelper.SaveExcelTemplate(column.Split(','), "Ebay侵权下线", "Ebay侵权下线", ref message);
    /// </summary>
    public class TemplateExcelHelper: ATemplateHelper
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        public TemplateExcelHelper(string filePath) : base(filePath) { }

        public override void OutPut(DataTable dt)
        {
            bool isShowExcel = false;

            if (dt == null)
            {
                return;
            }
            if (dt.Rows.Count == 0)
            {
                //return;
            }
           Missing miss = Missing.Value;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("请确保您的电脑已经安装Excel！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //xlApp.UserControl = true;
            Microsoft.Office.Interop.Excel.Workbooks workBooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workBook = null;
            if (!File.Exists(base.m_TemplateFilePath))
            {
                workBook = workBooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);//创建新的
            }
            else
            {
                workBook = workBooks.Add(base.m_TemplateFilePath);//根据现有excel模板产生新的Workbook
            }
            Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[1];//获取sheet1
            xlApp.DisplayAlerts = false;//保存Excel的时候，不弹出是否保存的窗口直接进行保存
            //workSheet.get_Range("A3", "B3").Merge(workSheet.get_Range("A3", "B3").MergeCells);//合并单元格
            if (workSheet == null)
            {
               MessageBox.Show("请确保您的电脑已经安装Excel！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                //Microsoft.Office.Interop.Excel.Range range = null;
                xlApp.Visible = isShowExcel;//若是true,则在导出的时候会显示excel界面
                int totalCount = dt.Rows.Count;

                if (File.Exists(base.m_TemplateFilePath))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            for (int k = 0; k < workBook.Names.Count; k++)
                            {
                                if (workBook.Names.Item(dt.Columns[j].Caption)!=null)//.Name == dt.Columns[j].Caption)
                                {
                                    //定义第一个全局命名区域
                                    int row = workBook.Names.Item(dt.Columns[j].Caption).RefersToRange.Row;
                                    int col = workBook.Names.Item(dt.Columns[j].Caption).RefersToRange.Column;
                                    workSheet.Cells[row+i+1, col] = "'" + dt.Rows[i][j].ToString();
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("没有找到数据表格模板，是否按默认的格式导出？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        return;
                    }

                    workSheet.Cells[1, 1] = dt.TableName;//导出标题
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        workSheet.Cells[4, j + 1] = dt.Columns[j].ColumnName;
                        workBook.Names.Add(dt.Columns[j].ColumnName, workSheet.Cells[4, j + 1]);//, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        //workBook.Names.Item(dt.Columns[j].ColumnName).RefersTo = workSheet.get_Range(workSheet.Cells[4, j + 1]);
                    }
                    Microsoft.Office.Interop.Excel.Range newExpenseTypeRange = workSheet.get_Range(workSheet.Cells[1, 1], workSheet.Cells[3, dt.Columns.Count]);
                    newExpenseTypeRange.Merge(workSheet.get_Range(workSheet.Cells[1, 1], workSheet.Cells[3, dt.Columns.Count]).MergeCells);//合并单元格
                    newExpenseTypeRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    newExpenseTypeRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    //写入数值
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j].GetType().Equals(typeof(DateTime)))
                                workSheet.Cells[i + 5, j + 1] = ((DateTime)(dt.Rows[i][j])).ToString("yyyy-MM-dd HH:mm:ss");
                            else
                                workSheet.Cells[i + 5, j + 1] = dt.Rows[i][j];//项目序号
                        }
                    }
                    WorkSheetPageSet(xlApp, workSheet);
                }
                string fileExt = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileOutPut = base.m_OutputFilePath.Insert(m_OutputFilePath.LastIndexOf("."), fileExt);
                workBook.SaveAs(fileOutPut, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //workSheet.SaveAs(base.templateFilePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workBooks.Close();
                if (MessageBox.Show("Excel导出成功：" + fileOutPut + "\r\n是否要打开？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(fileOutPut);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel导出失败，错误：" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                xlApp.Quit();
            }
            finally
            {

                xlApp.Quit();
            }
        }///// <summary>
         /// 1.Clear CircleReference
         /// 2.Set Page to Fit Wide
         /// 3.Set Column Text fit
         /// </summary>
         /// <param name="app"></param>
         /// <param name="ws"></param>
        private void WorkSheetPageSet(Microsoft.Office.Interop.Excel.Application app, Microsoft.Office.Interop.Excel.Worksheet ws)
        {
            //ClearCircleReference(ws);
            SetPagetoFitWide(ws);
            SetColumnFit(ws);
        }
        ///// <summary>
        /// Set Column Text fit
        /// </summary>
        /// <param name="sheet"></param>
        private static void SetColumnFit(Microsoft.Office.Interop.Excel.Worksheet sheet)
        {
            char column = 'B';
            for (int i = 0; i < 25; i++)
            {
                Microsoft.Office.Interop.Excel.Range range = sheet.get_Range(String.Format("{0}1", column.ToString()),
                 String.Format("{0}1", column.ToString()));
                if (range != null)
                {
                    range.EntireColumn.AutoFit();
                }
                column++;
            }
        }
        ///// <summary>
        /// Clear CircleReference
        /// </summary>
        /// <param name="sheet">Worksheet object</param>
        private void ClearCircleReference(Microsoft.Office.Interop.Excel.Worksheet sheet)
        {
            Microsoft.Office.Interop.Excel.Range range = sheet.CircularReference;
            while (range != null)
            {
                range.Clear();
                range = sheet.CircularReference;
            }
        }
        ///// <summary>
        /// Set Page to Fit Wide
        /// </summary>
        /// <param name="ws">Worksheet object</param>
        private static void SetPagetoFitWide(Microsoft.Office.Interop.Excel.Worksheet ws)
        {
            ws.PageSetup.Zoom = false;
            ws.PageSetup.FitToPagesWide = 1;
            ws.PageSetup.FitToPagesTall = false;
        }



        public override DataTable InPut(string tableName, string filePath)
        {
            bool isShowExcel = false;
            DataTable dataTableResult = null;
            Missing miss = Missing.Value;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                MessageBox.Show("请确保您的电脑已经安装Excel！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            //xlApp.UserControl = true;
            Microsoft.Office.Interop.Excel.Workbooks workBooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workBook = null;
            if (!File.Exists(filePath))
            {
                MessageBox.Show("未找到数据表格导：\r\n\""+base.m_TemplateFilePath+"\"", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            else
            {
                workBook = workBooks.Add(filePath);//根据现有excel模板产生新的Workbook
            }
            Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[1];//获取sheet1
            xlApp.DisplayAlerts = false;//保存Excel的时候，不弹出是否保存的窗口直接进行保存
            if (workSheet == null)
            {
                MessageBox.Show("请确保您的电脑已经安装Excel！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            try
            {
                xlApp.Visible = isShowExcel;
                dataTableResult = new DataTable(tableName);
                int rowIndexStart = 0;
                for (int k = 1; k < workBook.Names.Count + 1; k++)
                {
                    if (!dataTableResult.Columns.Contains(workBook.Names.Item(k).Name))
                    {
                        dataTableResult.Columns.Add(workBook.Names.Item(k).Name);
                    }
                    rowIndexStart = workBook.Names.Item(k).RefersToRange.Row;
                }
                
                for (int i = rowIndexStart+1; i < workSheet.UsedRange.Rows.Count+1; i++)
                {
                    DataRow newRow = dataTableResult.NewRow();
                    for (int k = 1; k < workBook.Names.Count+1; k++)
                    {
                        if (!dataTableResult.Columns.Contains(workBook.Names.Item(k).Name))
                        {
                            dataTableResult.Columns.Add(workBook.Names.Item(k).Name);
                        }

                        //定义第一个全局命名区域
                        int row = workBook.Names.Item(k).RefersToRange.Row;
                        int col = workBook.Names.Item(k).RefersToRange.Column;
                        //newRow[k-1] = workSheet.Cells[row + i + 1, col].ToString();
                        var value = workSheet.get_Range(workSheet.Cells[i, col], workSheet.Cells[i, col]).Value;
                        if(value.GetType().Equals(typeof(DateTime)))
                        {
                            newRow[k - 1] = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                            newRow[k - 1] = Convert.ToString(workSheet.get_Range(workSheet.Cells[i , col], workSheet.Cells[i , col]).Value);
                    }
                    dataTableResult.Rows.Add(newRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel导出失败，错误：" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                xlApp.Quit();
            }
            finally
            {

                xlApp.Quit();
            }
            return dataTableResult;
        }


        #region 参考方法
        /// <summary>
        /// 保存Excel模版
        /// </summary>
        /// <param name="columns">列名,例如：商品编码,商品名称,刊登单号,门店名称</param>
        /// <param name="FileName">文件名，例如：Ebay侵权下线</param>
        /// <param name="SheetName">工作表名称，例如：Ebay侵权下线</param>
        /// <param name="message">错误信息</param>
        public void SaveExcelTemplate(string[] columns, string FileName, string SheetName, ref string message)
        {
            string Filter = "Excel文件|*.csv|Excel文件|*.xls|Excel文件|*.xlsx";

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.DefaultExt = "csv";
            saveFileDialog1.FileName = FileName;
            saveFileDialog1.Filter = Filter;
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.Title = "Excel文件";
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            //获得文件路径
            string localFilePath = saveFileDialog1.FileName.ToString();
            if (Regex.IsMatch(localFilePath, @"\.csv$"))
            {
                localFilePath = Regex.Replace(saveFileDialog1.FileName, @"\.csv$", "", RegexOptions.IgnoreCase) + ".csv";
                File.WriteAllText(localFilePath, string.Join(",", columns), Encoding.Default);
            }
            else
            {
                //获取文件路径，不带文件名
                ArrayToExcelTemplate(columns, localFilePath, SheetName, ref message);
            }

            if (string.IsNullOrEmpty(message))
                MessageBox.Show("\n\n导出完毕! ", "提示 ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 导出模版
        /// </summary>
        /// <param name="columns">列名,例如：商品编码,商品名称,刊登单号,门店名称</param>
        /// <param name="localFilePath">本地路径</param>
        /// <param name="SheetName">工作表名称，例如：Ebay侵权下线</param>
        /// <param name="message">错误信息</param>
        public void ArrayToExcelTemplate(string[] columns, string localFilePath, string SheetName, ref string message)
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
            {
                message = "无法创建Excel对象，可能计算机未安装Excel！";
                return;
            }

            //創建Excel對象
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            if (worksheet == null) worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
            Microsoft.Office.Interop.Excel.Range range = null;

            long totalCount = columns.Length;
            worksheet.Name = SheetName;//第一个sheet在Excel中显示的名称
            int c;
            c = 0;
            ////写入标题
            for (int i = 0, count = columns.Length; i < count; i++)
            {
                //if (string.IsNullOrEmpty(columns[i])) continue;
                worksheet.Cells[1, c + 1] = columns[i];
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, c + 1];
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//居中  
                c++;

            }

            try
            {
                localFilePath = Regex.Replace(localFilePath, ".xls$|.xlsx$", "", RegexOptions.IgnoreCase);
                localFilePath += xlApp.Version.CompareTo("11.0") == 0 ? ".xls" : ".xlsx";
                workbook.SaveCopyAs(localFilePath);
            }
            catch (Exception ex)
            {
                message = "生成Excel附件过程中出现异常，详细信息如：" + ex.ToString();
            }


            try
            {
                if (xlApp != null)
                {

                    int lpdwProcessId;
                    GetWindowThreadProcessId(new IntPtr(xlApp.Hwnd), out lpdwProcessId);
                    System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
                }
            }
            catch (Exception ex)
            {
                message = "Delete Excel Process Error:" + ex.Message;
            }

        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="form"></param>
        /// <param name="callback"></param>
        public void ImportExcelToDataTable(Form form, Action<string, DataTable> callback, string SheetName = "Sheet1")
        {
            string Filter = "Excel文件|*.csv|Excel文件|*.xls|Excel文件|*.xlsx";

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Excel文件";
            openFileDialog1.Filter = Filter;
            openFileDialog1.ValidateNames = true;
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            var action = new Action(() =>
            {
                string localFilePath = openFileDialog1.FileName;
                if (File.Exists(localFilePath))
                {
                    string message = string.Empty;
                    string fileExten = Path.GetExtension(localFilePath);

                    DataTable dtExcel;
                    if (fileExten.ToLower().Contains(".csv"))
                    {
                        dtExcel = ImportCSVFile(localFilePath, "Table1", ref message);
                    }
                    else
                    {
                        dtExcel = ImportExcelFile(localFilePath, "Table1", SheetName, ref message);
                    }

                    if (callback != null)
                    {
                        if (form.InvokeRequired)
                        {
                            form.Invoke(callback, message, dtExcel);
                        }
                        else
                        {
                            callback(message, dtExcel);
                        }
                    }
                }
            });

            action.BeginInvoke(null, null);
        }

        /// <summary>
        /// 执行导入
        /// </summary>
        /// <param name="strFileName">对应文件路径</param>
        /// <param name="typeName">返回的Table名称</param>
        /// <param name="message">返回的错误</param>
        /// <returns>DataTable</returns>
        public DataTable ImportCSVFile(string strFileName, string typeName, ref string message)
        {
            if (string.IsNullOrEmpty(strFileName)) return null;

            string line = string.Empty;
            string[] split = null;
            bool isReplace;
            int subBegion;
            int subEnd;
            string itemString = string.Empty;
            string oldItemString = string.Empty;
            DataTable table = new DataTable(typeName);
            DataRow row = null;
            StreamReader sr = new StreamReader(strFileName, System.Text.Encoding.Default);
            //创建与数据源对应的数据列 
            line = sr.ReadLine();
            split = line.Split(',');
            foreach (String colname in split)
            {
                table.Columns.Add(colname, System.Type.GetType("System.String"));
            }
            //将数据填入数据表 
            int j = 0;
            while ((line = sr.ReadLine()) != null)
            {
                subEnd = 0;
                subBegion = 0;

                if (line.IndexOf('\"') > 0)
                {
                    isReplace = true;
                }
                else
                {
                    isReplace = false;
                }
                itemString = string.Empty;
                while (isReplace)
                {

                    subBegion = line.IndexOf('\"');
                    subEnd = line.Length - 1;
                    if (line.Length - 1 > subBegion)
                    {
                        subEnd = line.IndexOf('\"', subBegion + 1);
                    }

                    if (subEnd - subBegion > 0)
                    {
                        itemString = line.Substring(subBegion, subEnd - subBegion + 1);
                        oldItemString = itemString;
                        itemString = itemString.Replace(',', '|').Replace("\"", string.Empty);
                        line = line.Replace(oldItemString, itemString);

                    }

                    if (line.IndexOf('\"') == -1)
                    {
                        isReplace = false;
                    }

                }

                j = 0;
                row = table.NewRow();
                split = line.Split(',');
                foreach (String colname in split)
                {
                    row[j] = colname.Replace('|', ',');
                    j++;
                }
                table.Rows.Add(row);
            }
            sr.Close();
            //显示数据 

            return table;
        }


        /// <summary>
        /// Excel执行导入
        /// </summary>
        /// <param name="strFileName">对应文件路径</param>
        /// <param name="typeName">返回的Table名称</param>
        /// <param name="message">返回的错误</param>
        /// <returns></returns>
        public DataTable ImportExcelFile(string strFileName, string typeName, string SheetName, ref string message)
        {
            if (string.IsNullOrEmpty(strFileName)) return null;
            DataSet Exceldt;
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            OleDbConnection con = new OleDbConnection();
            try
            {
                //OleDbDataAdapter ExcelO = new OleDbDataAdapter(selectStr, @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + strFileName + ";Extended Properties=Excel 8.0;");
                string ConnStr = xlApp.Version.CompareTo("11.0") == 0 ? @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + strFileName + ";Extended Properties='Excel 8.0;IMEX=1;HDR=YES;'" : @"Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0;IMEX=1;HDR=YES'; Data Source=" + strFileName;
                con.ConnectionString = ConnStr;
                con.Open();
                DataTable dtOle = con.GetSchema("Tables");
                DataTableReader dtReader = new DataTableReader(dtOle);
                string TableName = "";
                while (dtReader.Read())
                {
                    TableName = dtReader["Table_Name"].ToString();
                    break;
                }
                OleDbDataAdapter excel = new OleDbDataAdapter(string.Format("select * from [" + SheetName + "$];", TableName), ConnStr);
                //OleDbDataAdapter excel = new OleDbDataAdapter(selectStr, @"Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties='Excel 8.0;IMEX=1;HDR=YES'; Data Source=" + strFileName);

                Exceldt = new DataSet();
                excel.Fill(Exceldt, typeName);
                return Exceldt.Tables.Count > 0 ? Exceldt.Tables[0] : null;
            }
            catch (OleDbException ex)
            {
                message = ex.Message;
                return null;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion
    }
}
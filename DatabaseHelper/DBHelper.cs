using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper
{
    /// <summary>
    /// 数据库操作辅助类
    /// </summary>
    public class DBHelper
    {
        private DBHelper(EnumDatabaseType databaseType)
        {
            switch (databaseType)
            {
                case EnumDatabaseType.SqlFile:
                    connector = new SqlServerConnector();
                    break;
                case EnumDatabaseType.SqlServer:
                    connector = new SqlServerConnector();
                    break;
            }
        }
        IConnector connector = null;
        public static DBHelper helper = null;
        public static DBHelper GetDBHelper(EnumDatabaseType databaseType = EnumDatabaseType.SqlFile)
        {
            if (helper == null)
                helper = new DBHelper(databaseType);
            return helper;
        }
        public IConnector GetConnector()
        {
            return connector;
        }
        /// <summary>
        /// 执行一个语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int Excute(string sql)
        {
            return connector.Excute(sql);
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="userRoler"></param>
        /// <returns></returns>
        public bool Login(string userName, string password,ref string realName, ref EnumUserRoler userRoler)
        {
            DataTable dt = connector.Query("select * from 用户表 where 用户名 = '" + userName + "' and 密码='" + password + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                connector.Query("update 用户表 set 最后登录时间 ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where  用户名 = '" + userName + "'");
                if (dt.Rows[0]["是否为管理员"].ToString().ToUpper() == "TRUE")
                {
                    userRoler = EnumUserRoler.Admin;
                }
                else
                    userRoler = EnumUserRoler.User;

                realName = dt.Rows[0]["真实姓名"].ToString();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取职能报表分类
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetModes(string tableName ="")
        {
            //if(!connector.IsTableExists("报表类型"))
            //{
            //    new DatabaseCreater().CreateTableMode(connector);
            //}
            //if (!connector.IsTableExists("报表目录"))
            //{
            //    DatabaseCreater.GetCreater().CreateTableTree(connector);
            //}
            //if (!connector.IsTableExists("用户表"))
            //{
            //    DatabaseCreater.GetCreater().CreateTableUser(connector);
            //}
            //if (!connector.IsTableExists("保养提醒"))
            //{
            //    DatabaseCreater.GetCreater().CreateTableDeviceCare(connector);
            //}
            //if (!connector.IsTableExists("文档表"))
            //{
            //    new DatabaseCreater().CreateTableDoc(connector);
            //}
            string strSQL = "select ID, ParentID, 类型名称,说明  from  报表类型";
            if(tableName!="")
            {
                strSQL += " where ID in (select 报表类型ID from 报表目录 where 报表名称=N'" + tableName + "')";
            }
            DataTable dt = connector.Query(strSQL);
            DataTable newDT = new DataTable();
            newDT.Columns.Add(new DataColumn("ID", typeof(int)));
            newDT.Columns.Add(new DataColumn("ParentID", typeof(int)));
            newDT.Columns.Add(new DataColumn("类型名称", typeof(string)));
            newDT.Columns.Add(new DataColumn("说明", typeof(int)));
            try
            {
                foreach (DataRow row in dt.Rows)
                    newDT.Rows.Add(row.ItemArray);
            }
            catch(Exception e)
            {

            }
            //SortedList<int, string> modes = new SortedList<int, string>();
            //foreach (DataRow row in dt.Rows)
            //{
            //    modes.Add(int.Parse(row[1].ToString()), row[0].ToString());
            //}
            return newDT;//modes.Values.ToArray();
        }
        /// <summary>
        /// 获取某个分类下的所有职能报表
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public KeyValuePair<string, string>[] GetTableNames(string mode = "")
        {
            DataTable dt;
            if (mode == "")
                dt = connector.Query("select 报表名称,备注 from 报表目录");
            else
                dt = connector.Query("select 报表名称,备注 from 报表目录 join 报表类型 on 报表类型.ID = 报表目录.报表类型ID and 报表类型.类型名称=N'" + mode + "'");
            List<KeyValuePair<string, string>> modes = new List<KeyValuePair<string, string>>();
            foreach (DataRow row in dt.Rows)
            {
                KeyValuePair<string, string> keyValue = new KeyValuePair<string, string>(row[0].ToString(), row[1].ToString());
                modes.Add(keyValue);
            }
            return modes.ToArray();
        }
        /// <summary>
        /// 获取职能表的所有字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IFieldTable GetTableFields(string tableName)
        {
            TableField[] fields = connector.GetFieldMark(tableName);
            BaseFieldTable table = new BaseFieldTable(tableName);
            fields.ToList().ForEach(f => table.AddField(f));
            return table;
        }
        /// <summary>
        /// 查询某个职能表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool ExistTable(string tableName)
        {
            if (connector.IsTableExists(tableName))
                return true;
            else
                return false;
        }
        public int DeleteTable(string tableName)
        {
            string strSQL = "Drop table " + tableName + "";
            connector.Excute(strSQL);

            strSQL = "delete from 报表目录 where 报表名称=N'" + tableName + "'";
            return connector.Excute(strSQL);
        }
        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        public void AddColumns(string tableName, TableField[] fields)
        {
            var tableFields = GetTableFields(tableName).GetTableFields();
            string strSQL = "ALTER TABLE " + tableName + " ADD ";
            foreach (TableField tf in fields)
            {
                if (tableFields.Any(f=>f.Name == tf.Name ))
                {
                    AlterColumns(tableName, new TableField[] { tf });
                }
                else
                {
                    connector.Excute(strSQL + " [" + tf.Name + "] " + FixFieldType(tf.Type)+(tf.NoNull? " NOT NULL " : ""));
                }
            }
            connector.SetFieldMark(tableName, "", fields);

        }
        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        public void DelColumns(string tableName, TableField[] fields)
        {
            string strSQL = "ALTER TABLE " + tableName + " DROP  ";
            foreach (TableField tf in fields)
            {
                connector.Excute(strSQL + " column " + tf.Name);
            }
            connector.DelFieldMark(tableName, fields);
        }
        /// <summary>
        /// 修改表中的列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        public void AlterColumns(string tableName, TableField[] fields)
        {
            string strSQL = " ALTER TABLE " + tableName + " ALTER ";
            foreach (TableField tf in fields)
            {
                connector.Excute(strSQL + " column " + tf.Name + " " + FixFieldType(tf.Type)+(tf.NoNull?" NOT NULL ":""));
            }
            connector.SetFieldMark(tableName, "", fields);
        }
        /// <summary>
        /// 修正数据库字段类型 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string FixFieldType(string type)
        {
            if (type.Contains("varchar"))
                type = "varchar(50)";
            if (type.Contains("NUMERIC"))
                type = "NUMERIC(6,2)";
            return type;
        }
        /// <summary>
        /// 获取表中记录条数
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
         public int GetDataRecordCount(string tableName,string where = "")
        {
            string strSQL = " select Count(*) from " + tableName+ (where!=""?" where "+ where:"");
            int iResult = 0;
            string sResult = connector.QueryStrReturn(strSQL).ToString();
            int.TryParse(sResult, out iResult);
            return iResult;
        }
        /// <summary>
        /// 分页查询，条件
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetDataFromTable(int pageSize, int pageIndex, string tableName,string whereClause,string[] Columns = null)
        {
            if (!ExistTable(tableName)) return null;

            string[] fields = null; 
            if(Columns!=null)
            {
                fields = Columns;
            }
            else
            {
                fields = connector.GetFieldMark(tableName).Select(f=>f.Name).ToArray();
            }
            DataTable data = null;
            //查询
            if (pageIndex < 1 || pageSize < 1)
            {
                data = connector.Query("select * from " + tableName + (whereClause==""?"":" where " + whereClause));
            }
            else
            {
                string cols = string.Join(",", fields.Where(f=>f!= "文档").ToArray());
                string strSQL = "select top " + pageSize + " "+ cols + " from " + tableName ;
                if(whereClause!="")
                {
                    strSQL += " where " + whereClause;
                }
                if (pageIndex > 1)
                {
                    if (strSQL.Contains("where"))
                    {
                        strSQL += " and ";
                    }
                    else
                    {
                        strSQL += " where ";
                    }
                    //strSQL += " ID not in(select top " + pageSize * (pageIndex - 1) + " ID from " + tableName + " order by ID desc)  order by ID desc";
                    strSQL += " ID not in(select top " + pageSize * (pageIndex - 1) + " ID from " + tableName + ")";

                }
                data = connector.Query(strSQL);
            }
            data.TableName = tableName;
            return data;
        }
        /// <summary>
        /// 删 除一条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="recordID"></param>
        public void DelDataFromTable(string tableName, string recordID)
        {
            string strSQL = " delete from " + tableName + " where ID=" + recordID;
            connector.Excute(strSQL);
        }
        public void DelDataFromTable(string tableName, string keyWord,string columnName)
        {
            string strSQL = " delete from " + tableName + " where "+ columnName + "=" + keyWord;
            connector.Excute(strSQL);
        }
        public DataTable GetDeviceCareTable()
        {
            DataTable deviceCareTable = new DataTable();
            deviceCareTable.Columns.AddRange(new string[] { "设备名称", "下次保养时间" }.Select(n => new DataColumn(n)).ToArray());
            DataTable dt = DBHelper.GetDBHelper().GetDataFromTable(0, 0, "保养提醒", "");
            foreach (DataRow row in dt.Rows)
            {
                int days = int.Parse(row["保养周期"].ToString());
                DataTable deviceTable = DBHelper.GetDBHelper().GetDataFromTable(0, 0, row["设备表名"].ToString(), row["保养时间"].ToString() + " < " + DateTime.Now.AddDays(-days + 7).ToString("yyyy-MM-dd"));
                string deviceName = "";
                foreach (DataColumn col in deviceTable.Columns)
                {
                    if (col.Caption.Contains("名称"))
                    {
                        deviceName = col.Caption;
                    }
                }
                if (deviceTable.Rows.Count > 0 && deviceName != "")
                {
                    foreach (DataRow deviceRow in deviceTable.DefaultView.ToTable(true, new string[] { deviceName, row["保养时间"].ToString() }).Rows)
                    {
                        DataRow r = deviceCareTable.NewRow();
                        r.ItemArray = new string[] { deviceRow[0].ToString(), DateTime.Parse(deviceRow[1].ToString()).AddDays(days).ToString("yyyy-MM-dd") };
                    }
                }
            }
            return deviceCareTable;
        }
        /// <summary>
        /// 获到职能表中一条记录对象的附件列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="recordID"></param>
        /// <returns></returns>
        public string[] GetTableRecordDocList(string tableName, string recordID)
        {
            

            string sql = "select 文档名称 from 文档表 where 所属报表ID in (select ID from 报表目录 where 报表名称=N'" + tableName + "') and " +
                "数据记录ID in (select ID from " + tableName + " where ID =" + recordID + ")";
            List<string> files = new List<string>();
            DataTable dt = connector.Query(sql);
            foreach (DataRow row in dt.Rows)
            {
                files.Add(row[0].ToString());
            }
            return files.ToArray();
        }
        /// <summary>
        /// 读取一个附件，并在本地临时目录生成此文件
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="recordID"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetDocFromTableRecord(string tableName, string recordID,string fileName)
        {
            string sql = "select 文档内容 from 文档表 where 所属报表ID in (select ID from 报表目录 where 报表名称=N'" + tableName + "')" +
                " and 数据记录ID in (select ID from " + tableName + " where ID =" + recordID + ")" +
                " and 文档名称 like N'%" + fileName + "%'";
            byte[] bytges = GetDocFromTableRecordBytes(tableName, recordID, fileName);
            if (bytges.Length>0)
                return CreateFileByBytes(fileName, bytges);
            else return "";
        }
        public byte[] GetDocFromTableRecordBytes(string tableName, string recordID, string fileName)
        {
            string sql = "select 文档内容 from 文档表 where 所属报表ID in (select ID from 报表目录 where 报表名称=N'" + tableName + "')" +
                " and 数据记录ID in (select ID from " + tableName + " where ID =" + recordID + ")" +
                " and 文档名称 like N'%" + fileName + "%'";
            object obj = connector.QueryStrReturn(sql);
            if (obj != null && obj.ToString() != "-1")
                return (byte[])obj;
            else return new byte[0];
        }
        /// <summary>
        /// 给对应的职能表中的某条记录上传一个附件
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="recordID"></param>
        /// <param name="filePath"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int InsertIntoTableRecordDoc(string tableName, int recordID, string filePath,string userName)
        {
            return InsertIntoTableRecordDoc(tableName, recordID, GetFileBytes(filePath), Path.GetFileName(filePath), userName);
        }
        public int InsertIntoTableRecordDoc(string tableName, int recordID, byte[] bytes,string fileName, string userName)
        {
            Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();
            string strSQL = "Insert into 文档表 values ((select ID from 报表目录 where 报表名称=N'" + tableName + "')," + recordID + ",N'" + fileName +
                "',@image,N'" + userName + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            files.Add("@image", bytes);
            int iResult = connector.Excute(strSQL, files);
            if (iResult > 0)
            {
                string sResult = connector.QueryStrReturn("select ident_current('文档表')").ToString();
                int.TryParse(sResult, out iResult);
            }
            return iResult;
        }
        /// <summary>
        /// 删 除职能条中一件记录对应附件中的一个
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="recordID"></param>
        /// <param name="docName"></param>
        /// <returns></returns>
        public int DeleteTableRecordDoc(string tableName, int recordID, string docName)
        {
            string strSQL = "delete from 文档表 where 所属报表ID in (select ID from 报表目录 where 报表名称=N'" + tableName + "') and " +
                " 数据记录ID in (select ID from " + tableName + " where ID =" + recordID + ")"+
                " and 文档名称=N'"+docName+"'";

           return connector.Excute(strSQL);
        }
        /// <summary>
        /// 向职能表中插入一条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public int InsertIntoTable(string tableName, TableField[] fields)
        {
            string strSQL = "Insert into " + tableName + " (";
            foreach (TableField field in fields)
            {
                strSQL += " [" + field.Name + "],";
            }
            strSQL = strSQL.Remove(strSQL.Length - 1);
            strSQL += " ) Values (";
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (TableField field in fields)
            {
                string param = "@" + field.Name;
                strSQL += param+",";
                //if(field.Type == "smalldatetime")
                    parameters.Add(new SqlParameter(param, field.Value));
                //else
                //    parameters.Add(new SqlParameter(param, "'"+field.Value+"'"));
                //strSQL += "N'" + field.Value + "',";
            }
            strSQL = strSQL.Remove(strSQL.Length - 1);
            strSQL += ") select  @@identity;";
            int iResult = connector.Excute(strSQL, parameters.ToArray());
            //if (iResult > 0)
            //{
            //    string sResult = connector.QueryStrReturn("select ident_current('" + tableName + "')").ToString();
            //    int.TryParse(sResult, out iResult);
            //}
            return iResult;
        }
        /// <summary>
        /// 向职能表中插入一条记录
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int InsertIntoTable(IFieldTable table, bool withCommon = true)
        {
            var tableFields = table.GetTableFields(withCommon).Where(f => f.Name.ToUpper() != "ID").ToArray();
            return  InsertIntoTable(table.TableName, tableFields);
            //Connection conn = null;
            //conn = dataSource.getConnection();
            //FileInputStream fis = new FileInputStream(new File(demo.zip));
            ////sql根据自己的实际情况去写，file_content字段属性是varbinary(max);
            //PreparedStream pre = conn.prepareStatment("insert into table (file_content) values(?)");
            //pre.setBlob(1, fis);    //这就是关键的一步，是不是很简单粗暴
            //pre.execute();
        }
        /// <summary>
        /// 根据二进制流创建一个本地文 件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private string CreateFileByBytes(string fileName, byte[] bytes)
        {
            string filePath = Path.GetTempPath() + fileName;
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter br = new BinaryWriter(fs);
            br.Write(bytes);
            br.Close();
            fs.Close();
            return filePath;

        }
        /// <summary>
        /// 将本地文 件读取为二进制字节流
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private byte[] GetFileBytes(string fileName)
        {
            byte[] imgBytesIn;
            Stream fs = null;
            BinaryReader br;
            try
            {
                Image img = Image.FromFile(fileName);
                string tempFile = Path.GetTempPath() + Path.GetFileName(fileName);
                fs =new FileStream(tempFile, FileMode.Create, FileAccess.ReadWrite);
                ImageCompress.Compress(img, fs);
                fs.Seek(0, SeekOrigin.Begin);
                img.Dispose();
            }
            catch(Exception e)
            {
                if(fs!=null) fs.Close();
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            }
            finally
            {
                br = new BinaryReader(fs);
                imgBytesIn = br.ReadBytes((int)fs.Length);
                br.Close();
                fs.Close();
            }
            return imgBytesIn;
        }
        /// <summary>
        /// 更新职能表中的某一条记录
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int UpdateTableField(IFieldTable table)
        {
            var tableFields = table.GetTableFields();

            return UpdateTableField(table.TableName, tableFields.Where(f => f.Name.ToUpper() == "ID").FirstOrDefault().Value, tableFields);
        }
        /// <summary>
        /// 更新职能表中的某一条记录
        /// </summary>
        /// <param name="talbeName"></param>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public int UpdateTableField(string talbeName, string id, TableField[] fields)
        {
            Debug.Assert(fields != null, "没有要更新的数据");

            string strSQL = "Update " + talbeName + " set ";

            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (TableField field in fields)
            {
                if (field.IsExtend && field.Name == "ID")
                {
                    continue;
                }
                string param = "@" + field.Name;
                strSQL += field.Name + "= "+ param + ",";
                //if (field.Type == "smalldatetime")
                    parameters.Add(new SqlParameter(param, field.Value));
                //else
                //    parameters.Add(new SqlParameter(param, "'" + field.Value + "'"));
                //strSQL += "N'" + field.Value + "',";
            }


            strSQL = strSQL.Remove(strSQL.Length - 1);
            strSQL += " where id =" + id;
            connector.Excute(strSQL, parameters.ToArray());
            int iResult = -1;
            int.TryParse(id, out iResult);
            return iResult;
        }
        /// <summary>
        /// 创建一个职能表，并记录在报表目录树中
        /// </summary>
        /// <param name="tableMode"></param>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        public void CreateTable(string tableMode, string tableName,  TableField[] fields, bool widthExtend = true,bool withoutextend = true)
        {
           if( DatabaseCreater.GetCreater().CreateTable(connector, tableName, tableMode, fields, widthExtend, withoutextend)!=0)
                connector.Excute("INSERT INTO 报表目录([报表类型ID],[报表名称]) values ((select top 1 ID from 报表类型 where 类型名称=N'" + tableMode + "'), N'" + tableName + "')");
        }
        public void CreateTable(string tableMode, string tableName,string templateName, TableField[] fields,bool widthExtend = true, bool withoutextend = true)
        {
            if (DatabaseCreater.GetCreater().CreateTable(connector, tableName, tableMode, fields,widthExtend, withoutextend)!=0)
            //if (tableMode != "")
                connector.Excute("INSERT INTO 报表目录([报表类型ID],[报表名称],[备注]) values ((select top 1 ID from 报表类型 where 类型名称=N'" + tableMode + "'), N'" + tableName + "','"+templateName+"')");
        }
        /// <summary>
        /// 删除一个职能表
        /// </summary>
        /// <param name="tableName"></param>
        public void RemoveTable(string tableName)
        {
            connector.Excute(" delete from 报表目录 where 报表名称=N'" + tableName + "' ");
            if (connector.IsTableExists(tableName))
                connector.Excute(" drop table " + tableName);
        }
        /// <summary>
        /// 获取数据库操作的最后一条错误
        /// </summary>
        /// <returns></returns>
        public string GetErrorMesasge()
        {
            return  connector.GetErrorMessage();
        }
        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public bool TestDBConnect(string connectString)
        {
            return connector.TestDBConnect(connectString);
        }
        /// <summary>
        /// 设置 新的数据库连接
        /// </summary>
        /// <param name="connString"></param>
        public void SetDBConnectString(string connString)
        {
            connector.SetDBConnectString(connString);
        }
    }
}

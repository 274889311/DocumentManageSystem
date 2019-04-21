using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper
{
    public class SqlServerConnector : AConnector
    {
        public SqlServerConnector(string connectionString = "")
        {
            if(connectionString!="")
            base.connector = new SqlConnection(connectionString);
        }
        public override int Excute(string sql, Dictionary<string, object> files, bool bTran = false)
        {
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand(sql, (SqlConnection)connector);
                foreach (string key in files.Keys)
                {
                    cmd.Parameters.Add(new SqlParameter(key, files[key]));
                    //cmd.Parameters[key].Value = files[key];
                }
                connector.Open();
                if (bTran)
                    transaction = connector.BeginTransaction();

                int iResult = cmd.ExecuteNonQuery();
                connector.Close();
                return iResult;
            }
            catch (SqlException e)
            {
                if (bTran && transaction != null)
                {
                    transaction.Rollback();
                    transaction = null;
                }
                connector.Close();
                ErrorMessage = e.Message; 
            }
            catch (Exception e)
            {
                if (bTran && transaction != null)
                {
                    transaction.Rollback();
                    transaction = null;
                    
                }
                connector.Close();
                ErrorMessage = e.Message; 
            }
            return 0;
        }
        public override TableField[] GetFieldMark(string tableName)
        {
            string sql = "select a.COLUMN_NAME,a.DATA_TYPE,a.IS_NULLABLE as NoNull,b.value as PROPERTY_VALUE from information_schema.COLUMNS as a  left join sys.extended_properties as b on a.TABLE_NAME = OBJECT_NAME(b.major_id) and a.ORDINAL_POSITION = b.minor_id where a.TABLE_NAME = N'" + tableName + "'";
            List<TableField> fields = new List<TableField>();
            DataTable dt = base.Query(sql);
            foreach(DataRow row in dt.Rows)
            {
                fields.Add(
                    new TableField()
                    {
                        Name = row["COLUMN_NAME"].ToString(),
                        IsSearched = row["PROPERTY_VALUE"].ToString() == "" ? false: bool.Parse(row["PROPERTY_VALUE"].ToString()),
                        Type = row["DATA_TYPE"].ToString(),
                        NoNull = row["NoNull"].ToString() == "NO" ? true : false,
                    });
                
            }
            return fields.ToArray();
        }
      
        public override bool IsTableExists(string strTableName)
        {

            string sqlStr = "if objectproperty(object_id(N'"+strTableName+"'),'IsUserTable')=1 select 1 else select 0";
            base.commander = connector.CreateCommand();
            commander.CommandText = sqlStr;
            base.connector.Open();
            object obj = commander.ExecuteScalar();
            base.connector.Close();
            if (obj.ToString() == "1")
            {
                return true;
            }
            return false;
        }

        public override void SetDBConnectString(string connectionString)
        {
           base.connector = new SqlConnection(connectionString);
        }

        public override void SetFieldMark(string tableName, string tableDiscript, TableField[] fields)
        {
            //--创建表
            //create table 表(a1 varchar(10), a2 char(2))

            //--为表添加描述信息
            //EXECUTE sp_addextendedproperty N'MS_Description', '人员信息表', N'user', N'dbo', N'table', N'表', NULL, NULL

            //--为字段a1添加描述信息
            //EXECUTE sp_addextendedproperty N'MS_Description', '姓名', N'user', N'dbo', N'table', N'表', N'column', N'a1'

            //--为字段a2添加描述信息
            //EXECUTE sp_addextendedproperty N'MS_Description', '性别', N'user', N'dbo', N'table', N'表', N'column', N'a2'

            //--更新表中列a1的描述属性：
            //EXEC sp_updateextendedproperty 'MS_Description','字段1','user',dbo,'table','表','column',a1

            //--删除表中列a1的描述属性：
            //EXEC sp_dropextendedproperty 'MS_Description','user',dbo,'table','表','column',a1

            //-------------------- -
            //作者：风之_诉
            //来源：CSDN
            //原文：https://blog.csdn.net/luming666/article/details/78538986 
            //版权声明：本文为博主原创文章，转载请附上博文链接！

            foreach (TableField field in fields)
            {
                if(field.IsSearched)
                {
                    Excute("EXECUTE sp_addextendedproperty N'MS_IsSearched',N'" + field.IsSearched.ToString() + "',N'user',N'dbo',N'table',N'" + tableName + "',N'column',N'" + field.Name + "'");
                }
            }
        }
        public override void DelFieldMark(string tableName,  TableField[] fields)
        {
            foreach (TableField field in fields)
            {
                if (field.IsSearched)
                {
                    Excute("EXECUTE sp_dropextendedproperty N'MS_IsSearched',N'user',N'dbo',N'table',N'" + tableName + "',N'column',N'" + field.Name + "'");
                }
            }
        }

        public override bool TestDBConnect(string connectionString)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
            return false;
        }
    }
}

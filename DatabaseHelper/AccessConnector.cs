using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper
{
    public class AccessConnector : AConnector
    {

        public AccessConnector(string connectString = "")
        {
            //conn = new OleDbConnection(ConfigurationManager.ConnectionStrings["DatabaseHelper.Properties.Settings.DocMngDatabaseConnectionString"].ToString());
            if (connectString != "")
                base.connector = new OleDbConnection(connectString);

            TypeList.Add("varchar", typeof(string));
            TypeList.Add("integer", typeof(int));
            TypeList.Add("short", typeof(short));
            TypeList.Add("double", typeof(double));
            TypeList.Add("real", typeof(float));
            TypeList.Add("byte", typeof(byte));
            TypeList.Add("NUMERIC", typeof(float));
            TypeList.Add("money", typeof(float));
            TypeList.Add("text", typeof(string));
            TypeList.Add("datetime", typeof(DateTime));
            TypeList.Add("bit", typeof(byte));
            TypeList.Add("OLEObject", typeof(string));
        }

        public override bool TestDBConnect(string connectString)
        {
            try
            {
                OleDbConnection connecter = new OleDbConnection(connectString);
                connecter.Open();
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }

            return false;
            
        }

        public override void SetDBConnectString(string connectString)
        {
            base.connector = new OleDbConnection(connectString);
        }
        public override int Excute(string sql, Dictionary<string, byte[]> files, bool bTran = false)
        {
            return base.Excute(sql,null,bTran);
        }
        Dictionary<string, Type> TypeList = new Dictionary<string, Type>();

        public Dictionary<string, Type> GetTypeMap()
        {
            return TypeList;
        }

        public override bool IsTableExists(string strTableName)
        {
            bool bExist = false;
            base.connector.Open();
            bExist = (((OleDbConnection)connector).GetSchema("Tables").Select("Table_Name='" + strTableName + "'").Length > 0) ? true : false;
            //conn.Open((strTableName), (IDispatch*)m_pConn, true), adOpenKeyset, adLockOptimistic, adCmdTable);
            connector.Close();
            return bExist;
        }

        public override TableField[] GetFieldMark(string tableName)
        {
            List<TableField> fields = new List<TableField>();
            try
            {
                ADODB.Connection adodb_conn = new ADODB.Connection();
                adodb_conn.Open(base.connector.ConnectionString, null, null, -1);
                ADOX.Catalog catalog = new ADOX.Catalog();
                catalog.ActiveConnection = adodb_conn;
                ADOX.Table table = catalog.Tables[tableName];
                foreach (ADOX.Column col in table.Columns)
                {
                    string type = col.Type.ToString();
                    if (type.Contains('('))
                    {
                        type = type.Substring(type.IndexOf('('));
                    }
                    TableField field = new TableField()
                    {
                        Name = col.Name,
                        Type = type,
                        NoNull = false
                    };
                    fields.Add(field);
                }
                Marshal.FinalReleaseComObject(catalog.ActiveConnection);
                Marshal.FinalReleaseComObject(catalog);
                return fields.ToArray();

            }
            catch (Exception e)
            {
                throw (e);
            }

        }

        public override void SetFieldMark(string tableName, string tableMemo, TableField[] fields)
        {
            try
            {
                ADODB.Connection adodb_conn = new ADODB.Connection();
                adodb_conn.Open(base.connector.ConnectionString, null, null, -1);
                ADOX.Catalog catalog = new ADOX.Catalog();
                catalog.ActiveConnection = adodb_conn;
                ADOX.Table table = catalog.Tables[tableName];
                foreach (TableField field in fields)
                {
                    ADOX.Column col = table.Columns[field.Name];
                }
                Marshal.FinalReleaseComObject(catalog.ActiveConnection);
                Marshal.FinalReleaseComObject(catalog);
            }
            catch (Exception e)
            {
                throw (e);
            }
            //---------------------
            //            作者：重庆 - 传说
            //来源：CSDN
            //原文：https://blog.csdn.net/zdb330906531/article/details/49420991 
            //版权声明：本文为博主原创文章，转载请附上博文链接！
        }

        public override void DelFieldMark(string tableName, TableField[] fields)
        {
            throw new NotImplementedException();
        }
    }
}

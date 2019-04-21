using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper
{
    public abstract class AConnector : IConnector
    {
        protected IDbConnection connector = null;
        protected IDbCommand commander = null;
        protected IDataReader dataReader = null;
        protected IDbTransaction transaction = null;
        protected static string ErrorMessage = "";
        public abstract int Excute(string sql, Dictionary<string, object> files, bool bTran = false);
        public int Excute(string sql, SqlParameter[] parameters = null, bool bTran = false)
        {
            try
            {
                commander = connector.CreateCommand();
                commander.CommandText = sql;
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                        commander.Parameters.Add(parameter);
                }
                connector.Open();
                if(bTran)
                    transaction = connector.BeginTransaction();

                int iResult = -1;
                if (sql.TrimEnd().EndsWith("identity;"))
                    int.TryParse(commander.ExecuteScalar().ToString(), out iResult);
                else
                    iResult = commander.ExecuteNonQuery();
                if (bTran && transaction != null)
                {
                    transaction.Commit();
                }
                connector.Close();
                return iResult;
            }
            catch (DbException e)
            {
                if (bTran && transaction != null)
                {
                    transaction.Rollback();
                    transaction = null;
                    
                }
                connector.Close();
                ErrorMessage = e.Message; 
                Debug.Assert(false, ErrorMessage);

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
                Debug.Assert(false, ErrorMessage);

            }
            return 0;
        }

        public abstract TableField[] GetFieldMark(string tableName);

        public abstract void SetFieldMark(string tableName, string tableDiscript, TableField[] fields);

        public abstract bool IsTableExists(string strTableName);

        public object QueryStrReturn(string sql)
        {
            try
            {
                commander = connector.CreateCommand();
                commander.CommandText = sql;
                connector.Open();
                object obj = commander.ExecuteScalar();
                commander.Dispose();
                connector.Close();
                return obj;
            }
            catch (DbException e)
            {
                ErrorMessage = e.Message; connector.Close();
                //Debug.Assert(false, ErrorMessage);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message; connector.Close();
                //Debug.Assert(false, ErrorMessage);

            }
            return "-1";
        }
        public DataTable Query(string sql)
        {
            try
            {
                commander = connector.CreateCommand();
                commander.CommandText = sql;
                connector.Open();
                dataReader = commander.ExecuteReader();
                DataTable dt = new DataTable();

                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    dt.Columns.Add(dataReader.GetName(i), dataReader.GetFieldType(i));
                }
                dt.Rows.Clear();
                while (dataReader.Read())
                {
                    DataRow row = dt.NewRow();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                       row[i] = dataReader[i];
                    }
                    dt.Rows.Add(row);
                }
                commander.Dispose();
                connector.Close();
                return dt;
            }
            catch (DbException e)
            {
                ErrorMessage = e.Message; connector.Close();
                Debug.Assert(false, ErrorMessage);

            }
            catch (Exception e)
            {
                ErrorMessage = e.Message; connector.Close();
                Debug.Assert(false, ErrorMessage);
            }
            return new DataTable();
        }

        public abstract void SetDBConnectString(string connectString);

        public abstract bool TestDBConnect(string connectString);

        public string GetErrorMessage()
        {
            return ErrorMessage;
        }

        public abstract void DelFieldMark(string tableName, TableField[] fields);
        
    }
}

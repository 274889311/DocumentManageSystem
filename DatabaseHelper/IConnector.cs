using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper
{
    public interface IConnector
    {
        object QueryStrReturn(string sql);
        DataTable Query(string sql);
        int Excute(string sql, SqlParameter[] parameters = null, bool bTran = false);
        int Excute(string sql, Dictionary<string, byte[]> files, bool bTran = false);
        bool IsTableExists(string strTableName);
        TableField[] GetFieldMark(string tableName);
        void SetFieldMark(string tableName, string tableDiscript, TableField[] fields);
        void DelFieldMark(string tableName, TableField[] fields);
        bool TestDBConnect(string connectString);
        void SetDBConnectString(string connectString);

        string GetErrorMessage();
    }
}

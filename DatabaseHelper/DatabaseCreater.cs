using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper
{
   public class DatabaseCreater
    {
        static DatabaseCreater creater = null;
        public static DatabaseCreater GetCreater()
        {
            if (creater == null)
                creater = new DatabaseCreater();
            return creater;
        }

        public int CreateTableUser(IConnector connector)
        {
            string sql = @"Create table 用户表 (ID int identity(1,1) primary key not null" +
                ",用户名 " + TableField.FieldTypeList[TableFieldType.文本] +
                ",密码 " + TableField.FieldTypeList[TableFieldType.文本] +
                ",密码提示 " + TableField.FieldTypeList[TableFieldType.文本] +
                ",真实姓名 " + TableField.FieldTypeList[TableFieldType.文本] +
                ",创建时间 " + TableField.FieldTypeList[TableFieldType.日期时间] +
                ",最后登录时间 " + TableField.FieldTypeList[TableFieldType.日期时间] +
                ",是否为管理员 " + TableField.FieldTypeList[TableFieldType.布尔] + ")";
            connector.Excute(sql);
            if(connector.IsTableExists("用户表"))
            {
                sql = "Insert into 用户表 values (N'admin',N'admin',N'admin',N'admin',N'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',N'" +
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',1)";
                return connector.Excute(sql);
            }
            return 0;
        }
        public int CreateTableMode(IConnector connector)
        {
            string sql = @"Create table 报表类型 (ID int identity(1,1) primary key not null" +
                 ",parentID " + TableField.FieldTypeList[TableFieldType.整型] +
                ",类型名称 " + TableField.FieldTypeList[TableFieldType.文本] +
                ",说明 " + TableField.FieldTypeList[TableFieldType.文本] + ")";
            connector.Excute(sql);
            return 0;
        }
        public void CreateTableTree(IConnector connector)
        {
            BaseFieldTable tableTree = new BaseFieldTable("报表目录", "文档管理系统表目录结构");
            tableTree.AddField(new TableField()
            {
                Name = "报表类型ID",
                Type = TableField.FieldTypeList[TableFieldType.整型]
            });
           
            tableTree.AddField(new TableField()
            {
                Name = "报表名称",
                Type = TableField.FieldTypeList[TableFieldType.文本]
            });
            tableTree.AddField(new TableField()
            {
                Name = "备注",
                Type = TableField.FieldTypeList[TableFieldType.文本]
            });
            CreateTable( connector, tableTree);
        }
        public void CreateTableDoc(IConnector connector)
        {
            BaseFieldTable tableTree = new BaseFieldTable("文档表", "文档总表");
            tableTree.AddField(new TableField()
            {
                Name = "所属报表ID",
                Type = TableField.FieldTypeList[TableFieldType.整型]
            });
            tableTree.AddField(new TableField()
            {
                Name = "数据记录ID",
                Type = TableField.FieldTypeList[TableFieldType.整型]
            });
            tableTree.AddField(new TableField()
            {
                Name = "文档名称",
                Type = TableField.FieldTypeList[TableFieldType.文本]
            });
            tableTree.AddField(new TableField()
            {
                Name = "文档内容",
                Type = TableField.FieldTypeList[TableFieldType.文档]
            });
            CreateTable(connector, tableTree);
        }

        public void CreateTableDeviceCare(IConnector connector)
        {
            BaseFieldTable tableTree = new BaseFieldTable("保养提醒", "系统管理设备的保养提醒服务");
            tableTree.AddField(new TableField()
            {
                Name = "设备表名",
                Type = TableField.FieldTypeList[TableFieldType.文本]
            });

            tableTree.AddField(new TableField()
            {
                Name = "保养时间",
                Type = TableField.FieldTypeList[TableFieldType.文本]
            });
            tableTree.AddField(new TableField()
            {
                Name = "保养周期",
                Type = TableField.FieldTypeList[TableFieldType.整型]
            });
            CreateTable(connector, tableTree);
        }
        public int CreateTable(IConnector connector, IFieldTable table,bool withCommon = true,bool withoutExtend = true)
        {
            string sql = @"Create Table " + table.TableName + " (";
            var fields = table.GetTableFields(withCommon, withoutExtend);
            
            foreach (TableField field in fields)
            {
                sql += field.Name + " " + FixFieldType(field.Type) + (field.NoNull? " NOT NULL " : "")+",";
            }
            if (sql.Last() == ',')
            {
                sql = sql.Remove(sql.LastIndexOf(','));
            }
            sql += ")";
            int iResult = connector.Excute(sql);
            connector.SetFieldMark(table.TableName, table.TableDiscript, table.GetTableFields(withCommon, withoutExtend));
            return iResult;
        }
        private string FixFieldType(string type)
        {
            if (type.Contains("varchar"))
                type = "varchar(50)";
            if (type.Contains("NUMERIC"))
                type = "NUMERIC(6,2)";
            return type;
        }
        public int CreateTable(IConnector connector, string tableName, string tableDiscript, TableField[] fields, bool withCommon = true, bool withoutextend = true)
        {
            IFieldTable table = GetTableFromField(tableName, tableDiscript, fields);
            return CreateTable(connector, table, withCommon, withoutextend);
        }
        private IFieldTable GetTableFromField(string tableName,string tableDiscript,TableField[] fields)
        {
            BaseFieldTable table = new BaseFieldTable(tableName, tableDiscript);
            foreach (TableField field in fields)
                table.AddField(field);
            return table;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper
{
    public enum TableFieldType
    {
        文本,
        长文本,
        整型,
        小数,
        日期,
        日期时间,
        布尔,
        文档
    }

    public class TableField
    {
        public static Dictionary<TableFieldType, string> FieldTypeList = new Dictionary<TableFieldType, string>()
        {
            {TableFieldType.文本,"nvarchar(50)" },
            {TableFieldType.长文本,"text" },
            {TableFieldType.整型,"int"},
            {TableFieldType.小数,"float"},
            {TableFieldType.日期时间,"smalldatetime" },
            {TableFieldType.布尔,"bit"},
            {TableFieldType.文档,"image"},
            {TableFieldType.日期,"date"}

        };
        public TableField()
        {
            Type = FieldTypeList[TableFieldType.文本];
        }
        public string Name = "";
        public string Type = "";
        public bool NoNull = false;
        public bool IsSearched = false;
        public bool IsExtend = false;
        public string Value = "";

    }
}

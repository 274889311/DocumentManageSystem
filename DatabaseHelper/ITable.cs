using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseHelper
{
    /// <summary>
    /// 字段列表接口
    /// </summary>
   public interface IFieldTable
    {
        string TableName { get; set; }
        string TableDiscript { get; set; }
        void AddField(TableField field);
        TableField[] GetTableFields(bool withCommon = true, bool withoutExtend = true);
        TableField[] GetSearchFields();
        void UpdateFieldValue(TableField field);
        string GetFieldValue(string fieldName);
        void UpdateFieldValue(string fieldName, string value);
    }
    /// <summary>
    /// 字段列表的抽象类
    /// </summary>
    public abstract class AFieldTable:IFieldTable
    {
        public AFieldTable(string name, string discript)
        {
            TableName = name;
            TableDiscript = discript;
            TableFields.Add(new TableField(){
                Name = "ID",
                Type = "int identity(1,1) primary key not null",
                IsExtend = true
            });
            CommonFields.AddRange(
                new TableField[] {
                    new TableField(){
                        Name = "修改人",
                        Type = TableField.FieldTypeList[TableFieldType.文本],
                        IsExtend = true,
                    },
                    new TableField(){
                        Name = "修改时间",
                        Type = TableField.FieldTypeList[TableFieldType.日期时间],
                        IsExtend = true,
                        Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    },
            });
        }
        public string TableName { get; set; }
        public string TableDiscript { get;set;}
        protected List<TableField> TableFields = new List<TableField>();
        protected List<TableField> SearchFields = new List<TableField>();
        protected List<TableField> CommonFields = new List<TableField>();

        public abstract void AddField(TableField field);
        public TableField[] GetTableFields(bool withCommon = true,bool withoutExtend = true) {
            foreach(TableField field in TableFields)
            {
                if(field.Name == "修改时间" && field.Value=="")
                {
                    field.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                }
            }
            if (withCommon)
            {
                if (CommonFields.Select(f => f.Name).All(f => TableFields.Select(tf => tf.Name).Contains(f)))
                    return TableFields.ToArray();
                else
                    return TableFields.Concat(CommonFields).ToArray();
            }
            else if(withoutExtend)
            {
                return TableFields.Where(f => !f.IsExtend).ToArray();
            }
            else
            {
                return TableFields.Except(CommonFields).ToArray();
            }
        }
        public TableField[] GetSearchFields() { return SearchFields.ToArray(); }
        public void UpdateFieldValue(TableField field)
        {
            foreach (TableField tf in TableFields)
            {
                if (tf.Name == field.Name)
                {
                    tf.Value = field.Value;
                    break;
                }
            }
        }
        public void UpdateFieldValue(string fieldName, string value)
        {
            foreach (TableField tf in TableFields)
            {
                if (tf.Name == fieldName)
                {
                    if(fieldName.Contains("日期") || fieldName.Contains("时间"))
                    {
                        DateTime dt;
                        if (!DateTime.TryParse(value, out dt))
                        {
                            var times = value.Split(' ');
                            tf.Value = string.Join(" ", times.Except(times.Where(t => t.Contains("星期"))).ToArray());
                        }
                        else
                            tf.Value = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                        tf.Value = value;
                    break;
                }
            }
        }
        public string GetFieldValue(string fieldName)
        {
            foreach (TableField tf in TableFields)
            {
                if (tf.Name == fieldName)
                {
                    return tf.Value;
                }
            }
            return "";
        }
    }
    /// <summary>
    /// 字段列表的派生类
    /// </summary>
    public class BaseFieldTable:AFieldTable
    {
        public BaseFieldTable(string name ,string Discript=""):base(name,Discript)
        {

        }
        public override void AddField(TableField field)
        {
            foreach(TableField f in TableFields)
            {
                if(f.Name == field.Name)
                {
                    f.Value = field.Value;
                    return;
                }
            }
            if (CommonFields.Select(f => f.Name).Contains(field.Name))
                field.IsExtend = true;
            TableFields.Add(field);
        }
        public void AddSearchFiled(TableField field)
        {
            SearchFields.Add(field);
        }
    }

}

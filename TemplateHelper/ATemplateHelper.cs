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

    public abstract class ATemplateHelper : ITemplateHelper
    {
        protected string m_TemplateFilePath = "";
        protected string m_OutputFilePath = "";
        public ATemplateHelper(string templateFilePath)
        {
            m_TemplateFilePath = templateFilePath;
            m_OutputFilePath = Application.StartupPath + "\\OutPut\\" + Path.GetFileName(templateFilePath);
            if (!Directory.Exists(Application.StartupPath + "\\OutPut\\"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\OutPut\\");
            }
        }
        public static ITemplateHelper GetTemplateHelper(string templateName, string tableName)
        {
            string templatePath = Application.StartupPath + "\\Templates\\" + templateName + "\\";
            if (!Directory.Exists(templatePath))
            {
                Directory.CreateDirectory(templatePath);
            }
            string[] files = Directory.GetFiles(templatePath);
            var file = files.Where(f => f.Contains(tableName)).FirstOrDefault();
            if (file != null)
            {
                string ext = Path.GetExtension(file);
                if (ext == ".xls" || ext == ".xlsx")
                {
                    return new TemplateExcelHelper(file);
                }
                if (ext == ".doc" || ext == ".docx")
                {
                    return new TemplateWordHelper(file);
                }
            }
            else
            {
                return new TemplateExcelHelper(templatePath + "\\" + tableName + ".xls");
            }
            return null;
        }

        public abstract void OutPut(DataTable dt);
        public abstract DataTable InPut(string tableName, string filePath);

    }

}

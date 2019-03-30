using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateHelper
{
    public interface ITemplateHelper
    {
        void OutPut(DataTable dt);
        DataTable InPut(string tableName, string filePath);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBHelper
{
    interface IConnector
    {
        void Excute(string sql);
        DataTable Query(string sql);

    }
}

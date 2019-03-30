using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemWindows
{
    /// <summary>
    /// 查询编辑器的工作状态
    /// </summary>
    public enum WorkingStateEnum
    {
        [Exportable("新建数据记录")]
        Creating,
        [Exportable("修改数据记录")]
        Editting,
        [Exportable("浏览数据记录")]
        View,
        Search,
    }
}

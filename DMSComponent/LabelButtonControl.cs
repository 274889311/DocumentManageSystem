using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMSComponent
{
    public class LabelButtonControl
    {
        public LabelButtonControl(Label label)
        {
            label.MouseEnter += (sender, e) =>
              {
                  label.ForeColor = System.Drawing.Color.Blue;
              };
            label.MouseLeave += (sender, e) =>
              {
                  label.ForeColor = System.Drawing.Color.White;
              };
        }
    }
}

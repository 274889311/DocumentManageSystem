using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMSComponent
{
    public class FormPosition
    {
        public FormPosition(Control control)
        {
            control.MouseDown += (sender, e) =>
            {
                downPoint = new Point(e.X, e.Y);
            };
            control.MouseMove += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    control.FindForm().Location = new Point(control.FindForm().Location.X + e.X - downPoint.X,
                        control.FindForm().Location.Y + e.Y - downPoint.Y);
                }

            };

        }
        Point downPoint;
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMSComponent
{
    public class FormStateControlor
    {
        public FormStateControlor(System.Windows.Forms.PictureBox formState)
        {
            
            formState.Paint += (sender, e) =>
                {
                    Point ptMouse = formState.PointToClient(Control.MousePosition);
                    if (formState.ClientRectangle.Contains(ptMouse))
                    {
                        int mousePosition =(int)( ptMouse.X/(formState.Width/3f));
                        float step = formState.Width / 3f;
                        RectangleF rect = new RectangleF(mousePosition * step, 0, step, formState.Height);
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.Blue)), rect);
                    }
                };
            formState.MouseMove += (sender, e) =>
            {
                formState.Refresh();
            };
            formState.MouseEnter += (sender, e) =>
            {
                formState.Refresh();
            };
            formState.MouseLeave += (sender, e) =>
            {
                formState.Refresh();
            };
            formState.MouseClick += (sender, e) =>
            {
                switch ((int)(e.X / (formState.Width / 3f)))
                {
                    case 0:
                        formState.FindForm().WindowState = System.Windows.Forms.FormWindowState.Minimized;
                        break;
                    case 1:
                        if (formState.FindForm().WindowState == System.Windows.Forms.FormWindowState.Maximized)
                        {
                            formState.FindForm().WindowState = System.Windows.Forms.FormWindowState.Normal;
                            formState.Image =Properties.Resources.Normall;
                        }
                        else
                        {
                            formState.FindForm().WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            formState.Image = Properties.Resources.Maxum;
                        }
                        break;
                    case 2:
                        if(MessageBox.Show("您确认要退出系统吗？","确定？",MessageBoxButtons.YesNo)== DialogResult.Yes)
                        {
                            formState.FindForm().Close();
                        }
                        break;
                }
            };
        }
    }
}

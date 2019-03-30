using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DMSComponent
{
    public partial class BackgroundPanel : UserControl
    {
        public BackgroundPanel()
        {
            InitializeComponent();
            if (Directory.Exists(Application.StartupPath + @"\Images\"))
            {
                ImageFiles = Directory.GetFiles(Application.StartupPath + @"\Images\");
                FileNames = ImageFiles.Select(f => new KeyValuePair<string, Image>(Path.GetFileNameWithoutExtension(f), Image.FromFile(f))).ToDictionary(k => k.Key, v => v.Value);
            }
            this.SizeChanged += (sender, e) =>
              {
                  ButtonBoxes.Clear();
                  ButtonBoxWidth = (int)(this.Width * 0.5f/5);
                  TipWidth = (int)(ButtonBoxWidth / 1.6f);
                  PointF ptStart = new PointF(this.Width / 2f - ButtonBoxWidth * 2.5f - TipWidth * 2, this.Height / 2f - ButtonBoxWidth / 2f - TipWidth / 2f);
                  for(int i=0;i<10;i++)
                  {
                      float x = ptStart.X + i % 5 * (ButtonBoxWidth + TipWidth);
                      float y = ptStart.Y + (i / 5) * (ButtonBoxWidth + TipWidth);
                      ButtonBoxes.Add(new Rectangle( (int)x , (int)y - ButtonBoxWidth / 2, ButtonBoxWidth, ButtonBoxWidth));
                  }
              };
            this.DoubleBuffered = true;
        }
        List<Rectangle> ButtonBoxes = new List<Rectangle>();
        int ButtonBoxWidth = 100;
        int TipWidth = 35;
        string[] ImageFiles;
        Dictionary<string, Image> FileNames;
        private void BackgroundPanel_Paint(object sender, PaintEventArgs e)
        {
            if (ImageFiles == null || ImageFiles.Length==0) return;
            if (FileNames == null || FileNames.Count == 0) return;
            if(FileNames.ContainsKey("背景"))
                e.Graphics.DrawImage(FileNames["背景"], this.ClientRectangle);
            int index = 0;
            Font font = new Font(this.Font.FontFamily, ButtonBoxWidth/10f, FontStyle.Bold);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Brush textBrush = new SolidBrush(Color.Blue);
            foreach(var kv in FileNames.Where(f=>f.Key!= "背景"))
            {
                Rectangle rect = ButtonBoxes[index];

                if (rect.Contains(this.PointToClient(MousePosition)))
                {
                    //rect.Offset(5, 5);
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200,Color.Gray)), rect);
                }
                if (rect.Contains(this.PointToClient(MousePosition)))
                {
                    rect.Offset(-5, -5);
                    textBrush = new SolidBrush(Color.OrangeRed);
                }else
                    textBrush = new SolidBrush(Color.Orange);
                e.Graphics.DrawImage(kv.Value, rect);
                rect.Offset(0, ButtonBoxWidth);
                rect.Height = TipWidth;
                e.Graphics.DrawString(kv.Key, font, textBrush, rect, sf);
                index++;
            }

        }

        private void BackgroundPanel_MouseMove(object sender, MouseEventArgs e)
        {
            PointF ptStart = new PointF(this.Width / 2f - ButtonBoxWidth * 2.5f - TipWidth * 2, this.Height / 2f - ButtonBoxWidth / 2f - TipWidth / 2f);
            Rectangle rect = new Rectangle((int)ptStart.X-ButtonBoxWidth/2, (int)ptStart.Y-ButtonBoxWidth/2, ButtonBoxWidth * 10 + TipWidth * 9, ButtonBoxWidth * 10 + TipWidth * 9);
            rect.Inflate(TipWidth, TipWidth);
            this.Invalidate(rect);
            //this.Invalidate();
        }
        public static Action<string> ButtonClickEvent;
             
        private void BackgroundPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (ButtonClickEvent == null) return;
            var Images = ImageFiles.Where(f => !Path.GetFileNameWithoutExtension(f).Contains("背景")).ToArray();
            var boxi = ButtonBoxes.Select((box, index) => new { box, index }).Where(boxIndex => boxIndex.box.Contains(e.Location)).FirstOrDefault();
            if(boxi!=null)
            {
                ButtonClickEvent.Invoke(Path.GetFileNameWithoutExtension(Images[boxi.index]));
            }
        }
    }
}

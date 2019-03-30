using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PageControl
{
    public partial class PagerControl : UserControl
    {
        public PagerControl()
        {
            InitializeComponent();
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = false;
        }
        public int GetPageCount()
        {
            return TotalCount;
        }
        public void SetPageCount(int totalCount,int pageButtonCount = 9)
        {
            TotalCount = totalCount;
            PageButtonCount = pageButtonCount;
            if (totalCount < 2)
            {
                this.Visible = false;
            }
            else
                this.Visible = true;

            CreatePageControl(TotalCount);
            RefreshPages();
        }
        public Action<int> PageChangedEvent;
        int TotalCount = 15;
        int PageButtonCount = 9;
        public int CurrentPageIndex = 1;
        public void CreatePageControl(int totalCount)
        {
            bt_first.Click += (sender, e) =>
              {
                  CurrentPageIndex = 1; RefreshPages();
              };
            bt_last.Click += (sender, e) =>
            {
                CurrentPageIndex = totalCount; RefreshPages();
            };
            bt_front.Click += (sender, e) =>
            {
                if(CurrentPageIndex>1)
                CurrentPageIndex--; RefreshPages();
            };
            bt_next.Click += (sender, e) =>
            {
                if(CurrentPageIndex<TotalCount)
                CurrentPageIndex++; RefreshPages();
            };
            bt_go.Click += (sender, e) =>
              {
                  int textBoxIndex = 0;
                  int.TryParse(tb_input.Text, out textBoxIndex);
                  if(textBoxIndex>0 && textBoxIndex<=TotalCount)
                  {
                      CurrentPageIndex = textBoxIndex;
                      RefreshPages();
                  }
              };
            TotalCount = totalCount;
            if (totalCount == 1)
                this.Visible = false;
            else
                this.Visible = true;
            if (PageButtonCount > TotalCount)
                PageButtonCount = TotalCount;

            for (int i = 1; i <= PageButtonCount; i++)
            {
                Button btn = new Button()
                {
                    Text = i.ToString(),
                    Name = "btn_" + i.ToString(),
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Margin = new Padding(0)
                };
                btn.Click += (sender, e) => {
                    int btnTextIndex = 0;
                    int.TryParse(btn.Text,out btnTextIndex);
                    if (btnTextIndex > 0)
                    {
                        CurrentPageIndex = btnTextIndex;
                        RefreshPages();
                    }
                };
                flowLayoutPanel1.Controls.Add(btn);
            }
            RefreshPages();
        }

        private void RefreshPages()
        {
            flowLayoutPanel1.SuspendLayout();
            if (CurrentPageIndex < 4)
            {
                for (int i = 1; i <= PageButtonCount; i++)
                {
                    if ( i > PageButtonCount - 2)
                    {
                        if (i == PageButtonCount - 1)
                            flowLayoutPanel1.Controls.Find("btn_" + i, false).FirstOrDefault().Text = "…";
                        else
                            flowLayoutPanel1.Controls.Find("btn_" + i, false).FirstOrDefault().Text = TotalCount.ToString();

                    }
                    else if(i<CurrentPageIndex)
                        flowLayoutPanel1.Controls.Find("btn_" + i, false).FirstOrDefault().Text = i.ToString();

                }
            }
            else if (CurrentPageIndex > TotalCount - 7 && TotalCount > 7)
            {
                for (int i = 1; i <= PageButtonCount; i++)
                {
                    if (i == 1)
                        flowLayoutPanel1.Controls.Find("btn_" + i, false).FirstOrDefault().Text = i.ToString();
                    else if (i == 2)
                        flowLayoutPanel1.Controls.Find("btn_" + i, false).FirstOrDefault().Text = "…";
                    else
                        flowLayoutPanel1.Controls.Find("btn_" + i, false).FirstOrDefault().Text = (TotalCount - 7 + i-2).ToString();
                }
            }
            else
            {
                flowLayoutPanel1.Controls.Find("btn_1", false).FirstOrDefault().Text = "1";
                flowLayoutPanel1.Controls.Find("btn_2", false).FirstOrDefault().Text = "…";
                flowLayoutPanel1.Controls.Find("btn_9", false).FirstOrDefault().Text = TotalCount.ToString();
                flowLayoutPanel1.Controls.Find("btn_8", false).FirstOrDefault().Text = "…";
                for (int i = 3; i < 8; i++)
                {
                    flowLayoutPanel1.Controls.Find("btn_" + i, false).FirstOrDefault().Text = (CurrentPageIndex - 5 + i).ToString();

                }
            }

            foreach(Button btn in flowLayoutPanel1.Controls)
            {
                if (btn.Text == CurrentPageIndex.ToString())
                    btn.Select();

            }
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            if(PageChangedEvent!=null)
                PageChangedEvent.Invoke(CurrentPageIndex);
        }
    }
}

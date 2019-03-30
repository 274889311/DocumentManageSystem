using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SystemWindows
{
    class RowMergeView2:DataGridView
    {
        #region <<单元格合并配置>>
        public struct MergeCellsParam
        {
            public int iStartCellColumn;
            public int iStartCellRow;
            public int iEndCellColumn;
            public int iEndCellRow;
            public string strText;

            public MergeCellsParam(int _iStartColumn, int _iStartRow, int _iEndColumn, int _iEndRow)
            {
                iStartCellColumn = _iStartColumn;
                iStartCellRow = _iStartRow;
                iEndCellColumn = _iEndColumn;
                iEndCellRow = _iEndRow;
                strText = null;
            }
        }
        public List<MergeCellsParam> mMergeCells = new List<MergeCellsParam>();
        #endregion


        #region 获取单元格合并所需的行列
        //public void UpdateMergeCells()
        //{
        //    mMergeCells.Clear();
        //    int Sum = 0;
        //    for (int i = 0; i < sql.Upname.Count; i++)
        //    {
        //        if (sql.RootCount[i] > 1)
        //        {
        //            mMergeCells.Add(new MergeCellsParam(0, Sum, 0, Sum + sql.RootCount[i] - 1));
        //            mMergeCells.Add(new MergeCellsParam(1, Sum, 1, Sum + sql.RootCount[i] - 1));
        //            Sum += sql.RootCount[i];
        //        }
        //        else
        //            Sum++;
        //    }
        //}
        #endregion


        #region <<单元格合并>>
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            for (int i = 0; i < mMergeCells.Count; i++)
            {
                if (mMergeCells[i].iStartCellRow == mMergeCells[i].iEndCellRow
                    && mMergeCells[i].iStartCellColumn == mMergeCells[i].iEndCellColumn)
                {
                    return;
                }
                var _mergeParam = mMergeCells[i];
                if (e.ColumnIndex >= _mergeParam.iStartCellColumn && e.ColumnIndex <= _mergeParam.iEndCellColumn
                    && e.RowIndex >= _mergeParam.iStartCellRow && e.RowIndex <= _mergeParam.iEndCellRow)
                {
                    FindRange(e, this);
                }
            }
        }

        #region 找到上下行内容相同的行
        private void FindRange(DataGridViewCellPaintingEventArgs e, DataGridView myGrid)
        {
            if (e.CellStyle.Alignment == DataGridViewContentAlignment.NotSet)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Brush gridBrush = new SolidBrush(this.GridColor);
            SolidBrush backBrush = new SolidBrush(e.CellStyle.BackColor);
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int UpRows = 0;
            int DownRows = 0;
            int count = 0;
            int cellwidth = e.CellBounds.Width;
            Pen gridLinePen = new Pen(gridBrush);
            if (e.Value != null)
            {
                string curValue = e.Value == null ? "" : e.Value.ToString().Trim();
                //MessageBox.Show("curValue:" + curValue);
                #region 获取下面的行数
                for (int i = e.RowIndex; i < myGrid.Rows.Count; i++)
                {
                    if (myGrid.Rows[i].Cells[e.ColumnIndex].Value != null)
                    {
                        if (myGrid.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(curValue))
                        {
                            //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;

                            DownRows++;
                            if (e.RowIndex != i)
                            {
                                cellwidth = cellwidth < myGrid.Rows[i].Cells[e.ColumnIndex].Size.Width ? cellwidth : myGrid.Rows[i].Cells[e.ColumnIndex].Size.Width;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                #endregion
                #region 获取上面的行数
                for (int i = e.RowIndex; i > 0; i--)
                {
                    if (myGrid.Rows[i].Cells[e.ColumnIndex].Value != null)
                    {
                        if (myGrid.Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(curValue))
                        {
                            //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;

                            UpRows++;
                            if (e.RowIndex != i)
                            {
                                cellwidth = cellwidth < myGrid.Rows[i].Cells[e.ColumnIndex].Size.Width ? cellwidth : myGrid.Rows[i].Cells[e.ColumnIndex].Size.Width;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                #endregion
                count = DownRows + UpRows - 1;
                if (count < 1)
                {
                    return;
                }
            }

            //if (GridView.Rows[e.RowIndex].Selected)
            // {
            //backBrush.Color = e.CellStyle.SelectionBackColor;
            backBrush.Color = Color.White;
            fontBrush.Color = e.CellStyle.SelectionForeColor;
            // }

            //以背景色填充
            e.Graphics.FillRectangle(backBrush, e.CellBounds);

            //画字符串
            //if (e.RowIndex < sql.RootCount[0])
            //{
            //    PaintingFont(e, cellwidth, UpRows, DownRows, count - 1);
            //}
            //else
                PaintingFont(e, cellwidth, UpRows, DownRows, count);


            if (DownRows == 1)
            {
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                count = 0;
            }
            // 画右边线
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

            e.Handled = true;
            #endregion
        }
        #endregion

        #region 画字符串
        // cellwidth = e.CellBounds.Width;
        // UpRows上面相同的行数
        // DownRows下面相同的行数
        // count 总行数
        private void PaintingFont(System.Windows.Forms.DataGridViewCellPaintingEventArgs e, int cellwidth, int UpRows, int DownRows, int count)
        {
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int fontheight = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Height;
            int fontwidth = (int)e.Graphics.MeasureString(e.Value.ToString(), e.CellStyle.Font).Width;
            int cellheight = e.CellBounds.Height;

            if (e.CellStyle.Alignment == DataGridViewContentAlignment.MiddleCenter)
            {
                e.Graphics.DrawString((String)e.Value, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellwidth - fontwidth) / 2, e.CellBounds.Y - cellheight * (UpRows - 1) + (cellheight * count - fontheight) / 2);
            }
        }
        #endregion

    }
}

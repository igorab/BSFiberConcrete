﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
namespace TestGrid
{
    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
        }
    }
}
namespace TestGrid
{
    class DataGridViewProgressCell : DataGridViewImageCell
    {
        // Used to make custom cell consistent with a DataGridViewImageCell
        static Image emptyImage;
        static DataGridViewProgressCell()
        {
            emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        public DataGridViewProgressCell()
        {
            this.ValueType = typeof(int);
        }
        // Method required to make the Progress Cell consistent with the default Image Cell.
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override object GetFormattedValue(object value,
        int rowIndex, ref DataGridViewCellStyle cellStyle,
        TypeConverter valueTypeConverter,
        TypeConverter formattedValueTypeConverter,
        DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }

        protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (Convert.ToInt16(value) == 0 || value==null)
            {
                value = 0;
            }
            int progressVal = Convert.ToInt32(value);

            float percentage = ((float)progressVal / 100.0f); // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
            Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
            Brush foreColorBrush = new SolidBrush(Color.Black);

            // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            if (percentage >= 0.0)
            {
            // Draw the progress bar and the text
            g.FillRectangle(new SolidBrush(Color.FromArgb(150, 0, 0)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width * 0.8)), cellBounds.Height/1- 10);
            g.FillRectangle(new SolidBrush(Color.FromArgb(253, 0, 0)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width * 0.8)), cellBounds.Height/2-5);
            g.FillRectangle(new SolidBrush(Color.FromArgb(253, 0, 0)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width * 0.8)), cellBounds.Height/2-5);
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 127, 127)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width * 0.8)), cellBounds.Height/3 - 4);
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 205, 205)), cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * cellBounds.Width * 0.8)), cellBounds.Height/4 - 4);
            g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + 80, cellBounds.Y + 2);
            }
            else
            {
                // draw the text
                if (this.DataGridView.CurrentRow.Index == rowIndex)
                {
                    g.DrawString(progressVal.ToString() + "%", cellStyle.Font, new SolidBrush(cellStyle.SelectionForeColor), cellBounds.X + 6, cellBounds.Y + 2);
                }
                else
                {
                    g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
                }
            }
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using UICommon;

namespace TP.CyclonAndScrubber
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucScrubber : ucCaptioned
    {
        /// <summary>
        /// 
        /// </summary>
        public ucScrubber()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;
            int xMax = Width - 1;
            int yMax = Height - 1;
            int rem;
            //int xCenter = Math.DivRem(xMax, 2, out rem);
            int yCenter = Math.DivRem(yMax, 2, out rem);
            int yShift = Math.DivRem(xMax, 4, out rem);

            // Draw a line in the PictureBox.
            Pen pen = Pens.Black;
            int x = 10;
            int y = 10;
            g.DrawRectangle(pen, 2 * x, 0, xMax - 3 * x, yMax - yShift);//большой

            g.DrawLine(pen, 2 * x, yMax - yShift, 3 * x, yMax);//линия слева
            g.DrawLine(pen, xMax - x, yMax - yShift, xMax - 2 * x, yMax);//линия справа
            g.DrawLine(pen, 3 * x, yMax, xMax - 2 * x, yMax);//линия снизу

            g.DrawRectangle(pen, x, yCenter, x, 2 * y);//маленький слева
            g.DrawRectangle(pen, x / 2, yCenter - 3 * y, x * 3 / 2, 3 * y);//побольше слева
            g.DrawRectangle(pen, xMax - x, y, x, 3 * y);//справа

            //g.FillPolygon();

            //g.DrawLine(pen, 0, 0, xMax, 0);
            //g.DrawLine(pen, 0, 0, 0, yMax - yShift);
            //g.DrawLine(pen, xMax, 0, xMax, yMax - yShift);

            base.OnPaint(e);
        }

    }
}

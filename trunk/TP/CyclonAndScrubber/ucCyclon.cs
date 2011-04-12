using System;
using System.Drawing;
using System.Windows.Forms;
using UICommon;

namespace TP.CyclonAndScrubber
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucCyclon : ucCaptioned
    {
        /// <summary>
        /// 
        /// </summary>
        public ucCyclon()
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
            int xCenter = Math.DivRem(xMax, 2, out rem);
            //int yCenter = Math.DivRem(yMax, 2, out rem);
            int yShift = Math.DivRem(xMax, 2, out rem);

            // Draw a line in the PictureBox.
            Pen pen = new Pen(LookAndFeel.Painter.Border.DefaultAppearance.ForeColor);// Pens.Black;
            g.DrawRectangle(pen, 0, 0, xMax, yMax - yShift);
            //g.DrawLine(pen, 0, 0, xMax, 0);
            //g.DrawLine(pen, 0, 0, 0, yMax - yShift);
            //g.DrawLine(pen, xMax, 0, xMax, yMax - yShift);
            g.DrawLine(pen, 0, yMax - yShift, xCenter, yMax);
            g.DrawLine(pen, xMax, yMax - yShift, xCenter, yMax);

            base.OnPaint(e);
        }
    }
}

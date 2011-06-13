using System;
using System.Drawing;
using System.Windows.Forms;
using UICommon;

namespace TP.CyclonAndScrubber
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucSB : ucCaptioned
    {
        /// <summary>
        /// 
        /// </summary>
        public ucSB()
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
            int xShift = Convert.ToInt32(XMax/4);
            //int xMirror = Mirrored ? 

            Pen pen = Pens.Black;

            Point[] points = new[]
                                 {
                                     new Point(0, 0),
                                     new Point(XMax, 0),
                                     new Point(XMax - xShift, YMax),
                                     new Point(xShift, YMax),
                                     new Point(0, 0),
                                 };

            g.FillPolygon(Brushes.DarkGray, points);
            g.DrawLines(pen, points);

            base.OnPaint(e);
        }

    }
}

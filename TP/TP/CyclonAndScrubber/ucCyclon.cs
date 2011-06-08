using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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

            myBrush1 = Brushes.Gray;
            myBrush2 = new LinearGradientBrush(new Point(0, YMax), new Point(GradWidth, YMax), Color.DarkGray, Color.Gray);
            myBrush3 = new LinearGradientBrush(new Point(XMax - GradWidth - 1, YMax), new Point(XMax, YMax), Color.Gray, Color.DimGray);
            pen = new Pen(LookAndFeel.Painter.Border.DefaultAppearance.ForeColor);

        }

        private readonly Brush myBrush1;
        private readonly LinearGradientBrush myBrush2;
        private readonly LinearGradientBrush myBrush3;
        private readonly Pen pen;
        private const int GradWidth = 10;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int rem;
            int yShift = Math.DivRem(YMax, 2, out rem);

            g.FillRectangle(myBrush1, 0, 0, YMax, YMax - yShift);
            g.FillRectangle(myBrush2, 0, 0, GradWidth, YMax - yShift);
            g.FillRectangle(myBrush3, YMax - GradWidth, YMax - yShift, YMax, YMax - yShift);
            g.DrawRectangle(pen, 0, 0, YMax, YMax - yShift);

            Point[] points = new[]
                                 {
                                     new Point(0, YMax - yShift),
                                     new Point(XCenter, YMax),
                                     new Point(XMax, YMax - yShift),
                                 };
            g.FillPolygon(myBrush1, points);
            g.DrawPolygon(pen, points);

            base.OnPaint(e);
        }
    }
}

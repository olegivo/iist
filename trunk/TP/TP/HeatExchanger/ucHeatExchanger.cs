using System;
using System.Drawing;
using System.Windows.Forms;
using UICommon;

namespace TP.HeatExchanger
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucHeatExchanger : ucCaptioned
    {
        /// <summary>
        /// 
        /// </summary>
        public ucHeatExchanger()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Height = 240;
            Width = 80;
            //int x = XCenter / 3;

            Pen pen = Pens.Blue;
            g.FillPolygon(Brushes.LightGray, new[]
                                                 {
                                                     new Point(0, 0),
                                                     new Point(XMax, 0),
                                                     new Point(XMax, YMax),
                                                     new Point(0, YMax),
                                                 });
            g.DrawRectangle(pen, 0, 0, XMax, YMax);
            g.DrawString(Caption, new Font("Arial", 10), Brushes.Black,
                         new RectangleF(10, 30, 40, 30));
        }

    }
}

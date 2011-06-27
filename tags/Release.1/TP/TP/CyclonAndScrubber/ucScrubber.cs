using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        private bool _mirrored;

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(false)]
        public bool Mirrored
        {
            get { return _mirrored; }
            set
            {
                if (_mirrored != value)
                {
                    _mirrored = value; 
                    Refresh();
                }
            }
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
            int yShift = Convert.ToInt32(YMax/4);
            //int xMirror = Mirrored ? 

            Pen pen = Pens.Blue;
            Brush brush = Brushes.Azure;
            
            int x = 10;
            int y = 10;

            Matrix prevTransform = g.Transform;
            if (Mirrored)
            {
                Matrix matrix = new Matrix(-1, 0, 0, 1, XMax, 0);
                g.Transform = matrix;
            }


            Rectangle rectangle = new Rectangle(2 * x, 0, XMax - 3 * x, YMax - xShift);
            g.FillRectangle(brush, rectangle);//большой
            g.DrawRectangle(pen, rectangle);//большой

            //дно
            Point[] points = new[]
                                 {
                                     new Point(2*x, YMax - xShift),
                                     new Point(3 * x, YMax),
                                     new Point(XMax - 2*x, YMax),
                                     new Point(XMax - x, YMax - xShift),
                                 };
            g.FillPolygon(brush, points);
            g.DrawPolygon(pen, points);

            //маленький слева
            rectangle = new Rectangle(x, YCenter, x, 2 * y);
            g.FillRectangle(brush, rectangle);
            g.DrawRectangle(pen, rectangle);
            //побольше слева
            rectangle = new Rectangle(x / 2, YCenter - 3 * y, x * 3 / 2, 3 * y);
            g.FillRectangle(brush, rectangle);
            g.DrawRectangle(pen, rectangle);
            //справаrectangle = new Rectangle(XMax - x, y, x, 3*y);
            g.FillRectangle(brush, rectangle);
            g.DrawRectangle(pen, rectangle);

            if(Mirrored)
                g.Transform = prevTransform;

            base.OnPaint(e);
        }

    }
}

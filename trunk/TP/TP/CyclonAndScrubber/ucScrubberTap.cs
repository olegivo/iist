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
    public partial class ucScrubberTap : ucCommonBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ucScrubberTap()
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


            //кран
            Point[] points = new[]
                                 {
                                     new Point(2*x, YMax-xShift), 
                                     new Point(XCenter, 0), 
                                     new Point(XMax, 0), 
                                     new Point(XMax, 3*y), 
                                     new Point(XMax-x, 3*y), 
                                     new Point(XMax-x, y), 
                                     new Point(XCenter+x, y), 
                                     new Point(3*x, YMax-xShift+y), 
                                 };
            g.FillPolygon(brush, points);
            g.DrawPolygon(pen, points);

            if(Mirrored)
                g.Transform = prevTransform;

            base.OnPaint(e);
        }

    }
}
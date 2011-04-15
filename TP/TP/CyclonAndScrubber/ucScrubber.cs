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
            int yShift = Convert.ToInt32(XMax/4);
            //int xMirror = Mirrored ? 

            Pen pen = Pens.Blue;
            int x = 10;
            int y = 10;

            Matrix prevTransform = g.Transform;
            if (Mirrored)
            {
                Matrix matrix = new Matrix(-1, 0, 0, 1, XMax, 0);
                g.Transform = matrix;
            }

            g.DrawRectangle(pen, 2 * x, 0, XMax - 3 * x, YMax - yShift);//большой

            g.DrawLines(pen, new[]
                                 {
                                     new Point(2*x, YMax - yShift),
                                     new Point(3 * x, YMax),
                                     new Point(XMax - 2*x, YMax),
                                     new Point(XMax - x, YMax - yShift),
                                 });

            g.DrawRectangle(pen, x, YCenter, x, 2 * y);//маленький слева
            g.DrawRectangle(pen, x / 2, YCenter - 3 * y, x * 3 / 2, 3 * y);//побольше слева
            g.DrawRectangle(pen, XMax - x, y, x, 3 * y);//справа

            if(Mirrored)
                g.Transform = prevTransform;

            base.OnPaint(e);
        }

    }
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UICommon
{
    /// <summary>
    /// Линия
    /// </summary>
    public partial class ucLine : ucCommonBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ucLine()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        [Category("Layout"), DefaultValue(Orientation.Horizontal), Description("Направление")]
        public Orientation Orientation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Category("Layout"), DefaultValue(ArrowEx.None), Description("Стрелка")]
        public ArrowEx Arrow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int rem;
            int x = Math.DivRem(XMax, 3, out rem);
            int y = Math.DivRem(YMax, 3, out rem);
            int n;

            Pen pen = new Pen(GetColor(), Math.Min(XMax/2, YMax/2));
            pen.EndCap = LineCap.ArrowAnchor;

            Point[] points = null;

            switch (Color)
            {
                case LineColor.Orange:
                    n = 21;
                    break;
                default:
                    n = 13;
                    break;
            }

            Point p1 = new Point();
            Point p2 = new Point();
            g.DrawLine(pen, 0, YCenter, XMax, YCenter);

            throw new NotImplementedException("доделать по направлениям");
            switch (Direction)
            {
                case AnchorStyles.Bottom:
                    p1 = new Point(XCenter, 0);
                    p2 = new Point(XCenter, YMax);
                    break;
            }

            //if (Orientation == Orientation.Horizontal)
            //{
            //    Height = n;
            //    switch (Arrow)
            //    {
            //        case ArrowEx.None:
            //            points = new[]
            //                         {
            //                             new Point(0, y),
            //                             new Point(0, YMax - y),
            //                             new Point(XMax, YMax - y),
            //                             new Point(XMax, y),
            //                         };
            //            break;

            //        case ArrowEx.Right:
            //            points = new[]
            //                         {
            //                             new Point(XMax, YCenter),
            //                             new Point(XMax - 15, 0),
            //                             new Point(XMax - 15, y),
            //                             new Point(0, y),
            //                             new Point(0, YMax - y),
            //                             new Point(XMax - 15, YMax - y),
            //                             new Point(XMax - 15, YMax),
            //                         };
            //            break;

            //        case ArrowEx.Left:
            //            points = new[]
            //                         {
            //                             new Point(0, YCenter),
            //                             new Point(15, 0),
            //                             new Point(15, y),
            //                             new Point(XMax, y),
            //                             new Point(XMax, YMax - y),
            //                             new Point(15, YMax - y),
            //                             new Point(15, YMax),
            //                         };
            //            break;

            //        case ArrowEx.Up:
            //            break;

            //        case ArrowEx.Down:
            //            break;
            //    }
            //}
            //else if (Orientation == Orientation.Vertical)
            //{
            //    Width = n;
            //    switch (Arrow)
            //    {
            //        case ArrowEx.None:
            //            points = new[]
            //                         {
            //                             new Point(x, 0),
            //                             new Point(x, YMax),
            //                             new Point(XMax - x, YMax),
            //                             new Point(XMax - x, 0),
            //                         };
            //            break;

            //        case ArrowEx.Down:
            //            points = new[]
            //                         {
            //                             new Point(XCenter, YMax),
            //                             new Point(0, YMax - 15),
            //                             new Point(x, YMax - 15),
            //                             new Point(x, 0),
            //                             new Point(XMax - x, 0),
            //                             new Point(XMax - x, YMax - 15),
            //                             new Point(XMax, YMax - 15),
            //                         };
            //            break;

            //        case ArrowEx.Up:
            //            points = new[]
            //                         {
            //                             new Point(XCenter, 0),
            //                             new Point(0, 15),
            //                             new Point(x, 15),
            //                             new Point(x, YMax),
            //                             new Point(XMax - x, YMax),
            //                             new Point(XMax - x, 15),
            //                             new Point(XMax, 15),
            //                         };
            //            break;

            //        case ArrowEx.Right:
            //            break;

            //        case ArrowEx.Left:
            //            break;
            //    }

            //}

            //if (points != null) 
            //    g.FillPolygon(pen.Brush, points);
        }

        private Color GetColor()
        {
            switch (Color)
            {
                case LineColor.Blue:
                    return System.Drawing.Color.Blue;
                case LineColor.Orange:
                    return System.Drawing.Color.Orange;
                case LineColor.Red:
                    return System.Drawing.Color.Red;
                case LineColor.LightBlue:
                    return System.Drawing.Color.LightBlue;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Layout"), DefaultValue(LineColor.Blue), Description("Цвет")]
        public LineColor Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AnchorStyles Direction { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum ArrowEx
    {
        None = 0,
        Right = 1,
        Left = 2,
        Up = 3,
        Down = 4,
    }


}

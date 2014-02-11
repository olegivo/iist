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
        /// <param name="e"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int width = LineWidth > 0 ? LineWidth : Math.Min(XMax/2, YMax/2);
            Pen pen = new Pen(GetColor(), width) {StartCap = StartCap, EndCap = EndCap};

            Point p1;
            Point p2;
            
            switch (Direction)
            {
                case AnchorStyles.Top:
                    p1 = new Point(XCenter, YMax);
                    p2 = new Point(XCenter, 0);
                    break;
                case AnchorStyles.Bottom:
                    p1 = new Point(XCenter, 0);
                    p2 = new Point(XCenter, YMax);
                    break;
                case AnchorStyles.Left:
                    p1 = new Point(XMax, YCenter);
                    p2 = new Point(0, YCenter);
                    break;
                case AnchorStyles.Right:
                    p1 = new Point(0, YCenter);
                    p2 = new Point(XMax, YCenter);
                    break;
                case AnchorStyles.Left | AnchorStyles.Bottom:
                    p1 = new Point(XMax, 0);
                    p2 = new Point(0, YMax);
                    break;
                case AnchorStyles.Right | AnchorStyles.Bottom:
                    p1 = new Point(0, 0);
                    p2 = new Point(XMax, YMax);
                    break;
                case AnchorStyles.Left | AnchorStyles.Top:
                    p1 = new Point(XMax, YMax);
                    p2 = new Point(0, 0);
                    break;
                case AnchorStyles.Right | AnchorStyles.Top:
                    p1 = new Point(0, YMax);
                    p2 = new Point(XMax, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Direction", Direction, "Неожиданное значение Direction");
            }

            g.DrawLine(pen, p1, p2);

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
                case LineColor.Green:
                    return System.Drawing.Color.Green;
                case LineColor.Black:
                    return System.Drawing.Color.Black;
                case LineColor.LightOrange:
                    return System.Drawing.Color.Wheat;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private AnchorStyles _direction = AnchorStyles.Right;
        private int _lineWidth;
        private LineCap _startCap = LineCap.NoAnchor;
        private LineCap _endCap = LineCap.NoAnchor;
        private LineColor _color;


        /// <summary>
        /// 
        /// </summary>
        [Category("Layout"), DefaultValue(LineColor.Blue), Description("Цвет")]
        public LineColor Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value; 
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(LineCap.NoAnchor)]
        public LineCap StartCap
        {
            get { return _startCap; }
            set
            {
                if (_startCap != value)
                {
                    _startCap = value; 
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(LineCap.NoAnchor)]
        public LineCap EndCap
        {
            get { return _endCap; }
            set
            {
                if (_endCap != value)
                {
                    _endCap = value; 
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(0)]
        public int LineWidth
        {
            get { return _lineWidth; }
            set
            {
                if (_lineWidth != value)
                {
                    _lineWidth = value; 
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(AnchorStyles.Right)]
        public AnchorStyles Direction
        {
            get { return _direction; }
            set
            {
                if (_direction != value)
                {
                    switch (value)
                    {
                        case AnchorStyles.Bottom | AnchorStyles.Top:
                        case AnchorStyles.Left | AnchorStyles.Right:
                            break;
                            
                        default:
                            _direction = value;
                            break;
                    }
                }
            }
        }
    }

}

﻿using System;
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

        private LineCap _StartCap = LineCap.NoAnchor;
        private LineCap _EndCap;

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(LineCap.NoAnchor)]
        public LineCap StartCap
        {
            get { return _StartCap; }
            set
            {
                if (_StartCap != value)
                {
                    _StartCap = value; 
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
            get { return _EndCap; }
            set
            {
                if (_EndCap != value)
                {
                    _EndCap = value; 
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int rem;
            int x = Math.DivRem(XMax, 3, out rem);
            int y = Math.DivRem(YMax, 3, out rem);
            int n;

            Pen pen = new Pen(GetColor(), Math.Min(XMax/2, YMax/2));
            pen.StartCap = StartCap;
            pen.EndCap = EndCap;

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
                case AnchorStyles.Right | AnchorStyles.Bottom:
                    p1 = new Point(0, 0);
                    p2 = new Point(XMax, YMax);
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Layout"), DefaultValue(LineColor.Blue), Description("Цвет")]
        public LineColor Color { get; set; }

        private AnchorStyles _Direction = AnchorStyles.Right;

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(AnchorStyles.Right)]
        public AnchorStyles Direction
        {
            get { return _Direction; }
            set
            {
                if (_Direction != value)
                {
                    switch (value)
                    {
                        case AnchorStyles.Bottom | AnchorStyles.Top:
                        case AnchorStyles.Left | AnchorStyles.Right:
                            break;
                            
                        default:
                            _Direction = value;
                            break;
                    }
                }
            }
        }
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

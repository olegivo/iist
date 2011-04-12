using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UICommon
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucPump : ucCaptioned
    {
        /// <summary>
        /// 
        /// </summary>
        public ucPump()
        {
            InitializeComponent();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        // делаем стиль прозрачным:
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x00000020;
        //        return cp;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Height = Width;//TODO: это лучше делать при изменении размеров, а не при каждой отрисовке
            int rem;
            int delta = Math.DivRem(XMax, 4, out rem);

            Pen pen = new Pen(MyColor);

            Pen penl = Pens.Black;
            g.FillEllipse(pen.Brush, 0, 0, XMax, YMax);
            g.FillEllipse(Brushes.White, delta, delta, XMax - 2 * delta, YMax - 2 * delta);
            g.FillPolygon(pen.Brush, new[]
                {
                    new Point(0,0),
                    new Point(XCenter,0),
                    new Point(XMax/6, YMax/8),
                    new Point(0,YMax/10),
                });
            g.DrawArc(penl, new Rectangle(0, 0, XMax, YMax), 0, 360);
            g.DrawArc(penl, new Rectangle(0 + delta, 0 + delta, XMax - 2 * delta, YMax - 2 * delta), 0, 360);
            g.DrawLine(penl, 0, 0, XCenter, 0);
            g.DrawLine(penl, 0, 0, 0, YMax / 10);
            g.DrawLine(penl, 0, YMax / 10, XMax / 6, YMax / 8);
        }

        /// <summary>
        /// TODO:Этот метод где-то используется? может его удалить?
        /// </summary>
        protected void InvalidateEx()
        {
            if (Parent == null)
                return;
            Rectangle rc = new Rectangle(Location, Size);
            Parent.Invalidate(rc, true);
        }

        private Color MyColor
        {
            get
            {
                switch (Color)
                {
                    case PumpColor.Blue:
                        return System.Drawing.Color.LightBlue;
                    case PumpColor.Green:
                        return System.Drawing.Color.LightGreen;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Цвет
        /// </summary>
        [Category("Layout"), DefaultValue(PumpColor.Blue), Description("Цвет")]
        public PumpColor Color { get; set; }
    }
}


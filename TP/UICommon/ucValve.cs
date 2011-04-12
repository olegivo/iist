using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UICommon
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucValve : ucCommonBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ucValve()
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
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
          
            Pen penl = Pens.Black;
            g.DrawLine(penl, 0, 0, XMax, YMax);
            g.DrawLine(penl, 0, YMax, XMax, 0);
            Pen pen = new Pen(GetColor());
            if (Orientation == Orientation.Horizontal)
            {
                Height = 20;
                Width = 40;
                g.FillPolygon(pen.Brush, new[]
                                             {
                                                 new Point(0, 0),
                                                 new Point(0, YMax),
                                                 new Point(XMax, 0),
                                                 new Point(XMax, YMax),
                                                 new Point(0, 0),
                                             });
                g.DrawLine(penl, 0, 0, 0, YMax);
                g.DrawLine(penl, XMax, YMax, XMax, 0);
            }
            else
            {
                Height = 40;
                Width = 20;
                g.FillPolygon(pen.Brush, new[]
                                             {
                                                 new Point(0, 0),
                                                 new Point(XMax, 0),
                                                 new Point(0, YMax),
                                                 new Point(XMax, YMax),
                                                 new Point(0, 0),
                                             });
                g.DrawLine(penl, 0, 0, XMax, 0);
                g.DrawLine(penl, 0, YMax, XMax, YMax);
            }
        }

        private void ucValue_Load(object sender, EventArgs e)
        {

        }

        private Color GetColor()
        {
            switch (Color)
            {
                case ValveColor.Green:
                    return System.Drawing.Color.Green;
                case ValveColor.Orange:
                    return System.Drawing.Color.Orange;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Layout"), DefaultValue(ValveColor.Green), Description("Цвет")]
        public ValveColor Color { get; set; }
    }
}


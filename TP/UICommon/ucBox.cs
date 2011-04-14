using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UICommon
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucBox:ucCaptioned
    {
        /// <summary>
        /// 
        /// </summary>
        public ucBox()
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
            int xMax = Width - 1;
            int yMax = Height - 1;

            //градиент
            var myBrush1 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, yMax), new Point(25, yMax), Color.DarkGray, Color.Gray);
            var myBrush2 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(xMax-26, yMax), new Point(xMax, yMax), Color.Gray,Color.DarkGray);
            e.Graphics.FillRectangle(Brushes.Gray, 0, 0, xMax, yMax);
            e.Graphics.FillRectangle(myBrush1, 0, 0, 25, yMax);
            e.Graphics.FillRectangle(myBrush2, xMax-25, 0, 25, yMax);

            //надпись
            g.DrawString(Caption, new Font("Arial", 12,FontStyle.Bold), Brushes.Black,
                         new RectangleF(25, yMax/2, xMax, yMax));

            float f = YMax - YMax * Level / 10;
            g.DrawLine(Pens.Black, 0, f, XMax,  f);
        }

        private float _level;

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float Level
        {
            get { return _level; }
            set
            {
                if (_level != value)
                {
                    _level = value; 
                    Refresh();
                }
            }
        }
    }
}

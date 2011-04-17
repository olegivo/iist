using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System;

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
            int GradWidth = 10;         //ширина градиента
            if (xMax < GradWidth) { throw new Exception("Box size is too small."); }

            //градиент
            var myBrush1 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, yMax), new Point(GradWidth, yMax), Color.DarkGray, Color.Gray);
            var myBrush2 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(xMax-GradWidth-1, yMax), new Point(xMax, yMax), Color.Gray,Color.DimGray);
            var myBrush3 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(GradWidth, GradWidth), Color.LightBlue, Color.Transparent);
            e.Graphics.FillRectangle(Brushes.Gray, 0, 0, xMax, yMax);
            e.Graphics.FillRectangle(myBrush1, 0, 0, GradWidth, yMax);
            e.Graphics.FillRectangle(myBrush2, xMax-GradWidth, 0, GradWidth, yMax);
            

            //уголки
            e.Graphics.FillPolygon(Brushes.Black, new[]{
                new Point(0,GradWidth),
                new Point(0,0),
                new Point(GradWidth,1)});
            e.Graphics.FillPolygon(Brushes.Black, new[]{
                new Point(xMax-GradWidth,0),
                new Point(xMax,0),
                new Point(xMax,GradWidth)});
            e.Graphics.FillPolygon(Brushes.Black, new[]{
                new Point(0,yMax-GradWidth),
                new Point(0,yMax),
                new Point(GradWidth,yMax)});
            e.Graphics.FillPolygon(Brushes.Black, new[]{
                new Point(xMax-GradWidth,yMax),
                new Point(xMax,yMax),
                new Point(xMax,yMax-GradWidth)});

            
            //линия и градиент уровня
            float f = YMax - YMax * Level / 10;
            e.Graphics.FillRectangle(myBrush3, GradWidth, f, xMax - 2 * GradWidth, yMax - f);
            g.DrawLine(Pens.Black, 0, f, XMax,  f);

            //надпись
            g.DrawString(Caption, new Font("Arial", 12, FontStyle.Bold), Brushes.Black,
                         new RectangleF(25, yMax / 2, xMax, yMax));
        }

        //сам уровень наполнения box'a
        private float _level;
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

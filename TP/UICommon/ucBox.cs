using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            CreateBrashes();
        }
        
        /// <summary>
        /// ширина градиента
        /// </summary>
        const int GradWidth = 10;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //градиент
            e.Graphics.FillRectangle(Brushes.Gray, 0, 0, XMax, YMax);
            e.Graphics.FillRectangle(myBrush1, 0, 0, GradWidth, YMax);
            e.Graphics.FillRectangle(myBrush2, XMax-GradWidth, 0, GradWidth, YMax);
            
            //уголки
            e.Graphics.FillPolygon(Brushes.Black, new[]{
                new Point(0,GradWidth),
                new Point(0,0),
                new Point(GradWidth,1)});
            e.Graphics.FillPolygon(Brushes.Black, new[]{
                new Point(XMax-GradWidth,0),
                new Point(XMax,0),
                new Point(XMax,GradWidth)});
            e.Graphics.FillPolygon(Brushes.Black, new[]{
                new Point(0,YMax-GradWidth),
                new Point(0,YMax),
                new Point(GradWidth,YMax)});
            e.Graphics.FillPolygon(Brushes.Black, new[]{
                new Point(XMax-GradWidth,YMax),
                new Point(XMax,YMax),
                new Point(XMax,YMax-GradWidth)});

            
            //линия и градиент уровня
            float f = YMax - YMax * Level / 100;
            e.Graphics.FillRectangle(myBrush3, GradWidth, f, XMax - 2 * GradWidth, YMax - f);
            e.Graphics.DrawLine(Pens.Black, 0, f, XMax-1,  f);

            base.OnPaint(e);
        }

        private float _level;
        private LinearGradientBrush myBrush1;
        private LinearGradientBrush myBrush2;
        private LinearGradientBrush myBrush3;

        /// <summary>
        /// Уровень наполнения (в процентах)
        /// Влияет на процент заливки снизу вверх
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float Level
        {
            get { return _level;}
            set
            {
                if (_level != value)
                {
                    _level = value; 
                    Refresh();
                }
            }
        }

        private void ucBox_SizeChanged(object sender, EventArgs e)
        {
            CaptionLocation = new Point(25, YMax / 2);
            SetMinWidth();
            CreateBrashes();
        }

        private void CreateBrashes()
        {
            myBrush1 = new LinearGradientBrush(new Point(0, YMax), new Point(GradWidth, YMax), Color.DarkGray, Color.Gray);
            myBrush2 = new LinearGradientBrush(new Point(XMax - GradWidth - 1, YMax), new Point(XMax, YMax), Color.Gray, Color.DimGray);
            myBrush3 = new LinearGradientBrush(new Point(0, 0), new Point(GradWidth, GradWidth), Color.LightBlue, Color.Transparent);
        }

        /// <summary>
        /// задать минимальную ширину
        /// </summary>
        private void SetMinWidth()
        {
            if (Size.Width < GradWidth * 2)
                Size = new Size(GradWidth*2, Size.Height);
        }
    }
}

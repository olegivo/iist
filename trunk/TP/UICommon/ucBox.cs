using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System;

using DevExpress.XtraEditors;
using DevExpress.XtraGauges.Core.Model;

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
        
        const int GradWidth = 10;   //ширина градиента

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //if (XMax < GradWidth) { throw new Exception("Box size is too small."); }

            //градиент
            var myBrush1 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, YMax), new Point(GradWidth, YMax), Color.DarkGray, Color.Gray);
            var myBrush2 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(XMax-GradWidth-1, YMax), new Point(XMax, YMax), Color.Gray,Color.DimGray);
            var myBrush3 = new System.Drawing.Drawing2D.LinearGradientBrush(new Point(0, 0), new Point(GradWidth, GradWidth), Color.LightBlue, Color.Transparent);
            e.Graphics.FillRectangle(Brushes.Gray, 0, 0, XMax, YMax);
            e.Graphics.FillRectangle(myBrush1, 0, 0, GradWidth, YMax);
            e.Graphics.FillRectangle(myBrush2, XMax-GradWidth, 0, GradWidth, YMax);
            
            //Dispose(myBrush1);
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
            float f = YMax - YMax * Level / 1500;//ucIndicator.MaxValue;
            e.Graphics.FillRectangle(myBrush3, GradWidth, f, XMax - 2 * GradWidth, YMax - f);
            e.Graphics.DrawLine(Pens.Black, 0, f, XMax-1,  f);

            base.OnPaint(e);
        }

        private float _level;

        /// <summary>
        /// уровень наполнения (заливки) box'a
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
        /// <summary>
        /// Задать минимальную ширину box'а
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucBox_SizeChanged(object sender, EventArgs e)
        {
            CaptionLocation = new Point(25, YMax / 2);

            if (Size.Width < GradWidth * 2)
                Size = new Size(GradWidth*2, Size.Height);
        }



     }
}

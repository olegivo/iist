using DevExpress.XtraEditors;
using System.Drawing;
using UICommon;
using System.ComponentModel;
using System.Windows.Forms;

namespace TP.DrumTypeFurnace
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucTransporter3d : ucCommonBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ucTransporter3d()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush BrownBrush = Brushes.Brown;
            Brush GreyBrush = Brushes.DarkGray;
            Brush DarkGrayBrush = Brushes.Gray;
            g.FillPolygon(BrownBrush, new[] { 
                new Point(XMax/2,YMax), 
                new Point(0,YMax/2), 
                new Point(0,YMax/2-YMax/8), 
                new Point(XMax/2+XMax/8,YMax)});
            g.FillPolygon(BrownBrush, new[] { 
                new Point(XMax/2,0), 
                new Point(XMax/2-XMax/8,0), 
                new Point(XMax,YMax/2+YMax/8), 
                new Point(XMax,YMax/2)});
            g.FillPolygon(GreyBrush, new[] { 
                new Point(0,YMax/2+YMax/8), 
                new Point(0,0), 
                new Point(XMax/2-XMax/8,0), 
                new Point(XMax,YMax/2+YMax/8),
                //new Point(XMax,YMax),
                new Point(XMax/2+XMax/8,YMax),
                new Point(0,YMax/2-YMax/8)
                });
            g.FillPolygon(DarkGrayBrush, new[] { 
                new Point(XMax/2+XMax/8,YMax), 
                new Point(XMax/2,YMax-YMax/8), 
                new Point(XMax-XMax/8,YMax/2), 
                new Point(XMax,YMax/2+YMax/8)});
            g.FillPolygon(DarkGrayBrush, new[] { 
                new Point(XMax/2-XMax/8,YMax-YMax/4), 
                new Point(XMax/4,YMax/2+YMax/8), 
                new Point(XMax/2+XMax/8,YMax/4), 
                new Point(XMax-XMax/4,YMax/2-YMax/8)});
            g.FillPolygon(DarkGrayBrush, new[] { 
                new Point(XMax/8,YMax/2), 
                new Point(0,YMax/2-YMax/8), 
                new Point(XMax/2-XMax/8,0), 
                new Point(XMax/2,YMax/8)});
            //Pen penl = Pens.Black;
            //g.DrawLine(penl, 0, 0, XMax, YMax);
            
        }
    }
}

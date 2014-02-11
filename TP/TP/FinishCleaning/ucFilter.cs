using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP.FinishCleaning
{
    public partial class ucFilter : DevExpress.XtraEditors.XtraUserControl
    {
        public ucFilter()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int rem;
            int xMax = Width - 1;
            int yMax = Height - 1;
            int xCenter = Math.DivRem(xMax, 2, out rem);
            int yCenter = Math.DivRem(yMax, 2, out rem);

            Pen pen = Pens.Black;

            g.FillPolygon(Brushes.Tan, new[]
                {
                    new Point(0,yMax/20),
                    new Point(xMax-xMax/4,yMax/20),
                    new Point(xMax-xMax/4,15*yMax/20),
                    new Point(0,15*yMax/20),
                    });
            g.FillPolygon(Brushes.Peru, new[]
                {
                    new Point(0,0),
                    new Point(xMax-xMax/4,0),
                    new Point(xMax-xMax/4,yMax/20),
                    new Point(0,yMax/20),
                    });
            g.FillPolygon(Brushes.Peru, new[]
                {
                    new Point(0,15*yMax/20),
                    new Point(xMax-xMax/4,15*yMax/20),
                    new Point(xMax-xMax/4,16*yMax/20),
                    new Point(0,16*yMax/20),
                    });
            g.FillPolygon(Brushes.Tan, new[]
                {
                    new Point(0,16*yMax/20),
                    new Point(xMax-xMax/4,16*yMax/20),
                    new Point(3*xMax/8,yMax),
                    
                    });

            g.DrawRectangle(pen, 0, 0, xMax - xMax / 4, 4 * yMax / 5);
            g.DrawRectangle(pen, 0, yMax / 20, xMax / 4, 8 * yMax / 20);
            g.DrawRectangle(pen, xMax / 4, yMax / 20, xMax / 4, 8 * yMax / 20);
            g.DrawRectangle(pen, 2 * xMax / 4, yMax / 20, xMax / 4, 8 * yMax / 20);

            g.DrawLine(pen, 0, 15 * yMax / 20, xMax - xMax / 4, 15 * yMax / 20);

            g.DrawLine(pen, 0, 4 * yMax / 5, 3 * xMax / 8, yMax);
            g.DrawLine(pen, xMax - xMax / 4, 4 * yMax / 5, 3 * xMax / 8, yMax);

            //рукав
            g.FillPolygon(Brushes.Linen, new[]
                {
                    new Point(xMax/8,11*yMax/20),
                    new Point(xMax,10*yMax/20),
                    new Point(xMax,13*yMax/20),
                    new Point(xMax/8,13*yMax/20),
                    });
            g.DrawLine(pen, xMax, 9 * yMax / 20, xMax, 14 * yMax / 20);
            g.DrawLine(pen, xMax / 8, 13 * yMax / 20, xMax, 13 * yMax / 20);
            g.DrawLine(pen, xMax / 8, 13 * yMax / 20, xMax / 8, 11 * yMax / 20);
            g.DrawLine(pen, xMax / 8, 11 * yMax / 20, xMax, 10 * yMax / 20);

        }
        private void ucFilter_Load(object sender, EventArgs e)
        {

        }
    }
}

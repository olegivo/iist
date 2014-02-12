using System;
using System.Drawing;
using System.Windows.Forms;

namespace TP.FinishCleaning
{
    public partial class ucCrater : DevExpress.XtraEditors.XtraUserControl
    {
        public ucCrater()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Width = Height;
            int xMax = Width - 1;
            int yMax = Height - 1;


            Pen pen = Pens.Black;

            g.FillPolygon(Brushes.GreenYellow, new[]
                {
                    new Point(0,yMax / 3+yMax / 7),
                    new Point(xMax,yMax/7),
                    new Point(xMax,yMax),
                    new Point(0,yMax),
                    });
            g.DrawLine(pen, 0, yMax / 3, xMax, 0);
            g.DrawLine(pen, xMax, 0, xMax, yMax);
            g.DrawLine(pen, 0, yMax / 3, 0, yMax);
            g.DrawLine(pen, 0, yMax, xMax, yMax);

            g.FillPolygon(Brushes.Sienna, new[]
                {
                    new Point(0,yMax / 3),
                    new Point(xMax,0),
                    new Point(xMax,yMax/7),
                    new Point(0,yMax / 3 + yMax/7),
                    });

            g.DrawLine(pen, 0, yMax / 3 + yMax / 7, xMax, yMax / 7);

            g.DrawLine(pen, 0, yMax / 3, xMax / 6, yMax / 3 + yMax / 12);
            g.DrawLine(pen, xMax / 6, yMax / 3 + yMax / 12, 2 * xMax / 6, yMax / 3 - 2 * yMax / 18);
            g.DrawLine(pen, 2 * xMax / 6, yMax / 3 - 2 * yMax / 18, 3 * xMax / 6, yMax / 3 - yMax / 25);


        }
        private void ucCrater_Load(object sender, EventArgs e)
        {

        }
    }
}

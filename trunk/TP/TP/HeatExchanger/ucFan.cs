using System;
using System.Drawing;
using System.Windows.Forms;
using UICommon;

namespace TP.HeatExchanger
{
    public partial class ucFan : ucCaptioned
    {
        public ucFan()
        {
            InitializeComponent();
            //BackColor = Color.Transparent;
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            //Height = 9*Width / 10;
            int x = XMax / 10;
            int y = YMax / 9;

            Pen pen = Pens.Blue;
            //левый элемент
            g.DrawLine(pen, 0, YMax, 0, 4 * y);
            g.DrawLine(pen, 4 * x, YMax, 4 * x, 4 * y);
            g.DrawLine(pen, 0, 4 * y, 4 * x, 4 * y);
            g.DrawLine(pen, 0, YMax, 4 * x, YMax);
            g.DrawLine(pen, 2 * x, YMax, 2 * x, 4 * y);
            //левая заливка
            g.FillPolygon(Brushes.Pink, new[]
                                            {
                                                new Point(1, 4*y + 1),
                                                new Point(1, YMax),
                                                new Point(2*x, YMax),
                                                new Point(2*x, 4*y + 1),
                                            });
            //правая заливка
            g.FillPolygon(Brushes.Pink, new[]
                                            {
                                                new Point(2*x + 1, 4*y + 1),
                                                new Point(2*x + 1, YMax),
                                                new Point(4*x - 1, YMax),
                                                new Point(4*x - 1, 4*y + 1),
                                            });
            g.DrawString(Caption, new Font("Arial", 10), Brushes.Black,
                         new RectangleF(5, 4 * y + 10, 40, 30));

            //средний элемент
            g.DrawLine(pen, 3 * x, 2 * y, 3 * x, 4 * y);
            g.DrawLine(pen, 7 * x, 2 * y, 7 * x, 7 * y);
            g.DrawLine(pen, 3 * x, 2 * y, 7 * x, 2 * y);
            g.DrawLine(pen, 4 * x, 7 * y, 7 * x, 7 * y);
            g.DrawLine(pen, 5 * x, 2 * y, 5 * x, 7 * y);

            //левая заливка
            g.FillPolygon(Brushes.Pink, new[]
                                            {
                                                new Point(3*x + 1, 2*y + 1),
                                                new Point(3*x + 1, 4*y - 1),
                                                new Point(4*x + 1, 4*y - 1),
                                                new Point(4*x + 1, 7*y),
                                                new Point(5*x - 1, 7*y),
                                                new Point(5*x - 1, 2*y + 1),
                                            });
            //правая заливка
            g.FillPolygon(Brushes.Pink, new[]
                                            {
                                                new Point(5*x + 1, 2*y + 1),
                                                new Point(5*x + 1, 7*y),
                                                new Point(7*x, 7*y),
                                                new Point(7*x, 2*y + 1),
                                            });
            g.DrawString(Caption, new Font("Arial", 10), Brushes.Black,
                         new RectangleF(3 * x + 5, 2 * y + 10, 40, 30));

            //правый элемент
            g.DrawLine(pen, 6 * x, 0, 6 * x, 2 * y);
            g.DrawLine(pen, 10 * x, 0, 10 * x, 5 * y);
            g.DrawLine(pen, 6 * x, 0, 10 * x, 0);
            g.DrawLine(pen, 7 * x, 5 * y, 10 * x, 5 * y);
            g.DrawLine(pen, 8 * x, 0, 8 * x, 5 * y);

            //левая заливка
            g.FillPolygon(Brushes.Pink, new[]
                                            {
                                                new Point(6*x + 1, 1),
                                                new Point(6*x + 1, 2*y - 1),
                                                new Point(7*x + 1, 2*y - 1),
                                                new Point(7*x + 1, 5*y),
                                                new Point(8*x - 1, 5*y),
                                                new Point(8*x - 1, 1),
                                            });
            //правая заливка
            g.FillPolygon(Brushes.Pink, new[]
                                            {
                                                new Point(8*x + 1, 1),
                                                new Point(8*x + 1, 5*y),
                                                new Point(10*x, 5*y),
                                                new Point(10*x, 1),
                                            });
            g.DrawString(Caption, new Font("Arial", 10), Brushes.Black,
                         new RectangleF(6*x + 5, 10, 40, 30));
        }

        private void ucFan_Load(object sender, EventArgs e)
        {
            LayoutFans();
        }

        private void ucFan_SizeChanged(object sender, EventArgs e)
        {
            LayoutFans();
        }

        private void ucFan_Resize(object sender, EventArgs e)
        {
            LayoutFans();
        }

        private void LayoutFans()
        {
            int x = XMax / 10;
            int y = YMax / 9;

            //int x = Convert.ToInt32(XMax / 3) - 3;
            //int y = Convert.ToInt32(XMax / 6);
            int s = Convert.ToInt32(1.5*x);
            foreach (var pump in new[] { ucPump1, ucPump2, ucPump3 })
            {
                pump.Size = new Size(s, s);
            }

            ucPump1.Location = new Point(2*x + s/4, 7*y - s/2);
            ucPump2.Location = new Point(5*x + s/4, 5*y - s/2);
            ucPump3.Location = new Point(8*x + s/4, 3*y - s/2);

            ucCaptionedV1.Location = new Point(ucPump1.Left + s / 4, ucPump1.Bottom + s / 8);
            ucCaptionedV2.Location = new Point(ucPump2.Left + s / 4, ucPump2.Bottom + s / 8);
            ucCaptionedV3.Location = new Point(ucPump3.Left + s / 4, ucPump3.Bottom + s / 8);

            ucCaptionedK1.Location = new Point(1, 4*y);
            ucCaptionedK2.Location = new Point(3*x, 2*y);
            ucCaptionedK3.Location = new Point(6*x, 1);
        }

        private void ucCaptioned5_Load(object sender, EventArgs e)
        {

        }
    }
}


using System.Drawing;
using System.Windows.Forms;

namespace UICommon
{
    /// <summary>
    /// Реле потока
    /// </summary>
    public partial class ucFlowRelay : ucCaptioned
    {
        /// <summary>
        /// 
        /// </summary>
        public ucFlowRelay()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Height = Width / 2;
            Width = 44;
            int x = XCenter / 3;

            Pen pen = Pens.Black;
            g.FillPolygon(Brushes.LightSkyBlue, new[]
                {
                    new Point(0,0),
                    new Point(XMax,0),
                    new Point(XMax,YMax),
                    new Point(0,YMax),
                    });
            g.DrawRectangle(pen, 0, 0, XMax, YMax);
            g.DrawLine(pen, XCenter + x, 0, XCenter - x, YMax);
        }
    }
}

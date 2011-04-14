using System.Drawing;
using System.Windows.Forms;

namespace UICommon
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucTube : ucCommonBase
    {
        public ucTube()
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

            Pen pen = Pens.Black;
            
            //основание
            g.FillPolygon(Brushes.Goldenrod, new[]
                                                 {
                                                     new Point(0, 2*YMax/3),
                                                     new Point(XMax, 2*YMax/3),
                                                     new Point(XMax, YMax),
                                                     new Point(0, YMax),
                                                 });
            g.DrawRectangle(pen, 0, 2*YMax/3, XMax, YMax/3);

            //верх
            g.FillPolygon(Brushes.Goldenrod, new[]
                                                 {
                                                     new Point(XMax/3, 0),
                                                     new Point(2*XMax/3, 0),
                                                     new Point(2*XMax/3, 3*YMax/6),
                                                     new Point(XMax/3, 3*YMax/6),
                                                 });
            g.DrawRectangle(pen, XMax/3, 0, XMax/3, 3*YMax/6);

            // переход
            g.FillPolygon(Brushes.Goldenrod, new[]
                                                 {
                                                     new Point(0, 2*YMax/3),
                                                     new Point(XMax/3, 3*YMax/6),
                                                     new Point(2*XMax/3, 3*YMax/6),
                                                     new Point(XMax, 2*YMax/3),
                                                 });

            g.DrawLine(pen, 0, 2*YMax/3, XMax/3, 3*YMax/6);
            g.DrawLine(pen, 2*XMax/3, 3*YMax/6, XMax, 2*YMax/3);
            g.DrawLine(pen, XMax/3, 3*YMax/6, 2*XMax/3, 3*YMax/6);


        }
    }
}

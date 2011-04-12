using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace UICommon
{
    /// <summary>
    /// Конвейер
    /// </summary>
    public partial class ucTransporter : ucCommonBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ucTransporter()
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
            Height = Width / 9;
            int x = XCenter / 10;

            Pen pen = new Pen(Color.Black);
            Brush hatchBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Black, Color.Coral);
            
            // заштрихованный приямоугольник
            Rectangle rect = new Rectangle(7 * x, 0, XMax - 7 * x, YMax);
            g.FillRectangle(hatchBrush, rect);
            g.DrawRectangle(pen, rect);
            
            // круг
            g.DrawArc(pen, new Rectangle(0, 0, 2 * x, YMax), 0, 360);
            
            //соединительная линия
            pen.Width = 2;//двойной ширины
            g.DrawLine(pen, new Point(2*x, YCenter), new Point(7*x, YCenter));
        }
    }
}

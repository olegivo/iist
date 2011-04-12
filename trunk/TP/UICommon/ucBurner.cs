using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UICommon
{
    /// <summary>
    /// Горелка
    /// </summary>
    public partial class ucBurner : ucCaptioned
    {
        /// <summary>
        /// 
        /// </summary>
        public ucBurner()
        {
            InitializeComponent();
        }

        // Включеное состояние
        [Category("Layout"), Description("Огонь")]
        public bool BurnerStatus { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);//вызов базового метода ucCaptioned.OnPaint

            Graphics g = e.Graphics;
            Pen pen = Pens.Black;
            
            switch (Jet)
            {
                case JetEx.Left:
                    Width = 60;
                    Height = 40;
                    g.FillRectangle(Brushes.LightGray, 20, 0, 40, 40);
                    g.DrawRectangle(pen, 20,0,XMax-20,YMax);
                    g.FillRectangle(Brushes.LightSlateGray, 0, 15, 30, 10);
                    break;

                case JetEx.Right:
                    Width = 60;
                    Height = 40;
                    g.FillRectangle(Brushes.LightGray, 0, 0, 40, 40);
                    g.DrawRectangle(pen, 0, 0, 39, 39);
                    g.FillRectangle(Brushes.LightSlateGray, 30, 15, 30, 10);
                    break;

                case JetEx.Up:
                    Width = 40;
                    Height = 60;
                    g.FillRectangle(Brushes.LightGray, 0, 20, 40, 40);
                    g.DrawRectangle(pen, 0, 20, 39, 39);
                    g.FillRectangle(Brushes.LightSlateGray, 15, 0, 10, 30);
                    break;

                case JetEx.Down:
                    Width = 40;
                    Height = 60;
                    g.FillRectangle(Brushes.LightGray, 0, 0, 40, 40);
                    g.DrawRectangle(pen, 0, 0, 39, 39);
                    g.FillRectangle(Brushes.LightSlateGray, 15, 30, 10, 30);
                    break;
            }

            g.DrawString(Caption, new Font("Arial", 10), Brushes.Black,
                         new RectangleF(XCenter +3, YCenter-7 , 30, 20));
        }

        /// <summary>
        /// TODO:Этот метод где-то используется? может его удалить?
        /// </summary>
        protected void InvalidateEx()
        {
            if (Parent == null)
                return;
            Rectangle rc = new Rectangle(Location, Size);
            Parent.Invalidate(rc, true);
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Layout"), DefaultValue(JetEx.Left), Description("Направление")]
        public JetEx Jet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public enum JetEx
        {
            Left = 0,
            Right =1,
            Up = 2,
            Down = 3,
        }
    }
}

using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace UICommon
{
    /// <summary>
    /// Общий базовый элемент управления.
    /// </summary>
    public class ucCommonBase : XtraUserControl
    {
        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="T:DevExpress.XtraEditors.XtraUserControl"/> class.
        /// </para>
        /// </summary>
        public ucCommonBase()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            SetStyle(ControlStyles.Opaque, true);
        }

        /// <summary>
        /// Центральная координата по X
        /// </summary>
        protected int XCenter
        {
            get { return Convert.ToInt32(XMax / 2); }
        }

        /// <summary>
        /// Центральная координата по Y
        /// </summary>
        protected int YCenter
        {
            get { return Convert.ToInt32(YMax / 2); }
        }

        /// <summary>
        /// Максимальная координата по X
        /// </summary>
        /// <value></value>
        protected int XMax
        {
            get { return Width - 1; }
        }

        /// <summary>
        /// Максимальная координата по Y
        /// </summary>
        /// <value></value>
        protected int YMax
        {
            get { return Height - 1; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //g.FillRectangle(new SolidBrush(Color.FromArgb(0, Color.Black)), 0, 0, Width, Height);
            //GroupBoxRenderer.DrawParentBackground(e.Graphics, new Rectangle(0, 0, Width, Height), this);
            //InvalidateEx();
            base.OnPaint(e);
        }

/*
        protected void InvalidateEx()
        {
            if (Parent == null)
                return;
            Rectangle rc = new Rectangle(Location, Size);
            Parent.Invalidate(rc, true);
        }
*/

        /// <summary>
        /// 
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                // делаем стиль прозрачным:
                const int WS_EX_TRANSPARENT = 0x00000020;
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_TRANSPARENT;
                return createParams;
                //CreateParams cp = base.CreateParams;
                //cp.ExStyle |= 0x00000020;
                //return cp;
            }
        }

/*
        protected override void OnMove(EventArgs e)
        {
            if (Parent != null) Parent.Invalidate(Bounds, true);
        }
*/
    }
}
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UICommon
{
    /// <summary>
    /// Элемент управления, отображающий заголовок
    /// </summary>
    public class ucCaptioned : ucCommonBase
    {
        private string _caption = "";

        /// <summary>
        /// Заголовок
        /// </summary>
        [DefaultValue("")]
        public string Caption
        {
            get { return _caption; }
            set
            {
                if (_caption != value)
                {
                    _caption = value;
                    Refresh();
                }
            }
        }

        private Point _captionLocation = new Point(0, 0);

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Point CaptionLocation
        {
            get { return _captionLocation; }
            set
            {
                if (_captionLocation != value)
                {
                    _captionLocation = value; 
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;

            // Draw a string on the PictureBox.
            g.DrawString(Caption, new Font("Arial", 10), Brushes.Black,
                         new RectangleF(CaptionLocation, new SizeF(XMax, YMax)));

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

    }
}
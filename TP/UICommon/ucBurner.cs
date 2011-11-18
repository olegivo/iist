﻿using System;
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
            Click += ucBurner_Click;
        }

        void ucBurner_Click(object sender, EventArgs e)
        {
            BurnerStatus =! BurnerStatus;
        }
       
        // Метод запускающий событие
        private void RaiseStatusChanged()
        {
            EventHandler handler = StateChanged;
            if (handler != null) handler(this, EventArgs.Empty);
            Refresh();
        }

        public event EventHandler StateChanged;

        private bool burnerStatus;

        /// <summary>
        /// Включеное состояние
        /// </summary>
        [Category("Layout"), Description("Огонь")]
        public bool BurnerStatus
        {
            get { return burnerStatus; }
            set
            {
                if(burnerStatus != value)
                {
                    burnerStatus = value;
                    // сообщение о событии
                    RaiseStatusChanged();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = Pens.Black;
            
            switch (Jet)
            {
                case JetEx.Left:
                    //Width = 60;
                    //Height = 40;
                    g.FillRectangle(Brushes.LightGray, XMax/3, 0, XMax, YMax);
                    g.DrawRectangle(pen, XMax/3,0,XMax,YMax);
                    g.FillPolygon(Brushes.LightSlateGray, new[]
                                                 {
                                                     new Point(0, 2*YMax/6),
                                                     new Point(XMax/2, 2*YMax/6),
                                                     new Point(XMax/2, 4*YMax/6),
                                                     new Point(0, 4*YMax/6),
                                                 });
                    break;

                case JetEx.Right:
                    //Width = 60;
                    //Height = 40;
                    g.FillRectangle(Brushes.LightGray, 0, 0, 2*XMax/3, YMax);
                    g.DrawRectangle(pen, 0, 0, 2 * XMax / 3, YMax);
                    g.FillPolygon(Brushes.LightSlateGray, new[]
                                                 {
                                                     new Point(XMax/2, 2*YMax/6),
                                                     new Point(XMax, 2*YMax/6),
                                                     new Point(XMax, 4*YMax/6),
                                                     new Point(XMax/2, 4*YMax/6),
                                                 });
                    break;

                case JetEx.Up:
                    //Width = 40;
                    //Height = 60;
                    g.FillRectangle(Brushes.LightGray, 0, YMax/3, XMax, YMax);
                    g.DrawRectangle(pen, 0, YMax / 3, XMax, YMax);
                    g.FillPolygon(Brushes.LightSlateGray, new[]
                                                 {
                                                     new Point(2*XMax/6, 0),
                                                     new Point(4*XMax/6, 0),
                                                     new Point(4*XMax/6, YMax/2),
                                                     new Point(2*XMax/6, YMax/2),
                                                 });
                    break;

                case JetEx.Down:
                    //Width = 40;
                    //Height = 60;
                    g.FillRectangle(Brushes.LightGray, 0, 0, XMax, 2*YMax/3);
                    g.DrawRectangle(pen, 0, 0, XMax, 2 * YMax / 3);
                    g.FillPolygon(Brushes.LightSlateGray, new[]
                                                 {
                                                     new Point(2*XMax/6, YMax/2),
                                                     new Point(4*XMax/6, YMax/2),
                                                     new Point(4*XMax/6, YMax),
                                                     new Point(2*XMax/6, YMax),
                                                 });
                    break;
            }

            if (BurnerStatus)
            {
                g.FillEllipse(Brushes.OrangeRed, XMax/2 - 5, YMax/2 - 5, 10, 10);
            }

            base.OnPaint(e);//вызов базового метода ucCaptioned.OnPaint

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

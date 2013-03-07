using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UICommon;

namespace TP.DrumTypeFurnace
{
    public partial class ucPe4b : ucCommonBase
    {
        public ucPe4b()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillPolygon(Brushes.DarkGray, new[]
                                                 {
                                                     new Point(0, 15*YMax/100),
                                                     new Point(35*XMax/200, 15*YMax/100), 
                                                     new Point(45*XMax/200, 20*YMax/100),
                                                     new Point(45*XMax/200, 55*YMax/100),
                                                     new Point(35*XMax/200, 60*YMax/100), 
                                                     new Point(0, 60*YMax/100),
                                                 });
            e.Graphics.FillRectangle(Brushes.RosyBrown, 45*XMax/200, 0, 15*XMax/200, 75*YMax/100);
            e.Graphics.FillRectangle(Brushes.DarkGray, 475*XMax/2000, 75*YMax/100, 10*XMax/200, 5*YMax/100);
            e.Graphics.FillRectangle(Brushes.RosyBrown, 45*XMax/200, 80*YMax/100, 15*XMax/200, 20*YMax/100);
            e.Graphics.FillRectangle(Brushes.DarkGray, 60*XMax/200, 30*YMax/100, 70*XMax/200, 15*YMax/100);
            e.Graphics.FillRectangle(Brushes.RosyBrown, 675*XMax/2000, 275*YMax/1000, 75*XMax/2000, 25*YMax/1000);
            e.Graphics.FillRectangle(Brushes.RosyBrown, 675*XMax/2000, 45*YMax/100, 75*XMax/2000, 25*YMax/1000);
            e.Graphics.FillRectangle(Brushes.RosyBrown, 100*XMax/200, 275*YMax/1000, 75*XMax/2000, 25*YMax/1000);
            e.Graphics.FillRectangle(Brushes.RosyBrown, 100*XMax/200, 45*YMax/100, 75*XMax/2000, 25*YMax/1000);
            e.Graphics.FillRectangle(Brushes.RosyBrown, 70*XMax/200, 30*YMax/100, 25*XMax/2000, 15*YMax/100);
            e.Graphics.FillRectangle(Brushes.RosyBrown, 1025*XMax/2000, 30*YMax/100, 25*XMax/2000, 15*YMax/100);
            e.Graphics.FillRectangle(Brushes.RosyBrown, 130*XMax/200, 20*YMax/100, 20*XMax/200, 35*YMax/100);
            e.Graphics.FillRectangle(Brushes.DarkGray, 150*XMax/200, 30*YMax/100, 25*XMax/200, 15*YMax/100);
            e.Graphics.FillRectangle(Brushes.DarkGray, 175*XMax/200, 35*YMax/100, 25*XMax/200, 5*YMax/100);


            //e.Graphics.DrawRectangle(Pens.Black, 0, 0, XMax, YMax);
                      
            //e.Graphics.DrawRectangle(Pens.Black, 10*XMax/200,10*,30,30);
        }
    }
}

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
    public partial class ucStakan : ucCommonBase
    {
        public ucStakan()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawPolygon(Pens.Black, new[]
                                                 {
                                                     new Point(0, YMax/2),
                                                     new Point(8*XMax/10, YMax/2), 
                                                     new Point(5*XMax/10, YMax),
                                                     new Point(0, YMax),
                                                 });
            e.Graphics.DrawLine(Pens.Black, 5*XMax/10, 9*YMax/10, 9*XMax/10, 25*YMax/100);  
            e.Graphics.DrawLine(Pens.Black, 5*XMax/10, 8*YMax/10, 9*XMax/10, 15*YMax/100); 
            e.Graphics.DrawEllipse(Pens.Black, 49*XMax/100, 8*YMax/10, 2*XMax/100, 1*YMax/10);
            e.Graphics.DrawEllipse(Pens.Black, 89*XMax/100, 15*YMax/100, 2*XMax/100, 1*YMax/10);
        }


    }
}

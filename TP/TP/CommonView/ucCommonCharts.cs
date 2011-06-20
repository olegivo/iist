using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TP.CommonView
{
    public partial class ucCommonCharts : DevExpress.XtraEditors.XtraUserControl
    {
        public ucCommonCharts()
        {
            InitializeComponent();
        }
        public UICommon.ucChart Chart1
        {
            get { return this.ucChart1; }
        }
        public UICommon.ucChart Chart2
        {
            get { return this.ucChart2; }
        }
        public UICommon.ucChart Chart3
        {
            get { return this.ucChart3; }
        }
        public UICommon.ucChart Chart4
        {
            get { return this.ucChart4; }
        }
        public UICommon.ucChart Chart5
        {
            get { return this.ucChart5; }
        }
        public UICommon.ucChart Chart6
        {
            get { return this.ucChart6; }
        }
        public UICommon.ucChart Chart7
        {
            get { return this.ucChart7; }
        }
        public UICommon.ucChart Chart8
        {
            get { return this.ucChart8; }
        }
    }
}

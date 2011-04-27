using System;
using System.ComponentModel;

namespace TP.HeatExchanger
{
    public partial class ucAllHeatExchanger : DevExpress.XtraEditors.XtraUserControl
    {
        public ucAllHeatExchanger()
        {
            InitializeComponent();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        private float Level_O2
        {
            get { return ucIndicator1.EditValue; }
            set { ucIndicator1.EditValue = value; }
        }

        private float Level_CO
        {
            get { return ucIndicator2.EditValue; }
            set { ucIndicator2.EditValue = value; }
        }

        private float Level_TP4
        {
            get { return ucIndicator3.EditValue; }
            set { ucIndicator3.EditValue = value; }
        }

        private float Level_TP5
        {
            get { return ucIndicator4.EditValue; }
            set { ucIndicator4.EditValue = value; }
        }

        private void spinEdit1_EditValueChanged(object sender, System.EventArgs e)
        {
            Level_O2 = (float)Convert.ToDecimal(spinEdit1.EditValue);
        }

        private void spinEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Level_CO = (float)Convert.ToDecimal(spinEdit2.EditValue);
        }

        private void spinEdit3_EditValueChanged(object sender, EventArgs e)
        {
            Level_TP4 = (float)Convert.ToDecimal(spinEdit3.EditValue);
        }

        private void spinEdit4_EditValueChanged(object sender, EventArgs e)
        {
            Level_TP5 = (float)Convert.ToDecimal(spinEdit4.EditValue);
        }
    }
}

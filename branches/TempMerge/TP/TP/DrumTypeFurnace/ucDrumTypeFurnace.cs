using System;
using System.ComponentModel;


namespace TP.DrumTypeFurnace
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucDrumTypeFurnace : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucDrumTypeFurnace()
        {
            InitializeComponent();
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float T1
        {
            get { return ucIndicator1.EditValue; }
            set { ucIndicator1.EditValue = value; }
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            T1 = (float)Convert.ToDecimal(spinEdit1.EditValue);
        }

        public float T2
        {
            get { return ucIndicator2.EditValue; }
            set { ucIndicator2.EditValue = value; }
        }

        private void SpinEdit2_EditValueChanged(object sender, EventArgs e)
        {
            T2 = (float)Convert.ToDecimal(spinEdit2.EditValue);
        }

        public float T8
        {
            get { return ucIndicator3.EditValue; }
            set { ucIndicator3.EditValue = value; }
        }

        private void spinEdit3_EditValueChanged(object sender, EventArgs e)
        {
            T8 = (float)Convert.ToDecimal(spinEdit3.EditValue);
        }

        public float DU9
        {
            get { return ucIndicator4.EditValue; }
            set { ucIndicator4.EditValue = value; }
        }

        private void SpinEdit4_EditValueChanged(object sender, EventArgs e)
        {
            DU9 = (float)Convert.ToDecimal(spinEdit4.EditValue);
        }

        public float S
        {
            get { return ucIndicator5.EditValue; }
            set { ucIndicator5.EditValue = value; }
        }

        private void SpinEdit5_EditValueChanged(object sender, EventArgs e)
        {
            S = (float)Convert.ToDecimal(spinEdit5.EditValue);
        }
    }
}

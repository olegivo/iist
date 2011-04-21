using System;
using System.ComponentModel;

namespace TP.FinishCleaning
{
    public partial class ucFinishCleaning : DevExpress.XtraEditors.XtraUserControl
    {
        public ucFinishCleaning()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float Level3
        {
            get { return ucIndicator2.EditValue; }
            set { ucIndicator2.EditValue = value; }
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Level3 = (float) Convert.ToDecimal(spinEdit1.EditValue);
        }
    }
}

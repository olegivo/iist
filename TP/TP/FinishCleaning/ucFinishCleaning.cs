﻿using System;
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
        public float Level_CO
        {
            get { return ucIndicator2.EditValue; }
            set { ucIndicator2.EditValue = value; }
        }

        public float Level_NO
        {
            get { return ucIndicator1.EditValue; }
            set { ucIndicator1.EditValue = value; }
        }

        public float Level_NO2
        {
            get { return ucIndicator3.EditValue; }
            set { ucIndicator3.EditValue = value; }
        }

        public float Level_O2
        {
            get { return ucIndicator5.EditValue; }
            set { ucIndicator5.EditValue = value; }
        }

        public float Level_SO2
        {
            get { return ucIndicator6.EditValue; }
            set { ucIndicator6.EditValue = value; }
        }

        public float Level_TC7
        {
            get { return ucIndicator4.EditValue; }
            set { ucIndicator4.EditValue = value; }
        }

        public float Level_TC6
        {
            get { return ucIndicator7.EditValue; }
            set { ucIndicator7.EditValue = value; }
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Level_CO = (float)Convert.ToDecimal(spinEdit1.EditValue);
        }

        private void spinEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Level_NO = (float)Convert.ToDecimal(spinEdit2.EditValue);
        }

        private void spinEdit3_EditValueChanged(object sender, EventArgs e)
        {
            Level_NO2 = (float)Convert.ToDecimal(spinEdit3.EditValue);
        }

        private void spinEdit4_EditValueChanged(object sender, EventArgs e)
        {
            Level_O2 = (float)Convert.ToDecimal(spinEdit4.EditValue);
        }

        private void spinEdit5_EditValueChanged(object sender, EventArgs e)
        {
            Level_SO2 = (float)Convert.ToDecimal(spinEdit5.EditValue);
        }

        private void spinEdit6_EditValueChanged(object sender, EventArgs e)
        {
            Level_TC7 = (float)Convert.ToDecimal(spinEdit6.EditValue);
        }

        private void spinEdit7_EditValueChanged(object sender, EventArgs e)
        {
            Level_TC6 = (float)Convert.ToDecimal(spinEdit7.EditValue);
        }
    }
}
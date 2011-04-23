using System;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace TP.ReheatChamber
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucReheatChamber : XtraUserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucReheatChamber()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ДУ4 (РЕ)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float Level2
        {
            get { return ucBox2.Level; }
            set { ucIndicator2.EditValue = ucBox2.Level = value; }
        }
        /// <summary>
        /// ДУ1 (НЕ)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float Level3
        {
            get { return ucBox3.Level; }
            set { ucIndicator3.EditValue = ucBox3.Level = value; }
        }
        /// <summary>
        /// ДУ11 (ПТ)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float Level4
        {
            get { return ucBox4.Level; }
            set { ucIndicator4.EditValue = ucBox4.Level = value; }
        }
        /// <summary>
        /// ТР3 (камера дожигания)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float Level1
        {
            //get { return ucBox4.Level; }  /*у камеры нет уровня*/
            set { ucIndicator1.EditValue = value; }
        }
        /// <summary>
        /// P (камера дожигания)
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float Level5
        {
            //get { return ucBox4.Level; }  /*у камеры нет уровня*/
            set { ucIndicator5.EditValue = value; }
        }
       
        
        
        /*********************СИМУЛЯТОРЫ_ДАТЧИКОВ************************************/
        private void numericUpDown2_ValueChanged(object sender, System.EventArgs e)
        {
            Level2 = (float)Convert.ToDecimal(numericUpDown2.Value);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Level3 = (float)Convert.ToDecimal(numericUpDown3.Value);

        }
        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            Level4 = (float)Convert.ToDecimal(numericUpDown4.Value);

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Level1 = (float)Convert.ToDecimal(numericUpDown1.Value);
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            Level5 = (float)Convert.ToDecimal(numericUpDown5.Value);
        }




    }
}

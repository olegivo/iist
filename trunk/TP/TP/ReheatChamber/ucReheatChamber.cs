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
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float Level1
        {
            get { return ucBox1.Level; }
            set { ucIndicator1.EditValue = ucBox1.Level = value; }
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

    }
}

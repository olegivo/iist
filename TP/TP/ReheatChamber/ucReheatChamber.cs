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
            set { ucGauge1.EditValue = ucBox1.Level = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public float Level2
        {
            get { return ucBox2.Level; }
            set { ucGauge2.EditValue = ucBox2.Level = value; }
        }

    }
}

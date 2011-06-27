namespace TP.CyclonAndScrubber
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucCyclonAndScrubber : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucCyclonAndScrubber()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ph1
        /// </summary>
        public float Ph1
        {
            set { ucIndicatorPh1.EditValue = value; }
        }

        /// <summary>
        /// Ph2
        /// </summary>
        public float Ph2
        {
            set { ucIndicatorPh2.EditValue = value; }
        }

        /// <summary>
        /// ДУ-10
        /// </summary>
        public float Level10
        {
            set { ucIndicatorLevel10.EditValue = value; }
        }
    }
}

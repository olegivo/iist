using System;
using System.ComponentModel;

namespace TP
{
    [DefaultEvent("IsSmokesChanged")]
    public partial class ucTube : DevExpress.XtraEditors.XtraUserControl
    {
        public ucTube()
        {
            InitializeComponent();
        }

        private bool _IsSmokes;

        /// <summary>
        /// 
        /// </summary>
        [Category("Tube"), DefaultValue(false)]
        public bool IsSmokes
        {
            get { return _IsSmokes; }
            set
            {
                _IsSmokes = value;
                if (IsSmokesChanged != null)
                    IsSmokesChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// При изменении дымохода
        /// </summary>
        public event EventHandler IsSmokesChanged;
    }
}

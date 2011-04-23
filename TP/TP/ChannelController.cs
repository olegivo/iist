using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TP
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChannelController : Component
    {
        /// <summary>
        /// 
        /// </summary>
        public ChannelController()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public ChannelController(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        //private frmTP _form;
        /// <summary>
        /// временно не работает
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        //public frmTP Form
        //{
        //    get
        //    {
        //        //return _form;
        //    }
        //    set
        //    {
        //        if (_form != value)
        //        {
        //            if (_form != null)
        //            {
        //                _form.spinEdit1.EditValueChanged -= spinEdit1_EditValueChanged;
        //                _form.spinEdit2.EditValueChanged -= spinEdit2_EditValueChanged;
        //            }

        //            _form = value;

        //            if (_form != null)
        //            {
        //                _form.spinEdit1.EditValueChanged += spinEdit1_EditValueChanged;
        //                _form.spinEdit2.EditValueChanged += spinEdit2_EditValueChanged;
        //            }
        //        }
        //    }
        //}

        //private void spinEdit2_EditValueChanged(object sender, EventArgs e)
        //{
        //    _form.ucReheatChamber1.Level2 = (float)Convert.ToDecimal(_form.spinEdit2.EditValue);
        //}

        //private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        //{
        //    _form.ucReheatChamber1.Level1 = (float)Convert.ToDecimal(_form.spinEdit1.EditValue);
        //}
    }
}

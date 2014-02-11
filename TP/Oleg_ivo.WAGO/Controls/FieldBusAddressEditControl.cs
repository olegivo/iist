using System;
using System.Windows.Forms;

namespace Oleg_ivo.WAGO.Controls
{
    ///<summary>
    ///
    ///</summary>
    public partial class FieldBusAddressEditControl : UserControl
    {
        ///<summary>
        ///
        ///</summary>
        public FieldBusAddressEditControl()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FieldBusNodesEditControl fieldBusNodesEditControl = tabControl1.SelectedTab.Controls[0] as FieldBusNodesEditControl;
            if (fieldBusNodesEditControl != null) fieldBusNodesEditControl.Fill();
        }

        private void FieldBusAddressEditControl_Load(object sender, EventArgs e)
        {
            tabControl1_SelectedIndexChanged(tabControl1, EventArgs.Empty);
        }
    }
}

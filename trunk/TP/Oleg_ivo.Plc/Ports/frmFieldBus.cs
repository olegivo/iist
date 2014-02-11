using System;
using System.Windows.Forms;

namespace Oleg_ivo.Plc.Ports
{
    public partial class frmFieldBus : Form
    {
        public frmFieldBus()
        {
            InitializeComponent();
            //TODO:при использовании необходимо инициализировать ucDevicePortCombobox1.FieldBusFactory;
        }

        private void ucFieldBusTypeCombobox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ucDevicePortCombobox1.FieldBusType = ucFieldBusTypeCombobox1.FieldBusType;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
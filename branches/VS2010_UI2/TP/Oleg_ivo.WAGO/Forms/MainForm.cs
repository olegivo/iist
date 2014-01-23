using System;
using System.Windows.Forms;

namespace Oleg_ivo.WAGO.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            btnStart.Enabled = rbRS485.Checked || rbEthernet.Checked;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            //DistributedMeasurementInformationSystem.Instance.
        }
    }
}
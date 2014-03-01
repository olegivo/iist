using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WAGOConfigurationImporter
{
    public partial class LogicalChannelsForm : Form
    {
        public LogicalChannelsForm()
        {
            InitializeComponent();
        }

        private void physicalChannelBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            //this.physicalChannelBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.plc27DataSet);

        }

        private void LogicalChannelsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'plc27DataSet.LogicalChannel' table. You can move, or remove it, as needed.
            this.logicalChannelTableAdapter.Fill(this.plc27DataSet.LogicalChannel);
            // TODO: This line of code loads data into the 'plc27DataSet.PhysicalChannel' table. You can move, or remove it, as needed.
           // this.physicalChannelTableAdapter.Fill(this.plc27DataSet.PhysicalChannel);

        }

        private void logicalChannelBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.logicalChannelBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.plc27DataSet);

        }
    }
}

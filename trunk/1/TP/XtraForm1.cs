using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace TP
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public ucTube Tube1
        {
            get { return ucTube1; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public bool IsSmokes
        {
            get { return Tube1.IsSmokes; }
            set { Tube1.IsSmokes = value; }
        }

        private void ucTube1_IsSmokesChanged(object sender, EventArgs e)
        {
            ovalShape1.Visible = IsSmokes;
        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            IsSmokes = checkButton1.Checked;
        }
    }
}
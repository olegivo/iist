using System;
using System.Windows.Forms;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Tools.Utils;

namespace Oleg_ivo.WAGO.Forms
{
    ///<summary>
    ///
    ///</summary>
    public partial class TestChannelsForm : Form
    {
        private LogicalChannel _logicalChannel;

        ///<summary>
        ///
        ///</summary>
        public TestChannelsForm()
        {
            InitializeComponent();
        }

        ///<summary>
        ///
        ///</summary>
        public LogicalChannel LogicalChannel
        {
            get { return _logicalChannel; }
            set
            {
                _logicalChannel = value;
                Text = string.Format("{0}/{1}", LogicalChannel, LogicalChannel.PhysicalChannel);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            ushort value = ushort.Parse(tbWriteValue.Text);
            LogicalChannel.SetValue(value);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            object value = LogicalChannel.GetValue();

            string result = "";

            if (value is bool[])
            {
                bool[] bools = value as bool[];

                ushort u = 0;
                for (int i = bools.Length - 1; i >= 0; i--)
                {
                    if(bools[i]) u += (ushort)(Math.Pow(2, i));
                }

                result = u.ToString();
            }
            else if (value is Array)
            {
                result = Utils.StringUtils.Array2String((Array) value);
            }
            else if (value is Double)
            {
                result = value.ToString();
            }

            tbReadValue.Text = result;
        }
    }
}
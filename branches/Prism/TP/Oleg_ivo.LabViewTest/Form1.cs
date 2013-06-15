using System;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DMS.Common.Messages;
using Oleg_ivo.HighLevelClient.LabViewAdapter;

namespace Oleg_ivo.LabViewTest
{
    ///<summary>
    ///
    ///</summary>
    public partial class Form1 : Form
    {
        ///<summary>
        ///
        ///</summary>
        public Form1()
        {
            InitializeComponent();
            Adapter.Instance.Equals(null);
        }

        ///<summary>
        ///
        ///</summary>
        public event EventHandler<CustomEventArgs> CustomEvent;

        private void InvokeCustomEvent(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> Handler = CustomEvent;
            if (Handler != null) Handler(this, e);
        }

        private void sbRaiseEvent_Click(object sender, EventArgs e)
        {
            CustomEventArgs args = new CustomEventArgs();
            InvokeCustomEvent(args);
        }

        private void beSentValue_Properties_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if(e.Button.Kind ==ButtonPredefines.Right)
            {
                Random random = new Random();
                double value = random.NextDouble();
                int min = 900, max = 1300;
                value = (min + (max - min)*value)*10;
                beSentValue.EditValue = Convert.ToInt16(value);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            beSentValue_Properties_ButtonPressed(null, new ButtonPressedEventArgs(beSentValue.Properties.Buttons[0]));
        }

        private void beSentValue_EditValueChanged(object sender, EventArgs e)
        {
            InternalLogicalChannelDataMessage message = new InternalLogicalChannelDataMessage()
            {
                DataMode = DataMode.Read,
                LogicalChannelId = 1,
                Value = beSentValue.EditValue
            };
            Adapter.Instance.Send(message);
        }
    }
}

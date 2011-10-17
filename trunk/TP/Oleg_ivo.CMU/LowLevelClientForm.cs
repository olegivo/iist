using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DMS.Common.Messages;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.Plc;
using Oleg_ivo.Tools.UI;
using Oleg_ivo.WAGO;

namespace Oleg_ivo.CMU
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LowLevelClientForm : Form
    {
        private ControlManagementUnit _ControlManagementUnit;

        /// <summary>
        /// 
        /// </summary>
        public LowLevelClientForm()
        {
            InitializeComponent();

            ControlManagementUnit.GetRegName = GetRegName;
            ControlManagementUnit.NeedProtocol += ControlManagementUnit_NeedProtocol;
        }

        private ControlManagementUnit ControlManagementUnit
        {
            get
            {
                if (_ControlManagementUnit==null)
                {
                    _ControlManagementUnit = new ControlManagementUnit();
                    _ControlManagementUnit.GetDistributedMeasurementInformationSystem =
                        GetDistributedMeasurementInformationSystem;
                    _ControlManagementUnit.BuildSystemConfiguration();
                }
                return _ControlManagementUnit;
            }
        }

        private DistributedMeasurementInformationSystemBase GetDistributedMeasurementInformationSystem()
        {
            return DistributedMeasurementInformationSystem.Instance;
        }


        void ControlManagementUnit_NeedProtocol(object sender, EventArgs e)
        {
            if (sender is double || sender is string)
                Protocol(sender);
        }

        private void Protocol(object sender)
        {
            string s = string.Format("{0}\t{1}{2}", DateTime.Now, sender, Environment.NewLine);

            SetText(textBox1, s);
        }

        private delegate void StDelegate(TextBox info, string s);
        private void SetText(TextBox info, string s)
        {
            if(info.InvokeRequired)
            {
                StDelegate ddd = SetText;
                info.Invoke(ddd, new object[] { info, s });
            }
            else
            {
                textBox1.Text += s;                
            }
        }

        /// <summary>
        /// �������� ������� <see cref="E:System.Windows.Forms.Form.Closing"/>.
        /// </summary>
        /// <param name="e">������ <see cref="T:System.ComponentModel.CancelEventArgs"/>, ���������� ������ �������. </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            ControlManagementUnit.NeedProtocol -= ControlManagementUnit_NeedProtocol;
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            ControlManagementUnit.SendMessage(new InternalMessage());
        }

        private bool CanRegister
        {
            set
            {
                btnRegister.Enabled = textBox2.Enabled = value;
                doubleListBoxControl1.Enabled = btnSendMessage.Enabled = btnUnregister.Enabled = !value;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                ControlManagementUnit.Register();
                CanRegister = false;
            }
            catch(Exception)
            {
                CanRegister = true;
                throw;
            }
        }

        private void btnUnregister_Click(object sender, EventArgs e)
        {
            try
            {
                ControlManagementUnit.Unregister();
                ControlManagementUnit.UnregisterCompleted += Proxy_UnregisterCompleted;
                CanRegister = true;
            }
            catch (Exception)
            {
                CanRegister = false;
                throw;
            }
        }

        private void Proxy_UnregisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            ControlManagementUnit.UnregisterCompleted -= Proxy_UnregisterCompleted;
            IList left = doubleListBoxControl1.SourceLeft;

            foreach (int id in doubleListBoxControl1.SourceRight)
            {
                TryRemovePoll(new MovingEventArgs(DoubleListBoxControl.Direction.RightToLeft, id));
                left.Add(id);
            }

            doubleListBoxControl1.InitSources(left, new List<int>());

        }

        private string GetRegName()
        {
            return textBox2.Text;
        }

        private void doubleListBoxControl1_ItemMoving(object sender, MovingEventArgs e)
        {
            if (e.MoveDirection == DoubleListBoxControl.Direction.LeftToRight)
                TryAddPoll(e);
            else
                TryRemovePoll(e);
        }

        private void TryAddPoll(MovingEventArgs e)
        {
            ControlManagementUnit.TryAddPoll(e, this);
        }

        private void TryRemovePoll(MovingEventArgs e)
        {
            ControlManagementUnit.TryRemovePoll(e);
        }

        private void doubleListBoxControl1_ItemMoved(object sender, MovedEventArgs e)
        {
            ChannelSubscribeMessage subscribeMessage = new ChannelSubscribeMessage
            {
                RegName = GetRegName(),
                Mode = e.MoveDirection == DoubleListBoxControl.Direction.LeftToRight,
                LogicalChannelId = (int)e.MovingObject
            };


            //����������� ������� � MES
            if (subscribeMessage.Mode)
                RegisterChannel(subscribeMessage);
            else
                UnRegisterChannel(subscribeMessage);

            doubleListBoxControl1.Refresh();
            RefreshBtnReadChannel();
        }

        /// <summary>
        /// ���������������� ����� � ��������� ��� �����
        /// </summary>
        /// <param name="subscribeMessage"></param>
        private void RegisterChannel(ChannelSubscribeMessage subscribeMessage)
        {
            ControlManagementUnit.RegisterChannel(subscribeMessage);
        }

        private void UnRegisterChannel(ChannelSubscribeMessage subscribeMessage)
        {
            ControlManagementUnit.UnregisterChannel(subscribeMessage);
        }

        private void RefreshBtnReadChannel()
        {
            btnChannelRead.Enabled = doubleListBoxControl1.SourceRight.Count > 0 && doubleListBoxControl1.SelectionRight.Count > 0;
        }

        private void doubleListBoxControl1_SelectionRightChanged(object sender, EventArgs e)
        {
            RefreshBtnReadChannel();
        }

        private void LowLevelClientForm_Load(object sender, EventArgs e)
        {
            List<int> _left = new List<int>();
            List<int> _right = new List<int>();

            //��������� ������ ��������������������� ������ (Id > 0):
            _left.AddRange(ControlManagementUnit.GetLogicalChannels());
            //_right.AddRange(Enumerable.Range(11, 10));
            
            doubleListBoxControl1.InitSources(_left, _right);
        }

        private void btnChannelRead_Click(object sender, EventArgs e)
        {
            foreach (int logicalChannelId in doubleListBoxControl1.SelectionRight)
            {
                InternalLogicalChannelDataMessage message =
                    new InternalLogicalChannelDataMessage
                    {
                        DataMode = DataMode.Read,
                        RegName = GetRegName(),
                        Value = Math.PI,
                        LogicalChannelId = logicalChannelId
                    };
                ControlManagementUnit.ReadChannel(message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();
        }

    }
}
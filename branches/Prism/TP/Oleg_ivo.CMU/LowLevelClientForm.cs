using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DMS.Common.Messages;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.PrismExtensions.Autofac;
using Oleg_ivo.PrismExtensions.Autofac.DependencyInjection;
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
        }

        internal ControlManagementUnit ControlManagementUnit
        {
            get
            {
                return _ControlManagementUnit ??
                       (_ControlManagementUnit =
                        new ControlManagementUnit
                            {
                                GetDistributedMeasurementInformationSystem = GetDistributedMeasurementInformationSystem
                            });
            }
        }

        [Dependency]
        public IDistributedMeasurementInformationSystem DMIS { private get; set; }

        private IDistributedMeasurementInformationSystem GetDistributedMeasurementInformationSystem()
        {
            return DMIS;
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
            if (info.InvokeRequired)
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
        /// Вызывает событие <see cref="E:System.Windows.Forms.Form.Closing"/>.
        /// </summary>
        /// <param name="e">Объект <see cref="T:System.ComponentModel.CancelEventArgs"/>, содержащий данные события. </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            ControlManagementUnit.NeedProtocol -= ControlManagementUnit_NeedProtocol;
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            ControlManagementUnit.SendMessage(new InternalMessage(GetRegName(), null));
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
            catch (Exception)
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

            foreach (LogicalChannel channel in doubleListBoxControl1.SourceRight)
            {
                TryRemovePoll(new MovingEventArgs(DoubleListBoxControl.Direction.RightToLeft, channel));
                left.Add(channel);
            }

            doubleListBoxControl1.InitSources(left, new List<LogicalChannel>());

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
            RegistrationMode registrationMode = e.MoveDirection ==
                                                DoubleListBoxControl.Direction.LeftToRight
                                                    ? RegistrationMode.Register
                                                    : RegistrationMode.Unregister;
            LogicalChannel channel = e.MovingObject as LogicalChannel;

            if (channel != null)
            {
                var registrationMessage = new ChannelRegistrationMessage(GetRegName(),
                                                                         null,
                                                                         registrationMode,
                                                                         DataMode.Read | DataMode.Write,
                                                                         channel.Id)
                    {
                        Description = channel.Description,
                        MinValue = channel.MinValue,
                        MaxValue = channel.MaxValue,
                        MinNormalValue = channel.MinNormalValue,
                        MaxNormalValue = channel.MaxNormalValue
                    };


                //регистрация каналов в MES
                switch (registrationMessage.RegistrationMode)
                {
                    case RegistrationMode.Register:
                        RegisterChannel(registrationMessage);
                        break;
                    case RegistrationMode.Unregister:
                        UnRegisterChannel(registrationMessage);
                        break;
                }
            }

            doubleListBoxControl1.Refresh();
            RefreshBtnReadChannel();
        }

        /// <summary>
        /// Зарегистрировать канал и настроить его опрос
        /// </summary>
        /// <param name="channelRegistrationMessage"></param>
        private void RegisterChannel(ChannelRegistrationMessage channelRegistrationMessage)
        {
            ControlManagementUnit.RegisterChannel(channelRegistrationMessage);
        }

        private void UnRegisterChannel(ChannelRegistrationMessage channelRegistrationMessage)
        {
            ControlManagementUnit.UnregisterChannel(channelRegistrationMessage);
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
            ControlManagementUnit controlManagementUnit = ControlManagementUnit;
            controlManagementUnit.BuildSystemConfiguration();
            controlManagementUnit.GetRegName = GetRegName;
            controlManagementUnit.NeedProtocol += ControlManagementUnit_NeedProtocol;

            List<LogicalChannel> left = new List<LogicalChannel>();
            List<LogicalChannel> right = new List<LogicalChannel>();

            //добавляем только проидентифицированные каналы (Id > 0):
            left.AddRange(controlManagementUnit.GetAvailableLogicalChannels());
            //_right.AddRange(Enumerable.Range(11, 10));

            doubleListBoxControl1.InitDisplayMember("Id");
            doubleListBoxControl1.InitSources(left, right);
            CanRegister = true;
        }

        private void btnChannelRead_Click(object sender, EventArgs e)
        {
            foreach (LogicalChannel logicalChannelId in doubleListBoxControl1.SelectionRight)
            {
                InternalLogicalChannelDataMessage message =
                    new InternalLogicalChannelDataMessage(GetRegName(), null, DataMode.Read, logicalChannelId.Id)
                        {
                            Value = Math.PI
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
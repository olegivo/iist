using NLog;
using Oleg_ivo.LowLevelClient.ServiceReferenceHomeTcp;
using DMS.Common.Events;
using System.ServiceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DMS.Common.Messages;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Tools.UI;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// ������ �������� � ����������
    ///</summary>
    public class ControlManagementUnit
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="ControlManagementUnit" />.
        /// ��� ����, ����� ��������� ������������ �������, ������� ������ ������� <see cref="GetDistributedMeasurementInformationSystem"/> 
        /// � ������� ������ <see cref="BuildSystemConfiguration"/>
        /// </summary>
        public ControlManagementUnit()
        {
            CallbackHandler.NeedProtocol += CallbackHandler_NeedProtocol;
            CallbackHandler.ChannelSubscribed += CallbackHandler_ChannelSubscribed;
            CallbackHandler.ChannelUnSubscribed += CallbackHandler_ChannelUnSubscribed;
            CallbackHandler.HasWriteChannel += CallbackHandler_HasWriteChannel;

            Planner = Planner.Instance;
            Planner.NewDadaReceived += Instance_NewDadaReceived;
        }

        /// <summary>
        /// ������� �����
        /// </summary>
        public event EventHandler<DataEventArgs> HasWriteChannel
        {
            add { CallbackHandler.HasWriteChannel += value; }
            remove { CallbackHandler.HasWriteChannel -= value; }
        }

        void CallbackHandler_HasWriteChannel(object sender, DataEventArgs e)
        {
            string s = string.Format("����� �{0} ������ - {1} [{2}] �������� �������� {3}",
                                     e.Message.LogicalChannelId,
                                     e.Message.RegNameFrom,
                                     e.Message.TimeStamp,
                                     e.Message.Value);
            Protocol(s);
            OnWriteChannel(e.Message);
        }

        private void OnWriteChannel(InternalLogicalChannelDataMessage message)
        {
            //TODO:���� ����� � ������ ����������� �������, ��� ������� ��������� ������ �����, ���������� � �����

        }

        /// <summary>
        /// 
        /// </summary>
        public void BuildSystemConfiguration()
        {
            if (DistributedMeasurementInformationSystem != null)
                DistributedMeasurementInformationSystem.BuildSystemConfiguration();
        }

        /// <summary>
        /// ����������� ������ �������
        /// </summary>
        private Planner Planner { get; set; }

        ///<summary>
        /// ������������� �������������-������������� ������� (�����)
        ///</summary>
        private IDistributedMeasurementInformationSystem DistributedMeasurementInformationSystem
        {
            get
            {
                if (GetDistributedMeasurementInformationSystem == null)
                    throw new NullReferenceException(
                        "�� ������ ������� ��� ��������� ������ ������������� �������������-������������� �������");
                return GetDistributedMeasurementInformationSystem();
            }
        }

        /// <summary>
        /// ������� ��� ��������� ������ ������������� �������������-������������� �������
        /// </summary>
        public GetDistributedMeasurementInformationSystemDelegate GetDistributedMeasurementInformationSystem { private get; set; }

        /// <summary>
        /// ������� ��� ��������� ������ ������������� �������������-������������� �������
        /// </summary>
        /// <returns></returns>
        public delegate IDistributedMeasurementInformationSystem GetDistributedMeasurementInformationSystemDelegate();

        ///<summary>
        /// ���������� ������
        ///</summary>
        protected virtual IEnumerable<LogicalChannel> GetLogicalChannels()
        {
            return DistributedMeasurementInformationSystem.PlcManager.LogicalChannels;
        }


        void Instance_NewDadaReceived(object sender, NewDataReceivedEventArgs e)
        {
            InternalLogicalChannelDataMessage message = new InternalLogicalChannelDataMessage(RegName, null, DataMode.Read,
                                                                                              e.LogicalChannel.Id)
                                                            {
                                                                Value = e.Value
                                                            };

            //���������� ������� ������ ����������� ����������� �� ����� ������:
            ReadChannel(message);
            //                var s = string.Format("{0} �������� � ����� ������ [{1}]", measurementPoll.LogicalChannel, e.SignalTime);
            //                Protocol(s);
            //System.Windows.Forms.MessageBox.Show();
        }

        private LowLevelMessageExchangeSystemClient LowLevelMessageExchangeSystemClient
        {
            get { return Proxy; }
        }

        InstanceContext site;
        LowLevelMessageExchangeSystemClient proxy;

        /// <summary>
        /// 
        /// </summary>
        public LowLevelMessageExchangeSystemClient Proxy
        {
            get
            {
                if (proxy == null)
                {
                    site = new InstanceContext(CallbackHandler);
                    proxy = new LowLevelMessageExchangeSystemClient(site);
                }
                return proxy;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        protected CallbackHandler CallbackHandler
        {
            get { return callbackHandler ?? (callbackHandler = new CallbackHandler()); }
        }

        protected string RegName
        {
            get
            {
                if (GetRegName == null)
                    throw new NullReferenceException("�� ����� ������� GetRegName");

                return GetRegName();
            }
        }

        private CallbackHandler callbackHandler;

        /// <summary>
        /// 
        /// </summary>
        public virtual GetRegNameDelegate GetRegName { protected get; set; }

        void CallbackHandler_ChannelSubscribed(object sender, ChannelSubscribeEventArgs e)
        {
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));

            if (channel == null)
            {
                Log.Warn("�� ������� ����� ����� {0} ��� ������������� ��������. ����� ��������, �� � ��� ������ �� ����� �����������");
                return;
            }

            if (channel.IsInput)
            {
                Protocol(string.Format("{0} ��� �������� �� ��������� ����� ������", channel));
                //��������� ������ ������ ��� ������������������� ������ ����� �������� �� ����
                Planner.StartPoll(channel);
            }
            else if (channel.IsOutput)
            {
                //TODO:�������� ����� � ������ ����������� �������, ��� ������� ��������� ������ �����
                Protocol(string.Format("{0} ��� �������� �� ��������� ����� ������", channel));
            }
            else
            {
                throw new InvalidOperationException(string.Format("{0} ����� ����������� ��� � �� ����� ���� ��������� ��� ��������", channel));
            }
        }

        void CallbackHandler_ChannelUnSubscribed(object sender, ChannelSubscribeEventArgs e)
        {
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));

            if (channel == null)
            {
                Log.Warn("�� ������� ����� ����� {0} ��� ������������� �������. ��� �� �����, ����� �������.");
                return;
            }

            if (channel.IsInput)
            {
                Protocol(string.Format("{0} ��� ������� �� ��������� ����� ������", channel));
                Planner.StopPoll(channel);
            }
            else if (channel.IsOutput)
            {
                //TODO:������� ����� �� ������ ����������� �������, ��� ������� ��������� ������ �����
                Protocol(string.Format("{0} ��� ������� �� ��������� ����� ������", channel));
            }
            else
            {
                throw new InvalidOperationException(string.Format("{0} ����� ����������� ��� � �� ����� ���� ��������� ��� ��������", channel));
            }
        }

        void CallbackHandler_NeedProtocol(object sender, EventArgs e)
        {
            if (sender is double || sender is string)
                Protocol(sender);
        }

        /// <summary>
        /// ��������������� ������
        /// (������������ ������� NeedProtocol)
        /// </summary>
        /// <param name="sender"></param>
        protected void Protocol(object sender)
        {
            if (NeedProtocol != null)
                NeedProtocol(sender, EventArgs.Empty);
        }

        /// <summary>
        /// ��������� ����������������
        /// </summary>
        public event EventHandler<EventArgs> NeedProtocol;

        /// <summary>
        /// ������� � ������� ������ ����������� ���������
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(InternalMessage message)
        {
            LowLevelMessageExchangeSystemClient.SendMessageAsync(message);
        }

        /// <summary>
        /// ������� � ������� ������ ����������� ��������� � ����� ������
        /// </summary>
        /// <param name="message"></param>
        public void ReadChannel(InternalLogicalChannelDataMessage message)
        {
            LowLevelMessageExchangeSystemClient.ReadChannelAsync(message);
        }

        /// <summary>
        /// ������� � ������� ������ ����������� ��������� � �����������
        /// </summary>
        public void Register()
        {
            RegistrationMessage message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
            LowLevelMessageExchangeSystemClient.Register(message);
        }

        /// <summary>
        /// ���������� ������� � ������� ������ ����������� ��������� � �����������
        /// </summary>
        public void RegisterAsync()
        {
            RegistrationMessage message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
            LowLevelMessageExchangeSystemClient.RegisterAsync(message);
        }

        /// <summary>
        /// ������� � ������� ������ ����������� ��������� �� ������ �����������
        /// </summary>
        public void Unregister()
        {
            LowLevelMessageExchangeSystemClient.UnregisterAsync(new RegistrationMessage(GetRegName(), null, RegistrationMode.Unregister, DataMode.Unknown));
        }

        /// <summary>
        /// ������ ����������� ���������
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted
        {
            add { LowLevelMessageExchangeSystemClient.UnregisterCompleted += value; }
            remove { LowLevelMessageExchangeSystemClient.UnregisterCompleted -= value; }
        }

        /// <summary>
        /// �������� ��������� ���������� ������
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LogicalChannel> GetAvailableLogicalChannels()
        {
            //��������� ������ ��������������������� ������ (Id > 0):
            return
                GetLogicalChannels()
                    .Where(channel => channel.Id > 0);
        }


        /// <summary>
        /// �������� ����� ������
        /// </summary>
        /// <param name="e"></param>
        /// <param name="synchronizingObject"></param>
        /// <exception cref="Exception"></exception>
        public void TryAddPoll(MovingEventArgs e, ISynchronizeInvoke synchronizingObject)
        {
            var logicalChannel = e.MovingObject as LogicalChannel;
            if (logicalChannel == null) return;

            var channel =
                GetLogicalChannels()
                    .AsEnumerable()
                    .FirstOrDefault(LogicalChannel.GetFindChannelPredicate(logicalChannel.Id));

            if (channel == null)
                throw new Exception("����� �� ������");

            double interval;
            if (channel.PollPeriod == TimeSpan.Zero)
                channel.PollPeriod = TimeSpan.FromMilliseconds(1000);

            string s = channel.PollPeriod.TotalMilliseconds.ToString();

            /*
                        s = InputBox.Show("������� �������� ������ ������ (� �������������)",
                                          string.Format("�������� ������ ������ [{0}]", channel),
                                          "1000");
                */


            if (double.TryParse(s, out interval))
            {
                Planner.AddPoll(channel, interval, synchronizingObject);
            }
            else
            {
                MessageBox.Show("��������� �������� �� �������� ����������", "������ �������� ��������� ������ ������",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        public void TryRemovePoll(MovingEventArgs e)
        {
            var logicalChannel = e.MovingObject as LogicalChannel;
            if (logicalChannel == null) return;
            var channel =
                GetLogicalChannels()
                    .AsEnumerable()
                    .FirstOrDefault(LogicalChannel.GetFindChannelPredicate(logicalChannel.Id));

            if (channel == null)
                throw new Exception("����� �� ������");

            Planner.RemovePoll(channel);
        }

        /// <summary>
        /// ���������������� ����� � ������� ������ �����������
        /// </summary>
        /// <param name="channelRegistrationMessage"></param>
        public void RegisterChannel(ChannelRegistrationMessage channelRegistrationMessage)
        {
            LowLevelMessageExchangeSystemClient.ChannelRegisterAsync(channelRegistrationMessage);
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(channelRegistrationMessage.LogicalChannelId));

            Protocol(string.Format("{0} ��������������� � ������� ������ �����������", channel));
        }

        /// <summary>
        /// �������� ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="channelRegistrationMessage"></param>
        public void UnregisterChannel(ChannelRegistrationMessage channelRegistrationMessage)
        {
            LowLevelMessageExchangeSystemClient.ChannelUnRegisterAsync(channelRegistrationMessage);
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(channelRegistrationMessage.LogicalChannelId));

            Protocol(string.Format("{0} ������ ����������� � ������� ������ �����������", channel));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public delegate string GetRegNameDelegate();
}
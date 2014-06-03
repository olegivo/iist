using System.ServiceModel.Channels;
using DMS.Common;
using NLog;
using Oleg_ivo.Base.Communication;
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
using Oleg_ivo.Tools.ExceptionCatcher;
using Oleg_ivo.Tools.UI;
using Timer = System.Timers.Timer;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// ������ �������� � ����������
    ///</summary>
    public class ControlManagementUnit : IClientBase
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="ControlManagementUnit" />.
        /// ��� ����, ����� ��������� ������������ �������, ������� ������ ������� <see cref="GetDistributedMeasurementInformationSystem"/> 
        /// � ������� ������ <see cref="BuildSystemConfiguration"/>
        /// </summary>
        public ControlManagementUnit()
        {
            reconnectTimer = new Timer(5000);
            reconnectTimer.Elapsed += reconnectTimer_Elapsed;

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
        public event EventHandler<MessageEventArgs<InternalLogicalChannelDataMessage>> HasWriteChannel
        {
            add { CallbackHandler.HasWriteChannel += value; }
            remove { CallbackHandler.HasWriteChannel -= value; }
        }

        void CallbackHandler_HasWriteChannel(object sender, MessageEventArgs<InternalLogicalChannelDataMessage> e)
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
        public Func<IDistributedMeasurementInformationSystem> GetDistributedMeasurementInformationSystem { private get; set; }

        ///<summary>
        /// ���������� ������
        ///</summary>
        protected virtual IEnumerable<LogicalChannel> GetLogicalChannels()
        {
            return DistributedMeasurementInformationSystem.PlcManager.LogicalChannels;
        }


        void Instance_NewDadaReceived(object sender, NewDataReceivedEventArgs e)
        {
            if (e.LogicalChannel.IsStateChannel)
            {
                //��������� �� ��������� ��������� ������ (�������)
                var value = (bool?)e.Value;
                //TODO: ���� ����� ������������� ������ ���������� ��������� (��������/����������)
                var state = value.HasValue && value.Value ? LogicalChannelState.Work : LogicalChannelState.Break;
                foreach (var logicalChannel in e.LogicalChannel.Entity.LogicalChannelStateHolders)
                {
                    ChangeChannelState(new InternalLogicalChannelStateMessage(RegName, null, logicalChannel.Id, state));
                }
            }
            else
            {
                //���������� ������� ������ ����������� ����������� �� ����� ������:
                var message = new InternalLogicalChannelDataMessage(RegName, null, DataMode.Read, e.LogicalChannel.Id, e.LogicalChannel.IsDiscrete)
                {
                    Value = e.Value
                };
                ReadChannel(message);
            }
            //                var s = string.Format("{0} �������� � ����� ������ [{1}]", measurementPoll.LogicalChannel, e.SignalTime);
            //                Protocol(s);
            //System.Windows.Forms.MessageBox.Show();
        }

        private void ChangeChannelState(InternalLogicalChannelStateMessage internalLogicalChannelStateMessage)
        {
            LowLevelMessageExchangeSystemClient.ChangeChannelStateAsync(internalLogicalChannelStateMessage);
        }

        public LowLevelMessageExchangeSystemClient LowLevelMessageExchangeSystemClient
        {
            get { return proxy; }
        }

        private InstanceContext site;
        private LowLevelMessageExchangeSystemClient proxy;

        private void CreateProxy()
        {
            UnsubscribeProxy();

            site = new InstanceContext(CallbackHandler);
            if (proxy != null) 
                proxy.SafeClose();
            proxy = new LowLevelMessageExchangeSystemClient(site);
            
            SubscribeProxy();
        }

        protected virtual void SubscribeProxy()
        {
            site.Faulted += site_Faulted;
            proxy.UnregisterCompleted += proxy_UnregisterCompleted;
            proxy.SendErrorCompleted += proxy_SendErrorCompleted;
        }

        protected virtual void UnsubscribeProxy()
        {
            if (site != null)
            {
                site.Faulted -= site_Faulted;
            }
            if (proxy != null)
            {
                proxy.UnregisterCompleted -= proxy_UnregisterCompleted;
            }
        }

        void site_Faulted(object sender, EventArgs e)
        {
            var channel = sender as IChannel;
            if (channel != null)
            {
                channel.Abort();
                channel.Close();
            }

            //Disable the keep alive timer now that the channel is faulted
            //_keepAliveTimer.Stop();

            //The proxy channel should no longer be used
            AbortProxy();

            //Enable the try again timer and attempt to reconnect
            reconnectTimer.Start();
        }

        public void AbortProxy()
        {
            if (proxy != null)
            {
                proxy.Abort();
                proxy.Close();
                proxy = null;
            }
        }

        private void reconnectTimer_Elapsed(object sender, EventArgs e)
        {
            if (proxy == null)
                CreateProxy();
            
            if (proxy != null)
            {
                reconnectTimer.Stop();
                //_keepAliveTimer.Start();
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

        /// <summary>
        /// 
        /// </summary>
        public Func<string> GetRegName { get; set; }

        private CallbackHandler callbackHandler;
        private readonly Timer reconnectTimer;

        void CallbackHandler_ChannelSubscribed(object sender, MessageEventArgs<ChannelSubscribeMessage> e)
        {
            var channel =
                GetLogicalChannels().FirstOrDefault(LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));

            if (channel == null)
            {
                Log.Warn("�� ������� ����� ����� {0} ��� ������������� ��������. ����� ��������, �� � ��� ������ �� ����� �����������");
                return;
            }

            if (!channel.IsDiscrete)/*���������� ������ �� ����� ��������� => ������ Work*/
            {
                var stateLogicalChannelId = channel.Entity.StateLogicalChannelId;
                if (stateLogicalChannelId.HasValue)
                {
                    var stateChannel = GetLogicalChannels().FirstOrDefault(LogicalChannel.GetFindChannelPredicate(stateLogicalChannelId.Value));
                    if (stateChannel != null)
                        Planner.StartPoll(stateChannel);
                }
                
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

        void CallbackHandler_ChannelUnSubscribed(object sender, MessageEventArgs<ChannelSubscribeMessage> e)
        {
            var channel =
                GetLogicalChannels().FirstOrDefault(LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));

            if (channel == null)
            {
                Log.Warn("�� ������� ����� ����� {0} ��� ������������� �������. ��� �� �����, ����� �������.");
                return;
            }

            var stateLogicalChannelId = channel.Entity.StateLogicalChannelId;
            if (stateLogicalChannelId.HasValue)
            {
                var stateChannel = GetLogicalChannels().FirstOrDefault(LogicalChannel.GetFindChannelPredicate(stateLogicalChannelId.Value));
                if (stateChannel != null)
                    Planner.StopPoll(stateChannel);
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
            CreateProxy();
            RegistrationMessage message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
            LowLevelMessageExchangeSystemClient.Register(message);
        }

        /// <summary>
        /// ���������� ������� � ������� ������ ����������� ��������� � �����������
        /// </summary>
        public void RegisterAsync()
        {
            CreateProxy();
            RegistrationMessage message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
            LowLevelMessageExchangeSystemClient.RegisterAsync(message);
        }

        /// <summary>
        /// ������� � ������� ������ ����������� ��������� �� ������ �����������
        /// </summary>
        public void Unregister()
        {
            //����� ���, ��� ��������� ��������� �������� ������ ������������� ��� ������� ������ ���������
            Planner.StopAllPolls();
            LowLevelMessageExchangeSystemClient.UnregisterAsync(new RegistrationMessage(GetRegName(), null, RegistrationMode.Unregister, DataMode.Unknown));
        }

        /// <summary>
        /// ������ ����������� ���������
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted;

        private void proxy_UnregisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (UnregisterCompleted != null) 
                UnregisterCompleted(this, e);
        }

        public event EventHandler<AsyncCompletedEventArgs> SendErrorCompleted;

        private void proxy_SendErrorCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (SendErrorCompleted != null)
                SendErrorCompleted(this, e);
        }

        /// <summary>
        /// �������� ��������� ���������� ������
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LogicalChannel> GetAvailableLogicalChannels(bool withStateChannels = false)
        {
            //��������� ������ ��������������������� ������ (Id > 0):
            return
                GetLogicalChannels()
                    .Where(channel => channel.Id > 0 && (!channel.IsStateChannel || withStateChannels));
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
                    .FirstOrDefault(LogicalChannel.GetFindChannelPredicate(logicalChannel.Id));

            if (channel == null)
                throw new Exception("����� �� ������");

            double interval;
            if (channel.PollPeriod == TimeSpan.Zero)
                channel.PollPeriod = TimeSpan.FromMilliseconds(1000);

            string s = (channel.PollPeriod ?? TimeSpan.FromSeconds(5)).TotalMilliseconds.ToString();

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

        public bool IsCommunicationFailed
        {
            get { return LowLevelMessageExchangeSystemClient.State != CommunicationState.Opened; }
        }


        public void SendErrorAsync(ExtendedThreadExceptionEventArgs e)
        {
            LowLevelMessageExchangeSystemClient.SendErrorAsync(
                new InternalErrorMessage(GetRegName(), null, e.Exception), e);
        }

        public void Dispose()
        {
            if (LowLevelMessageExchangeSystemClient != null)
                LowLevelMessageExchangeSystemClient.SafeClose();
        }
    }

}
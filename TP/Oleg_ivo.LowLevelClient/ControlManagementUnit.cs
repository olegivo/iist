using System.ServiceModel.Channels;
using System.Threading.Tasks;
using DMS.Common;
using NLog;
using Oleg_ivo.Base.Autofac;
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

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// ������ �������� � ����������
    ///</summary>
    public class ControlManagementUnit : IClientBase
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly Planner planner;
        private readonly IDistributedMeasurementInformationSystem distributedMeasurementInformationSystem;

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="ControlManagementUnit" />.
        /// </summary>
        /// <param name="planner"></param>
        /// <param name="distributedMeasurementInformationSystem"></param>
        public ControlManagementUnit(Planner planner, IDistributedMeasurementInformationSystem distributedMeasurementInformationSystem)
        {
            this.distributedMeasurementInformationSystem =
                Enforce.ArgumentNotNull(distributedMeasurementInformationSystem,
                    "distributedMeasurementInformationSystem");
            this.planner = Enforce.ArgumentNotNull(planner, "planner");
            this.planner.NewDadaReceived += planner_NewDadaReceived;

            callbackHandler = new CallbackHandler();
            callbackHandler.NeedProtocol += callbackHandler_NeedProtocol;
            callbackHandler.ChannelSubscribed += callbackHandler_ChannelSubscribed;
            callbackHandler.ChannelUnSubscribed += callbackHandler_ChannelUnSubscribed;
            callbackHandler.HasWriteChannel += callbackHandler_HasWriteChannel;

            reliableConnector = new ReliableConnector(this);
        }

        /// <summary>
        /// ������� �����
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelDataMessage>> HasWriteChannel
        {
            add { callbackHandler.HasWriteChannel += value; }
            remove { callbackHandler.HasWriteChannel -= value; }
        }

        void callbackHandler_HasWriteChannel(object sender, MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            var s = string.Format("����� �{0} ������ - {1} [{2}] �������� �������� {3}",
                                     e.Message.LogicalChannelId,
                                     e.Message.RegNameFrom,
                                     e.Message.TimeStamp,
                                     e.Message.Value);
            Log.Debug(s);
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
            if (distributedMeasurementInformationSystem != null)
                distributedMeasurementInformationSystem.BuildSystemConfiguration();
        }

        ///<summary>
        /// ���������� ������
        ///</summary>
        protected virtual IEnumerable<LogicalChannel> GetLogicalChannels()
        {
            return distributedMeasurementInformationSystem.PlcManager.LogicalChannels;
        }

        void planner_NewDadaReceived(object sender, NewDataReceivedEventArgs e)
        {
            if (IsCommunicationFailed)
            {
                Log.Error("������������ � �������� ��������. ��������� ������� ���� ������� �� �������������� �����.");
                planner.StopAllPolls();
                //TODO:��������, �� ������������� ����� �������, �� ������������ ���������� ������ �� �������������� �����, �������� �� �����? ����� ����� �������
                return;
            }
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

        public ICommunicationObject Proxy { get { return proxy; } }

        private void CreateProxy()
        {
            UnsubscribeProxy();

            Log.Debug("�������� ������ �����");
            site = new InstanceContext(callbackHandler);
            if (proxy != null) 
                proxy.SafeClose();
            proxy = new LowLevelMessageExchangeSystemClient(site);

            reliableConnector.SetProxy(proxy);

            SubscribeProxy();
        }

        protected virtual void SubscribeProxy()
        {
            site.Faulted += site_Faulted;

            //proxy.InnerChannel.Faulted += reliableConnector.ProxyFaulted;

            proxy.RegisterCompleted += proxy_RegisterCompleted;
            proxy.UnregisterCompleted += proxy_UnregisterCompleted;
            proxy.SendErrorCompleted += proxy_SendErrorCompleted;
            proxy.ChannelRegisterCompleted += proxy_ChannelRegisterCompleted;
            proxy.ChannelUnRegisterCompleted += proxy_ChannelUnRegisterCompleted;
        }

        protected virtual void UnsubscribeProxy()
        {
            if (site != null)
            {
                site.Faulted -= site_Faulted;
            }
            if (proxy != null)
            {
                proxy.RegisterCompleted -= proxy_RegisterCompleted;
                proxy.UnregisterCompleted -= proxy_UnregisterCompleted;
                proxy.SendErrorCompleted -= proxy_SendErrorCompleted;
                proxy.ChannelRegisterCompleted -= proxy_ChannelRegisterCompleted;
                proxy.ChannelRegisterCompleted -= proxy_ChannelUnRegisterCompleted;
            }
        }

        void site_Faulted(object sender, EventArgs e)
        {
            Log.Error("Site faulted");//TODO:���� ��������� �� �������!
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
            //reconnectTimer.Start();
        }

        public void AbortProxy()
        {
            if (proxy != null)
            {
                Log.Debug("����������� ������ �����");
                proxy.Abort();
                proxy.Close();
                proxy = null;
            }
        }

        public bool IsRegistered
        {
            get { return isRegistered; }
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

        private readonly CallbackHandler callbackHandler;
        private readonly ReliableConnector reliableConnector;

        void callbackHandler_ChannelSubscribed(object sender, MessageEventArgs<ChannelSubscribeMessage> e)
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
                        planner.StartPoll(stateChannel);
                }
                
            }
            if (channel.IsInput)
            {
                Log.Info("{0} ��� �������� �� ��������� ����� ������", channel);
                Protocol(string.Format("{0} ��� �������� �� ��������� ����� ������", channel));
                //��������� ������ ������ ��� ������������������� ������ ����� �������� �� ����
                planner.StartPoll(channel);
            }
            else if (channel.IsOutput)
            {
                Log.Info("{0} ��� �������� �� ��������� ����� ������", channel);
                //TODO:�������� ����� � ������ ����������� �������, ��� ������� ��������� ������ �����
                Protocol(string.Format("{0} ��� �������� �� ��������� ����� ������", channel));
            }
            else
            {
                throw new InvalidOperationException(string.Format("{0} ����� ����������� ��� � �� ����� ���� ��������� ��� ��������", channel));
            }
        }

        void callbackHandler_ChannelUnSubscribed(object sender, MessageEventArgs<ChannelSubscribeMessage> e)
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
                    planner.StopPoll(stateChannel);
            }

            if (channel.IsInput)
            {
                Log.Info("{0} ��� ������� �� ��������� ����� ������", channel);
                Protocol(string.Format("{0} ��� ������� �� ��������� ����� ������", channel));
                planner.StopPoll(channel);
            }
            else if (channel.IsOutput)
            {
                Log.Info("{0} ��� ������� �� ��������� ����� ������", channel);
                //TODO:������� ����� �� ������ ����������� �������, ��� ������� ��������� ������ �����
                Protocol(string.Format("{0} ��� ������� �� ��������� ����� ������", channel));
            }
            else
            {
                throw new InvalidOperationException(string.Format("{0} ����� ����������� ��� � �� ����� ���� ��������� ��� ��������", channel));
            }
        }

        void callbackHandler_NeedProtocol(object sender, EventArgs e)
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
            lock (registerLock)
            {
                Log.Trace("������ ���������� �����������");
                CreateProxy();
                var message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
                LowLevelMessageExchangeSystemClient.Register(message);
                isRegistered = true;
                if(RegisterCompleted!=null)
                    RegisterCompleted(this, new AsyncCompletedEventArgs(null, false, null));
            } 
        }

        /// <summary>
        /// ���������� ������� � ������� ������ ����������� ��������� � �����������
        /// </summary>
        public void RegisterAsync()
        {
            Log.Trace("������ ����������� �����������");
            CreateProxy();
            var message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
            LowLevelMessageExchangeSystemClient.RegisterAsync(message);
        }

        /// <summary>
        /// ������� � ������� ������ ����������� ��������� �� ������ �����������
        /// </summary>
        public void Unregister()
        {
            //����� ���, ��� ��������� ��������� �������� ������ ������������� ��� ������� ������ ���������
            planner.StopAllPolls();
            //LowLevelMessageExchangeSystemClient.Unregister(new RegistrationMessage(GetRegName(), null, RegistrationMode.Unregister, DataMode.Unknown));
            LowLevelMessageExchangeSystemClient.UnregisterAsync(new RegistrationMessage(GetRegName(), null, RegistrationMode.Unregister, DataMode.Unknown));
        }

        /// <summary>
        /// ������ ����������� ���������
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted;

        private void proxy_UnregisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            lock (registerLock) isRegistered = e.Error != null;
            if (isRegistered)
                Log.Info("������ ����������� �� ������� ����������� �������");
            else
                Log.Error("������ ����������� �� ������� ����������� ��������:\n{0}", e.Error);
            
            if (UnregisterCompleted != null) 
                UnregisterCompleted(this, e);

            Task.Factory.StartNew(() => LowLevelMessageExchangeSystemClient.Disconnect(GetRegName()))
                .ContinueWith(task => Log.Info("Disconnected"));
        }

        /// <summary>
        /// ����������� ���������
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> RegisterCompleted;

        private void proxy_RegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                lock (registerLock) isRegistered = e.Error == null;
                
                if (isRegistered)
                    Log.Info("����������� �� ������� ����������� �������");
                else
                {
                    Log.Error("����������� �� ������� ����������� ��������:\n{0}", e.Error);
                    throw e.Error;
                }
            }
            finally
            {
                if (RegisterCompleted != null)
                    RegisterCompleted(this, e);
            }
        }

        public event EventHandler<AsyncCompletedEventArgs> SendErrorCompleted;

        private void proxy_SendErrorCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (SendErrorCompleted != null)
                SendErrorCompleted(this, e);
        }

        public event EventHandler<AsyncCompletedEventArgs> ChannelRegisterCompleted;

        void proxy_ChannelRegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (ChannelRegisterCompleted != null)
                ChannelRegisterCompleted(this, e);
        }

        public event EventHandler<AsyncCompletedEventArgs> ChannelUnRegisterCompleted;

        void proxy_ChannelUnRegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (ChannelUnRegisterCompleted != null)
                ChannelUnRegisterCompleted(this, e);
        }

        /// <summary>
        /// �������� ��������� ���������� ������
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LogicalChannel> GetAvailableLogicalChannels(bool withStateChannels = false)
        {
            //��������� ������ ��������������������� ������ (Id > 0):
            var channels = GetLogicalChannels()
                .Where(channel => channel.Id > 0 && (!channel.IsStateChannel || withStateChannels)).ToList();
            return
                channels;
        }


        /// <summary>
        /// �������� ����� ������
        /// </summary>
        /// <param name="logicalChannel"></param>
        /// <exception cref="Exception"></exception>
        public bool TryAddPoll(LogicalChannel logicalChannel)
        {
            if (logicalChannel == null) return false;

            var channel =
                GetLogicalChannels()
                    .FirstOrDefault(LogicalChannel.GetFindChannelPredicate(logicalChannel.Id));

            if (channel == null)
                throw new Exception("����� �� ������");

            if (channel.PollPeriod == TimeSpan.Zero)
                channel.PollPeriod = TimeSpan.FromMilliseconds(1000);
            
            planner.AddPoll(channel);
            return true;
        }

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        /// <param name="logicalChannel"></param>
        /// <exception cref="Exception"></exception>
        public bool TryRemovePoll(LogicalChannel logicalChannel)
        {
            if (logicalChannel == null) return false;

            var channel =
                GetLogicalChannels()
                    .AsEnumerable()
                    .FirstOrDefault(LogicalChannel.GetFindChannelPredicate(logicalChannel.Id));

            if (channel == null)
                throw new Exception("����� �� ������");

            planner.RemovePoll(channel);
            return true;
        }

        /// <summary>
        /// ���������������� ����� � ������� ������ �����������
        /// </summary>
        /// <param name="channelRegistrationMessage"></param>
        public void RegisterChannel(ChannelRegistrationMessage channelRegistrationMessage)
        {
            LowLevelMessageExchangeSystemClient.ChannelRegisterAsync(channelRegistrationMessage, channelRegistrationMessage);
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(channelRegistrationMessage.LogicalChannelId));

            Log.Info("{0} ��������������� � ������� ������ �����������", channel);
            Protocol(string.Format("{0} ��������������� � ������� ������ �����������", channel));
        }

        /// <summary>
        /// �������� ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="channelRegistrationMessage"></param>
        public void UnregisterChannel(ChannelRegistrationMessage channelRegistrationMessage)
        {
            LowLevelMessageExchangeSystemClient.ChannelUnRegisterAsync(channelRegistrationMessage, channelRegistrationMessage);
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(channelRegistrationMessage.LogicalChannelId));

            Log.Info("{0} ������ ����������� � ������� ������ �����������", channel);
            Protocol(string.Format("{0} ������ ����������� � ������� ������ �����������", channel));
        }

        private readonly object registerLock = new object();

        public bool IsCommunicationFailed
        {
            get
            {
                lock (registerLock)
                {
                    Log.Trace("�������� ������������");
                    if (LowLevelMessageExchangeSystemClient == null)
                    {
                        Log.Warn("������ �� ������");
                        return true;
                    }
                    if (LowLevelMessageExchangeSystemClient.State == CommunicationState.Faulted)
                    {
                        Log.Warn("����� ����� �������");
                        return true;
                    }
                    return false;
                }
            }
        }

        private bool isRegistered;

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
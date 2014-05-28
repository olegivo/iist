using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Autofac;
using DMS.Common.MessageExchangeSystem.LowLevel;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.MES.High;
using Oleg_ivo.MES.Registered;

namespace Oleg_ivo.MES.Low
{
    ///<summary>
    /// ������� ������ ����������� c ��������� �������� ������
    ///</summary>
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Reentrant,
        AutomaticSessionShutdown = false,
        IncludeExceptionDetailInFaults = true)]
    public class LowLevelMessageExchangeSystem : AbstractLevelMessageExchangeSystem<RegisteredLowLevelClient>, ILowLevelMessageExchangeSystem
    {
        private readonly IComponentContext context;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        
        #region Constructors

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="LowLevelMessageExchangeSystem" />.
        /// </summary>
        public LowLevelMessageExchangeSystem(IComponentContext context)
        {
            this.context = Enforce.ArgumentNotNull(context,"context");
        }

        private bool subscribed;
        internal void NotifySubscribeEvents(HighLevelMessageExchangeSystem highLevelMessageExchangeSystem)
        {
            if (!subscribed)
            {
                highLevelMessageExchangeSystem.ChannelSubscribed += High_ChannelSubscribed;
                highLevelMessageExchangeSystem.ChannelUnSubscribed += High_ChannelUnSubscribed;
                subscribed = true;
            }
        }

        private void High_ChannelUnSubscribed(object sender, HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            var channel =
                GetRegisteredLogicalChannel(
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(e.Message.LogicalChannelId,
                                                                     DataMode.Unknown));

            if (channel == null)
                throw new Exception("���������� ���������� �� ������, ������� �� ���������������");

            channel.InvokeUnSubscribed(e.Message);
        }

        private void High_ChannelSubscribed(object sender, HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            var channel =
                GetRegisteredLogicalChannel(
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(e.Message.LogicalChannelId,
                                                                     DataMode.Unknown));
            if (channel == null)
                throw new Exception("���������� ����������� �� �����, ������� �� ���������������");

            channel.InvokeSubscribed(e.Message);
        }

        #endregion

        #region Implementation of IMessageReceiver

        /// <summary>
        /// ������� ������� �������� ��������� ���������
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(InternalMessage message)
        {
            string s = string.Format("LowLevelClient -> MessageExchangeSystem : {0}{1}", message.TimeStamp, Environment.NewLine);
            InvokeMessageReceived(s);
        }

        #endregion

        #region Implementation of IMessageExchangeSystem

        /// <summary>
        /// ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        private void Register(RegistrationMessage message, ILowLevelClientCallback clientCallback)
        {
            if (message.RegistrationMode != RegistrationMode.Register)
                throw new ArgumentException("��� ����������� ������� � ��������� ������������ ���� ������ �����������");

            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);

            if (registeredLowLevelClient != null)
                throw new Exception("������ ��� ���������������");

            registeredLowLevelClient = context.ResolveUnregistered<RegisteredLowLevelClient>();
            registeredLowLevelClient.Ticker = message.RegNameFrom;
            AddClient(message.RegNameFrom, registeredLowLevelClient);

            //                registeredLowLevelClient = (RegisteredLowLevelClient)_registeredClients[message.RegNameFrom];

            //Thread t = new Thread(w.RegisteredLowLevelClientProcess.SendUpdateToClient) { IsBackground = true };
            //t.Start();

            //�������� ������� ������ �� ������� ������ � ��������
            //������ ������� � ������ �������� �������

            registeredLowLevelClient.AddCallback(clientCallback);

            OnRegistered(registeredLowLevelClient, message);
        }

        /// <summary>
        /// ������ ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        private void Unregister(RegistrationMessage message, ILowLevelClientCallback clientCallback)
        {
            if (message.RegistrationMode != RegistrationMode.Unregister) 
                throw new ArgumentException("��� ������ ����������� ������� � ��������� ������������ ���� �����������");

            //�������� ������� ������ �� ������� ������ � �������
            //������ ������� �� ������ �������� �������
            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);

            if (registeredLowLevelClient != null)
            {
                registeredLowLevelClient.RemoveCallback(clientCallback);//������ ������� �����������

                OnUnRegistered(registeredLowLevelClient, message);//���������� � ���, ��� ������ ������� �����������

                //                if (registeredLowLevelClient.HasCallbacks)
                RemoveClient(message.RegNameFrom);

            }
        }

        #endregion

        #region Implementation of ILowLevelMessageExchangeSystem

        [Dependency(Required = true)]
        public Func<HighLevelMessageExchangeSystem> HighLevelMessageExchangeSystemProvider { get; set; }

        public HighLevelMessageExchangeSystem HighLevelMessageExchangeSystem
        {
            get { return HighLevelMessageExchangeSystemProvider.Invoke(); }
        }

        /// <summary>
        /// ������ ������ �� ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        private void ReadChannel(InternalLogicalChannelDataMessage message)
        {
            if (message.DataMode == DataMode.Read)
            {
                //������ ����� ������ �� ������. ����� ���������� ������
                HighLevelMessageExchangeSystem.ReadChannel(message);
            }
            else
            {
                log.Warn(
                    "������ [{0}] �������� � ������ ����� ������ �� ������ [{1}]. "
                    + "�� � ��������� ������ ����� ������, �������� �� ������",
                    message.LogicalChannelId, message.RegNameFrom);

            }
        }

        /// <summary>
        /// ��������� ��������� ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        private void ChangeChannelState(InternalLogicalChannelStateMessage message)
        {
            log.Info("��������� ������ {0} ����������: {1}", message.LogicalChannelId, message.State);
            //������ ����� ������ �� ������. ����� ���������� ������
            HighLevelMessageExchangeSystem.ChangeChannelState(message);
        }

        /// <summary>
        /// ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelRegister(ChannelRegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Debug("������ ����������� ������ {0} ({1})", message.LogicalChannelId, message.DataMode.ToString());
            var caller = new Action<ChannelRegistrationMessage>(ChannelRegister);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// ���������� ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelRegister(ChannelRegistrationMessage message, IAsyncResult result)
        {
            log.Info("����� ��� ���������������");
        }

        /// <summary>
        /// ������ ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelUnRegister(ChannelRegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Debug("������ ������ ����������� ������ {0}", message.LogicalChannelId);
            var caller = new Action<ChannelRegistrationMessage>(ChannelUnRegister);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// ���������� ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelUnRegister(ChannelRegistrationMessage message, IAsyncResult result)
        {
            log.Info("����������� ������ ���� ��������");
        }

        /// <summary>
        /// ������ ������ ������ �� ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginReadChannel(InternalLogicalChannelDataMessage message, AsyncCallback callback, object state)
        {
            log.Debug("������ ������ ������ {0}", message.LogicalChannelId);
            var caller = new Action<InternalLogicalChannelDataMessage>(ReadChannel);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// ���������� ������ ������ �� ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndReadChannel(InternalLogicalChannelDataMessage message, IAsyncResult result)
        {
            log.Info("����� ��� ��������");
        }

        public IAsyncResult BeginChangeChannelState(InternalLogicalChannelStateMessage message, AsyncCallback callback, object state)
        {
            log.Debug("������ ������ ������ {0}", message.LogicalChannelId);
            var caller = new Action<InternalLogicalChannelStateMessage>(ChangeChannelState);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        public void EndChangeChannelState(InternalLogicalChannelStateMessage message, IAsyncResult result)
        {
            log.Info("����� ��� ��������");
        }


        #endregion


        private void OnRegistered(RegisteredLowLevelClient registeredLowLevelClient, InternalMessage message)
        {
            InvokeClientRegistered(new LowLevelClientEventArgs(registeredLowLevelClient, message));
        }

        private void OnUnRegistered(RegisteredLowLevelClient registeredLowLevelClient, InternalMessage message)
        {
            InvokeClientUnregistered(new LowLevelClientEventArgs(registeredLowLevelClient, message));
        }

        private void InvokeClientUnregistered(LowLevelClientEventArgs e)
        {
            var handler = ClientUnregistered;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<LowLevelClientEventArgs> ClientUnregistered;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void InvokeClientRegistered(LowLevelClientEventArgs e)
        {
            var handler = ClientRegistered;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<LowLevelClientEventArgs> ClientRegistered;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler MessageReceived;

        private void InvokeMessageReceived(object e)
        {
            EventHandler handler = MessageReceived;
            if (handler != null) handler(e, EventArgs.Empty);
        }


        /// <summary>
        /// �������� ������������������ ���������� �����
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public RegisteredLogicalChannelExtended GetRegisteredLogicalChannel(Func<RegisteredLogicalChannelExtended, bool> predicate)
        {
            //���� � ������ ������
            RegisteredLogicalChannelExtended channel = FindSubscribedChannel(predicate);
            return channel;
        }

        /// <summary>
        /// ����� ����� � ������� ������ (���� �� ���� ��� ���-�� ��������)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private RegisteredLogicalChannelExtended FindSubscribedChannel(Func<RegisteredLogicalChannelExtended, bool> predicate)
        {
            //HACK: ������-�� � ��������� ������� ��� �������� Instance. � _registeredClients ��� �� ������ ��������  !!!
            var registeredLogicalChannel =
                RegisteredClients
                    .SelectMany(client => client.RegisteredLogicalChannels.Values)//�������� ��� ������ ���� ��������
                    .Where(predicate).FirstOrDefault();//��� Id - ��������
            return registeredLogicalChannel;
        }

        /// <summary>
        /// �������� ������������������� ������� �� �����.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="withRegisteredChannels">������ � ������������������� ��������. 
        /// ����� �������������, ����� ��������� ����� �������-��������� ������</param>
        /// <param name="withCallbacks"></param>
        /// <returns></returns>
        private RegisteredLowLevelClient GetRegisteredLowLevelClient(InternalMessage message, bool withRegisteredChannels, bool withCallbacks)
        {
            RegisteredLowLevelClient registeredLowLevelClient = this[message.RegNameFrom];
            if (registeredLowLevelClient != null)
            {
                if ((withRegisteredChannels && !registeredLowLevelClient.HasRegisteredLogicalChannels)
                        ||
                        (withCallbacks && !registeredLowLevelClient.HasCallbacks))
                    registeredLowLevelClient = null;
            }

            return registeredLowLevelClient;
        }

        /// <summary>
        /// ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        public void ChannelRegister(ChannelRegistrationMessage message)
        {
            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);
            if (registeredLowLevelClient == null)
                throw new Exception("������ ������ �� ���������������");

            RegisteredLogicalChannelExtended channel =
                registeredLowLevelClient.GetRegisteredLogicalChannel(
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId, DataMode.Unknown));
            if (channel != null)
                throw new Exception("������ ����� ��� ���������������");

            registeredLowLevelClient.ChannelRegister(message);
            InvokeChannelRegistered(new LowRegisteredLogicalChannelSubscribeEventArgs(registeredLowLevelClient, message));
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<LowRegisteredLogicalChannelSubscribeEventArgs> ChannelRegistered;

        private void InvokeChannelRegistered(LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            EventHandler<LowRegisteredLogicalChannelSubscribeEventArgs> handler = ChannelRegistered;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// ������ ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="message"></param>
        public void ChannelUnRegister(ChannelRegistrationMessage message)
        {
            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);
            if (registeredLowLevelClient == null)
                throw new Exception("������ ������ �� ���������������");

            RegisteredLogicalChannelExtended channel =
                registeredLowLevelClient.GetRegisteredLogicalChannel(
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId, DataMode.Unknown));
            if (channel == null)
                throw new Exception("������ ����� �� ���������������");

            registeredLowLevelClient.ChannelUnRegister(message);
            InvokeChannelUnregistered(new LowRegisteredLogicalChannelSubscribeEventArgs(registeredLowLevelClient, message));
        }
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<LowRegisteredLogicalChannelSubscribeEventArgs> ChannelUnregistered;

        private void InvokeChannelUnregistered(LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            EventHandler<LowRegisteredLogicalChannelSubscribeEventArgs> handler = ChannelUnregistered;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// �������� ��� ������������������ � ������ ������� ������
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RegisteredLogicalChannelExtended> GetAllRegisteredChannels()
        {
            return RegisteredClients.SelectMany(client => client.RegisteredLogicalChannels.Values);
        }

        /// <summary>
        /// �������������� ��� ������� ������ ��������� ������� ������
        /// </summary>
        public override string RegName
        {
            get { return "MESLowLevel"; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientRegName"></param>
        protected override void RemoveClient(string clientRegName)
        {
            //������� ������������������ ������
            var registeredHighLevelClient = this[clientRegName];

            foreach (int registeredLogicalChannelId in registeredHighLevelClient.RegisteredLogicalChannels.Keys.ToArray())
            {
                //��������� ������� ��������� �� ������� � ���, ��� �� �������� ����������� ������ 
                //(����� ���������� �� ����� ����� �� ���� ����������)
                var registrationMessage = new ChannelRegistrationMessage(clientRegName, RegName, 
                                                                         RegistrationMode.Unregister,
                                                                         DataMode.Unknown,
                                                                         registeredLogicalChannelId);
                ChannelUnRegister(registrationMessage);
            }

            base.RemoveClient(clientRegName);
        }

        #region Implementation of IMessageExchangeSystem

        #region Implementation of IMessageExchangeSystem

        /// <summary>
        /// ������ ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginRegister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Debug("������ ����������� ������� {0}", message.RegNameFrom);

            ILowLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<ILowLevelClientCallback>();

            var caller = new RegistrationCaller(Register);
            IAsyncResult result = caller.BeginInvoke(message, clientCallback, callback, state);
            return result;
        }

        /// <summary>
        /// ���������� ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndRegister(RegistrationMessage message, IAsyncResult result)
        {
            log.Info("������ ��� ���������������");
        }

        /// <summary>
        /// ������ ������ ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginUnregister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Debug("������ ������ ����������� ������� {0}", message.RegNameFrom);

            ILowLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<ILowLevelClientCallback>();

            var caller = new RegistrationCaller(Unregister);
            IAsyncResult result = caller.BeginInvoke(message, clientCallback, callback, state);
            return result;
        }

        /// <summary>
        /// ���������� ������ ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndUnregister(RegistrationMessage message, IAsyncResult result)
        {
            log.Info("����������� ������� ���� ��������");
        }

        #endregion

        #endregion

        private delegate void RegistrationCaller(RegistrationMessage message, ILowLevelClientCallback clientCallback);

        /// <summary>
        /// �������� �����
        /// </summary>
        /// <param name="message"></param>
        public void WriteChannel(InternalLogicalChannelDataMessage message)
        {
            RegisteredLogicalChannelExtended subscribedChannel =
                FindSubscribedChannel(RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId,
                                                                                       DataMode.Write));

            if (subscribedChannel != null)
            {
                if ((subscribedChannel.DataMode & DataMode.Write) != DataMode.Unknown)
                    subscribedChannel.InvokeWrite(message);
                else
                    log.Warn(
                        "������ [{0}] �������� � ������ ����� ������ � ����� [{1}]. "
                            + "�� �� �� ����� ���� ����������� � ������ ������. "
                            + "��������� ��������� ������ ������ ��� ������",
                        message.LogicalChannelId, message.RegNameFrom);
            }
            else
            {
                log.Warn(
                    "������ [{0}] �������� � ������ ����� ������ � ����� [{1}]. "
                        +"���������� ����� ����� � ���� �������, ��������������� ������ ������. "
                        +"��������, ����� �� ��������������� ��� ����������� ��������������� ��� ����� ������.", 
                    message.RegNameFrom,
                    message.LogicalChannelId);
            }
        }
    }
}
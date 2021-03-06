using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using Autofac;
using DMS.Common.MessageExchangeSystem.HighLevel;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.MES.Low;
using Oleg_ivo.MES.Registered;

using System.Linq;
using Oleg_ivo.MES.Services;

namespace Oleg_ivo.MES.High
{
    ///<summary>
    /// ������� ������ ����������� c ��������� �������� ������
    ///</summary>
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Reentrant,
        AutomaticSessionShutdown = false,
        IncludeExceptionDetailInFaults = true)]
    [KnownType(typeof(RegisteredLogicalChannel))]
    public class HighLevelMessageExchangeSystem : AbstractLevelMessageExchangeSystem<RegisteredHighLevelClient, IHighLevelClientCallback>, IHighLevelMessageExchangeSystem
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// ���� ������� ��� � ������ ���������������� ��������, �� �� �� ���� �� ���������� ���������� � ������������������ �������.
        /// </summary>
        private readonly List<RegisteredHighLevelClient> InterestedRegisteredClients = new List<RegisteredHighLevelClient>();

        #region Constructors

        #endregion

        public HighLevelMessageExchangeSystem(IComponentContext context, InternalMessageLogger internalLogger) : base(context, internalLogger)
        {
        }

        /// <summary>
        /// �������������� ��� ������� ������ ��������� �������� ������
        /// </summary>
        public override string RegName
        {
            get { return "MESHighLevel"; }
        }

        private void Low_ChannelRegistered(object sender, LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            foreach (RegisteredHighLevelClient registeredHighLevelClient in InterestedRegisteredClients.Where(client => client.Interested))
                registeredHighLevelClient.ChannelRegister(e.Message);
        }

        private void Low_ChannelUnregistered(object sender, LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            foreach (RegisteredHighLevelClient registeredHighLevelClient in InterestedRegisteredClients.Where(client => client.Interested))
                registeredHighLevelClient.ChannelUnRegister(e.Message);
        }

        #region ��������������� ������ � �������� ��� �����������

        /*
        internal void OnUpdate(double d)
        {
            EventHandler handler = BeforeUpdate;
            if (handler != null) handler(d, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler BeforeUpdate;
*/

        #endregion

        [Dependency(Required = true)]
        public Func<LowLevelMessageExchangeSystem> LowLevelMessageExchangeSystemProvider { get; set; }

        public LowLevelMessageExchangeSystem LowLevelMessageExchangeSystem
        {
            get { return LowLevelMessageExchangeSystemProvider.Invoke(); }
        }

        /// <summary>
        /// �������� ������������������ � ������� ������
        /// </summary>
        /// <param name="message">��������� � �������������� ��������������</param>
        /// <returns></returns>
        public RegisteredLogicalChannel[] GetRegisteredChannels(InternalMessage message)
        {
            bool success = true;
            var registeredHighLevelClient = GetRegisteredHighLevelClient(message);
            if (registeredHighLevelClient == null)
                throw new Exception(
                    string.Format(
                        "��� ������� �������� ������������������ � ������� ������ ��������� ������: ������ � ��������������� ������ [{0}] �� ������",
                        message.RegNameFrom));

            if (!InterestedRegisteredClients.Contains(registeredHighLevelClient))
                InterestedRegisteredClients.Add(registeredHighLevelClient);//��������� � ���������������� ��������

            try
            {
                var channels =
                    LowLevelMessageExchangeSystem
                        .GetAllRegisteredChannels()
                        .Cast<RegisteredLogicalChannel>()
                        .ToArray();
                return channels;
            }
            catch (Exception ex)
            {
                success = false;
                throw;
            }
            finally
            {
                if (success)//���� �� ����� ���������� ������ ������ �� ���������, �� ����� �������� ������ ��������� ������� � ���������� ����
                {
                    registeredHighLevelClient.Interested = true;//������ ���� ������������������
                }
            }
        }

        /// <summary>
        /// ������ �������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state)
        {
            log.Trace("������ �������� �� ������ ������ {0}", message.LogicalChannelId);

            var caller = new Action<ChannelSubscribeMessage>(ChannelSubscribe);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// ���������� �������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelSubscribe(ChannelSubscribeMessage message, IAsyncResult result)
        {
            log.Info("������ ��� �������� �� �����");
        }

        /// <summary>
        /// ������ ������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelUnSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state)
        {
            log.Trace("������ ������� �� ������ ������ {0}", message.LogicalChannelId);

            var caller = new Action<ChannelSubscribeMessage>(ChannelUnSubscribe);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// ���������� ������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelUnSubscribe(ChannelSubscribeMessage message, IAsyncResult result)
        {
            log.Info("������ ��� ������� �� ������");
        }

        private void OnRegistered(RegisteredHighLevelClient callback, InternalMessage message)
        {
            InvokeClientRegistered(new HighLevelClientEventArgs(callback, message));
        }

        private void OnUnregistered(RegisteredHighLevelClient callback, InternalMessage message)
        {
            InvokeClientUnregistered(new HighLevelClientEventArgs(callback, message));
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<HighLevelClientEventArgs> ClientRegistered;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<HighLevelClientEventArgs> ClientUnregistered;

        private void InvokeClientUnregistered(HighLevelClientEventArgs e)
        {
            var handler = ClientUnregistered;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void InvokeClientRegistered(HighLevelClientEventArgs e)
        {
            var handler = ClientRegistered;
            if (handler != null) handler(this, e);
        }

        #region Implementation of HighLevelIMessageExchangeSystem

        /// <summary>
        /// �������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        private void ChannelSubscribe(ChannelSubscribeMessage message)
        {
            MessageLogger.ProtocolMessage(message);

            if (message.Mode != SubscribeMode.Subscribe)
                throw new ArgumentException("��� �������� �� ����� � ��������� ������������ ���� �������");

            var registeredHighLevelClient = GetRegisteredHighLevelClient(message);
            if (registeredHighLevelClient != null)
                registeredHighLevelClient.ChannelSubscribe(message);

            InvokeChannelSubscribed(new HighRegisteredLogicalChannelSubscribeEventArgs(registeredHighLevelClient, message));
            if (registeredHighLevelClient != null)
            {
                // ������ ����� �� ������, � ����� ������ ��������� �����, 
                // ������� ����� ���������� ��� � ������� ��������� ������
                var predicate =
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(
                        message.LogicalChannelId,
                        DataMode.Unknown);
                var registeredChannel = GetRegisteredChannel(predicate);//���� ����� 
                var channelStateMessage = new InternalLogicalChannelStateMessage(message.RegNameFrom,
                    message.RegNameTo,
                    message.LogicalChannelId,
                    registeredChannel.State);
                registeredHighLevelClient.SendChannelStateToClient(channelStateMessage);
            }
        }

        /// <summary>
        /// ������� �� ������ ��������������� ������ (������� ��������� �� ������� � ���, ��� �� ������������ �� ������)
        /// </summary>
        /// <param name="message"></param>
        private void ChannelUnSubscribe(ChannelSubscribeMessage message)
        {
            MessageLogger.ProtocolMessage(message);

            if (message.Mode != SubscribeMode.Unsubscribe)
                throw new ArgumentException("��� ������� �� ������ � ��������� ������������ ���� ��������");

            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message);
            if (registeredHighLevelClient != null)
                registeredHighLevelClient.ChannelUnSubscribe(message);

            InvokeChannelUnSubscribed(new HighRegisteredLogicalChannelSubscribeEventArgs(registeredHighLevelClient, message));
        }

        /// <summary>
        /// ����������� ������ � ����������� �����
        /// </summary>
        /// <param name="message"></param>
        public void IsAllowWriteChannel(IInternalMessage message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ������ ������ ������ � ����������� �����
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginWriteChannel(InternalLogicalChannelDataMessage message, AsyncCallback callback, object state)
        {
            log.Trace("������ ������ ������ {0}", message.LogicalChannelId);
            var caller = new Action<InternalLogicalChannelDataMessage>(WriteChannel);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// ���������� ������ ������ � ����������� �����
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndWriteChannel(InternalLogicalChannelDataMessage message, IAsyncResult result)
        {
            log.Info("����� ��� �������");
        }

        /// <summary>
        /// ������ � ����������� �����
        /// </summary>
        /// <param name="message"></param>
        public void WriteChannel(InternalLogicalChannelDataMessage message)
        {
            if (message.DataMode == DataMode.Write)
            {
                //������ ����� ������ �� ������. ����� ���������� ����
                LowLevelMessageExchangeSystem.WriteChannel(message);
            }
            else
                log.Warn(
                    "������ [{0}] �������� � ������ ����� ������ � ����� [{1}]. "
                    + "�� � ��������� ������ ����� ������, �������� �� ������",
                    message.LogicalChannelId, message.RegNameFrom);
        }


        #endregion

        #region Implementation of IMessageExchangeSystem

        /// <summary>
        /// ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        protected override void Register(RegistrationMessage message, IHighLevelClientCallback clientCallback)
        {
            base.Register(message, clientCallback);

            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message, false);
            // ��� ������ �������� ����������, ��������� ��� ������� �������
            // �������� ��������� �����. ��������� �� � ����� ����� ���������
            // �� ������ ����� �� ������ �����

            //��� ������������� ������ ������� ������, ��������� ���
            //� ���-������� � ��������� � ��������� ������

            //todo: �������� ��� ������������ � ���� �� ������
            bool isRegistered = registeredHighLevelClient != null && registeredHighLevelClient.HasCallbacks;

            if (!isRegistered)
            {
                if (registeredHighLevelClient == null)
                {
                    var clientId = ClientsProvider.GetClientId(message.RegNameFrom);
                    if (!clientId.HasValue)
                        throw new FaultException(string.Format("�� ������ ������ {0}", message.RegNameFrom));

                    registeredHighLevelClient =
                        new RegisteredHighLevelClient(this, message.DataMode,
                            clientId.Value)
                        {
                            RegName = message.RegNameFrom
                        };
                    Context.InjectAttributedProperties(registeredHighLevelClient);
                    AddClient(message.RegNameFrom, registeredHighLevelClient);
                }

                //�������� ������� ������ �� ������� ������ � ��������
                //������ ������� � ������ �������� �������

                registeredHighLevelClient.AddCallback(clientCallback);

                OnRegistered(registeredHighLevelClient, message);

                //todo: ����� ����������� �������� ������� � ���� ������������������ ������� (� ���������� �����, ����� ��������?)
                /*
                                var registeredLogicalChannels = LowLevelMessageExchangeSystem.Instance.GetAllRegisteredChannels();
                                foreach (var RegisteredLogicalChannelExtended in registeredLogicalChannels)
                                {
                                    registeredHighLevelClient.ChannelRegister(new ChannelRegistrationMessage { LogicalChannelId = RegisteredLogicalChannelExtended.Id });
                                }
                */

            }
        }

        private bool subscribed;
        internal void NotifySubscribeEvents(LowLevelMessageExchangeSystem lowLevelMessageExchangeSystem)
        {
            if (!subscribed)
            {
                lowLevelMessageExchangeSystem.ChannelRegistered += Low_ChannelRegistered;
                lowLevelMessageExchangeSystem.ChannelUnregistered += Low_ChannelUnregistered;
                subscribed = true;
            }
        }

        private RegisteredHighLevelClient GetRegisteredHighLevelClient(InternalMessage message, bool withCallbacks = true)
        {
            RegisteredHighLevelClient registeredHighLevelClient = this[message.RegNameFrom];
            if (withCallbacks && registeredHighLevelClient != null && !registeredHighLevelClient.HasCallbacks)
                registeredHighLevelClient = null;

            return registeredHighLevelClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SendMessageToClients(InternalMessage message)
        {
            foreach (var value in RegisteredClients)
                value.SendMessageToClient(message);
        }

        /// <summary>
        /// ������ ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        protected override void Unregister(RegistrationMessage message, IHighLevelClientCallback clientCallback)
        {
            base.Unregister(message, clientCallback);

            //�������� ������� ������ �� ������� ������ � �������
            //������ ������� �� ������ �������� �������
            var registeredHighLevelClient = GetRegisteredHighLevelClient(message);

            if (registeredHighLevelClient != null)
            {
                registeredHighLevelClient.RemoveCallback(clientCallback);//������ ������� �����������

                OnUnregistered(registeredHighLevelClient, message);//���������� � ���, ��� ������ ������� �����������

                //                if (registeredHighLevelClient.HasCallbacks) 
                RemoveClient(message.RegNameFrom);

            }
        }

        #endregion

        /// <summary>
        /// �������� ������������������ ���������� �����
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public RegisteredLogicalChannelExtended GetRegisteredChannel(Func<RegisteredLogicalChannelExtended, bool> predicate)
        {
            //���� � ������� ������, ���� �� ������� - ���� � ������ ������
            RegisteredLogicalChannelExtended channel = FindSubscribedChannel(predicate) ??
                                               LowLevelMessageExchangeSystem.GetRegisteredLogicalChannel(predicate);
            return channel;
        }

        /// <summary>
        /// ����� ����� � ������� ������ (���� �� ���� ��� ���-�� ��������)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private RegisteredLogicalChannelExtended FindSubscribedChannel(Func<RegisteredLogicalChannelExtended, bool> predicate)
        {
            RegisteredLogicalChannelExtended registeredLogicalChannel =
                RegisteredClients
                    .SelectMany(client => client.RegisteredLogicalChannels.Values)//�������� ��� ������ ���� ��������
                    .Where(predicate).Distinct().FirstOrDefault();//TODO: ����������� ����� �� ���� ����� ��������� ��������� ��������
            return registeredLogicalChannel;
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<HighRegisteredLogicalChannelSubscribeEventArgs> ChannelSubscribed;

        private void InvokeChannelSubscribed(HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            EventHandler<HighRegisteredLogicalChannelSubscribeEventArgs> handler = ChannelSubscribed;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<HighRegisteredLogicalChannelSubscribeEventArgs> ChannelUnSubscribed;

        private void InvokeChannelUnSubscribed(HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            EventHandler<HighRegisteredLogicalChannelSubscribeEventArgs> handler = ChannelUnSubscribed;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// ��������� �����
        /// </summary>
        /// <param name="message"></param>
        public void ReadChannel(InternalLogicalChannelDataMessage message)
        {
            RegisteredLogicalChannelExtended subscribedChannel =
                FindSubscribedChannel(RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId,
                                                                                       DataMode.Read));

            if (subscribedChannel != null)
            {
                if ((subscribedChannel.DataMode & DataMode.Read) != DataMode.Unknown)
                    subscribedChannel.InvokeRead(message);
                else
                    log.Warn(
                        "������ [{0}] �������� � ������ ����� ������ �� ������ [{1}]. "
                            + "�� �� �� ����� ���� ����������� � ������ ������. "
                            + "��������� ��������� ������ ������ ��� ������",
                        message.LogicalChannelId, message.RegNameFrom);
            }
            else
            {
                log.Warn(
                    "������ [{0}] �������� � ������ ����� ������ �� ������ [{1}]. �� �� ���� ����� �� ��������",
                    message.RegNameFrom,
                    message.LogicalChannelId);
            }
        }

        /// <summary>
        /// ��������� �����
        /// </summary>
        /// <param name="message"></param>
        public void ChangeChannelState(InternalLogicalChannelStateMessage message)
        {
            var subscribedChannel =
                FindSubscribedChannel(RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId,
                                                                                       DataMode.Read));

            if (subscribedChannel != null)
            {
                subscribedChannel.InvokeChangeState(message);
            }
            else
            {
                log.Warn(
                    "������ [{0}] �������� �� ��������� ������� ������ [{1}] ([{2}]). �� �� ���� ����� �� ��������",
                    message.RegNameFrom,
                    message.LogicalChannelId,
                    message.State);
            }
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
                //��������� ������� ��������� �� ������� � ���, ��� �� ������������ �� ������ 
                //(����� ��������� �������� ����� �� ���� ����������)
                var channelSubscribeMessage = new ChannelSubscribeMessage(clientRegName,
                                                                          RegName,
                                                                          SubscribeMode.Unsubscribe,
                                                                          registeredLogicalChannelId);
                ChannelUnSubscribe(channelSubscribeMessage);
            }

            InterestedRegisteredClients.Remove(registeredHighLevelClient);
            base.RemoveClient(clientRegName);

        }

        #region Implementation of IMessageExchangeSystem

        /// <summary>
        /// ���������� ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public override void EndRegister(RegistrationMessage message, IAsyncResult result)
        {
            NotifyRegisteredChannelsToClients();
        }

        /// <summary>
        /// ��������� ������������������ ����� �������� � ������������������ � ������� �������
        /// </summary>
        private void NotifyRegisteredChannelsToClients()
        {
            var highLevelClients = RegisteredClients.Except(InterestedRegisteredClients);
            foreach (var client in highLevelClients)
            {
                var registeredChannels = GetRegisteredChannels(new InternalMessage(client.RegName, null));
                foreach (var registeredChannel in registeredChannels)
                {
                    var registrationMessage = new ChannelRegistrationMessage(null,
                                                                             client.RegName,
                                                                             RegistrationMode.Register,
                                                                             registeredChannel.DataMode,
                                                                             registeredChannel.LogicalChannelId)
                        {
                            MinValue = registeredChannel.MinValue,
                            MaxValue = registeredChannel.MaxValue,
                            MinNormalValue = registeredChannel.MinNormalValue,
                            MaxNormalValue = registeredChannel.MaxNormalValue,
                            Description = registeredChannel.Description
                        };
                    client.ChannelRegisterAsync(registrationMessage);
                }
            }
        }

        #endregion

    }
}
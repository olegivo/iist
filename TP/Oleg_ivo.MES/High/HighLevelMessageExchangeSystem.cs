using System;
using System.Collections.Generic;
using System.ServiceModel;
using DMS.Common.MessageExchangeSystem.HighLevel;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.MES.Low;
using Oleg_ivo.MES.Registered;

using System.Linq;

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
    public class HighLevelMessageExchangeSystem : AbstractLevelMessageExchangeSystem<RegisteredHighLevelClient>, IHighLevelMessageExchangeSystem
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        #region Singleton

        private static HighLevelMessageExchangeSystem _instance;
        /// <summary>
        /// ���� ������� ��� � ������ ���������������� ��������, �� �� �� ���� �� ���������� ���������� � ������������������ �������.
        /// </summary>
        private readonly List<RegisteredHighLevelClient> InterestedRegisteredClients = new List<RegisteredHighLevelClient>();

        ///<summary>
        /// ������������ ���������
        ///</summary>
        public static HighLevelMessageExchangeSystem Instance
        {
            get { return _instance ?? (_instance = new HighLevelMessageExchangeSystem()); }
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="HighLevelMessageExchangeSystem" />.
        /// </summary>
        private HighLevelMessageExchangeSystem()
        {
        }

        #endregion

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
                registeredHighLevelClient.ChannelRegister(e.ChannelRegistrationMessage);
        }

        private void Low_ChannelUnregistered(object sender, LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            foreach (RegisteredHighLevelClient registeredHighLevelClient in InterestedRegisteredClients.Where(client => client.Interested))
                registeredHighLevelClient.ChannelUnRegister(e.ChannelRegistrationMessage);
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

        /// <summary>
        /// �������� ������������������ � ������� ������
        /// </summary>
        /// <param name="message">��������� � �������������� ��������������</param>
        /// <returns></returns>
        public int[] GetRegisteredChannels(InternalMessage message)
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
                return
                    LowLevelMessageExchangeSystem.Instance.GetAllRegisteredChannels().Select(channel => channel.Id).ToArray();
            }
            catch (Exception)
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

        private delegate void ChannelSubscribeCaller(ChannelSubscribeMessage message);

        /// <summary>
        /// ������ �������� �� ������ ��������������� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state)
        {
            log.Debug("������ �������� �� ������ ������ {0}", message.LogicalChannelId);

            var caller = new ChannelSubscribeCaller(ChannelSubscribe);
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
            log.Debug("������ ������� �� ������ ������ {0}", message.LogicalChannelId);

            var caller = new ChannelSubscribeCaller(ChannelUnSubscribe);
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
            if (message.Mode != SubscribeMode.Subscribe)
                throw new ArgumentException("��� �������� �� ����� � ��������� ������������ ���� �������");

            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message);
            if (registeredHighLevelClient != null)
                registeredHighLevelClient.ChannelSubscribe(message);

            InvokeChannelSubscribed(new HighRegisteredLogicalChannelSubscribeEventArgs(registeredHighLevelClient, message));
        }

        /// <summary>
        /// ������� �� ������ ��������������� ������ (������� ��������� �� ������� � ���, ��� �� ������������ �� ������)
        /// </summary>
        /// <param name="message"></param>
        private void ChannelUnSubscribe(ChannelSubscribeMessage message)
        {
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
            log.Debug("������ ������ ������ {0}", message.LogicalChannelId);
            var caller = new WriteChannelCaller(WriteChannel);
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
                LowLevelMessageExchangeSystem.Instance.WriteChannel(message);
            }
            else
                log.Warn(
                    "������ [{0}] �������� � ������ ����� ������ � ����� [{1}]. "
                    + "�� � ��������� ������ ����� ������, �������� �� ������",
                    message.LogicalChannelId, message.RegNameFrom);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void SendMessage(InternalMessage message)
        {
            string s = string.Format("HighLevelClient -> MessageExchangeSystem : {0}{1}", message.TimeStamp, Environment.NewLine);
            InvokeMessageReceived(s);
        }


        /// <summary>
        /// 
        /// </summary>
        public event EventHandler MessageReceived;

        private void InvokeMessageReceived(object e)
        {
            EventHandler handler = MessageReceived;
            if (handler != null) handler(e, EventArgs.Empty);
        }

        #endregion

        #region Implementation of IMessageExchangeSystem

        /// <summary>
        /// ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        private void Register(RegistrationMessage message, IHighLevelClientCallback clientCallback)
        {
            if (message.RegistrationMode != RegistrationMode.Register)
                throw new ArgumentException("��� ����������� ������� � ��������� ������������ ���� ������ �����������");

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
                    registeredHighLevelClient = new RegisteredHighLevelClient(message.DataMode) { Ticker = message.RegNameFrom };
                    AddClient(message.RegNameFrom, registeredHighLevelClient);
                }
                //                registeredHighLevelClient = (RegisteredHighLevelClient)_registeredClients[message.RegNameFrom];

                //Thread t = new Thread(w.RegisteredHighLevelClientProcess.SendUpdateToClient) { IsBackground = true };
                //t.Start();

                //�������� ������� ������ �� ������� ������ � ��������
                //������ ������� � ������ �������� �������

                registeredHighLevelClient.AddCallback(clientCallback);

                OnRegistered(registeredHighLevelClient, message);

                //todo: ����� ����������� �������� ������� � ���� ������������������ ������� (� ���������� �����, ����� ��������?)
                /*
                                var registeredLogicalChannels = LowLevelMessageExchangeSystem.Instance.GetAllRegisteredChannels();
                                foreach (var registeredLogicalChannel in registeredLogicalChannels)
                                {
                                    registeredHighLevelClient.ChannelRegister(new ChannelRegistrationMessage { LogicalChannelId = registeredLogicalChannel.Id });
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

        private RegisteredHighLevelClient GetRegisteredHighLevelClient(InternalMessage message)
        {
            return GetRegisteredHighLevelClient(message, true);
        }

        private RegisteredHighLevelClient GetRegisteredHighLevelClient(InternalMessage message, bool withCallbacks)
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
            foreach (RegisteredHighLevelClient value in RegisteredClients)
                value.SendMessageToClient(message);
        }

        /// <summary>
        /// ������ ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        private void Unregister(RegistrationMessage message, IHighLevelClientCallback clientCallback)
        {
            if (message.RegistrationMode != RegistrationMode.Unregister)
                throw new ArgumentException("��� ������ ����������� ������� � ��������� ������������ ���� �����������");

            //�������� ������� ������ �� ������� ������ � �������
            //������ ������� �� ������ �������� �������
            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message);

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
        public RegisteredLogicalChannel GetRegisteredChannel(Func<RegisteredLogicalChannel, bool> predicate)
        {
            //���� � ������� ������, ���� �� ������� - ���� � ������ ������
            RegisteredLogicalChannel channel = FindSubscribedChannel(predicate) ??
                                               LowLevelMessageExchangeSystem.Instance.GetRegisteredLogicalChannel(predicate);
            return channel;
        }

        /// <summary>
        /// ����� ����� � ������� ������ (���� �� ���� ��� ���-�� ��������)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private RegisteredLogicalChannel FindSubscribedChannel(Func<RegisteredLogicalChannel, bool> predicate)
        {
            RegisteredLogicalChannel registeredLogicalChannel =
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
            RegisteredLogicalChannel subscribedChannel =
                FindSubscribedChannel(RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId,
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

            base.RemoveClient(clientRegName);

        }

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

            IHighLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<IHighLevelClientCallback>();

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

            IHighLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<IHighLevelClientCallback>();

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

        private delegate void RegistrationCaller(RegistrationMessage message, IHighLevelClientCallback clientCallback);
        private delegate void WriteChannelCaller(InternalLogicalChannelDataMessage message);
    }
}
using System;
using System.Collections.Generic;
using System.ServiceModel;
using Autofac;
using DMS.Common.MessageExchangeSystem;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.MES.Registered;
using Oleg_ivo.MES.Services;

namespace Oleg_ivo.MES
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractLevelMessageExchangeSystem<TRegisteredClient, TClientCallback> : IMessageExchangeSystem 
        where TRegisteredClient : IRegisteredChannelsHolder 
        where TClientCallback : IClientCallback
    {
        private readonly Logger log = LogManager.GetCurrentClassLogger();
        protected readonly InternalMessageLogger MessageLogger;
        protected readonly IComponentContext Context;

        private readonly Dictionary<string, TRegisteredClient> registeredClients = new Dictionary<string, TRegisteredClient>();

        protected AbstractLevelMessageExchangeSystem(IComponentContext context, InternalMessageLogger messageLogger)
        {
            Context = Enforce.ArgumentNotNull(context, "context");
            this.MessageLogger = Enforce.ArgumentNotNull(messageLogger, "messageLogger");
        }

        [Dependency(Required = true)]
        public ClientsProvider ClientsProvider { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<TRegisteredClient> RegisteredClients
        {
            get
            {
                TRegisteredClient[] ar = new TRegisteredClient[registeredClients.Count];
                registeredClients.Values.CopyTo(ar, 0);
                return ar;
            }
        }

        /// <summary>
        /// �������������� ��� ������� ������ ���������
        /// </summary>
        public virtual string RegName { get { return "MESHighLevel"; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        protected TRegisteredClient this[string key]
        {
            get { return registeredClients.ContainsKey(key) ? registeredClients[key] : default(TRegisteredClient); }
            private set
            {
                if (Equals(value, default(TRegisteredClient)))//������ ��������
                {
                    if (registeredClients.ContainsKey(key))
                        registeredClients.Remove(key);//�������
                    else
                        log.Warn("�� ������ ���� ��� �������� {0}", key);
                }
                else//������ ��������
                {
                    if (registeredClients.ContainsKey(key))
                        registeredClients[key] = value;//��������
                    else
                        registeredClients.Add(key, value);//���������
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regName"></param>
        /// <param name="registeredClient"></param>
        protected void AddClient(String regName, TRegisteredClient registeredClient)
        {
            if (!Equals(registeredClient, default(TRegisteredClient)))
                this[regName] = registeredClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientRegName"></param>
        protected virtual void RemoveClient(String clientRegName)
        {
            //������� �� ��������� ������������������ ��������
            this[clientRegName] = default(TRegisteredClient);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler MessageReceived;

        protected void InvokeMessageReceived(object e)
        {
            EventHandler handler = MessageReceived;
            if (handler != null) handler(e, EventArgs.Empty);
        }

        #region Implementation of IMessageReceiver

        /// <summary>
        /// ������� ������� �������� ��������� ���������
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(InternalMessage message)
        {
            string s = string.Format("{0} -> {1} : {2}{3}", message.RegNameFrom, RegName, message.TimeStamp, Environment.NewLine);
            InvokeMessageReceived(s);
        }

        #endregion

        /// <summary>
        /// ������ �������� ��������� �� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginSendError(InternalErrorMessage message, AsyncCallback callback, object state)
        {
            log.Trace("������ �������� ��������� �� ������ �� ������� {0}", message.RegNameFrom);
            //TODO: ��� ������������ BeginSendError: throw new FaultException<InternalException>(new InternalException("Test"),"Reason");
            var caller = new Action<InternalErrorMessage>(SendError);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        private void SendError(InternalErrorMessage message)
        {
            MessageLogger.ProtocolMessage(message);
            InvokeErrorReceived(new ErrorReceivedEventArgs(message));
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ErrorReceivedEventArgs> ErrorReceived;

        private void InvokeErrorReceived(ErrorReceivedEventArgs e)
        {
            EventHandler<ErrorReceivedEventArgs> handler = ErrorReceived;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// ���������� �������� ��������� �� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndSendError(InternalErrorMessage message, IAsyncResult result)
        {
            log.Trace("��������� �� ������ �� ������� ��������");
        }

        /// <summary>
        /// ������ ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public virtual IAsyncResult BeginRegister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Trace("������ ����������� ������� {0}", message.RegNameFrom);

            var clientCallback = OperationContext.Current.GetCallbackChannel<TClientCallback>();

            var caller = new Action<RegistrationMessage, TClientCallback>(Register);
            IAsyncResult result = caller.BeginInvoke(message, clientCallback, callback, state);
            return result;
        }

        protected virtual void Register(RegistrationMessage message, TClientCallback clientcallback)
        {
            MessageLogger.ProtocolMessage(message);
            if (message.RegistrationMode != RegistrationMode.Register)
                throw new ArgumentException("��� ����������� ������� � ��������� ������������ ���� ������ �����������");
        }

        /// <summary>
        /// ���������� ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public virtual void EndRegister(RegistrationMessage message, IAsyncResult result)
        {
            //TODO: ������-�� message==null
            log.Info("������ {0} ��� ���������������", /*message.RegNameFrom*/"");
        }

        /// <summary>
        /// ������ ������ ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public virtual IAsyncResult BeginUnregister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Trace("������ ������ ����������� ������� {0}", message.RegNameFrom);

            var clientCallback = OperationContext.Current.GetCallbackChannel<TClientCallback>();

            var caller = new Action<RegistrationMessage, TClientCallback>(Unregister);
            IAsyncResult result = caller.BeginInvoke(message, clientCallback, callback, state);
            return result;
        }

        protected virtual void Unregister(RegistrationMessage message, TClientCallback clientcallback)
        {
            MessageLogger.ProtocolMessage(message);
            if (message.RegistrationMode != RegistrationMode.Unregister)
                throw new ArgumentException("��� ������ ����������� ������� � ��������� ������������ ���� �����������");
        }

        /// <summary>
        /// ���������� ������ ����������� �������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public virtual void EndUnregister(RegistrationMessage message, IAsyncResult result)
        {
            //TODO: ������-�� message==null
            log.Info("����������� ������� {0} ���� ��������", /*message.RegNameFrom*/"");
        }

        /// <summary>
        /// ���������� ������� �� �������
        /// </summary>
        /// <param name="clientName"></param>
        public void Disconnect(string clientName)
        {
            log.Info("{0} disconnected from {1}", clientName, RegName);
        }
    }
}
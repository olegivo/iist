using System;
using System.Collections.Generic;
using Autofac;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.MES.Registered;

namespace Oleg_ivo.MES
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractLevelMessageExchangeSystem<TRegisteredClient> where TRegisteredClient : IRegisteredChannelsHolder
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<string, TRegisteredClient> _registeredClients =
            new Dictionary<string, TRegisteredClient>();

        [Dependency(Required = true)]
        public IComponentContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<TRegisteredClient> RegisteredClients
        {
            get
            {
                TRegisteredClient[] ar = new TRegisteredClient[_registeredClients.Count];
                _registeredClients.Values.CopyTo(ar, 0);
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
            get { return _registeredClients.ContainsKey(key) ? _registeredClients[key] : default(TRegisteredClient); }
            private set
            {
                if (Equals(value, default(TRegisteredClient)))//������ ��������
                {
                    if (_registeredClients.ContainsKey(key))
                        _registeredClients.Remove(key);//�������
                    else
                        log.Warn("�� ������ ���� ��� �������� {0}", key);
                }
                else//������ ��������
                {
                    if (_registeredClients.ContainsKey(key))
                        _registeredClients[key] = value;//��������
                    else
                        _registeredClients.Add(key, value);//���������
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

        private delegate void SendErrorCaller(InternalErrorMessage message);

        /// <summary>
        /// ������ �������� ��������� �� ������
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginSendError(InternalErrorMessage message, AsyncCallback callback, object state)
        {
            log.Debug("������ �������� ��������� �� ������ �� ������� {0}", message.RegNameFrom);
            //TODO: ��� ������������ BeginSendError: throw new FaultException<InternalException>(new InternalException("Test"),"Reason");
            var caller = new SendErrorCaller(SendError);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        private void SendError(InternalErrorMessage message)
        {
            //TODO: SendError - ��������������� ������, ����������� � �������� �������� � ������� ������
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
            log.Debug("��������� �� ������ �� ������� ��������");
        }

    }
}
using System;
using System.Collections.Generic;
using DMS.Common.Messages;
using Oleg_ivo.MES.Registered;

namespace Oleg_ivo.MES
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractLevelMessageExchangeSystem<TRegisteredClient> where TRegisteredClient : IRegisteredChannelsHolder
    {
        private readonly Dictionary<string, TRegisteredClient> _registeredClients =
            new Dictionary<string, TRegisteredClient>();

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
        /// 
        /// </summary>
        /// <param name="key"></param>
        protected TRegisteredClient this[string key]
        {
            get { return _registeredClients.ContainsKey(key) ? _registeredClients[key] : default(TRegisteredClient); }
            private set
            {
                if (Equals(value, default(TRegisteredClient)))//пустое значение
                {
                    if (_registeredClients.ContainsKey(key))
                        _registeredClients.Remove(key);//удаляем
                    else
                        Console.WriteLine("Не найден ключ для удаления {0}", key);
                }
                else//другое значение
                {
                    if (_registeredClients.ContainsKey(key))
                        _registeredClients[key] = value;//заменяем
                    else
                        _registeredClients.Add(key, value);//добавляем
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
        /// <param name="regName"></param>
        protected virtual void RemoveClient(String regName)
        {
            //убираем из коллекции зарегистрированных клиентов
            this[regName] = default(TRegisteredClient);
        }

        private delegate void SendErrorCaller(InternalErrorMessage message);

        /// <summary>
        /// Начало передачи сообщения об ошибке
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginSendError(InternalErrorMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало передачи сообщения об ошибке от клиента {0}", message.RegName);
            //TODO: для тестирования BeginSendError: throw new FaultException<InternalException>(new InternalException("Test"),"Reason");
            var caller = new SendErrorCaller(SendError);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        private void SendError(InternalErrorMessage message)
        {
            //TODO: SendError - протоколировать ошибки, поступающие с клиентов верхнего и нижнего уровня
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
        /// Завершение передачи сообщения об ошибке
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndSendError(InternalErrorMessage message, IAsyncResult result)
        {
            Console.WriteLine("Сообщения об ошибке от клиента передано");
        }

    }
}
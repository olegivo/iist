using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using DMS.Common.MessageExchangeSystem.LowLevel;
using DMS.Common.Messages;
using Oleg_ivo.MES.Registered;

namespace Oleg_ivo.MES.Low
{
    ///<summary>
    /// Система обмена сообщениями c клиентами верхнего уровня
    ///</summary>
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single, 
        ConcurrencyMode = ConcurrencyMode.Reentrant, 
        AutomaticSessionShutdown = false, 
        IncludeExceptionDetailInFaults = true)]
    public class LowLevelMessageExchangeSystem : AbstractLevelMessageExchangeSystem<RegisteredLowLevelClient>, ILowLevelMessageExchangeSystem
    {
        #region Singleton

        private static LowLevelMessageExchangeSystem _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static LowLevelMessageExchangeSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LowLevelMessageExchangeSystem();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LowLevelMessageExchangeSystem" />.
        /// </summary>
        private LowLevelMessageExchangeSystem()
        {
            High.HighLevelMessageExchangeSystem.Instance.ChannelSubscribed += High_ChannelSubscribed;
            High.HighLevelMessageExchangeSystem.Instance.ChannelUnSubscribed += High_ChannelUnSubscribed;
        }

        private void High_ChannelUnSubscribed(object sender, High.HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            var channel =
                GetRegisteredLogicalChannel(
                    RegisteredLogicalChannel.GetFindChannelPredicate(e.ChannelSubscribeMessage.LogicalChannelId));

            if (channel == null)
                throw new Exception("Невозможно отписаться от канала, который не зарегистрирован");

            channel.InvokeUnSubscribed(e.ChannelSubscribeMessage);
        }

        private void High_ChannelSubscribed(object sender, High.HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            var channel =
                GetRegisteredLogicalChannel(
                    RegisteredLogicalChannel.GetFindChannelPredicate(e.ChannelSubscribeMessage.LogicalChannelId));

            if (channel == null)
                throw new Exception("Невозможно подписаться на канал, который не зарегистрирован");

            channel.InvokeSubscribed(e.ChannelSubscribeMessage);
        }

        #endregion

        #region Implementation of IMessageReceiver

        /// <summary>
        /// Послать данному приёмнику сообщений сообщение
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
        /// Регистрация клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        private void Register(RegistrationMessage message, ILowLevelClientCallback clientCallback)
        {
            if (!message.Mode)
                throw new ArgumentException("Для регистрации клиента в сообщении используется флаг отмены регистрации");

            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);

            if (registeredLowLevelClient != null)
                throw new Exception("Клиент уже зарегистрирован");
            
            registeredLowLevelClient = new RegisteredLowLevelClient {Ticker = message.RegName};
            AddClient(message.RegName, registeredLowLevelClient);
            
            //                registeredLowLevelClient = (RegisteredLowLevelClient)_registeredClients[message.RegName];

            //Thread t = new Thread(w.RegisteredLowLevelClientProcess.SendUpdateToClient) { IsBackground = true };
            //t.Start();

            //получить рабочий объект из данного тикера и добавить
            //прокси клиента в список обратных вызовов

            registeredLowLevelClient.AddCallback(clientCallback);

            OnRegistered(registeredLowLevelClient, message);
        }

        /// <summary>
        /// Отмена регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        private void Unregister(RegistrationMessage message, ILowLevelClientCallback clientCallback)
        {
            if (message.Mode) throw new ArgumentException("Для отмены регистрации клиента в сообщении используется флаг регистрации");

            //получить рабочий объект из данного тикера и удалить
            //прокси клиента из списка обратных вызовов
            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);

            if (registeredLowLevelClient != null)
            {
                registeredLowLevelClient.RemoveCallback(clientCallback);//лишаем клиента уведомлений

                OnUnRegistered(registeredLowLevelClient, message);//уведомляем о том, что клиент отменил регистрацию

//                if (registeredLowLevelClient.HasCallbacks)
                RemoveClient(message.RegName);

            }
        }

        #endregion

        #region Implementation of ILowLevelMessageExchangeSystem

        /// <summary>
        /// Чтение данных из контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        private void ReadChannel(InternalLogicalChannelDataMessage message)
        {
            //пришли новые данные по каналу. будем передавать наверх
            High.HighLevelMessageExchangeSystem.Instance.ReadChannel(message);
        }

        private delegate void ReadChannelCaller(InternalLogicalChannelDataMessage message);
        private delegate void ChannelRegistrationCaller(ChannelSubscribeMessage message);

        /// <summary>
        /// Начало регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelRegister(ChannelSubscribeMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало регистрации канала {0}", message.LogicalChannelId);
            var caller = new ChannelRegistrationCaller(ChannelRegister);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelRegister(ChannelSubscribeMessage message, IAsyncResult result)
        {
            Console.WriteLine("Канал был зарегистрирован");
        }

        /// <summary>
        /// Начало отмены регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelUnRegister(ChannelSubscribeMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало отмены регистрации канала {0}", message.LogicalChannelId);
            var caller = new ChannelRegistrationCaller(ChannelUnRegister);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение отмены регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelUnRegister(ChannelSubscribeMessage message, IAsyncResult result)
        {
            Console.WriteLine("Регистрация канала была отменена");
        }

        /// <summary>
        /// Начало чтения данных из контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginReadChannel(InternalLogicalChannelDataMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало чтения канала {0}", message.LogicalChannelId);
            var caller = new ReadChannelCaller(ReadChannel);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение чтения данных из контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndReadChannel(InternalLogicalChannelDataMessage message, IAsyncResult result)
        {
            Console.WriteLine("Канал был прочитан");
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
        /// Получить зарегистрированный логический канал
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public RegisteredLogicalChannel GetRegisteredLogicalChannel(Func<RegisteredLogicalChannel, bool> predicate)
        {
            //ищем в нижней службе
            RegisteredLogicalChannel channel = FindSubscribedChannel(predicate);
            return channel;
        }

        /// <summary>
        /// Найти канал в верхней службе (если на него уже кто-то подписан)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private RegisteredLogicalChannel FindSubscribedChannel(Func<RegisteredLogicalChannel, bool> predicate)
        {
            //HACK: почему-то в некоторых случаях без указания Instance. в _registeredClients нет ни одного элемента  !!!
            RegisteredLogicalChannel registeredLogicalChannel =
                Instance.RegisteredClients
                    .SelectMany(client => client.RegisteredLogicalChannels.Values)//выбираем все каналы всех клиентов
                    .Where(predicate).FirstOrDefault();//где Id - заданный
            return registeredLogicalChannel;
        }

        /// <summary>
        /// Получить зарегистрированного клиента по имени.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="withRegisteredChannels">Только с зарегистрированными каналами. 
        /// Может потребоваться, когда требуется найти клиента-владельца канала</param>
        /// <param name="withCallbacks"></param>
        /// <returns></returns>
        private RegisteredLowLevelClient GetRegisteredLowLevelClient(InternalMessage message, bool withRegisteredChannels, bool withCallbacks)
        {
            RegisteredLowLevelClient registeredLowLevelClient = this[message.RegName];
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
        /// Регистрация канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        public void ChannelRegister(ChannelSubscribeMessage message)
        {
            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);
            if (registeredLowLevelClient == null)
                throw new Exception("Данный клиент не зарегистрирован");

            RegisteredLogicalChannel channel = registeredLowLevelClient.GetRegisteredLogicalChannel(RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId));
            if (channel != null)
                throw new Exception("Данный канал уже зарегистрирован");

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
        /// Отмена регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        public void ChannelUnRegister(ChannelSubscribeMessage message)
        {
            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);
            if (registeredLowLevelClient == null)
                throw new Exception("Данный клиент не зарегистрирован");

            RegisteredLogicalChannel channel = registeredLowLevelClient.GetRegisteredLogicalChannel(RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId));
            if (channel == null)
                throw new Exception("Данный канал не зарегистрирован");

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
        /// Получить все зарегистрированные в нижней системе каналы
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RegisteredLogicalChannel> GetAllRegisteredChannels()
        {
            return RegisteredClients.SelectMany(client => client.RegisteredLogicalChannels.Values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regName"></param>
        protected override void RemoveClient(string regName)
        {
            //убираем зарегистрированные каналы
            var registeredHighLevelClient = this[regName];

            foreach (int registeredLogicalChannelId in registeredHighLevelClient.RegisteredLogicalChannels.Keys.ToArray())
            {
                //имитируем посылку сообщения от клиента о том, что он отменяет регистрацию канала 
                //(тогда подписчики на канал будут об этом уведомлены)
                ChannelSubscribeMessage channelSubscribeMessage = new ChannelSubscribeMessage
                {
                    LogicalChannelId = registeredLogicalChannelId,
                    RegName = regName,
                    Mode = false
                };
                ChannelUnRegister(channelSubscribeMessage);
            }

            base.RemoveClient(regName);
        }

        #region Implementation of IMessageExchangeSystem

        #region Implementation of IMessageExchangeSystem

        /// <summary>
        /// Начало регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginRegister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало регистрации клиента {0}", message.RegName);

            ILowLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<ILowLevelClientCallback>();

            var caller = new RigistrationCaller(Register);
            IAsyncResult result = caller.BeginInvoke(message, clientCallback, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndRegister(RegistrationMessage message, IAsyncResult result)
        {
            Console.WriteLine("Клиент был зарегистрирован");
        }

        /// <summary>
        /// Начало отмены регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginUnregister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало отмены регистрации клиента {0}", message.RegName);

            ILowLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<ILowLevelClientCallback>();

            var caller = new RigistrationCaller(Unregister);
            IAsyncResult result = caller.BeginInvoke(message, clientCallback, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение отмены регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndUnregister(RegistrationMessage message, IAsyncResult result)
        {
            Console.WriteLine("Регистрация клиента была отменена");
        }

        #endregion

        #endregion

        private delegate void RigistrationCaller(RegistrationMessage message, ILowLevelClientCallback clientCallback);
    }
}
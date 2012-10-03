using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using DMS.Common.MessageExchangeSystem.LowLevel;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.MES.High;
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
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        
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
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(e.ChannelSubscribeMessage.LogicalChannelId,
                                                                     DataMode.Unknown));

            if (channel == null)
                throw new Exception("Невозможно отписаться от канала, который не зарегистрирован");

            channel.InvokeUnSubscribed(e.ChannelSubscribeMessage);
        }

        private void High_ChannelSubscribed(object sender, HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            var channel =
                GetRegisteredLogicalChannel(
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(e.ChannelSubscribeMessage.LogicalChannelId,
                                                                     DataMode.Unknown));
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
            if (message.RegistrationMode != RegistrationMode.Register)
                throw new ArgumentException("Для регистрации клиента в сообщении используется флаг отмены регистрации");

            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);

            if (registeredLowLevelClient != null)
                throw new Exception("Клиент уже зарегистрирован");

            registeredLowLevelClient = new RegisteredLowLevelClient { Ticker = message.RegNameFrom };
            AddClient(message.RegNameFrom, registeredLowLevelClient);

            //                registeredLowLevelClient = (RegisteredLowLevelClient)_registeredClients[message.RegNameFrom];

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
            if (message.RegistrationMode != RegistrationMode.Unregister) 
                throw new ArgumentException("Для отмены регистрации клиента в сообщении используется флаг регистрации");

            //получить рабочий объект из данного тикера и удалить
            //прокси клиента из списка обратных вызовов
            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);

            if (registeredLowLevelClient != null)
            {
                registeredLowLevelClient.RemoveCallback(clientCallback);//лишаем клиента уведомлений

                OnUnRegistered(registeredLowLevelClient, message);//уведомляем о том, что клиент отменил регистрацию

                //                if (registeredLowLevelClient.HasCallbacks)
                RemoveClient(message.RegNameFrom);

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
            if (message.DataMode == DataMode.Read)
            {
                //пришли новые данные по каналу. будем передавать наверх
                HighLevelMessageExchangeSystem.Instance.ReadChannel(message);
            }
            else
            {
                log.Warn(
                    "Клиент [{0}] извещает о чтении новых данных из канала [{1}]. "
                    + "Но в сообщении указан режим данных, отличный от чтения",
                    message.LogicalChannelId, message.RegNameFrom);

            }
        }

        private delegate void ReadChannelCaller(InternalLogicalChannelDataMessage message);
        private delegate void ChannelRegistrationCaller(ChannelRegistrationMessage message);

        /// <summary>
        /// Начало регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelRegister(ChannelRegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Debug("Начало регистрации канала {0} ({1})", message.LogicalChannelId, message.DataMode.ToString());
            var caller = new ChannelRegistrationCaller(ChannelRegister);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelRegister(ChannelRegistrationMessage message, IAsyncResult result)
        {
            log.Info("Канал был зарегистрирован");
        }

        /// <summary>
        /// Начало отмены регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelUnRegister(ChannelRegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Debug("Начало отмены регистрации канала {0}", message.LogicalChannelId);
            var caller = new ChannelRegistrationCaller(ChannelUnRegister);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение отмены регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelUnRegister(ChannelRegistrationMessage message, IAsyncResult result)
        {
            log.Info("Регистрация канала была отменена");
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
            log.Debug("Начало чтения канала {0}", message.LogicalChannelId);
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
            log.Info("Канал был прочитан");
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
        public RegisteredLogicalChannelExtended GetRegisteredLogicalChannel(Func<RegisteredLogicalChannelExtended, bool> predicate)
        {
            //ищем в нижней службе
            RegisteredLogicalChannelExtended channel = FindSubscribedChannel(predicate);
            return channel;
        }

        /// <summary>
        /// Найти канал в верхней службе (если на него уже кто-то подписан)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private RegisteredLogicalChannelExtended FindSubscribedChannel(Func<RegisteredLogicalChannelExtended, bool> predicate)
        {
            //HACK: почему-то в некоторых случаях без указания Instance. в _registeredClients нет ни одного элемента  !!!
            RegisteredLogicalChannelExtended registeredLogicalChannel =
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
        /// Регистрация канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        public void ChannelRegister(ChannelRegistrationMessage message)
        {
            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);
            if (registeredLowLevelClient == null)
                throw new Exception("Данный клиент не зарегистрирован");

            RegisteredLogicalChannelExtended channel =
                registeredLowLevelClient.GetRegisteredLogicalChannel(
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId, DataMode.Unknown));
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
        public void ChannelUnRegister(ChannelRegistrationMessage message)
        {
            RegisteredLowLevelClient registeredLowLevelClient = GetRegisteredLowLevelClient(message, false, false);
            if (registeredLowLevelClient == null)
                throw new Exception("Данный клиент не зарегистрирован");

            RegisteredLogicalChannelExtended channel =
                registeredLowLevelClient.GetRegisteredLogicalChannel(
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId, DataMode.Unknown));
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
        public IEnumerable<RegisteredLogicalChannelExtended> GetAllRegisteredChannels()
        {
            return RegisteredClients.SelectMany(client => client.RegisteredLogicalChannels.Values);
        }

        /// <summary>
        /// Регистрационое имя системы обмена сообщений нижнего уровня
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
            //убираем зарегистрированные каналы
            var registeredHighLevelClient = this[clientRegName];

            foreach (int registeredLogicalChannelId in registeredHighLevelClient.RegisteredLogicalChannels.Keys.ToArray())
            {
                //имитируем посылку сообщения от клиента о том, что он отменяет регистрацию канала 
                //(тогда подписчики на канал будут об этом уведомлены)
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
        /// Начало регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginRegister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Debug("Начало регистрации клиента {0}", message.RegNameFrom);

            ILowLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<ILowLevelClientCallback>();

            var caller = new RegistrationCaller(Register);
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
            log.Info("Клиент был зарегистрирован");
        }

        /// <summary>
        /// Начало отмены регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginUnregister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Debug("Начало отмены регистрации клиента {0}", message.RegNameFrom);

            ILowLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<ILowLevelClientCallback>();

            var caller = new RegistrationCaller(Unregister);
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
            log.Info("Регистрация клиента была отменена");
        }

        #endregion

        #endregion

        private delegate void RegistrationCaller(RegistrationMessage message, ILowLevelClientCallback clientCallback);

        /// <summary>
        /// Записать канал
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
                        "Клиент [{0}] извещает о записи новых данных в канал [{1}]. "
                            + "Но он не может быть использован в режиме записи. "
                            + "Проверьте настройки режима данных для канала",
                        message.LogicalChannelId, message.RegNameFrom);
            }
            else
            {
                log.Warn(
                    "Клиент [{0}] извещает о записи новых данных в канал [{1}]. "
                        +"Невозможно найти канал с этим номером, удовлетворяющий режиму записи. "
                        +"Возможно, канал не зарегистрирован или неправильно сконфигурирован его режим данных.", 
                    message.RegNameFrom,
                    message.LogicalChannelId);
            }
        }
    }
}
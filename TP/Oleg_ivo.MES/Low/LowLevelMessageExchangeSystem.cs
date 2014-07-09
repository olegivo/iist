using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Autofac;
using DMS.Common.MessageExchangeSystem.LowLevel;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.MES.High;
using Oleg_ivo.MES.Registered;
using Oleg_ivo.MES.Services;

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
    public class LowLevelMessageExchangeSystem : AbstractLevelMessageExchangeSystem<RegisteredLowLevelClient, ILowLevelClientCallback>, ILowLevelMessageExchangeSystem
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="LowLevelMessageExchangeSystem" />.
        /// </summary>
        public LowLevelMessageExchangeSystem(IComponentContext context, InternalMessageLogger internalLogger)
            : base(context, internalLogger)
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
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(e.Message.LogicalChannelId,
                                                                     DataMode.Unknown));

            if (channel == null)
                throw new Exception("Невозможно отписаться от канала, который не зарегистрирован");

            channel.InvokeUnSubscribed(e.Message);
        }

        private void High_ChannelSubscribed(object sender, HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            var channel =
                GetRegisteredLogicalChannel(
                    RegisteredLogicalChannelExtended.GetFindChannelPredicate(e.Message.LogicalChannelId,
                                                                     DataMode.Unknown));
            if (channel == null)
                throw new Exception("Невозможно подписаться на канал, который не зарегистрирован");

            channel.InvokeSubscribed(e.Message);
        }

        #endregion

        #region Implementation of IMessageExchangeSystem

        /// <summary>
        /// Регистрация клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        protected override void Register(RegistrationMessage message, ILowLevelClientCallback clientCallback)
        {
            base.Register(message, clientCallback);

            var registeredLowLevelClient = this[message.RegNameFrom];

            if (registeredLowLevelClient != null)
            {
                if (registeredLowLevelClient.HasCallbacks)
                    throw new Exception("Клиент уже зарегистрирован");
            }
            else
            {
                registeredLowLevelClient = new RegisteredLowLevelClient(Context);
                Context.InjectAttributedProperties(registeredLowLevelClient);
                registeredLowLevelClient.RegName = message.RegNameFrom;
                AddClient(message.RegNameFrom, registeredLowLevelClient);
            }

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
        protected override void Unregister(RegistrationMessage message, ILowLevelClientCallback clientCallback)
        {
            base.Unregister(message, clientCallback);

            //получить рабочий объект из данного тикера и удалить
            //прокси клиента из списка обратных вызовов
            var registeredLowLevelClient = GetRegisteredLowLevelClient(message);
            
            registeredLowLevelClient.RemoveCallback(clientCallback);//лишаем клиента уведомлений

            OnUnRegistered(registeredLowLevelClient, message);//уведомляем о том, что клиент отменил регистрацию

            //                if (registeredLowLevelClient.HasCallbacks)
            RemoveClient(message.RegNameFrom);
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
        /// Чтение данных из контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        private void ReadChannel(InternalLogicalChannelDataMessage message)
        {
            if (message.DataMode == DataMode.Read)
            {
                //пришли новые данные по каналу. будем передавать наверх
                HighLevelMessageExchangeSystem.ReadChannel(message);
            }
            else
            {
                log.Warn(
                    "Клиент [{0}] извещает о чтении новых данных из канала [{1}]. "
                    + "Но в сообщении указан режим данных, отличный от чтения",
                    message.LogicalChannelId, message.RegNameFrom);

            }
        }

        /// <summary>
        /// Изменение состояния контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        private void ChangeChannelState(InternalLogicalChannelStateMessage message)
        {
            MessageLogger.ProtocolMessage(message);

            log.Info("Состояние канала {0} изменилось: {1}", message.LogicalChannelId, message.State);
            //пришли новые данные по каналу. будем передавать наверх
            HighLevelMessageExchangeSystem.ChangeChannelState(message);
        }

        /// <summary>
        /// Начало регистрации канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelRegister(ChannelRegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Trace("Начало регистрации канала {0} ({1})", message.LogicalChannelId, message.DataMode.ToString());
            var caller = new Action<ChannelRegistrationMessage>(ChannelRegister);
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
            log.Trace("Начало отмены регистрации канала {0}", message.LogicalChannelId);
            var caller = new Action<ChannelRegistrationMessage>(ChannelUnRegister);
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
            log.Trace("Начало чтения канала {0}", message.LogicalChannelId);
            var caller = new Action<InternalLogicalChannelDataMessage>(ReadChannel);
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
            log.Trace("Канал был прочитан");
        }

        public IAsyncResult BeginChangeChannelState(InternalLogicalChannelStateMessage message, AsyncCallback callback, object state)
        {
            log.Trace("Начало чтения канала {0}", message.LogicalChannelId);
            var caller = new Action<InternalLogicalChannelStateMessage>(ChangeChannelState);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        public void EndChangeChannelState(InternalLogicalChannelStateMessage message, IAsyncResult result)
        {
            log.Trace("Канал поменял статус");
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
            var registeredLogicalChannel =
                RegisteredClients
                    .SelectMany(client => client.RegisteredLogicalChannels.Values)//выбираем все каналы всех клиентов
                    .Where(predicate).FirstOrDefault();//где Id - заданный
            return registeredLogicalChannel;
        }

        /// <summary>
        /// Получить зарегистрированного клиента по имени.
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="Exception">В случае, когда клиент не найден по имени или когда у клиента нет обратных вызовов, генерируется исключение</exception>
        /// <returns></returns>
        private RegisteredLowLevelClient GetRegisteredLowLevelClient(InternalMessage message)
        {
            var registeredLowLevelClient = this[message.RegNameFrom];
            if (registeredLowLevelClient == null)
                throw new Exception("Клиент не зарегистрирован");
            if (!registeredLowLevelClient.HasCallbacks)
                throw new Exception("У клиента нет обратных вызовов"); 
            return registeredLowLevelClient;
        }

        /// <summary>
        /// Регистрация канала в системе обмена сообщениями
        /// </summary>
        /// <param name="message"></param>
        public void ChannelRegister(ChannelRegistrationMessage message)
        {
            MessageLogger.ProtocolMessage(message);

            var registeredLowLevelClient = GetRegisteredLowLevelClient(message);
            //if(!registeredLowLevelClient.HasRegisteredLogicalChannels)
            //    throw new Exception("У клиента нет зарегистрированных каналов");

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
            MessageLogger.ProtocolMessage(message);

            var registeredLowLevelClient = GetRegisteredLowLevelClient(message);
            if (!registeredLowLevelClient.HasRegisteredLogicalChannels)
                throw new Exception("У клиента нет зарегистрированных каналов");

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
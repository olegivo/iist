using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using DMS.Common.MessageExchangeSystem.HighLevel;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.MES.Low;
using Oleg_ivo.MES.Registered;

using System.Linq;

namespace Oleg_ivo.MES.High
{
    ///<summary>
    /// Система обмена сообщениями c клиентами верхнего уровня
    ///</summary>
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Reentrant,
        AutomaticSessionShutdown = false,
        IncludeExceptionDetailInFaults = true)]
    [KnownType(typeof(RegisteredLogicalChannel))]
    public class HighLevelMessageExchangeSystem : AbstractLevelMessageExchangeSystem<RegisteredHighLevelClient>, IHighLevelMessageExchangeSystem
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Если клиента нет в списке заинтересованных клиентов, то он ни разу не запрашивал информацию о зарегистрированных каналах.
        /// </summary>
        private readonly List<RegisteredHighLevelClient> InterestedRegisteredClients = new List<RegisteredHighLevelClient>();

        #region Constructors

        #endregion

        /// <summary>
        /// Регистрационое имя системы обмена сообщений верхнего уровня
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

        #region Вспомогательные классы и свойства для регистрации

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
        /// Получить зарегистрированные в системе каналы
        /// </summary>
        /// <param name="message">Сообщение с идентификацией запрашивающего</param>
        /// <returns></returns>
        public RegisteredLogicalChannel[] GetRegisteredChannels(InternalMessage message)
        {
            bool success = true;
            var registeredHighLevelClient = GetRegisteredHighLevelClient(message);
            if (registeredHighLevelClient == null)
                throw new Exception(
                    string.Format(
                        "При попытке получить зарегистрированные в системе каналы произошла ошибка: клиент с регистрационным именем [{0}] не найден",
                        message.RegNameFrom));

            if (!InterestedRegisteredClients.Contains(registeredHighLevelClient))
                InterestedRegisteredClients.Add(registeredHighLevelClient);//добавляем к заинтересованным клиентам

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
                if (success)//если во время выполнения метода ничего не произошло, то ПОСЛЕ возврата метода добавляем клиента в доверенную зону
                {
                    registeredHighLevelClient.Interested = true;//ставим флаг заинтересованности
                }
            }
        }

        private delegate void ChannelSubscribeCaller(ChannelSubscribeMessage message);

        /// <summary>
        /// Начало подписки на чтение контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state)
        {
            log.Debug("Начало подписки на чтение канала {0}", message.LogicalChannelId);

            var caller = new ChannelSubscribeCaller(ChannelSubscribe);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение подписки на чтение контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelSubscribe(ChannelSubscribeMessage message, IAsyncResult result)
        {
            log.Info("Клиент был подписан на канал");
        }

        /// <summary>
        /// Начало отписки от чтения контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelUnSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state)
        {
            log.Debug("Начало отписки от чтение канала {0}", message.LogicalChannelId);

            var caller = new ChannelSubscribeCaller(ChannelUnSubscribe);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение отписки от чтения контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelUnSubscribe(ChannelSubscribeMessage message, IAsyncResult result)
        {
            log.Info("Клиент был отписан от канала");
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
        /// Подписка на чтение контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        private void ChannelSubscribe(ChannelSubscribeMessage message)
        {
            if (message.Mode != SubscribeMode.Subscribe)
                throw new ArgumentException("Для подписки на канал в сообщении используется флаг отписки");

            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message);
            if (registeredHighLevelClient != null)
                registeredHighLevelClient.ChannelSubscribe(message);

            InvokeChannelSubscribed(new HighRegisteredLogicalChannelSubscribeEventArgs(registeredHighLevelClient, message));
        }

        /// <summary>
        /// Отписка от чтения контролируемого канала (посылка сообщения от клиента о том, что он отписывается от канала)
        /// </summary>
        /// <param name="message"></param>
        private void ChannelUnSubscribe(ChannelSubscribeMessage message)
        {
            if (message.Mode != SubscribeMode.Unsubscribe)
                throw new ArgumentException("Для отписки от канала в сообщении используется флаг подписки");

            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message);
            if (registeredHighLevelClient != null)
                registeredHighLevelClient.ChannelUnSubscribe(message);

            InvokeChannelUnSubscribed(new HighRegisteredLogicalChannelSubscribeEventArgs(registeredHighLevelClient, message));
        }

        /// <summary>
        /// Возможность записи в управляемый канал
        /// </summary>
        /// <param name="message"></param>
        public void IsAllowWriteChannel(IInternalMessage message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Начало записи данных в управляемый канал
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginWriteChannel(InternalLogicalChannelDataMessage message, AsyncCallback callback, object state)
        {
            log.Debug("Начало записи канала {0}", message.LogicalChannelId);
            var caller = new WriteChannelCaller(WriteChannel);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение записи данных в управляемый канал
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndWriteChannel(InternalLogicalChannelDataMessage message, IAsyncResult result)
        {
            log.Info("Канал был записан");
        }

        /// <summary>
        /// Запись в управляемый канал
        /// </summary>
        /// <param name="message"></param>
        public void WriteChannel(InternalLogicalChannelDataMessage message)
        {
            if (message.DataMode == DataMode.Write)
            {
                //пришли новые данные по каналу. будем передавать вниз
                LowLevelMessageExchangeSystem.WriteChannel(message);
            }
            else
                log.Warn(
                    "Клиент [{0}] извещает о записи новых данных в канал [{1}]. "
                    + "Но в сообщении указан режим данных, отличный от записи",
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
        /// Регистрация клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        private void Register(RegistrationMessage message, IHighLevelClientCallback clientCallback)
        {
            if (message.RegistrationMode != RegistrationMode.Register)
                throw new ArgumentException("Для регистрации клиента в сообщении используется флаг отмены регистрации");

            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message, false);
            // Это ПЛОХОЙ алгоритм оповещения, поскольку для каждого клиента
            // создаётся отдельный поток. Следовало бы в место этого создавать
            // по одному поток на каждый тикер

            //при необходимости создаём рабочий объект, добавляем его
            //в хэш-таблицу и запускаем в отдельном потоке

            //todo: подумать над регистрацией с того же тикера
            bool isRegistered = registeredHighLevelClient != null && registeredHighLevelClient.HasCallbacks;

            if (!isRegistered)
            {
                if (registeredHighLevelClient == null)
                {
                    registeredHighLevelClient = new RegisteredHighLevelClient(this, message.DataMode) { Ticker = message.RegNameFrom };
                    AddClient(message.RegNameFrom, registeredHighLevelClient);
                }
                //                registeredHighLevelClient = (RegisteredHighLevelClient)_registeredClients[message.RegNameFrom];

                //Thread t = new Thread(w.RegisteredHighLevelClientProcess.SendUpdateToClient) { IsBackground = true };
                //t.Start();

                //получить рабочий объект из данного тикера и добавить
                //прокси клиента в список обратных вызовов

                registeredHighLevelClient.AddCallback(clientCallback);

                OnRegistered(registeredHighLevelClient, message);

                //todo: после регистрации сообщаем клиенту о всех зарегистрированных каналах (в дальнейшем может, через свойство?)
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
        /// Отмена регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        private void Unregister(RegistrationMessage message, IHighLevelClientCallback clientCallback)
        {
            if (message.RegistrationMode != RegistrationMode.Unregister)
                throw new ArgumentException("Для отмены регистрации клиента в сообщении используется флаг регистрации");

            //получить рабочий объект из данного тикера и удалить
            //прокси клиента из списка обратных вызовов
            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message);

            if (registeredHighLevelClient != null)
            {
                registeredHighLevelClient.RemoveCallback(clientCallback);//лишаем клиента уведомлений

                OnUnregistered(registeredHighLevelClient, message);//уведомляем о том, что клиент отменил регистрацию

                //                if (registeredHighLevelClient.HasCallbacks) 
                RemoveClient(message.RegNameFrom);

            }
        }

        #endregion

        /// <summary>
        /// Получить зарегистрированный логический канал
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public RegisteredLogicalChannelExtended GetRegisteredChannel(Func<RegisteredLogicalChannelExtended, bool> predicate)
        {
            //ищем в верхней службе, если не находим - ищем в нижней службе
            RegisteredLogicalChannelExtended channel = FindSubscribedChannel(predicate) ??
                                               LowLevelMessageExchangeSystem.GetRegisteredLogicalChannel(predicate);
            return channel;
        }

        /// <summary>
        /// Найти канал в верхней службе (если на него уже кто-то подписан)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private RegisteredLogicalChannelExtended FindSubscribedChannel(Func<RegisteredLogicalChannelExtended, bool> predicate)
        {
            RegisteredLogicalChannelExtended registeredLogicalChannel =
                RegisteredClients
                    .SelectMany(client => client.RegisteredLogicalChannels.Values)//выбираем все каналы всех клиентов
                    .Where(predicate).Distinct().FirstOrDefault();//TODO: попробовать когда на один канал подписано несколько клиентов
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
        /// Прочитать канал
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
                        "Клиент [{0}] извещает о чтении новых данных из канала [{1}]. "
                            + "Но он не может быть использован в режиме чтения. "
                            + "Проверьте настройки режима данных для канала",
                        message.LogicalChannelId, message.RegNameFrom);
            }
            else
            {
                log.Warn(
                    "Клиент [{0}] извещает о чтении новых данных из канала [{1}]. Но на него никто не подписан",
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
            //убираем зарегистрированные каналы
            var registeredHighLevelClient = this[clientRegName];

            foreach (int registeredLogicalChannelId in registeredHighLevelClient.RegisteredLogicalChannels.Keys.ToArray())
            {
                //имитируем посылку сообщения от клиента о том, что он отписывается от канала 
                //(тогда источники подписки будут об этом уведомлены)
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
        /// Начало регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginRegister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Debug("Начало регистрации клиента {0}", message.RegNameFrom);

            IHighLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<IHighLevelClientCallback>();

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

            NotifyRegisteredChannelsToClients();
        }

        /// <summary>
        /// Уведомить зарегистрированных новых клиентов о зарегистрированных в системе каналах
        /// </summary>
        private void NotifyRegisteredChannelsToClients()
        {
            var highLevelClients = RegisteredClients.Except(InterestedRegisteredClients);
            foreach (var client in highLevelClients)
            {
                var registeredChannels = GetRegisteredChannels(new InternalMessage(client.Ticker, null));
                foreach (var registeredChannel in registeredChannels)
                {
                    var registrationMessage = new ChannelRegistrationMessage(null,
                                                                             client.Ticker,
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

        /// <summary>
        /// Начало отмены регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginUnregister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            log.Debug("Начало отмены регистрации клиента {0}", message.RegNameFrom);

            IHighLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<IHighLevelClientCallback>();

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

        private delegate void RegistrationCaller(RegistrationMessage message, IHighLevelClientCallback clientCallback);
        private delegate void WriteChannelCaller(InternalLogicalChannelDataMessage message);
    }
}
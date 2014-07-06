using System.ServiceModel.Channels;
using System.Threading.Tasks;
using DMS.Common;
using NLog;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.Communication;
using Oleg_ivo.LowLevelClient.ServiceReferenceHomeTcp;
using DMS.Common.Events;
using System.ServiceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DMS.Common.Messages;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Tools.ExceptionCatcher;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// Модуль контроля и управления
    ///</summary>
    public class ControlManagementUnit : IClientBase
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private readonly Planner planner;
        private readonly IDistributedMeasurementInformationSystem distributedMeasurementInformationSystem;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ControlManagementUnit" />.
        /// </summary>
        /// <param name="planner"></param>
        /// <param name="distributedMeasurementInformationSystem"></param>
        public ControlManagementUnit(Planner planner, IDistributedMeasurementInformationSystem distributedMeasurementInformationSystem)
        {
            this.distributedMeasurementInformationSystem =
                Enforce.ArgumentNotNull(distributedMeasurementInformationSystem,
                    "distributedMeasurementInformationSystem");
            this.planner = Enforce.ArgumentNotNull(planner, "planner");
            this.planner.NewDadaReceived += planner_NewDadaReceived;

            callbackHandler = new CallbackHandler();
            callbackHandler.NeedProtocol += callbackHandler_NeedProtocol;
            callbackHandler.ChannelSubscribed += callbackHandler_ChannelSubscribed;
            callbackHandler.ChannelUnSubscribed += callbackHandler_ChannelUnSubscribed;
            callbackHandler.HasWriteChannel += callbackHandler_HasWriteChannel;

            reliableConnector = new ReliableConnector(this);
        }

        /// <summary>
        /// Записан канал
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelDataMessage>> HasWriteChannel
        {
            add { callbackHandler.HasWriteChannel += value; }
            remove { callbackHandler.HasWriteChannel -= value; }
        }

        void callbackHandler_HasWriteChannel(object sender, MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            var s = string.Format("Канал №{0} клиент - {1} [{2}] получено значение {3}",
                                     e.Message.LogicalChannelId,
                                     e.Message.RegNameFrom,
                                     e.Message.TimeStamp,
                                     e.Message.Value);
            Log.Debug(s);
            Protocol(s);
            OnWriteChannel(e.Message);
        }

        private void OnWriteChannel(InternalLogicalChannelDataMessage message)
        {
            //TODO:если канал в списке подписанных каналов, для которых разрешена запись извне, передавать в канал

        }

        /// <summary>
        /// 
        /// </summary>
        public void BuildSystemConfiguration()
        {
            if (distributedMeasurementInformationSystem != null)
                distributedMeasurementInformationSystem.BuildSystemConfiguration();
        }

        ///<summary>
        /// Логические каналы
        ///</summary>
        protected virtual IEnumerable<LogicalChannel> GetLogicalChannels()
        {
            return distributedMeasurementInformationSystem.PlcManager.LogicalChannels;
        }

        void planner_NewDadaReceived(object sender, NewDataReceivedEventArgs e)
        {
            if (IsCommunicationFailed)
            {
                Log.Error("Коммуникация с сервером нарушена. Остановка опросов всех каналов до восстановления связи.");
                planner.StopAllPolls();
                //TODO:возможно, не останавливать опрос каналов, но буферизовать полученные данные до восстановления связи, отправив их потом? Тогда нужна очередь
                return;
            }
            if (e.LogicalChannel.IsStateChannel)
            {
                //сообщение об изменении состояния канала (каналов)
                var value = (bool?)e.Value;
                //TODO: пока здесь анализируется только дискретное состояние (доступен/нудоступен)
                var state = value.HasValue && value.Value ? LogicalChannelState.Work : LogicalChannelState.Break;
                foreach (var logicalChannel in e.LogicalChannel.Entity.LogicalChannelStateHolders)
                {
                    ChangeChannelState(new InternalLogicalChannelStateMessage(RegName, null, logicalChannel.Id, state));
                }
            }
            else
            {
                //заставляем систему обмена сообщениями реагировать на новые данные:
                var message = new InternalLogicalChannelDataMessage(RegName, null, DataMode.Read, e.LogicalChannel.Id, e.LogicalChannel.IsDiscrete)
                {
                    Value = e.Value
                };
                ReadChannel(message);
            }
            //                var s = string.Format("{0} сообщает о новых данных [{1}]", measurementPoll.LogicalChannel, e.SignalTime);
            //                Protocol(s);
            //System.Windows.Forms.MessageBox.Show();
        }

        private void ChangeChannelState(InternalLogicalChannelStateMessage internalLogicalChannelStateMessage)
        {
            LowLevelMessageExchangeSystemClient.ChangeChannelStateAsync(internalLogicalChannelStateMessage);
        }

        public LowLevelMessageExchangeSystemClient LowLevelMessageExchangeSystemClient
        {
            get { return proxy; }
        }

        private InstanceContext site;
        private LowLevelMessageExchangeSystemClient proxy;

        public ICommunicationObject Proxy { get { return proxy; } }

        private void CreateProxy()
        {
            UnsubscribeProxy();

            Log.Debug("Создание канала связи");
            site = new InstanceContext(callbackHandler);
            if (proxy != null) 
                proxy.SafeClose();
            proxy = new LowLevelMessageExchangeSystemClient(site);

            reliableConnector.SetProxy(proxy);

            SubscribeProxy();
        }

        protected virtual void SubscribeProxy()
        {
            site.Faulted += site_Faulted;

            //proxy.InnerChannel.Faulted += reliableConnector.ProxyFaulted;

            proxy.RegisterCompleted += proxy_RegisterCompleted;
            proxy.UnregisterCompleted += proxy_UnregisterCompleted;
            proxy.SendErrorCompleted += proxy_SendErrorCompleted;
            proxy.ChannelRegisterCompleted += proxy_ChannelRegisterCompleted;
            proxy.ChannelUnRegisterCompleted += proxy_ChannelUnRegisterCompleted;
        }

        protected virtual void UnsubscribeProxy()
        {
            if (site != null)
            {
                site.Faulted -= site_Faulted;
            }
            if (proxy != null)
            {
                proxy.RegisterCompleted -= proxy_RegisterCompleted;
                proxy.UnregisterCompleted -= proxy_UnregisterCompleted;
                proxy.SendErrorCompleted -= proxy_SendErrorCompleted;
                proxy.ChannelRegisterCompleted -= proxy_ChannelRegisterCompleted;
                proxy.ChannelRegisterCompleted -= proxy_ChannelUnRegisterCompleted;
            }
        }

        void site_Faulted(object sender, EventArgs e)
        {
            Log.Error("Site faulted");//TODO:сюда добраться не ожидаем!
            var channel = sender as IChannel;
            if (channel != null)
            {
                channel.Abort();
                channel.Close();
            }

            //Disable the keep alive timer now that the channel is faulted
            //_keepAliveTimer.Stop();

            //The proxy channel should no longer be used
            AbortProxy();

            //Enable the try again timer and attempt to reconnect
            //reconnectTimer.Start();
        }

        public void AbortProxy()
        {
            if (proxy != null)
            {
                Log.Debug("Прекращение канала связи");
                proxy.Abort();
                proxy.Close();
                proxy = null;
            }
        }

        public bool IsRegistered
        {
            get { return isRegistered; }
        }

        protected string RegName
        {
            get
            {
                if (GetRegName == null)
                    throw new NullReferenceException("Не задан делегат GetRegName");

                return GetRegName();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Func<string> GetRegName { get; set; }

        private readonly CallbackHandler callbackHandler;
        private readonly ReliableConnector reliableConnector;

        void callbackHandler_ChannelSubscribed(object sender, MessageEventArgs<ChannelSubscribeMessage> e)
        {
            var channel =
                GetLogicalChannels().FirstOrDefault(LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));

            if (channel == null)
            {
                Log.Warn("Не удалось найти канал {0} для осуществления подписки. Канал подписан, но с ним ничего не будет происходить");
                return;
            }

            if (!channel.IsDiscrete)/*дискретные каналы не имеют состояния => всегда Work*/
            {
                var stateLogicalChannelId = channel.Entity.StateLogicalChannelId;
                if (stateLogicalChannelId.HasValue)
                {
                    var stateChannel = GetLogicalChannels().FirstOrDefault(LogicalChannel.GetFindChannelPredicate(stateLogicalChannelId.Value));
                    if (stateChannel != null)
                        planner.StartPoll(stateChannel);
                }
                
            }
            if (channel.IsInput)
            {
                Log.Info("{0} был подписан на получение новых данных", channel);
                Protocol(string.Format("{0} был подписан на получение новых данных", channel));
                //запустить таймер опроса для зарегистрированного канала после подписки на него
                planner.StartPoll(channel);
            }
            else if (channel.IsOutput)
            {
                Log.Info("{0} был подписан на установку новых данных", channel);
                //TODO:добавить канал в список подписанных каналов, для которых разрешена запись извне
                Protocol(string.Format("{0} был подписан на установку новых данных", channel));
            }
            else
            {
                throw new InvalidOperationException(string.Format("{0} имеет неизвестный тип и не может быть обработан при подписке", channel));
            }
        }

        void callbackHandler_ChannelUnSubscribed(object sender, MessageEventArgs<ChannelSubscribeMessage> e)
        {
            var channel =
                GetLogicalChannels().FirstOrDefault(LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));

            if (channel == null)
            {
                Log.Warn("Не удалось найти канал {0} для осуществления отписки. Тем не менее, канал отписан.");
                return;
            }

            var stateLogicalChannelId = channel.Entity.StateLogicalChannelId;
            if (stateLogicalChannelId.HasValue)
            {
                var stateChannel = GetLogicalChannels().FirstOrDefault(LogicalChannel.GetFindChannelPredicate(stateLogicalChannelId.Value));
                if (stateChannel != null)
                    planner.StopPoll(stateChannel);
            }

            if (channel.IsInput)
            {
                Log.Info("{0} был отписан от получения новых данных", channel);
                Protocol(string.Format("{0} был отписан от получения новых данных", channel));
                planner.StopPoll(channel);
            }
            else if (channel.IsOutput)
            {
                Log.Info("{0} был отписан от установки новых данных", channel);
                //TODO:удалить канал из списка подписанных каналов, для которых разрешена запись извне
                Protocol(string.Format("{0} был отписан от установки новых данных", channel));
            }
            else
            {
                throw new InvalidOperationException(string.Format("{0} имеет неизвестный тип и не может быть обработан при подписке", channel));
            }
        }

        void callbackHandler_NeedProtocol(object sender, EventArgs e)
        {
            if (sender is double || sender is string)
                Protocol(sender);
        }

        /// <summary>
        /// Протоколировать объект
        /// (генерируется событие NeedProtocol)
        /// </summary>
        /// <param name="sender"></param>
        protected void Protocol(object sender)
        {
            if (NeedProtocol != null)
                NeedProtocol(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Требуется протоколирование
        /// </summary>
        public event EventHandler<EventArgs> NeedProtocol;

        /// <summary>
        /// Послать в систему обмена сообщениями сообщение
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(InternalMessage message)
        {
            LowLevelMessageExchangeSystemClient.SendMessageAsync(message);
        }

        /// <summary>
        /// Послать в систему обмена сообщениями сообщение о новых данных
        /// </summary>
        /// <param name="message"></param>
        public void ReadChannel(InternalLogicalChannelDataMessage message)
        {
            LowLevelMessageExchangeSystemClient.ReadChannelAsync(message);
        }

        /// <summary>
        /// Послать в систему обмена сообщениями сообщение о регистрации
        /// </summary>
        public void Register()
        {
            lock (registerLock)
            {
                Log.Trace("Запуск синхронной регистрации");
                CreateProxy();
                var message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
                LowLevelMessageExchangeSystemClient.Register(message);
                isRegistered = true;
                if(RegisterCompleted!=null)
                    RegisterCompleted(this, new AsyncCompletedEventArgs(null, false, null));
            } 
        }

        /// <summary>
        /// Асинхронно послать в систему обмена сообщениями сообщение о регистрации
        /// </summary>
        public void RegisterAsync()
        {
            Log.Trace("Запуск асинхронной регистрации");
            CreateProxy();
            var message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
            LowLevelMessageExchangeSystemClient.RegisterAsync(message);
        }

        /// <summary>
        /// Послать в систему обмена сообщениями сообщение об отмене регистрации
        /// </summary>
        public void Unregister()
        {
            //перед тем, как совершить последнюю операцию сессии останавливаем все попытки посыла сообщений
            planner.StopAllPolls();
            //LowLevelMessageExchangeSystemClient.Unregister(new RegistrationMessage(GetRegName(), null, RegistrationMode.Unregister, DataMode.Unknown));
            LowLevelMessageExchangeSystemClient.UnregisterAsync(new RegistrationMessage(GetRegName(), null, RegistrationMode.Unregister, DataMode.Unknown));
        }

        /// <summary>
        /// Отмена регистрации завершена
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted;

        private void proxy_UnregisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            lock (registerLock) isRegistered = e.Error != null;
            if (isRegistered)
                Log.Info("Отмена регистрации на сервере завершилась успешно");
            else
                Log.Error("Отмена регистрации на сервере завершилась неудачно:\n{0}", e.Error);
            
            if (UnregisterCompleted != null) 
                UnregisterCompleted(this, e);

            Task.Factory.StartNew(() => LowLevelMessageExchangeSystemClient.Disconnect(GetRegName()))
                .ContinueWith(task => Log.Info("Disconnected"));
        }

        /// <summary>
        /// Регистрация завершена
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> RegisterCompleted;

        private void proxy_RegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                lock (registerLock) isRegistered = e.Error == null;
                
                if (isRegistered)
                    Log.Info("Регистрация на сервере завершилась успешно");
                else
                {
                    Log.Error("Регистрация на сервере завершилась неудачно:\n{0}", e.Error);
                    throw e.Error;
                }
            }
            finally
            {
                if (RegisterCompleted != null)
                    RegisterCompleted(this, e);
            }
        }

        public event EventHandler<AsyncCompletedEventArgs> SendErrorCompleted;

        private void proxy_SendErrorCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (SendErrorCompleted != null)
                SendErrorCompleted(this, e);
        }

        public event EventHandler<AsyncCompletedEventArgs> ChannelRegisterCompleted;

        void proxy_ChannelRegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (ChannelRegisterCompleted != null)
                ChannelRegisterCompleted(this, e);
        }

        public event EventHandler<AsyncCompletedEventArgs> ChannelUnRegisterCompleted;

        void proxy_ChannelUnRegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (ChannelUnRegisterCompleted != null)
                ChannelUnRegisterCompleted(this, e);
        }

        /// <summary>
        /// Получить доступные логические каналы
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LogicalChannel> GetAvailableLogicalChannels(bool withStateChannels = false)
        {
            //добавляем только проидентифицированные каналы (Id > 0):
            var channels = GetLogicalChannels()
                .Where(channel => channel.Id > 0 && (!channel.IsStateChannel || withStateChannels)).ToList();
            return
                channels;
        }


        /// <summary>
        /// Добавить опрос канала
        /// </summary>
        /// <param name="logicalChannel"></param>
        /// <exception cref="Exception"></exception>
        public bool TryAddPoll(LogicalChannel logicalChannel)
        {
            if (logicalChannel == null) return false;

            var channel =
                GetLogicalChannels()
                    .FirstOrDefault(LogicalChannel.GetFindChannelPredicate(logicalChannel.Id));

            if (channel == null)
                throw new Exception("Канал не найден");

            if (channel.PollPeriod == TimeSpan.Zero)
                channel.PollPeriod = TimeSpan.FromMilliseconds(1000);
            
            planner.AddPoll(channel);
            return true;
        }

        /// <summary>
        /// Удалить опрос канала
        /// </summary>
        /// <param name="logicalChannel"></param>
        /// <exception cref="Exception"></exception>
        public bool TryRemovePoll(LogicalChannel logicalChannel)
        {
            if (logicalChannel == null) return false;

            var channel =
                GetLogicalChannels()
                    .AsEnumerable()
                    .FirstOrDefault(LogicalChannel.GetFindChannelPredicate(logicalChannel.Id));

            if (channel == null)
                throw new Exception("Канал не найден");

            planner.RemovePoll(channel);
            return true;
        }

        /// <summary>
        /// Зарегистрировать канал в системе обмена сообщениями
        /// </summary>
        /// <param name="channelRegistrationMessage"></param>
        public void RegisterChannel(ChannelRegistrationMessage channelRegistrationMessage)
        {
            LowLevelMessageExchangeSystemClient.ChannelRegisterAsync(channelRegistrationMessage, channelRegistrationMessage);
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(channelRegistrationMessage.LogicalChannelId));

            Log.Info("{0} зарегистрирован в системе обмена сообщениями", channel);
            Protocol(string.Format("{0} зарегистрирован в системе обмена сообщениями", channel));
        }

        /// <summary>
        /// Отменить регистрацию канала в системе обмена сообщениями
        /// </summary>
        /// <param name="channelRegistrationMessage"></param>
        public void UnregisterChannel(ChannelRegistrationMessage channelRegistrationMessage)
        {
            LowLevelMessageExchangeSystemClient.ChannelUnRegisterAsync(channelRegistrationMessage, channelRegistrationMessage);
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(channelRegistrationMessage.LogicalChannelId));

            Log.Info("{0} отмена регистрации в системе обмена сообщениями", channel);
            Protocol(string.Format("{0} отмена регистрации в системе обмена сообщениями", channel));
        }

        private readonly object registerLock = new object();

        public bool IsCommunicationFailed
        {
            get
            {
                lock (registerLock)
                {
                    Log.Trace("Проверка коммуникаций");
                    if (LowLevelMessageExchangeSystemClient == null)
                    {
                        Log.Warn("Прокси не создан");
                        return true;
                    }
                    if (LowLevelMessageExchangeSystemClient.State == CommunicationState.Faulted)
                    {
                        Log.Warn("Канал связи нарушен");
                        return true;
                    }
                    return false;
                }
            }
        }

        private bool isRegistered;

        public void SendErrorAsync(ExtendedThreadExceptionEventArgs e)
        {
            LowLevelMessageExchangeSystemClient.SendErrorAsync(
                new InternalErrorMessage(GetRegName(), null, e.Exception), e);
        }

        public void Dispose()
        {
            if (LowLevelMessageExchangeSystemClient != null)
                LowLevelMessageExchangeSystemClient.SafeClose();
        }
    }
}
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
using Timer = System.Timers.Timer;

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
            this.planner.NewDadaReceived += Instance_NewDadaReceived;

            //TODO:Timers to RX
            reconnectTimer = new Timer(5000);
            reconnectTimer.Elapsed += reconnectTimer_Elapsed;

            CallbackHandler.NeedProtocol += CallbackHandler_NeedProtocol;
            CallbackHandler.ChannelSubscribed += CallbackHandler_ChannelSubscribed;
            CallbackHandler.ChannelUnSubscribed += CallbackHandler_ChannelUnSubscribed;
            CallbackHandler.HasWriteChannel += CallbackHandler_HasWriteChannel;
        }

        /// <summary>
        /// Записан канал
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelDataMessage>> HasWriteChannel
        {
            add { CallbackHandler.HasWriteChannel += value; }
            remove { CallbackHandler.HasWriteChannel -= value; }
        }

        void CallbackHandler_HasWriteChannel(object sender, MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            string s = string.Format("Канал №{0} клиент - {1} [{2}] получено значение {3}",
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


        void Instance_NewDadaReceived(object sender, NewDataReceivedEventArgs e)
        {
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
            if (IsCommunicationFailed)
            {
                Log.Error("Communication failed");
                return;
            }
            LowLevelMessageExchangeSystemClient.ChangeChannelStateAsync(internalLogicalChannelStateMessage);
        }

        public LowLevelMessageExchangeSystemClient LowLevelMessageExchangeSystemClient
        {
            get { return proxy; }
        }

        private InstanceContext site;
        private LowLevelMessageExchangeSystemClient proxy;

        private void CreateProxy()
        {
            UnsubscribeProxy();

            site = new InstanceContext(CallbackHandler);
            if (proxy != null) 
                proxy.SafeClose();
            proxy = new LowLevelMessageExchangeSystemClient(site);
            
            SubscribeProxy();
        }

        protected virtual void SubscribeProxy()
        {
            site.Faulted += site_Faulted;
            proxy.RegisterCompleted += proxy_RegisterCompleted;
            proxy.UnregisterCompleted += proxy_UnregisterCompleted;
            proxy.SendErrorCompleted += proxy_SendErrorCompleted;
        }

        protected virtual void UnsubscribeProxy()
        {
            if (site != null)
            {
                site.Faulted -= site_Faulted;
            }
            if (proxy != null)
            {
                proxy.UnregisterCompleted -= proxy_UnregisterCompleted;
            }
        }

        void site_Faulted(object sender, EventArgs e)
        {
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
            reconnectTimer.Start();
        }

        public void AbortProxy()
        {
            if (proxy != null)
            {
                proxy.Abort();
                proxy.Close();
                proxy = null;
            }
        }

        private void reconnectTimer_Elapsed(object sender, EventArgs e)
        {
            if (proxy == null)
                CreateProxy();
            
            if (proxy != null)
            {
                reconnectTimer.Stop();
                //_keepAliveTimer.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        protected CallbackHandler CallbackHandler
        {
            get { return callbackHandler ?? (callbackHandler = new CallbackHandler()); }
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

        private CallbackHandler callbackHandler;
        private readonly Timer reconnectTimer;

        void CallbackHandler_ChannelSubscribed(object sender, MessageEventArgs<ChannelSubscribeMessage> e)
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

        void CallbackHandler_ChannelUnSubscribed(object sender, MessageEventArgs<ChannelSubscribeMessage> e)
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

        void CallbackHandler_NeedProtocol(object sender, EventArgs e)
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
            if (IsCommunicationFailed)
            {
                Log.Error("Communication failed");
                return;
            }

            LowLevelMessageExchangeSystemClient.ReadChannelAsync(message);
        }

        /// <summary>
        /// Послать в систему обмена сообщениями сообщение о регистрации
        /// </summary>
        public void Register()
        {
            CreateProxy();
            RegistrationMessage message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
            LowLevelMessageExchangeSystemClient.Register(message);
        }

        /// <summary>
        /// Асинхронно послать в систему обмена сообщениями сообщение о регистрации
        /// </summary>
        public void RegisterAsync()
        {
            CreateProxy();
            RegistrationMessage message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
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
            if (e.Error == null)
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
            if (e.Error != null)
                Log.Error("Регистрация на сервере завершилась неудачно:\n{0}", e.Error);
            else
                Log.Info("Регистрация на сервере завершилась успешно");

            if (RegisterCompleted != null)
                RegisterCompleted(this, e);
        }

        public event EventHandler<AsyncCompletedEventArgs> SendErrorCompleted;

        private void proxy_SendErrorCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (SendErrorCompleted != null)
                SendErrorCompleted(this, e);
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

            double interval;
            if (channel.PollPeriod == TimeSpan.Zero)
                channel.PollPeriod = TimeSpan.FromMilliseconds(1000);

            string s = (channel.PollPeriod ?? TimeSpan.FromSeconds(5)).TotalMilliseconds.ToString();

            /*
                        s = InputBox.Show("Укажите интервал опроса канала (в миллисекундах)",
                                          string.Format("Интервал опроса канала [{0}]", channel),
                                          "1000");
                */


            if (double.TryParse(s, out interval))
            {
                planner.AddPoll(channel, interval);
                return true;
            }
            
            MessageBox.Show("Указанное значение не является корректным", "Ошибка указания интервала опроса канала",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
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
            LowLevelMessageExchangeSystemClient.ChannelRegisterAsync(channelRegistrationMessage);
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
            LowLevelMessageExchangeSystemClient.ChannelUnRegisterAsync(channelRegistrationMessage);
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(channelRegistrationMessage.LogicalChannelId));

            Log.Info("{0} отмена регистрации в системе обмена сообщениями", channel);
            Protocol(string.Format("{0} отмена регистрации в системе обмена сообщениями", channel));
        }

        public bool IsCommunicationFailed
        {
            get { return LowLevelMessageExchangeSystemClient.State != CommunicationState.Opened; }
        }


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
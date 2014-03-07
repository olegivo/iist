using System.ServiceModel.Channels;
using NLog;
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
using Oleg_ivo.Tools.UI;
using Timer = System.Timers.Timer;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// Модуль контроля и управления
    ///</summary>
    public class ControlManagementUnit : IDisposable
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ControlManagementUnit" />.
        /// Для того, чтобы построить конфигурацию каналов, следует задать делегат <see cref="GetDistributedMeasurementInformationSystem"/> 
        /// и вызвать метода <see cref="BuildSystemConfiguration"/>
        /// </summary>
        public ControlManagementUnit()
        {
            reconnectTimer = new Timer(5000);
            reconnectTimer.Elapsed += _reconnectTimer_Elapsed;

            CallbackHandler.NeedProtocol += CallbackHandler_NeedProtocol;
            CallbackHandler.ChannelSubscribed += CallbackHandler_ChannelSubscribed;
            CallbackHandler.ChannelUnSubscribed += CallbackHandler_ChannelUnSubscribed;
            CallbackHandler.HasWriteChannel += CallbackHandler_HasWriteChannel;

            Planner = Planner.Instance;
            Planner.NewDadaReceived += Instance_NewDadaReceived;
        }

        /// <summary>
        /// Записан канал
        /// </summary>
        public event EventHandler<DataEventArgs> HasWriteChannel
        {
            add { CallbackHandler.HasWriteChannel += value; }
            remove { CallbackHandler.HasWriteChannel -= value; }
        }

        void CallbackHandler_HasWriteChannel(object sender, DataEventArgs e)
        {
            string s = string.Format("Канал №{0} клиент - {1} [{2}] получено значение {3}",
                                     e.Message.LogicalChannelId,
                                     e.Message.RegNameFrom,
                                     e.Message.TimeStamp,
                                     e.Message.Value);
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
            if (DistributedMeasurementInformationSystem != null)
                DistributedMeasurementInformationSystem.BuildSystemConfiguration();
        }

        /// <summary>
        /// Планировщик опроса каналов
        /// </summary>
        private Planner Planner { get; set; }

        ///<summary>
        /// Распределённая информационно-измерительная система (фасад)
        ///</summary>
        private IDistributedMeasurementInformationSystem DistributedMeasurementInformationSystem
        {
            get
            {
                if (GetDistributedMeasurementInformationSystem == null)
                    throw new NullReferenceException(
                        "Не указан делегат для получения фасада распределённой информационно-измерительной системы");
                return GetDistributedMeasurementInformationSystem();
            }
        }

        /// <summary>
        /// Делегат для получения фасада распределённой информационно-измерительной системы
        /// </summary>
        public GetDistributedMeasurementInformationSystemDelegate GetDistributedMeasurementInformationSystem { private get; set; }

        /// <summary>
        /// Делегат для получения фасада распределённой информационно-измерительной системы
        /// </summary>
        /// <returns></returns>
        public delegate IDistributedMeasurementInformationSystem GetDistributedMeasurementInformationSystemDelegate();

        ///<summary>
        /// Логические каналы
        ///</summary>
        protected virtual IEnumerable<LogicalChannel> GetLogicalChannels()
        {
            return DistributedMeasurementInformationSystem.PlcManager.LogicalChannels;
        }


        void Instance_NewDadaReceived(object sender, NewDataReceivedEventArgs e)
        {
            InternalLogicalChannelDataMessage message = new InternalLogicalChannelDataMessage(RegName, null, DataMode.Read,
                                                                                              e.LogicalChannel.Id)
                                                            {
                                                                Value = e.Value
                                                            };

            //заставляем систему обмена сообщениями реагировать на новые данные:
            ReadChannel(message);
            //                var s = string.Format("{0} сообщает о новых данных [{1}]", measurementPoll.LogicalChannel, e.SignalTime);
            //                Protocol(s);
            //System.Windows.Forms.MessageBox.Show();
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
            proxy = new LowLevelMessageExchangeSystemClient(site);
            
            SubscribeProxy();
        }

        protected virtual void SubscribeProxy()
        {
            site.Faulted += site_Faulted;
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

        private void _reconnectTimer_Elapsed(object sender, System.EventArgs e)
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

        private CallbackHandler callbackHandler;
        private readonly Timer reconnectTimer;

        /// <summary>
        /// 
        /// </summary>
        public virtual GetRegNameDelegate GetRegName { protected get; set; }

        void CallbackHandler_ChannelSubscribed(object sender, ChannelSubscribeEventArgs e)
        {
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));

            if (channel == null)
            {
                Log.Warn("Не удалось найти канал {0} для осуществления подписки. Канал подписан, но с ним ничего не будет происходить");
                return;
            }

            if (channel.IsInput)
            {
                Protocol(string.Format("{0} был подписан на получение новых данных", channel));
                //запустить таймер опроса для зарегистрированного канала после подписки на него
                Planner.StartPoll(channel);
            }
            else if (channel.IsOutput)
            {
                //TODO:добавить канал в список подписанных каналов, для которых разрешена запись извне
                Protocol(string.Format("{0} был подписан на установку новых данных", channel));
            }
            else
            {
                throw new InvalidOperationException(string.Format("{0} имеет неизвестный тип и не может быть обработан при подписке", channel));
            }
        }

        void CallbackHandler_ChannelUnSubscribed(object sender, ChannelSubscribeEventArgs e)
        {
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));

            if (channel == null)
            {
                Log.Warn("Не удалось найти канал {0} для осуществления отписки. Тем не менее, канал отписан.");
                return;
            }

            if (channel.IsInput)
            {
                Protocol(string.Format("{0} был отписан от получения новых данных", channel));
                Planner.StopPoll(channel);
            }
            else if (channel.IsOutput)
            {
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
            LowLevelMessageExchangeSystemClient.ReadChannelAsync(message);
        }

        /// <summary>
        /// Послать в систему обмена сообщениями сообщение о регистрации
        /// </summary>
        public void Register()
        {
            RegistrationMessage message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
            LowLevelMessageExchangeSystemClient.Register(message);
        }

        /// <summary>
        /// Асинхронно послать в систему обмена сообщениями сообщение о регистрации
        /// </summary>
        public void RegisterAsync()
        {
            RegistrationMessage message = new RegistrationMessage(RegName, null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
            LowLevelMessageExchangeSystemClient.RegisterAsync(message);
        }

        /// <summary>
        /// Послать в систему обмена сообщениями сообщение об отмене регистрации
        /// </summary>
        public void Unregister()
        {
            LowLevelMessageExchangeSystemClient.UnregisterAsync(new RegistrationMessage(GetRegName(), null, RegistrationMode.Unregister, DataMode.Unknown));
        }

        /// <summary>
        /// Отмена регистрации завершена
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted;

        public event EventHandler<AsyncCompletedEventArgs> SendErrorCompleted;

        private void proxy_UnregisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (UnregisterCompleted != null) 
                UnregisterCompleted(this, e);
        }

        private void proxy_SendErrorCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (SendErrorCompleted != null)
                SendErrorCompleted(this, e);
        }

        /// <summary>
        /// Получить доступные логические каналы
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LogicalChannel> GetAvailableLogicalChannels()
        {
            //добавляем только проидентифицированные каналы (Id > 0):
            return
                GetLogicalChannels()
                    .Where(channel => channel.Id > 0);
        }


        /// <summary>
        /// Добавить опрос канала
        /// </summary>
        /// <param name="e"></param>
        /// <param name="synchronizingObject"></param>
        /// <exception cref="Exception"></exception>
        public void TryAddPoll(MovingEventArgs e, ISynchronizeInvoke synchronizingObject)
        {
            var logicalChannel = e.MovingObject as LogicalChannel;
            if (logicalChannel == null) return;

            var channel =
                GetLogicalChannels()
                    .AsEnumerable()
                    .FirstOrDefault(LogicalChannel.GetFindChannelPredicate(logicalChannel.Id));

            if (channel == null)
                throw new Exception("Канал не найден");

            double interval;
            if (channel.PollPeriod == TimeSpan.Zero)
                channel.PollPeriod = TimeSpan.FromMilliseconds(1000);

            string s = channel.PollPeriod.TotalMilliseconds.ToString();

            /*
                        s = InputBox.Show("Укажите интервал опроса канала (в миллисекундах)",
                                          string.Format("Интервал опроса канала [{0}]", channel),
                                          "1000");
                */


            if (double.TryParse(s, out interval))
            {
                Planner.AddPoll(channel, interval, synchronizingObject);
            }
            else
            {
                MessageBox.Show("Указанное значение не является корректным", "Ошибка указания интервала опроса канала",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Удалить опрос канала
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        public void TryRemovePoll(MovingEventArgs e)
        {
            var logicalChannel = e.MovingObject as LogicalChannel;
            if (logicalChannel == null) return;
            var channel =
                GetLogicalChannels()
                    .AsEnumerable()
                    .FirstOrDefault(LogicalChannel.GetFindChannelPredicate(logicalChannel.Id));

            if (channel == null)
                throw new Exception("Канал не найден");

            Planner.RemovePoll(channel);
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

            Protocol(string.Format("{0} отмена регистрации в системе обмена сообщениями", channel));
        }

        /// <summary>
        /// Метод должен быть вызван перед первым использованием
        /// </summary>
        public void Init()
        {
            if(proxy==null)
                CreateProxy();
        }

        public void SendErrorAsync(InternalErrorMessage message, ExtendedThreadExceptionEventArgs e)
        {
            LowLevelMessageExchangeSystemClient.SendErrorAsync(message,e);
        }

        public void Dispose()
        {
            if (LowLevelMessageExchangeSystemClient != null)
                LowLevelMessageExchangeSystemClient.SafeClose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public delegate string GetRegNameDelegate();
}
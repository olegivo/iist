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
using Oleg_ivo.Tools.UI;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// Модуль контроля и управления
    ///</summary>
    public class ControlManagementUnit
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ControlManagementUnit" />.
        /// Для того, чтобы построить конфигурацию каналов, следует задать делегат <see cref="GetDistributedMeasurementInformationSystem"/> 
        /// и вызвать метода <see cref="BuildSystemConfiguration"/>
        /// </summary>
        public ControlManagementUnit()
        {
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
        private DistributedMeasurementInformationSystemBase DistributedMeasurementInformationSystem
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
        public delegate DistributedMeasurementInformationSystemBase GetDistributedMeasurementInformationSystemDelegate();

        ///<summary>
        /// Логические каналы
        ///</summary>
        protected virtual IEnumerable<LogicalChannel> GetLogicalChannels()
        {
            return DistributedMeasurementInformationSystem.PlcManagerBase.LogicalChannels;
        }


        void Instance_NewDadaReceived(object sender, NewDataReceivedEventArgs e)
        {
            InternalLogicalChannelDataMessage message = new InternalLogicalChannelDataMessage(null, RegName, DataMode.Read,
                                                                                              e.LogicalChannel.Id)
                //TODO: заполнить RegNameFrom          
                                                            {
                                                                Value = e.Value
                                                            };

            //заставляем систему обмена сообщениями реагировать на новые данные:
            ReadChannel(message);
            //                var s = string.Format("{0} сообщает о новых данных [{1}]", measurementPoll.LogicalChannel, e.SignalTime);
            //                Protocol(s);
            //System.Windows.Forms.MessageBox.Show();
        }

        private LowLevelMessageExchangeSystemClient LowLevelMessageExchangeSystemClient
        {
            get { return Proxy; }
        }

        static InstanceContext site;
        static LowLevelMessageExchangeSystemClient proxy;

        /// <summary>
        /// 
        /// </summary>
        public static LowLevelMessageExchangeSystemClient Proxy
        {
            get
            {
                if (proxy == null)
                {
                    CallbackHandler callbackHandler = new CallbackHandler();
                    site = new InstanceContext(callbackHandler);
                    proxy = new LowLevelMessageExchangeSystemClient(site);
                }
                return proxy;
            }
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

        private GetRegNameDelegate getRegName;

        /// <summary>
        /// 
        /// </summary>
        public virtual GetRegNameDelegate GetRegName
        {
            protected get { return getRegName; }
            set { getRegName = value; }
        }

        void CallbackHandler_ChannelSubscribed(object sender, ChannelSubscribeEventArgs e)
        {
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));


            Protocol(string.Format("{0} был подписан на получение новых данных", channel));
            //запустить таймер опроса для зарегистрированного канала после подписки на него
            Planner.StartPoll(channel);
        }

        void CallbackHandler_ChannelUnSubscribed(object sender, ChannelSubscribeEventArgs e)
        {
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));

            Protocol(string.Format("{0} был отписан от получения новых данных", channel));

            Planner.StopPoll(channel);
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
        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted
        {
            add { LowLevelMessageExchangeSystemClient.UnregisterCompleted += value; }
            remove { LowLevelMessageExchangeSystemClient.UnregisterCompleted -= value; }
        }

        /// <summary>
        /// Получить доступные логические каналы
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetAvailableLogicalChannels()
        {
            //добавляем только проидентифицированные каналы (Id > 0):
            return 
                GetLogicalChannels()
                    .Select(channel => channel.Id > 0 ? channel.Id : 0)
                        .Where(i => i > 0);

        }


        /// <summary>
        /// Добавить опрос канала
        /// </summary>
        /// <param name="e"></param>
        /// <param name="synchronizingObject"></param>
        /// <exception cref="Exception"></exception>
        public void TryAddPoll(MovingEventArgs e, ISynchronizeInvoke synchronizingObject)
        {
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate((int)e.MovingObject));

            if (channel == null)
                throw new Exception("Канал не найден");

            double interval;
            if (channel.PollPeriod == TimeSpan.Zero)
                channel.PollPeriod = TimeSpan.FromMilliseconds(1000);

            string s;

            s = channel.PollPeriod.TotalMilliseconds.ToString();

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
            LogicalChannel channel =
                GetLogicalChannels().AsEnumerable()
                        .FirstOrDefault(LogicalChannel.GetFindChannelPredicate((int)e.MovingObject));

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
    }

    /// <summary>
    /// 
    /// </summary>
    public delegate string GetRegNameDelegate();
}
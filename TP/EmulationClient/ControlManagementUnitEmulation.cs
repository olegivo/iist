using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DMS.Common.Events;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Tools.UI;

namespace EmulationClient
{
    /// <summary>
    /// Модуль контроля и управления для эмуляции
    /// </summary>
    public class ControlManagementUnitEmulation : ControlManagementUnit, INotifyPropertyChanged
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ControlManagementUnitEmulation" />.
        /// </summary>
        public ControlManagementUnitEmulation()
        {
            CanRegister = true;
            
            NeedProtocol += ControlManagementUnitEmulation_NeedProtocol;
            Proxy.RegisterCompleted += Proxy_RegisterCompleted;
            Proxy.UnregisterCompleted += Proxy_UnregisterCompleted;
            Proxy.ChannelRegisterCompleted += Proxy_ChannelRegisterCompleted;
            Proxy.ChannelUnRegisterCompleted += Proxy_ChannelUnRegisterCompleted;
        }

        public event EventHandler<DataEventArgs> HasWriteChannel
        {
            add { CallbackHandler.HasWriteChannel += value; }
            remove { CallbackHandler.HasWriteChannel -= value; }
        }

        void Proxy_ChannelUnRegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string s = string.Format("отмена регистрации канала {0} на сервере завершилась {1}", e.UserState, e.Error == null ? "успешно" : string.Format("неудачно: {0}", e.Error));
            Protocol(s);
            RegisteredChannelsCount--;
        }

        void Proxy_ChannelRegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string s = string.Format("Регистрация канала {0} на сервере завершилась {1}", e.UserState, e.Error == null ? "успешно" : string.Format("неудачно: {0}", e.Error));
            Protocol(s);
            RegisteredChannelsCount++;
        }

        protected int RegisteredChannelsCount
        {
            get
            {
                return registeredChannelsCount;
            }
            set
            {
                if (registeredChannelsCount != value)
                {
                    registeredChannelsCount = value;
                    //Console.WriteLine("RegisteredChannelsCount = {0}", RegisteredChannelsCount);
                    CanUnregisterChannels = registeredChannelsCount > 0;
                }
            }
        }

        void Proxy_UnregisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string s = string.Format("Отмена регистрации на сервере завершилась {0}", e.Error == null ? "успешно" : string.Format("неудачно: {0}", e.Error));
            Protocol(s);
            CanRegister = true;
        }

        void Proxy_RegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string s = string.Format("Регистрация на сервере завершилась {0}", e.Error == null ? "успешно" : string.Format("неудачно: {0}", e.Error));
            Protocol(s);
            if (AutoRegisterAllChannels) RegisterAllChannels();
            CanRegister = false;
        }

        internal bool AutoRegisterAllChannels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void RegisterAllChannels()
        {
            foreach (LogicalChannel channel in LogicalChannels)
            {
                Proxy.ChannelRegisterAsync(
                    new ChannelRegistrationMessage(RegName, null, RegistrationMode.Register,
                                                   channel.Id > 100 ? DataMode.Write : DataMode.Read, channel.Id),
                    channel.Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnregisterAllChannels()
        {
            foreach (LogicalChannel channel in LogicalChannels)
            {
                Proxy.ChannelUnRegisterAsync(new ChannelRegistrationMessage(RegName, null, RegistrationMode.Unregister,
                                                                            DataMode.Unknown, channel.Id));
            }
        }

        void ControlManagementUnitEmulation_NeedProtocol(object sender, EventArgs e)
        {
            _log.Debug("{0}", sender);
        }

        private IEnumerable<LogicalChannel> _logicalChannels;


        public List<LogicalChannel> LogicalChannels
        {
            get { return GetLogicalChannels() as List<LogicalChannel>; }
            set
            {
                _logicalChannels = value;
                if (LogicalChannels != null)
                {
                    foreach (var logicalChannel in LogicalChannels)
                    {
                        TryAddPoll(new MovingEventArgs(DoubleListBoxControl.Direction.LeftToRight, logicalChannel.Id),
                                   new Control());
                    }
                }
            }
        }

        ///<summary>
        /// Логические каналы
        ///</summary>
        protected override IEnumerable<LogicalChannel> GetLogicalChannels()
        {
            return _logicalChannels;
        }

        private bool canRegister;

        public bool CanUnregister
        {
            get { return !canRegister; }
        }

        public bool CanRegister
        {
            get { return canRegister; }
            set
            {
                if (canRegister != value)
                {
                    canRegister = value;
                    InvokePropertyChanged("CanRegister");
                    InvokePropertyChanged("CanUnregister");
                    InvokePropertyChanged("CanRegisterChannels");
                    InvokePropertyChanged("CanUnregisterChannels");
                }
            }
        }

        private bool canUnregisterChannels;
        private int registeredChannelsCount;

        public bool CanRegisterChannels
        {
            get { return !CanUnregisterChannels && CanUnregister; }
        }

        public bool CanUnregisterChannels
        {
            get { return canUnregisterChannels; }
            set
            {
                if (canUnregisterChannels != value)
                {
                    canUnregisterChannels = value;
                    InvokePropertyChanged("CanUnregisterChannels");
                    InvokePropertyChanged("CanRegisterChannels");
                }
            }
        }

        #region Implementation of INotifyPropertyChanged

        /// <summary>
        /// Возникает при изменениях значения свойства.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
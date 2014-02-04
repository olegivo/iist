using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
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
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ControlManagementUnitEmulation" />.
        /// </summary>
        public ControlManagementUnitEmulation()
        {
            CanRegister = true;
            IsModulationMode = true; 
            NeedProtocol += ControlManagementUnitEmulation_NeedProtocol;
            Proxy.RegisterCompleted += Proxy_RegisterCompleted;
            Proxy.UnregisterCompleted += Proxy_UnregisterCompleted;
            Proxy.ChannelRegisterCompleted += Proxy_ChannelRegisterCompleted;
            Proxy.ChannelUnRegisterCompleted += Proxy_ChannelUnRegisterCompleted;
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
                    //Log.Debug("RegisteredChannelsCount = {0}", RegisteredChannelsCount);
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

            Temperatura6 = 120;
            Temperatura7 = 110;
            ConcentrationO2 = 5;
            ConcentrationCO = 3000;
            ConcentrationSO2 = 500;
            ConcentrationNO = 600;
            ConcentrationNO2 = 650;
        }

        internal bool AutoRegisterAllChannels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void RegisterAllChannels()
        {
            foreach (LogicalChannel channel in LogicalChannels)
            {
                DataMode dataMode = DataMode.Unknown;
                if (channel.IsOutput) 
                    dataMode |= DataMode.Write;
                if (channel.IsInput)
                    dataMode |= DataMode.Read;
                
                if(dataMode==DataMode.Unknown)
                {
                    throw new ArgumentOutOfRangeException("Невозможно определить режим данных для данного канала" + channel);
                }

                Proxy.ChannelRegisterAsync(
                    new ChannelRegistrationMessage(RegName, null, RegistrationMode.Register,
                                                   dataMode, channel.Id),
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
            Log.Debug("{0}", sender);
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

        private int _temperatura6;
        public int Temperatura6
        {
            get{ return _temperatura6;}
            set
            {
                if (_temperatura6 != value)
                {
                    _temperatura6 = value;
                    InvokePropertyChanged("Temperatura6");
                }
            }
        }

        private int _temperatura7;
        public int Temperatura7
        {
            get { return _temperatura7; }
            set
            {
                if (_temperatura7 != value)
                {
                    _temperatura7 = value;
                    InvokePropertyChanged("Temperatura7");
                }
            }
        }

        private int _concentrationO2;
        public int ConcentrationO2
        {
            get { return _concentrationO2; }
            set
            {
                if (_concentrationO2 != value)
                {
                    _concentrationO2 = value;
                    InvokePropertyChanged("ConcentrationO2");
                }
            }
        }

        private int _concentrationCO;
        public int ConcentrationCO
        {
            get { return _concentrationCO; }
            set
            {
                if (_concentrationCO != value)
                {
                    _concentrationCO = value;
                    InvokePropertyChanged("ConcentrationCO");
                }
            }
        }

        private int _concentrationSO2;
        public int ConcentrationSO2
        {
            get { return _concentrationSO2; }
            set
            {
                if (_concentrationSO2 != value)
                {
                    _concentrationSO2 = value;
                    InvokePropertyChanged("ConcentrationSO2");
                }
            }
        }

        private int _concentrationNO;
        public int ConcentrationNO
        {
            get { return _concentrationNO; }
            set
            {
                if (_concentrationNO != value)
                {
                    _concentrationNO = value;
                    InvokePropertyChanged("ConcentrationNO");
                }
            }
        }

        private int _concentrationNO2;

        public int ConcentrationNO2
        {
            get { return _concentrationNO2; }
            set
            {
                if (_concentrationNO2 != value)
                {
                    _concentrationNO2 = value;
                    InvokePropertyChanged("ConcentrationNO2");
                }
            }
        }

        private bool _ismodulationmode;
        public bool IsModulationMode
        {
            get { return _ismodulationmode; }
            set
            {
                if (_ismodulationmode != value)
                {
                    _ismodulationmode = value;
                    InvokePropertyChanged("IsModulationMode");
                    InvokePropertyChanged("OffModulationMode");
                }
            }
        }
        public  bool OffModulationMode
        {
            get { return !_ismodulationmode; }
        }

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
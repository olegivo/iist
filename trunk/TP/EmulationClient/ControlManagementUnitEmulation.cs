using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DMS.Common.Messages;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Tools.UI;

namespace EmulationClient
{
    /// <summary>
    /// Модуль контроля и управления для эмуляции
    /// </summary>
    public class ControlManagementUnitEmulation : ControlManagementUnit
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ControlManagementUnit" />.
        /// </summary>
        public ControlManagementUnitEmulation()
        {
            NeedProtocol += ControlManagementUnitEmulation_NeedProtocol;
            Proxy.RegisterCompleted += Proxy_RegisterCompleted;
            Proxy.UnregisterCompleted += Proxy_UnregisterCompleted;
            Proxy.ChannelRegisterCompleted += Proxy_ChannelRegisterCompleted;
            Proxy.ChannelUnRegisterCompleted += Proxy_ChannelUnRegisterCompleted;
        }

        void Proxy_ChannelUnRegisterCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string s = string.Format("отмена регистрации канала {0} на сервере завершилась {1}", e.UserState, e.Error == null ? "успешно" : string.Format("неудачно: {0}", e.Error));
            Protocol(s);
        }

        void Proxy_ChannelRegisterCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string s = string.Format("Регистрация канала {0} на сервере завершилась {1}", e.UserState, e.Error == null ? "успешно" : string.Format("неудачно: {0}", e.Error));
            Protocol(s);
        }

        void Proxy_UnregisterCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string s = string.Format("Отмена регистрации на сервере завершилась {0}", e.Error == null ? "успешно" : string.Format("неудачно: {0}", e.Error));
            Protocol(s);
        }

        void Proxy_RegisterCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string s = string.Format("Регистрация на сервере завершилась {0}", e.Error == null ? "успешно" : string.Format("неудачно: {0}", e.Error));
            Protocol(s);
            RegisterAllChannels();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RegisterAllChannels()
        {
            foreach (LogicalChannel channel in LogicalChannels)
            {
                Proxy.ChannelRegisterAsync(new ChannelRegistrationMessage
                                               {
                                                   RegName = RegName,
                                                   LogicalChannelId = channel.Id,
                                                   DataMode = channel.Id > 100 ? DataMode.Write : DataMode.Read,
                                                   RegistrationMode = RegistrationMode.Register
                                               },
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
                Proxy.ChannelUnRegisterAsync(new ChannelRegistrationMessage
                                               {
                                                   RegName = RegName,
                                                   LogicalChannelId = channel.Id,
                                                   DataMode = channel.Id > 100 ? DataMode.Write : DataMode.Read,
                                                   RegistrationMode = RegistrationMode.Unregister
                                               },
                                           channel.Id);
            }
        }

        void ControlManagementUnitEmulation_NeedProtocol(object sender, EventArgs e)
        {
            Console.WriteLine("{0}", sender);
        }

        private IEnumerable<LogicalChannel> _logicalChannels;


        public List<LogicalChannel> LogicalChannels
        {
            get { return GetLogicalChannels() as List<LogicalChannel>; }
            set
            {
                _logicalChannels = value;
                if (LogicalChannels!=null)
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
    }
}
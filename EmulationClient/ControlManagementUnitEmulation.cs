using System;
using System.Collections.Generic;
using DMS.Common.Messages;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.Plc.Channels;

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
                Proxy.ChannelRegisterAsync(new ChannelSubscribeMessage
                                               {
                                                   LogicalChannelId = channel.Id,
                                                   DataMode = channel.Id > 100 ? DataMode.Write : DataMode.Read,
                                                   Mode = true
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
                Proxy.ChannelUnRegisterAsync(new ChannelSubscribeMessage
                                               {
                                                   LogicalChannelId = channel.Id,
                                                   DataMode = channel.Id > 100 ? DataMode.Write : DataMode.Read,
                                                   Mode = false
                                               },
                                           channel.Id);
            }
        }

        void ControlManagementUnitEmulation_NeedProtocol(object sender, EventArgs e)
        {
            Console.WriteLine("{0}", sender);
        }

        private List<LogicalChannel> _logicalChannels;

        ///<summary>
        ///
        ///</summary>
        internal new List<LogicalChannel> LogicalChannels
        {
            get { return _logicalChannels; }
            set { _logicalChannels = value; }
        }

    }
}
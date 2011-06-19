using System;
using System.Collections.Generic;
using Oleg_ivo.LowLevelClient;
using Oleg_ivo.Plc.Channels;

namespace EmulationClient
{
    /// <summary>
    /// 
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
        }

        void Proxy_RegisterCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string s = string.Format("Регистрация на сервере завершилась {0}", e.Error == null ? "успешно" : string.Format("неудачно: {0}", e.Error));
            Protocol(s);
        }
                
        void ControlManagementUnitEmulation_NeedProtocol(object sender, EventArgs e)
        {
            Console.WriteLine("{0}", sender);
        }

        private List<LogicalChannel> _logicalChannels;

        ///<summary>
        ///
        ///</summary>
        internal new IEnumerable<LogicalChannel> LogicalChannels
        {
            get { return _logicalChannels; }
        }

        public void InitChannels()
        {
            _logicalChannels = new List<LogicalChannel>();
            //контролируемые параметры
            _logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                                     {
                                         Id = 1,
                                         Description = "Температура",
                                         PollPeriod = TimeSpan.FromMilliseconds(500),
                                         MinValue = 0,
                                         MaxValue = 1000
                                     });
            _logicalChannels.Add(new InputLogicalChannel(null, 0, 0)
                                     {
                                         Id = 2,
                                         Description = "Концентрация",
                                         PollPeriod = TimeSpan.FromMilliseconds(500),
                                         MinValue = 0,
                                         MaxValue = 1000
                                     });

            //управляемые параметры
            _logicalChannels.Add(new OutputLogicalChannel(null, 0, 0)
                                     {
                                         Id = 101,
                                         Description = "Горелка",
                                         PollPeriod = TimeSpan.FromMilliseconds(500),
                                         MinValue = 0,
                                         MaxValue = 1000
                                     });
            _logicalChannels.Add(new OutputLogicalChannel(null, 0, 0)
                                     {
                                         Id = 102,
                                         Description = "Количество оборотов дымососа",
                                         PollPeriod = TimeSpan.FromMilliseconds(500),
                                         MinValue = 0,
                                         MaxValue = 1000
                                     });
        }
    }
}
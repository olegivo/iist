using System;
using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение, содержащее данные
    /// </summary>
    public class InternalDataMessage : InternalMessage
    {
        protected InternalDataMessage()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        /// <param name="dataMode">Режим данных (либо чтение, либо запись)</param>
        /// <exception cref="ArgumentOutOfRangeException">В случае, если режим данных не чтение и не запись</exception>
        public InternalDataMessage(string regNameFrom, string regNameTo, DataMode dataMode) : base(regNameFrom, regNameTo)
        {
            if (dataMode != DataMode.Read && dataMode != DataMode.Write)
                throw new ArgumentOutOfRangeException("dataMode", dataMode, "Режим данных может быть либо 'чтение', 'запись'");
            DataMode = dataMode;
        }

        /// <summary>
        /// Данные, передаваемые в сообщении
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Режим данных
        /// </summary>
        [DataMember]
        public DataMode DataMode { get; set; }
    }
}
using System;
using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение, содержащее данные логического канала
    /// </summary>
    public class InternalLogicalChannelDataMessage : InternalLogicalChannelMessage
    {
        protected InternalLogicalChannelDataMessage()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        /// <param name="dataMode">Режим данных (либо чтение, либо запись)</param>
        /// <param name="logicalChannelId">Номер логического канала</param>
        /// <param name="isDiscreteData"></param>
        /// <exception cref="ArgumentOutOfRangeException">В случае, если режим данных не чтение и не запись</exception>
        public InternalLogicalChannelDataMessage(string regNameFrom, string regNameTo, DataMode dataMode,
            int logicalChannelId, bool isDiscreteData) : base(regNameFrom, regNameTo, logicalChannelId)
        {
            if (dataMode != DataMode.Read && dataMode != DataMode.Write)
                throw new ArgumentOutOfRangeException("dataMode", dataMode,
                    "Режим данных может быть либо 'чтение', 'запись'");
            DataMode = dataMode;
            IsDiscreteData = isDiscreteData;
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

        public bool IsDiscreteData { get; set; }

        public override object Clone()
        {
            return new InternalLogicalChannelDataMessage
                            (RegNameFrom,
                             RegNameTo,
                             DataMode,
                             LogicalChannelId,
                             IsDiscreteData)
            {
                Value = Value
            };
        }
    }
}
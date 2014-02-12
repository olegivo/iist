using System;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение, содержащее данные логического канала
    /// </summary>
    public class InternalLogicalChannelDataMessage : InternalDataMessage
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
        /// <exception cref="ArgumentOutOfRangeException">В случае, если режим данных не чтение и не запись</exception>
        public InternalLogicalChannelDataMessage(string regNameFrom, string regNameTo, DataMode dataMode, int logicalChannelId) : base(regNameFrom, regNameTo, dataMode)
        {
            LogicalChannelId = logicalChannelId;
        }

        /// <summary>
        /// Идентификатор логического канала
        /// </summary>
        public int LogicalChannelId { get; set; }
    }
}
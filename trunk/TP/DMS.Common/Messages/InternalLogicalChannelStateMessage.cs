using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение, содержащее состояние логического канала
    /// </summary>
    public class InternalLogicalChannelStateMessage : InternalMessage
    {
        protected InternalLogicalChannelStateMessage()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="regNameFrom">Регистрационое имя, от которого посылается сообщение</param>
        /// <param name="regNameTo">Регистрационое имя, которому посылается сообщение</param>
        /// <param name="logicalChannelId">Номер логического канала</param>
        /// <param name="state">Состояние канала</param>
        public InternalLogicalChannelStateMessage(string regNameFrom, string regNameTo, int logicalChannelId, LogicalChannelState state) : base(regNameFrom, regNameTo)
        {
            LogicalChannelId = logicalChannelId;
            State = state;
        }

        /// <summary>
        /// Идентификатор логического канала
        /// </summary>
        public int LogicalChannelId { get; set; }

        /// <summary>
        /// Состояние канала
        /// </summary>
        [DataMember]
        public LogicalChannelState State { get; set; }
    }
}
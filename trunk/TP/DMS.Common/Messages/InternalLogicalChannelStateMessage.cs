using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение, содержащее состояние логического канала
    /// </summary>
    public class InternalLogicalChannelStateMessage : InternalLogicalChannelMessage
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
        public InternalLogicalChannelStateMessage(string regNameFrom, string regNameTo, int logicalChannelId,
            LogicalChannelState state) : base(regNameFrom, regNameTo, logicalChannelId)
        {
            State = state;
        }

        /// <summary>
        /// Состояние канала
        /// </summary>
        [DataMember]
        public LogicalChannelState State { get; set; }

        public override object Clone()
        {
            return new InternalLogicalChannelStateMessage
                (RegNameFrom,
                 RegNameTo,
                 LogicalChannelId,
                 State);
        }
    }
}
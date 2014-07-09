using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение, содержащее управляющую информацию, связанную с логическим каналом
    /// </summary>
    public abstract class InternalServiceChannelMessage : InternalServiceMessage
    {
        protected InternalServiceChannelMessage(string regNameFrom, string regNameTo, int logicalChannelId) : base(regNameFrom, regNameTo)
        {
            LogicalChannelId = logicalChannelId;
        }

        protected InternalServiceChannelMessage()
        {
        }

        /// <summary>
        /// Номер логического канала
        /// </summary>
        [DataMember]
        public int LogicalChannelId { get; set; }
    }
}
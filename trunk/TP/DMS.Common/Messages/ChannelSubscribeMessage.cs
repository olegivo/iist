using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение для подписки / отписки от канала
    /// </summary>
    public class ChannelSubscribeMessage : InternalServiceMessage
    {
        /// <summary>
        /// Подписка (<see langword="true"/>) / отписки (<see langword="false"/>) от канала
        /// </summary>
        [DataMember]
        public bool Mode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int LogicalChannelId { get; set; }
    }
}
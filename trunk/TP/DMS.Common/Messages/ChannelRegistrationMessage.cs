using System.Runtime.Serialization;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение для регистрации/отмены регистрации канала
    /// </summary>
    public class ChannelRegistrationMessage : RegistrationMessage
    {
        /// <summary>
        /// Номер канала
        /// </summary>
        [DataMember]
        public int LogicalChannelId { get; set; }

        /// <summary>
        /// Режим работы с каналом
        /// </summary>
        [DataMember]
        public ChannelMode ChannelMode { get; set; }
    }
}
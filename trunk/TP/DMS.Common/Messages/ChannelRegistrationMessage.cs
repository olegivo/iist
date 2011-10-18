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
        public int LogicalChannelId { get; set; }

        /// <summary>
        /// Режим работы с каналом
        /// </summary>
        public ChannelMode ChannelMode { get; set; }
    }
}
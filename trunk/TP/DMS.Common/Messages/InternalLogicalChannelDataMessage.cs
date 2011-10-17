namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение, содержащее данные логического канала
    /// </summary>
    public class InternalLogicalChannelDataMessage : InternalDataMessage
    {
        /// <summary>
        /// Идентификатор логического канала
        /// </summary>
        public int LogicalChannelId { get; set; }
    }
}
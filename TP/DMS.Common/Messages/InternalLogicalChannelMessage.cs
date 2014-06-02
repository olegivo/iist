namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение, cвязанное с логическим каналом
    /// </summary>
    public abstract class InternalLogicalChannelMessage : InternalMessage
    {
        protected InternalLogicalChannelMessage(string regNameFrom, string regNameTo, int logicalChannelId)
            : base(regNameFrom, regNameTo)
        {
            LogicalChannelId = logicalChannelId;
        }

        protected InternalLogicalChannelMessage()
        {
        }

        /// <summary>
        /// Идентификатор логического канала
        /// </summary>
        public int LogicalChannelId { get; set; }
    }
}
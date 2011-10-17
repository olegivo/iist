namespace DMS.Common.Messages
{
    /// <summary>
    /// Сообщение, содержащее данные
    /// </summary>
    public class InternalDataMessage : InternalMessage
    {
        /// <summary>
        /// Данные, передаваемые в сообщении
        /// </summary>
        public object Value { get; set; }
    }
}
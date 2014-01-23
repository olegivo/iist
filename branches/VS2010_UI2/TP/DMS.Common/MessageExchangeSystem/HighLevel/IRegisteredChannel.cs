namespace DMS.Common.MessageExchangeSystem.HighLevel
{
    public interface IRegisteredChannel
    {
        /// <summary>
        /// Номер канала
        /// </summary>
        int LogicalChannelId { get; set; }

        /// <summary>
        /// Минимальное допустимое значение для канала
        /// </summary>
        double? MinValue { get; }

        /// <summary>
        /// Максимальное допустимое значение для канала
        /// </summary>
        double? MaxValue { get; }

        /// <summary>
        /// Минимальное нормальное значение для канала
        /// </summary>
        double? MinNormalValue { get; }

        /// <summary>
        /// Максимальное нормальное значение для канала
        /// </summary>
        double? MaxNormalValue { get; }

        ///<summary>
        /// Описание канала
        ///</summary>
        string Description { get; }

    }
}
using System;

namespace DMS.Common.Messages
{
    /// <summary>
    /// Режим работы с каналом
    /// </summary>
    [Flags]
    public enum ChannelMode
    {
        /// <summary>
        /// Недоступен ни для чтения, ни для записи
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// Доступен для чтения
        /// </summary>
        Read = 1,

        /// <summary>
        /// Доступен для записи
        /// </summary>
        Write = 2
    }
}
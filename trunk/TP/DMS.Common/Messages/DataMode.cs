namespace DMS.Common.Messages
{
    /// <summary>
    /// Режим данных
    /// </summary>
    public enum DataMode
    {
        /// <summary>
        /// Неизвестно
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Режим чтения (из более низкоуровневых подсистем в более высокоуровневые)
        /// </summary>
        Read = 1,

        /// <summary>
        /// Режим записи (из более высокоуровневых подсистем в более низкоуровневые)
        /// </summary>
        Write = 2
    }
}
using System.Collections.Generic;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// Держатель зарегистрированных логических каналов
    /// </summary>
    public interface IRegisteredChannelsHolder
    {
        /// <summary>
        /// Зарегистрированные логические каналы
        /// </summary>
        Dictionary<int, RegisteredLogicalChannel> RegisteredLogicalChannels { get; }
    }
}
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
        Dictionary<int, RegisteredLogicalChannelExtended> RegisteredLogicalChannels { get; }
    }
}
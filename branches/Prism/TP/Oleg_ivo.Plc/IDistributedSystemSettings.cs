using System.Collections.Generic;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc
{
    public interface IDistributedSystemSettings
    {
        /// <summary>
        /// ќпции загрузки полевых шин
        /// </summary>
        Dictionary<FieldBusType, FieldBusLoadOptions> FieldBusLoadOptions { get; }

        /// <summary>
        /// –ежим эмул€ции (обращени€ к реальным устройствам не будет)
        /// </summary>
        bool IsEmulationMode { get; set; }
    }
}
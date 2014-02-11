using System.Collections.Generic;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc
{
    public interface IDistributedSystemSettings
    {
        /// <summary>
        /// Название настроек (используется для храниения в конфигурационном файле)
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Режим эмуляции (обращения к реальным устройствам не будет)
        /// </summary>
        bool IsEmulationMode { get; set; }

        /// <summary>
        /// Опции загрузки полевых шин
        /// </summary>
        Dictionary<FieldBusType, FieldBusLoadOptions> FieldBusLoadOptions { get; set; }
    }
}
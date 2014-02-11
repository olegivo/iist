using System.Collections.Generic;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc
{
    ///<summary>
    /// Настройки распределённой системы
    ///</summary>
    public class DistributedSystemSettings : IDistributedSystemSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"></see> class.
        /// </summary>
        public DistributedSystemSettings()
        {
            FieldBusLoadOptions = new Dictionary<FieldBusType, FieldBusLoadOptions>();
            //InitLoadOptions();
        }

        /// <summary>
        /// Опции загрузки полевых шин
        /// </summary>
        public Dictionary<FieldBusType, FieldBusLoadOptions> FieldBusLoadOptions { get; set; }

        /// <summary>
        /// Название настроек (используется для храниения в конфигурационном файле)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Режим эмуляции (обращения к реальным устройствам не будет)
        /// </summary>
        public bool IsEmulationMode { get; set; }
    }
}
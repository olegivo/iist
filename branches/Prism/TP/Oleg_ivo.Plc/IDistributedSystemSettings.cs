using System.Collections.Generic;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc
{
    public interface IDistributedSystemSettings
    {
        /// <summary>
        /// Опции загрузки полевых шин
        /// </summary>
        Dictionary<FieldBusType, FieldBusLoadOptions> FieldBusLoadOptions { get; }
    }
}
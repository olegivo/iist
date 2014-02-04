using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;

namespace Oleg_ivo.Plc.Ports
{
    internal class RS232FieldBusManager : ActiveFieldBusManager
    {
        public RS232FieldBusManager(ModbusAccessor fieldBusAccessor, Entities.FieldBus fieldBus, IDistributedMeasurementInformationSystem dmis) 
            : base(fieldBus, fieldBusAccessor, dmis)
        {
        }
    }
}
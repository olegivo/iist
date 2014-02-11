using System;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Factory
{
    /// <summary>
    /// Фабрика полевых шин
    /// </summary>
    public class FieldBusFactory : FieldBusFactoryBase
    {
        public FieldBusFactory(IDistributedMeasurementInformationSystem dmis)
            : base(dmis)
        {
        }

        [Obsolete("use property FieldBusAddresses of instance FieldBusManager instead")]
        public override FieldBusNodeAddressCollection GetFieldBusNodesAddresses(FieldBusType fieldBusType)
        {
            var fieldBusDAC = new FieldBusDAC();
            return fieldBusDAC.GetAddresses(fieldBusType);
        }
    }
}
using Autofac;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.WAGO.Devices;
using Oleg_ivo.WAGO.Factory;

namespace Oleg_ivo.WAGO.Autofac
{
    public class WagoAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<DistributedMeasurementInformationSystem>()
                .SingleInstance()
                .As<IDistributedMeasurementInformationSystem>();
            
            //фабрики:
            builder
                .RegisterType<FieldBusFactory>()
                .As<IFieldBusFactory>();
            builder
                .RegisterType<WagoFieldBusNodeFactory>()
                .As<IFieldBusNodeFactory>();
            builder
                .RegisterType<WagoPhysicalChannelsFactory>()
                .As<IPhysicalChannelsFactory>();
            builder
                .RegisterType<LogicalChannelsFactory>()
                .As<ILogicalChannelsFactory>();
            builder
                .RegisterType<WagoPlcFactory>()
                .As<IPlcFactory>();

            builder
                .RegisterType<PlcManager>()
                .As<IPlcManager>();
        }
    }
}
using System.Runtime.InteropServices;
using Autofac;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Entities;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.WAGO.Configuration;
using Oleg_ivo.WAGO.Devices;
using Oleg_ivo.WAGO.Factory;

namespace Oleg_ivo.WAGO.Autofac
{
    public class WagoAutofacModule : BaseAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(DbConnectionProvider.Instance);
            builder.RegisterType<PlcDataContext>().SingleInstance();//TODO: регистрация конструктора в контексте

            builder.RegisterType<ConfigurationManager>()
                   .SingleInstance();
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
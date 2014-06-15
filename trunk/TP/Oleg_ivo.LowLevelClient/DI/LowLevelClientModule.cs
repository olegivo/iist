using Autofac;
using DMS.Common;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Entities;

namespace Oleg_ivo.LowLevelClient.DI
{
    public class LowLevelClientModule : BaseAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PlcDataContext>().SingleInstance(); //TODO: регистрация конструктора в контексте
            builder.RegisterType<Planner>().SingleInstance();
            builder.RegisterType<ControlManagementUnit>().SingleInstance();
            builder.RegisterType<ErrorSenderWrapper<ControlManagementUnit>>().UsingConstructor(new[] { typeof(ControlManagementUnit) }).SingleInstance();
            builder.RegisterType<PlcManager>().As<IPlcManager>();

        }
    }
}

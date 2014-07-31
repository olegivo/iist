using Autofac;
using Oleg_ivo.CMU.WPF.ViewModels;
using Oleg_ivo.CMU.WPF.Views;
using Oleg_ivo.WAGO.Autofac;

namespace Oleg_ivo.CMU.WPF.IoC
{
    public class CmuAutofacModule : WagoAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainView>();//.UsingConstructor(typeof(MainViewModel));

            //builder.RegisterType<DeviceConfigurationViewModel>();
            //builder.RegisterType<DeviceConfigurationView>().UsingConstructor(typeof(DeviceConfigurationViewModel));

        }
    }
}
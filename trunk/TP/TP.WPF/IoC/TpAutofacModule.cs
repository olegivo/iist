using Autofac;
using DMS.Common;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.HighLevelClient;
using TP.WPF.ViewModels;
using TP.WPF.Views;

namespace TP.WPF.IoC
{
    public class TpAutofacModule : BaseAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TpPrismModule>();
            builder.RegisterType<ClientProvider>().SingleInstance();
            builder.RegisterType<ErrorSenderWrapper<ClientProvider>>().UsingConstructor(new[] { typeof(ClientProvider) }).SingleInstance();

            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainShell>();//.UsingConstructor(typeof(MainViewModel));

            //builder.RegisterType<DeviceConfigurationViewModel>();
            //builder.RegisterType<DeviceConfigurationView>().UsingConstructor(typeof(DeviceConfigurationViewModel));

        }
    }
}
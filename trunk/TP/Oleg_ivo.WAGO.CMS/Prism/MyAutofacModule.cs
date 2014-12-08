using Autofac;
using Oleg_ivo.Base.WPF.Dialogs;
using Oleg_ivo.WAGO.Autofac;
using Oleg_ivo.WAGO.CMS.Dialogs;
using Oleg_ivo.WAGO.CMS.View;
using Oleg_ivo.WAGO.CMS.ViewModel;

namespace Oleg_ivo.WAGO.CMS.Prism
{
    public class MyAutofacModule : WagoAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MyPrismModule>();
            
            //MVVM
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainView>().UsingConstructor(typeof(MainViewModel));

            builder.RegisterType<DeviceConfigurationViewModel>();
            builder.RegisterType<DeviceConfigurationView>().UsingConstructor(typeof(DeviceConfigurationViewModel));

            //MVVM Dialogs
            builder.RegisterType<ModalDialogService>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<PolynomDialogViewModel>();
            builder.RegisterType<PolynomDialog>().AsImplementedInterfaces();

            builder.RegisterType<ParametersEditDialogViewModel>();
            builder.RegisterType<ParametersEditDialog>().AsImplementedInterfaces();
        }
    }
}
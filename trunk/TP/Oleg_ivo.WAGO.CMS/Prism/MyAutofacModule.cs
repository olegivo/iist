using Autofac;
using Oleg_ivo.WAGO.CMS.Dialogs;
using Oleg_ivo.WAGO.CMS.View;
using Oleg_ivo.WAGO.CMS.ViewModel;
using UICommon.WPF.Dialogs;

namespace Oleg_ivo.WAGO.CMS.Prism
{
    public class MyAutofacModule : Module
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
        }
    }
}
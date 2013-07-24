using Autofac;
using Oleg_ivo.WAGO.CMS.View;
using Oleg_ivo.WAGO.CMS.ViewModel;

namespace Oleg_ivo.WAGO.CMS.Prism
{
    public class MyAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MyPrismModule>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<MainView>().UsingConstructor(typeof(MainViewModel));
        }
    }
}
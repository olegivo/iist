using System.Windows;
using Autofac;
using Microsoft.Practices.Prism.Modularity;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.WAGO.Configuration;
using Prism.AutofacExtension;

namespace Oleg_ivo.WAGO.CMS.Prism
{
    public class MyBootstrapper : AutofacBootstrapper
    {
        private readonly string[] args;

        public MyBootstrapper(string[] args)
        {
            this.args = args;
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterModule(new CommandLineHelperAutofacModule<WagoCommandLineOptions>(args));

            builder.RegisterType<MainWindow>();

            // register autofac module
            builder.RegisterModule<MyAutofacModule>();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            // register prism module
            var prismModuleType = typeof(MyPrismModule);
            ModuleCatalog.AddModule(new ModuleInfo(prismModuleType.Name, prismModuleType.AssemblyQualifiedName));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (MainWindow)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
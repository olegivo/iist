using System.Windows;
using Autofac;
using Microsoft.Practices.Prism.Modularity;
using Oleg_ivo.Base.Autofac.Modules;
using Oleg_ivo.WAGO.Configuration;
using Prism.AutofacExtension;

namespace Oleg_ivo.CMU.WPF.IoC
{
    public class CmuBootStraper : AutofacBootstrapper
    {
        private readonly string[] args;

        public CmuBootStraper(string[] args)
        {
            this.args = args;
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterModule(new CommandLineHelperAutofacModule<WagoCommandLineOptions>(args));
            builder.RegisterModule<CmuAutofacModule>();

            builder.RegisterType<MainWindow>();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            // register prism module
            var prismModuleType = typeof(CmuPrismModule);
            ModuleCatalog.AddModule(new ModuleInfo(prismModuleType.Name, prismModuleType.AssemblyQualifiedName));
        }

    }
}
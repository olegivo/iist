using System.Windows;
using Autofac;
using Microsoft.Practices.Prism.Modularity;
using Oleg_ivo.Base.Autofac.Modules;
using Prism.AutofacExtension;
using TP.WPF.Views;

namespace TP.WPF.IoC
{
    public class TpBootStraper : AutofacBootstrapper
    {
        private readonly string[] args;

        public TpBootStraper(string[] args)
        {
            this.args = args;
        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            builder.RegisterModule(new CommandLineHelperAutofacModule<TpCommandLineOptions>(args));
            builder.RegisterModule<TpAutofacModule>();

            builder.RegisterType<MainWindow>();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainShell>();
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
            var prismModuleType = typeof(TpPrismModule);
            ModuleCatalog.AddModule(new ModuleInfo(prismModuleType.Name, prismModuleType.AssemblyQualifiedName));
        }

    }
}
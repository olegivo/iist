using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using TP.WPF.Views;

namespace TP.WPF
{
    public class MyOwnBootStraper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return this.Container.Resolve<MainShell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            App.Current.MainWindow = (Window)this.Shell;
            App.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            //ToDo : Add modules that you need. You can also use Configuration for this.

            moduleCatalog.AddModule(typeof(SimpleModule));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }
    }
}
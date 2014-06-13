using System.Windows;
using NLog;
using Oleg_ivo.CMU.WPF.IoC;

namespace Oleg_ivo.CMU.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            System.Reactive.PlatformServices.EnlightenmentProvider.EnsureLoaded();
            var bootstrapper = new CmuBootStraper(e.Args);
            bootstrapper.Run();
        }
    }
}

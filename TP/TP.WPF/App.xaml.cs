using System.Windows;
using TP.WPF.IoC;

namespace TP.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 
        /// </summary>
        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new TpBootStraper(e.Args);
            bootstrapper.Run();
        }
    }
}

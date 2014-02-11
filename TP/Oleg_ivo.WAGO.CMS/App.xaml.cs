using System.Windows;
using Oleg_ivo.WAGO.CMS.Prism;

namespace Oleg_ivo.WAGO.CMS
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
            var bootstrapper = new MyBootstrapper(e.Args);
            bootstrapper.Run();
        }
    }
}

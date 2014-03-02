using System.Windows;

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
            MyOwnBootStraper bootstrapper = new MyOwnBootStraper();
            bootstrapper.Run();
        }
    }
}

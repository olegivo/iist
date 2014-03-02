using System.Windows;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using TP.WPF.ViewModels;

namespace TP.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>

	public partial class MainView
    {
        public MainView()
        {
            InitializeComponent(); 
        }

        [Dependency(Required = true)]
        public MainViewModel ViewModel
        {
            get { return (MainViewModel)DataContext; }
            set { DataContext = value; }
        }

        /*
        private void ucConnectionWindow_NeedRegister(object sender, EventArgs e)
        {
            channelController1.Register();
        }
        */

        private void Path_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	// TODO: Add event handler implementation here.
			base.OnMouseDown(e);
			DragMove();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            WindowState = (WindowState == WindowState.Maximized) ? WindowState.Normal : WindowState.Maximized;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
			WindowState=WindowState.Minimized;
        }

        private void textBox1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            textBox1.ScrollToEnd();
        }

        //private void channelController1_CanRegisterChanged(object sender, EventArgs e)
        //{
        //    sbUnregister.Enabled = !(sbRegister.Enabled = channelController1.CanRegister);
        //}

    }
}

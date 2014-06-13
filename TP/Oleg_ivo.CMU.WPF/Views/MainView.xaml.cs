using System.Windows;
using Oleg_ivo.CMU.WPF.ViewModels;

namespace Oleg_ivo.CMU.WPF.Views
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

        public MainView(MainViewModel viewModel):this()
        {
            ViewModel = viewModel;
            //viewModel.DMIS.BuildSystemConfiguration();
            //всё стартует, теперь нужно строить интерфейс
        }

        public MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
            set { DataContext = value; }
        }

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.OnLoad();
        }
    }
}

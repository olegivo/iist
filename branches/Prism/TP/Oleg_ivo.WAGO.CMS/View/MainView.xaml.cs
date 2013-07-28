using Oleg_ivo.WAGO.CMS.ViewModel;

namespace Oleg_ivo.WAGO.CMS.View
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
            DataContext = viewModel;
            viewModel.DMIS.BuildSystemConfiguration();
            //всё стартует, теперь нужно строить интерфейс
        }
    }
}

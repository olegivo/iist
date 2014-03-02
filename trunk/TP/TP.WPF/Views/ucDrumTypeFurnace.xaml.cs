using System.Windows.Controls;
using Oleg_ivo.Base.Autofac.DependencyInjection;

namespace TP.WPF.Views
{
	/// <summary>
	/// Interaction logic for ucDrumTypeFurnace.xaml
	/// </summary>
	public partial class ucDrumTypeFurnace : UserControl
	{
		public ucDrumTypeFurnace()
		{
			this.InitializeComponent();
		}

        [Dependency]
        public ViewModels.DrumTypeFurnaceViewModel ViewModel
        {
            set { this.DataContext = value; }
        }
	}
}
using System.Windows.Controls;
using System.Windows.Markup;
using MixModes.Synergy.VisualFramework.Windows;
using TP.WPF.Views;

namespace TP.WPF
{
	/// <summary>
	/// Interaction logic for MainShell.xaml
	/// </summary>
	public partial class MainShell
	{
		public MainShell()
		{
			this.InitializeComponent();
			
			// Insert code required on object creation below this point.
		    var v = new Views.MainWindow();




            //Создание лэйаута
            DockPane _mainPane = new DockPane();
		    _mainPane.Width = 600;
		    _mainPane.Height = 400;
		    _mainPane.MinHeight = 240;
		    _mainPane.MinWidth = 320;
		    _mainPane.Header = "главное окно мониторинга";
		    _mainPane.Content = new Views.ucDrumTypeFurnace();
            WindowsManager.AddFloatingWindow(_mainPane);


		    //UcListView.Items.Add(new Views.ucDrumTypeFurnace());
		}
	}
}
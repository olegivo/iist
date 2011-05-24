using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UICommonWPF
{
	/// <summary>
	/// Interaction logic for ucReheatChamber.xaml
	/// </summary>
	public partial class ucReheatChamber : UserControl
	{
		public ucReheatChamber()
		{
			this.InitializeComponent();
		}
		


		private void Slider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
		{
			// TODO: Add event handler implementation here.
			this.TextBox1.Text=(Convert.ToString(this.Slider1.Value));
		}
		//TextBox.WidthProperty(400);
		//slider.ValueChanged.

	}
}
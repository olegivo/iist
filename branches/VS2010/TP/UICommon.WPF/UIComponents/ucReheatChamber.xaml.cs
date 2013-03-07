using System;
using System.Windows.Controls;

namespace UICommon.WPF.UIComponents
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
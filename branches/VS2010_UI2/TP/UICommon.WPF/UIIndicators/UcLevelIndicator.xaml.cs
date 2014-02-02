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

namespace UICommon.WPF
{
	/// <summary>
	/// Interaction logic for ucLevelIndicator.xaml
	/// </summary>
	public partial class ucLevelIndicator : UserControl
	{
		public ucLevelIndicator()
		{
			this.InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
		}

	    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
	    {
			//TODO:http://www.wiredprairie.us/blog/index.php/archives/1111
            //var v = sender as IndicatorViewModel;
	        throw new NotImplementedException();
	    }
	}
}
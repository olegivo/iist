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

namespace TP
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

	    private float _temperature;
		//public float Temperature{set;get;}
        public float Temperature
        {
            get { return _temperature; }
            set
            {
                if (_temperature != value)
                {
                    _temperature = value;
                }
            }
        }
		
	}
}
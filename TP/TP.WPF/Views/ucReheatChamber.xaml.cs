using System.Windows.Controls;

namespace TP.WPF.Views
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
using System;
using System.Collections.Generic;
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
using TP.WPF.ViewModels;

namespace TP.WPF
{
	/// <summary>
	/// Interaction logic for ucFan.xaml
	/// </summary>
	public partial class ucFan : UserControl
	{
		public ucFan()
		{
			this.InitializeComponent();
            this.DataContextChanged +=OnDataContextChanged;
		}

	    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
	    {
            //TODO:old value -=
            var ivm = this.DataContext as IndicatorViewModel;
            if (ivm != null)
            {
                ivm.PropertyChanged += ivm_PropertyChanged;
            }
            SetVisualState();
	    }

        void ivm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SetVisualState();
        }

	    private void SetVisualState()
	    {
	        var ivm = this.DataContext as IndicatorViewModel;
	        if (ivm != null)
	        {
	            //ivm.PropertyChanged
	            VisualStateManager.GoToState(this, ivm.IsValueHigherNormal == true ? "AlarmState" : "Default", true);
	            //http://lfhck.com/question/363851/binding-visualstatemanager-view-state-to-a-mvvm-viewmodel
	        }
	    }
	}
}
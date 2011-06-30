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
	/// Interaction logic for ucConnectionWindow.xaml
	/// </summary>
	public partial class ucConnectionWindow : UserControl
	{
	    public event EventHandler NeedRegister;

	    private void InvokeNeedRegister()
	    {
	        EventHandler handler = NeedRegister;
	        if (handler != null) handler(this, EventArgs.Empty);
	    }

	    public ucConnectionWindow()
		{
			this.InitializeComponent();
		}
		
		// кнопка закрытия окна подключения к серверу
		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.UserControl.Visibility=System.Windows.Visibility.Hidden;
		}

		// кнопка отмены регистрации на сервере
		public void sbUnregister_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			
		}
		// кнопка регистрации на сервере
		private void sbRegister_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            InvokeNeedRegister();
			// кнопка регистрации на сервере
			//TP.channelController1.Register();
		}
	}
}
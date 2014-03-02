using System;
using System.Windows.Controls;


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
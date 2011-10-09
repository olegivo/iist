using System.Windows;

namespace EmulationClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ControlManagementUnitEmulation controlManagementUnitEmulation;

        public Window1()
        {
            InitializeComponent();
            App app = (App) App.Current;
            controlManagementUnitEmulation = app.ControlManagementUnit;
        }

        private void btnUnregister_Click(object sender, RoutedEventArgs e)
        {
            controlManagementUnitEmulation.UnregisterAllChannels();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            controlManagementUnitEmulation.RegisterAllChannels();
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            // вкл. горелку

        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            // выкл. горелку
        }
    }
}

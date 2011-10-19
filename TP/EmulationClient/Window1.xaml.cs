using System;
using System.Windows;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

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
            controlManagementUnitEmulation.NeedProtocol += new System.EventHandler<System.EventArgs>(controlManagementUnitEmulation_NeedProtocol);
        }
        private void controlManagementUnitEmulation_NeedProtocol(object sender, EventArgs e)
        {
            textBox1.Text = System.DateTime.Now + " " + sender;  
        }

        private void btnUnregister_Click(object sender, RoutedEventArgs e)
        {
            controlManagementUnitEmulation.UnregisterAllChannels();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            controlManagementUnitEmulation.RegisterAllChannels();
        }
    }
}

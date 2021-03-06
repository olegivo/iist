﻿using System;
using System.Windows;

namespace EmulationClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            App app = (App) Application.Current;
            ControlManagementUnitEmulation = app.ControlManagementUnit;
            ControlManagementUnitEmulation.NeedProtocol += controlManagementUnitEmulation_NeedProtocol;
            grid.DataContext = ControlManagementUnitEmulation;
        }

        /// <summary>
        /// 
        /// </summary>
        public ControlManagementUnitEmulation ControlManagementUnitEmulation { get; set; }

        private void controlManagementUnitEmulation_NeedProtocol(object sender, EventArgs e)
        {
            textBox1.AppendText(string.Format("{0}\t{1}{2}", DateTime.Now, sender, Environment.NewLine));
            textBox1.ScrollToEnd();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            ControlManagementUnitEmulation.RegisterAsync();
        }

        private void btnUnregister_Click(object sender, RoutedEventArgs e)
        {
            ControlManagementUnitEmulation.UnregisterAsync();
        }

        private void btnRegisterChannels_Click(object sender, RoutedEventArgs e)
        {
            ControlManagementUnitEmulation.RegisterAllChannels();
        }

        private void btnUnregisterChannels_Click(object sender, RoutedEventArgs e)
        {
            ControlManagementUnitEmulation.UnregisterAllChannels();
        }
    }
}

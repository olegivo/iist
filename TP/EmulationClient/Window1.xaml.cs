﻿using System;
using System.Windows;
using DMS.Common.Events;

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
            ControlManagementUnitEmulation.HasWriteChannel += ControlManagementUnitEmulation_HasWriteChannel;
            grid.DataContext = ControlManagementUnitEmulation;
        }

        private void ControlManagementUnitEmulation_HasWriteChannel(object sender, DataEventArgs e)
        {
            //данные, которые придут в канал должны влиять на флаг горелки (и т.п.) 
        }

        /// <summary>
        /// 
        /// </summary>
        public ControlManagementUnitEmulation ControlManagementUnitEmulation { get; set; }

        private void controlManagementUnitEmulation_NeedProtocol(object sender, EventArgs e)
        {
            textBox1.Text += string.Format("{0}\t{1}{2}", DateTime.Now, sender, Environment.NewLine);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            ControlManagementUnitEmulation.Register();
        }

        private void btnUnregister_Click(object sender, RoutedEventArgs e)
        {
            ControlManagementUnitEmulation.Unregister();
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

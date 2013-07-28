using System;
using System.Windows;
using System.Windows.Controls;
using Oleg_ivo.WAGO.CMS.ViewModel;

namespace Oleg_ivo.WAGO.CMS.View
{
    /// <summary>
    /// Interaction logic for DeviceConfigurationView.xaml
    /// </summary>
    public partial class DeviceConfigurationView : UserControl
    {
        public DeviceConfigurationView()
        {
            InitializeComponent();
        }

        public DeviceConfigurationView(DeviceConfigurationViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        private DeviceConfigurationViewModel ViewModel
        {
            get { return (DeviceConfigurationViewModel)DataContext; }
            set { DataContext = value; }
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            TreeViewItem root = (TreeViewItem)tree.Items[0];
            //root = new TreeViewItem { Header = "System" };
            //tree.Items.Add(root);
            //BuildPortsConfig(root);
            root.ExpandSubtree();
        }

        /*
        private void BuildPortsConfig(TreeViewItem root)
        {
            root.Items.Clear();

            foreach (FieldBusManager fieldBusManager in ViewModel.DMIS.PlcManager.FieldBusManagers)
            {
                var item = new TreeViewItem { Header = fieldBusManager.ToString(), Tag = fieldBusManager };
                root.Items.Add(item);
                BuildBusFieldNodesConfig(item, fieldBusManager);
            }
        }

        private void BuildBusFieldNodesConfig(TreeViewItem parentItem, FieldBusManager busManager)
        {
            if (busManager.FieldBusNodes == null || busManager.FieldBusNodes.Count == 0)
            {
                busManager.BuildFieldBusNodes(ViewModel.DMIS.PlcManager.FieldBusNodesFactory);
            }

            if (busManager.FieldBusNodes != null)
            {
                parentItem.Items.Clear();

                foreach (FieldBusNode fieldNode in busManager.FieldBusNodes)
                {
                    var item = new TreeViewItem { Header = fieldNode.ToString(), Tag = fieldNode };
                    parentItem.Items.Add(item);
                    BuildModulesConfig(item, fieldNode);
                }
            }
        }

        private void BuildModulesConfig(TreeViewItem parentItem, FieldBusNode fieldBusNode)
        {
            if (fieldBusNode.PhysicalChannels == null)
            {
                //Log.Debug("{0}: надо построить конфигурацию модулей ввода-вывода", fieldBusNode);
                return;
                //throw new NotImplementedException("надо построить конфигурацию модулей ввода-вывода");
            }

            parentItem.Items.Clear();

            foreach (PhysicalChannel physicalChannel in fieldBusNode.PhysicalChannels)
            {
                LogicalChannelCollection logicalChannels = physicalChannel.LogicalChannels != null &&
                                                           physicalChannel.LogicalChannels.Count > 0
                    ? physicalChannel.LogicalChannels
                    : physicalChannel.IOModule.BuildDefaultLogicalChannels();

                //сначала физический канал
                TreeViewItem pcItem = new TreeViewItem { Header = physicalChannel.ToString(), Tag = physicalChannel };
                parentItem.Items.Add(pcItem);

                //в физическом канале будет логический канал
                foreach (LogicalChannel logicalChannel in logicalChannels)
                {
                    TreeViewItem lcNode = new TreeViewItem { Header = logicalChannel.ToString(), Tag = logicalChannel };
                    pcItem.Items.Add(lcNode);
                }
            }

        }
        */

    }
    public class CustomDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            DataTemplate dataTemplate = null;
            if (element != null && item != null)
            {
                var type = item.GetType();
                dataTemplate = geTemplate(type, element);
            }

            return dataTemplate;
        }

        private DataTemplate geTemplate(Type type, FrameworkElement element)
        {
            if (type == null) return null;
            var templateName = string.Format("{0}Template", type.Name);
            var dataTemplate = element.TryFindResource(templateName) as DataTemplate;
            if (dataTemplate != null) 
                Console.WriteLine("Найден {0}", templateName);
            else
            {
                Console.WriteLine("Не найден {0}, продолжаем поиск для базового класса", templateName);
                dataTemplate = geTemplate(type.BaseType, element);
            }
            return dataTemplate;
        }
    }
}

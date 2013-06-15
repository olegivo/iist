using System;
using System.Windows.Forms;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.WAGO.Forms
{
    ///<summary>
    ///
    ///</summary>
    public partial class DeviceConfigurationForm : Form
    {
        ///<summary>
        ///
        ///</summary>
        public DeviceConfigurationForm()
        {
            InitializeComponent();
        }

        private void SystemConfigurationForm_Load(object sender, EventArgs e)
        {
            btnFieldNodes.Enabled = btnModules.Enabled = false;
            tree.SelectedNode = tree.Nodes.Add("System");
            BuildPortsConfig();
            foreach (TreeNode treeNode in tree.Nodes[0].Nodes)
            {
                BuildBusFieldNodesConfig(treeNode);
                foreach (TreeNode node in treeNode.Nodes)
                {
                    BuildModulesConfig(node);
                }
            }
        }

        private void BuildPortsConfig()
        {
            tree.BeginUpdate();

            TreeNode parentNode = tree.Nodes[0];

            parentNode.Nodes.Clear();
            
            foreach (FieldBusManager fieldBusManager in DistributedMeasurementInformationSystem.Instance.PlcManagerBase.FieldBusManagers)
            {
                TreeNode portNode = parentNode.Nodes.Add(fieldBusManager.ToString());
                portNode.Tag = fieldBusManager;
                //BuildBusFieldNodesConfig(portManager, portNode);
            }

            parentNode.Expand();

            tree.EndUpdate();
        }

        private void BuildBusFieldNodesConfig(TreeNode parentNode)
        {
            FieldBusManager busManager = parentNode.Tag as FieldBusManager;
            if (busManager != null)
            {
                if (busManager.FieldBusNodes == null || busManager.FieldBusNodes.Count == 0)
                {
                    busManager.BuildFieldBusNodes(DistributedMeasurementInformationSystem.Instance.PlcManagerBase.FieldBusNodesFactory);
                }

                if (busManager.FieldBusNodes != null)
                {
                    tree.BeginUpdate();

                    parentNode.Nodes.Clear();


                    foreach (FieldBusNode fieldNode in busManager.FieldBusNodes)
                    {
                        TreeNode node = new TreeNode(fieldNode.ToString()) {Tag = fieldNode};
                        parentNode.Nodes.Add(node);
                        //BuildModulesConfig(fieldNode, node);
                    }

                    parentNode.Expand();

                    tree.EndUpdate();
                }
            }
        }

        private void BuildModulesConfig(TreeNode parentNode)
        {
            FieldBusNode _fieldBusNode = parentNode.Tag as FieldBusNode;
            if (_fieldBusNode != null)
            {
                if (_fieldBusNode.PhysicalChannels == null)
                {
                    Console.WriteLine("{0}: надо построить конфигурацию модулей ввода-вывода", _fieldBusNode);
                    return;
                    //throw new NotImplementedException("надо построить конфигурацию модулей ввода-вывода");
                }
                tree.BeginUpdate();

                parentNode.Nodes.Clear();

                foreach (PhysicalChannel physicalChannel in _fieldBusNode.PhysicalChannels)
                {
                    LogicalChannelCollection logicalChannels = physicalChannel.LogicalChannels != null && physicalChannel.LogicalChannels.Count > 0 
                        ? physicalChannel.LogicalChannels 
                        : physicalChannel.IOModule.BuildDefaultLogicalChannels();

                    //сначала физический канал
                    TreeNode pcNode = new TreeNode(physicalChannel.ToString()) {Tag = physicalChannel};
                    parentNode.Nodes.Add(pcNode);

                    //в физическом канале будет логический канал
                    foreach (LogicalChannel logicalChannel in logicalChannels)
                    {
                        TreeNode lcNode = new TreeNode(logicalChannel.ToString()) {Tag = logicalChannel};
                        pcNode.Nodes.Add(lcNode);
                    }
                }

                parentNode.Expand();

                tree.EndUpdate();
            }
        }

        private void btnPorts_Click(object sender, EventArgs e)
        {
            BuildPortsConfig();            
        }

        private void btnFieldNodes_Click(object sender, EventArgs e)
        {
            BuildBusFieldNodesConfig(tree.SelectedNode);
        }

        private void btnModules_Click(object sender, EventArgs e)
        {
            BuildModulesConfig(tree.SelectedNode);
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Action)
            {
                case TreeViewAction.Unknown:
                    break;
                case TreeViewAction.ByKeyboard:
                    break;
                case TreeViewAction.ByMouse:
                    break;
                case TreeViewAction.Collapse:
                    break;
                case TreeViewAction.Expand:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            bool bPorts = false;
            bool bTest = false;
            bool bFieldNodes = false;
            bool bModules = false;

            switch (e.Node.Level)
            {
                case 0://System
                    bPorts = true;
                    break;
                case 1://Ports
                    bFieldNodes = true;
                    break;
                case 2://ПЛК
                    bTest = bModules = true;
                    break;
                case 3://Модули
                    break;
                case 4://Физ. каналы
                    break;
            }

            btnPorts.Enabled = bPorts;
            btnTest.Enabled = bTest;
            btnFieldNodes.Enabled = bFieldNodes;
            btnModules.Enabled = bModules;

            if(!DesignMode)
            {
                channelsControl1.SetParentFilter(e.Node.Tag); //устанавливаем фильтрацию по родительскому элементу ДО заполнения

                levelEditor1.SetEditorFilter(e.Node.Tag); //устанавливаем фильтрацию по родительскому элементу ДО заполнения

                if (e.Node.Tag != null) levelEditor1.Fill(e.Node.Tag);
            }
        }

        private void SystemConfigurationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (TreeNode node in tree.Nodes[0].Nodes)
            {
                FieldBusManager busManager = node.Tag as FieldBusManager;
                if (busManager != null)
                {
                    busManager.Dispose();
                }
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            FieldBusNode fieldBusNodeAccessor = tree.SelectedNode.Tag as FieldBusNode;
            if (fieldBusNodeAccessor != null)
            {
                TestIOForm testIOForm = new TestIOForm(fieldBusNodeAccessor);
                testIOForm.ShowDialog();
            }
        }

        private void tree_DoubleClick(object sender, EventArgs e)
        {
            TreeNode selectedNode = tree.SelectedNode;
            if (selectedNode != null)
            {
                if(selectedNode.Tag is LogicalChannel)
                {
                    LogicalChannel logicalChannel = (LogicalChannel) selectedNode.Tag;
                    TestChannelsForm testChannelsForm = new TestChannelsForm {LogicalChannel = logicalChannel};
                    testChannelsForm.ShowDialog();
                }
            }
            //switch (selectedNode.Level)
            //{
            //    case 1://Ports
            //        BuildBusFieldNodesConfig(selectedNode);
            //        break;
            //    case 2://ПЛК
            //        //btnTest_Click(sender, e);
            //        BuildModulesConfig(selectedNode);
            //        break;
            //}
        }

        private void btnObtainInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tree.SelectedNode;
            if (selectedNode != null && selectedNode.Tag != null)
            {
                PlcManagerBase plcManager = DistributedMeasurementInformationSystem.Instance.PlcManagerBase;

                if (selectedNode.Tag is FieldBusManager)
                {
/*
                    FieldBusManager fieldBusManager = (FieldBusManager) selectedNode.Tag;
*/

                }
                else if (selectedNode.Tag is FieldBusNode)
                {
                    FieldBusNode fieldBusNode = (FieldBusNode) selectedNode.Tag;
                    plcManager.PhysicalChannelsFactory.SavePhysicalChannels(fieldBusNode);
                }
                else if (selectedNode.Tag is PhysicalChannel)
                {
                    PhysicalChannel physicalChannel = (PhysicalChannel) selectedNode.Tag;
                    plcManager.LogicalChannelsFactory.SaveLogicalChannels(physicalChannel);
                }
                else if (selectedNode.Tag is LogicalChannel)
                {
/*
                    LogicalChannel logicalChannel = (LogicalChannel) selectedNode.Tag;
*/

                }
/*
                else
                {

                }
*/
            }
        }

    }
}
using System;
using System.IO.Ports;
using System.Windows.Forms;
using Modbus.Device;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Forms
{
    ///<summary>
    ///
    ///</summary>
    public partial class TestModbusMessagesForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dmis"></param>
        public TestModbusMessagesForm(IDistributedMeasurementInformationSystem dmis)
        {
            InitializeComponent();
            this.dmis = dmis;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillConnectionParameters();
        }

        private void FillConnectionParameters()
        {
            // lbPorts
            foreach (string portName in dmis.PlcManager.FieldBusFactory.FindPorts(FieldBusType.RS485))
                lbPorts.Items.Add(portName);
            lbPorts.SelectedIndex = 0;

            // cbBaudrate
            cbBaudrate.Items.AddRange(new object[]
                                      {
                                          9600
                                      });
            cbBaudrate.SelectedIndex = 0;

            // cbData
            cbData.Items.AddRange(new object[]
                                  {
                                      8
                                  });
            cbData.SelectedIndex = 0;

            // cbParity
            cbParity.Items.AddRange(new object[]
                                    {
                                        Parity.None
                                    });
            cbParity.SelectedIndex = 0;

            // cbStopBits
            cbStopBits.Items.AddRange(new object[]
                                      {
                                          1
                                      });
            cbStopBits.SelectedIndex = 0;

            // cbFlowControl
            cbFlowControl.Items.AddRange(new object[]
                                         {
                                             "No"
                                         });
            cbFlowControl.SelectedIndex = 0;
        }

        private void lbPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnConnect.Enabled = (lbPorts.SelectedIndex >= 0);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void lbPorts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Connect();
        }


        private void Connect()
        {
            serialPortConnect.PortName = (string)lbPorts.SelectedItem;
            serialPortConnect.CreatePort();
            if (serialPortConnect.TryOpenPort())
            {
                lbPorts.Enabled = false;
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                grDataInput.Enabled = true;
                tbSendData.Focus();
            }
        }

        private void Disconnect()
        {
            serialPortConnect.ClosePort();
            lbPorts.Enabled = true;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            grDataInput.Enabled = false;
            btnConnect.Focus();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void serialPortConnect_LogMessage(SerialPortConnect sender, SerialPortConnectMessageEventArgs eventArgs)
        {
            TextBox textBox;
            switch (eventArgs.FlowType)
            {
                case FlowType.Connect:
                    textBox = tbConnectLog;
                    break;
                case FlowType.Input:
                    textBox = tbInputLog;
                    break;
                case FlowType.Output:
                    textBox = tbOutputLog;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            textBox.AppendText(eventArgs.Message);
        }

        private void tbSendData_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyData == (Keys.Control | Keys.Enter) && btnSend.Enabled)
            {
                btnSend.PerformClick();
            }
        }

        private void menuItemClear_Click(object sender, EventArgs e)
        {
            if (lastSelectedTextBox != null)
            {
                lastSelectedTextBox.Clear();
            }
        }

        private TextBox lastSelectedTextBox;
        private IDistributedMeasurementInformationSystem dmis;

        private void tb_Enter(object sender, EventArgs e)
        {
            lastSelectedTextBox = sender as TextBox;
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            lastSelectedTextBox = null;
        }

        private void cbBaudrate_SelectedValueChanged(object sender, EventArgs e)
        {
            serialPortConnect.BaudRate = (int) ((ComboBox) sender).SelectedItem;
        }

        private void cbData_SelectedValueChanged(object sender, EventArgs e)
        {
            serialPortConnect.DataBits = (int)((ComboBox)sender).SelectedItem;
        }

        private void cbParity_SelectedValueChanged(object sender, EventArgs e)
        {
            serialPortConnect.Parity = (Parity) ((ComboBox)sender).SelectedItem;
        }

        private void cbStopBits_SelectedValueChanged(object sender, EventArgs e)
        {
            serialPortConnect.StopBits = (StopBits) ((ComboBox)sender).SelectedItem;
        }

        private void cbFlowControl_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // create modbus master
            IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(serialPortConnect.Port);

            byte slaveID = 1;
            ushort startAddress = 0;
            ushort numRegisters = 10;

            // read five registers		
            ushort[] registers = master.ReadHoldingRegisters(slaveID, startAddress, numRegisters);

            for (int i = 0; i < numRegisters; i++)
                serialPortConnect.sendMessage(string.Format("Register {0}={1}", startAddress + i, registers[i]), FlowType.Input);
        }

        

        private void serialPortConnect_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
        }

        private void serialPortConnect_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }

        private void serialPortConnect_PinChanged(object sender, SerialPinChangedEventArgs e)
        {

        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            dmis.BuildSystemConfiguration();
            dmis.ShowConfiguration();
        }
    }
}
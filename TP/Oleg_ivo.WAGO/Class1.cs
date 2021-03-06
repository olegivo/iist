using System;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using FtdAdapter;
using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;
using NLog;

namespace Oleg_ivo.WAGO
{
    public class Class1
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        ///<summary>
        /// Simple Modbus serial RTU master write holding registers example.
        ///</summary>
        public void f01()
        {
            using (SerialPort port = new SerialPort("COM1"))
            {
                // configure serial port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();

                // create modbus master
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                byte slaveID = 1;
                ushort startAddress = 100;
                ushort[] registers = new ushort[] {1, 2, 3};

                // write three registers
                master.WriteMultipleRegisters(slaveID, startAddress, registers);
            }
        }

        /// <summary>
        /// Simple Modbus serial ASCII master read holding registers example.
        /// </summary>
        public void f02()
        {
            using (SerialPort port = new SerialPort("COM1"))
            {
                // configure serial port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.Open();

                // create modbus master
                IModbusSerialMaster master = ModbusSerialMaster.CreateAscii(port);

                byte slaveID = 1;
                ushort startAddress = 1;
                ushort numRegisters = 5;

                // read five registers		
                ushort[] registers = master.ReadHoldingRegisters(slaveID, startAddress, numRegisters);

                for (int i = 0; i < numRegisters; i++)
                    Log.Debug("Register {0}={1}", startAddress + i, registers[i]);
            }

            // output: 
            // Register 1=0
            // Register 2=0
            // Register 3=0
            // Register 4=0
            // Register 5=0

        }

        /// <summary>
        /// Simple Modbus serial USB RTU master write multiple coils example.
        /// </summary>
        public void f03()
        {
            using (FtdUsbPort port = new FtdUsbPort())
            {
                // configure usb port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = FtdParity.None;
                port.StopBits = FtdStopBits.One;
                port.OpenByIndex(0);

                // create modbus master
                IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

                byte slaveID = 1;
                ushort startAddress = 1;

                // write three coils
                master.WriteMultipleCoils(slaveID, startAddress, new bool[] { true, false, true });
            }

        }

        /// <summary>
        /// Simple Modbus serial USB ASCII master write multiple coils example.
        /// </summary>
        public void f04()
        {
            using (FtdUsbPort port = new FtdUsbPort())
            {
                // configure usb port
                port.BaudRate = 9600;
                port.DataBits = 8;
                port.Parity = FtdParity.None;
                port.StopBits = FtdStopBits.One;
                port.OpenByIndex(0);

                // create modbus master
                IModbusSerialMaster master = ModbusSerialMaster.CreateAscii(port);

                byte slaveID = 1;
                ushort startAddress = 1;

                // write three coils
                master.WriteMultipleCoils(slaveID, startAddress, new bool[] { true, false, true });
            }
        }

        /// <summary>
        /// Simple Modbus TCP master read inputs example.
        /// </summary>
        public void f05()
        {
            using (TcpClient client = new TcpClient("127.0.0.1", 502))
            {
                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                // read five input values
                ushort startAddress = 100;
                ushort numInputs = 5;
                bool[] inputs = master.ReadInputs(startAddress, numInputs);

                for (int i = 0; i < numInputs; i++)
                    Log.Debug("Input {0}={1}", startAddress + i, inputs[i] ? 1 : 0);
            }

            // output: 
            // Input 100=0
            // Input 101=0
            // Input 102=0
            // Input 103=0
            // Input 104=0

        }

        /// <summary>
        /// Simple Modbus UDP master write coils example.
        /// </summary>
        public void f06()
        {
            using (UdpClient client = new UdpClient())
            {
                IPEndPoint endPoint = new IPEndPoint(new IPAddress(new byte[] { 127, 0, 0, 1 }), 502);
                client.Connect(endPoint);

                ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

                ushort startAddress = 1;

                // write three coils
                master.WriteMultipleCoils(startAddress, new bool[] { true, false, true });
            }

        }

        /// <summary>
        /// Simple Modbus serial RTU slave example.
        /// </summary>
        public void f07()
        {
            using (SerialPort slavePort = new SerialPort("COM2"))
            {
                // configure serial port
                slavePort.BaudRate = 9600;
                slavePort.DataBits = 8;
                slavePort.Parity = Parity.None;
                slavePort.StopBits = StopBits.One;
                slavePort.Open();

                byte unitID = 1;

                // create modbus slave
                ModbusSlave slave = ModbusSerialSlave.CreateRtu(unitID, slavePort);
                slave.DataStore = DataStoreFactory.CreateDefaultDataStore();

                slave.Listen();
            }

        }

        /// <summary>
        /// Simple Modbus Serial ASCII slave example.
        /// </summary>
        public void f08()
        {
            using (SerialPort slavePort = new SerialPort("COM2"))
            {
                // configure serial port
                slavePort.BaudRate = 9600;
                slavePort.DataBits = 8;
                slavePort.Parity = Parity.None;
                slavePort.StopBits = StopBits.One;
                slavePort.Open();

                byte unitID = 1;

                // create modbus slave
                ModbusSlave slave = ModbusSerialSlave.CreateAscii(unitID, slavePort);
                slave.DataStore = DataStoreFactory.CreateDefaultDataStore();

                slave.Listen();
            }

        }

        /// <summary>
        /// Simple Modbus serial RTU slave example.
        /// </summary>
        public void f09()
        {
            using (SerialPort slavePort = new SerialPort("COM2"))
            {
                // configure serial port
                slavePort.BaudRate = 9600;
                slavePort.DataBits = 8;
                slavePort.Parity = Parity.None;
                slavePort.StopBits = StopBits.One;
                slavePort.Open();

                byte unitID = 1;

                // create modbus slave
                ModbusSlave slave = ModbusSerialSlave.CreateRtu(unitID, slavePort);
                slave.DataStore = DataStoreFactory.CreateDefaultDataStore();

                slave.Listen();
            }

        }

        /// <summary>
        /// Simple Modbus TCP slave example.
        /// </summary>
        public void f10()
        {
            byte slaveID = 1;
            int port = 502;
            IPAddress address = new IPAddress(new byte[] { 127, 0, 0, 1 });

            // create and start the TCP slave
            TcpListener slaveTcpListener = new TcpListener(address, port);
            slaveTcpListener.Start();

            ModbusSlave slave = ModbusTcpSlave.CreateTcp(slaveID, slaveTcpListener);
            slave.DataStore = DataStoreFactory.CreateDefaultDataStore();

            slave.Listen();

            // prevent the main thread from exiting
            Thread.Sleep(Timeout.Infinite);

        }

        /// <summary>
        /// Simple Modbus UDP slave example.
        /// </summary>
        public void f11()
        {
            using (UdpClient client = new UdpClient(502))
            {
                ModbusUdpSlave slave = ModbusUdpSlave.CreateUdp(client);
                slave.DataStore = DataStoreFactory.CreateDefaultDataStore();

                slave.Listen();

                // prevent the main thread from exiting
                Thread.Sleep(Timeout.Infinite);
            }

        }

        /// <summary>
        /// Modbus TCP master and slave example.
        /// </summary>
        public void f12()
        {
            byte slaveID = 1;
            int port = 502;
            IPAddress address = new IPAddress(new byte[] { 127, 0, 0, 1 });

            // create and start the TCP slave
            TcpListener slaveTcpListener = new TcpListener(address, port);
            slaveTcpListener.Start();
            ModbusSlave slave = ModbusTcpSlave.CreateTcp(slaveID, slaveTcpListener);
            Thread slaveThread = new Thread(slave.Listen);
            slaveThread.Start();

            // create the master
            TcpClient masterTcpClient = new TcpClient(address.ToString(), port);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(masterTcpClient);

            ushort numInputs = 5;
            ushort startAddress = 100;

            // read five register values
            ushort[] inputs = master.ReadInputRegisters(startAddress, numInputs);

            for (int i = 0; i < numInputs; i++)
                Log.Debug("Register {0}={1}", startAddress + i, inputs[i]);

            // clean up
            masterTcpClient.Close();
            slaveTcpListener.Stop();

            // output
            // Register 100=0
            // Register 101=0
            // Register 102=0
            // Register 103=0
            // Register 104=0

        }

        /// <summary>
        /// Modbus serial ASCII master and slave example.
        /// </summary>
        public void f13()
        {
            using (SerialPort masterPort = new SerialPort("COM1"))
            using (SerialPort slavePort = new SerialPort("COM2"))
            {
                // configure serial ports
                masterPort.BaudRate = slavePort.BaudRate = 9600;
                masterPort.DataBits = slavePort.DataBits = 8;
                masterPort.Parity = slavePort.Parity = Parity.None;
                masterPort.StopBits = slavePort.StopBits = StopBits.One;
                masterPort.Open();
                slavePort.Open();

                // create modbus slave on seperate thread
                byte slaveID = 1;
                ModbusSlave slave = ModbusSerialSlave.CreateAscii(slaveID, slavePort);
                Thread slaveThread = new Thread(new ThreadStart(slave.Listen));
                slaveThread.Start();

                // create modbus master
                ModbusSerialMaster master = ModbusSerialMaster.CreateAscii(masterPort);

                master.Transport.Retries = 5;
                ushort startAddress = 100;
                ushort numRegisters = 5;

                // read five register values
                ushort[] registers = master.ReadHoldingRegisters(slaveID, startAddress, numRegisters);

                for (int i = 0; i < numRegisters; i++)
                    Log.Debug("Register {0}={1}", startAddress + i, registers[i]);
            }

            // output
            // Register 100=0
            // Register 101=0
            // Register 102=0
            // Register 103=0
            // Register 104=0


        }

        /// <summary>
        /// Write and Read 32 bit value example.
        /// </summary>
        public void f14(IModbusMaster master, byte slaveID, ushort startAddress)
        {
            uint largeValue = UInt16.MaxValue + 5;

            ushort lowOrderValue = BitConverter.ToUInt16(BitConverter.GetBytes(largeValue), 0);
            ushort highOrderValue = BitConverter.ToUInt16(BitConverter.GetBytes(largeValue), 2);


            // write large value in two 16 bit chunks
            master.WriteMultipleRegisters(slaveID, startAddress, new ushort[] { lowOrderValue, highOrderValue });

            // read large value in two 16 bit chunks and perform conversion
            ushort[] registers = master.ReadHoldingRegisters(slaveID, startAddress, 2);
            uint value = ModbusUtility.GetUInt32(registers[1], registers[0]);
        }
    }
}
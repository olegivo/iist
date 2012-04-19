using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Modbus.Device;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    ///
    ///</summary>
    public class ModbusTcpIpAccessor : ModbusIpAccessor
    {

        ///<summary>
        ///
        ///</summary>
        ///<param name="port"></param>
        ///<param name="ipAddress"></param>
        ///<param name="fieldBusType"></param>
        public ModbusTcpIpAccessor(int port, IPAddress ipAddress, FieldBusType fieldBusType)
            : base(port, ipAddress, fieldBusType)
        {
            //if(ipAddress==null)
            //    throw new ArgumentNullException("ipAddress");
        }

        #region fields
        private TcpClient _client;

        #endregion

        #region properties
        ///<summary>
        /// ������ ����������� �� ��������� TCP
        ///</summary>
        private TcpClient Client
        {
            get
            {
                if (_client == null)
                    try
                    {
                        //IPEndPoint ipEndPoint = new IPEndPoint(IPAddress, Port);
                        //todo: ModbusTcpIpAccessor.Client - ���������� ������ � TCP-IP ������?
                        _client = new TcpClient(IPAddress.ToString(), Port);
                    }

                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        Debug.WriteLine(string.Format("���������� ������������ � TCP-IP ������ {0}:{1}", IPAddress, Port));
                    }

                return _client;
            }
        }
        #endregion


        #region Overrides of ModbusAccessor

        ///<summary>
        /// ��� �����
        ///</summary>
        public override string PortName
        {
            get
            {
                return string.Format("{0} : {1}.",
                                     IPAddress,
                                     Port);
            }
        }

        #endregion

        ///<summary>
        /// ���������������� ���������� �� Modbus 
        ///</summary>
        public override void InitializeModbusMaster()
        {
            if (_modbusAdapter == null)
            {
                Debug.WriteLine("������������� ���������� �� Modbus...");
                if (IPAddress!=null)
#if MBT
                    _modbusAdapter = new WagoTcpModbusAdapter(IPAddress.ToString(), (ushort)Port, true, 1000);
#else
                    _modbusAdapter = new NModbusAdapter(GetModbusMaster());
#endif
                else
                    Console.WriteLine("�� ����� IPAddress");
            }
        }

        private ModbusIpMaster GetModbusMaster()
        {
            //throw new NotImplementedException("InitializeModbusMaster");
            // ��������� ����������
            //_client = null;//todo: ModbusTcpIpAccessor.InitializeModbusMaster() - �� ����� �����������������?
            ModbusIpMaster modbusMaster = null;
            if (Client != null)
            {
                if (!Client.Connected)
                {
                    //TcpClient t = Client;
                    //t.BeginConnect(IPAddress, Port, ConnectCallback, t);
                    try
                    {
                        Client.Connect(IPAddress, Port);
                        Client.LingerState = new LingerOption(true, 3600);
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw new InvalidOperationException("���������� ��������� �������� ����������� � TCP-�������", ex);
                    }
                }

                if (modbusMaster == null)
                {
                    Debug.WriteLine("������������� ���������� �� Modbus...");
                    modbusMaster = ModbusIpMaster.CreateIp(Client);
                }

            }

            return modbusMaster;
        }

        ///<summary>
        /// ��������� ����������� ������� ����
        ///</summary>
        ///<returns></returns>
        public override bool CheckOnline()
        {
            if (_modbusAdapter == null)
            {
                Console.WriteLine("�� ��������������� _modbusAdapter");
                return false;
            }
            Console.WriteLine("������������ ����������� � {0}...", _client);
            bool[] coils = _modbusAdapter.ReadCoils(0, 0, 1);
            return coils != null && coils.Length > 0;
        }
    }
}
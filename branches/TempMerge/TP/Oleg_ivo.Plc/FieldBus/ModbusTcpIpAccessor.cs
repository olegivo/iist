using System;
using System.Net;
using System.Net.Sockets;
using Modbus.Device;
using NLog;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    ///
    ///</summary>
    public class ModbusTcpIpAccessor : ModbusIpAccessor
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

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
                        Log.Debug(ex.Message);
                        Log.Debug("���������� ������������ � TCP-IP ������ {0}:{1}", IPAddress, Port);
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
            Log.Debug("������������� ���������� �� Modbus/TCP");
            CheckClient();
            if (_modbusAdapter == null)
            {
                CreateModbusMaster();
            }
        }

        private void CreateModbusMaster()
        {
            if (IPAddress != null)
#if MBT
                    _modbusAdapter = new WagoTcpModbusAdapter(IPAddress.ToString(), (ushort)Port, true, 1000);
#else
            {
                Log.Debug("������������� ���������� �� Modbus...");
                ModbusIpMaster modbusIpMaster = GetModbusMaster();
                _modbusAdapter = new NModbusAdapter(modbusIpMaster);
            }
#endif
            else
                Log.Debug("�� ����� IPAddress");
        }

        /// <summary>
        /// ��������� ��������� �� ������, ���� �������� - ��������������
        /// </summary>
        private void CheckClient()
        {
            Log.Debug("�������� �������������� TCP-�������");
            var pollFailed = (Client.Client.Poll(10, SelectMode.SelectRead) && (Client.Available == 0));
            if (!Client.Connected || pollFailed)
            {
                Log.Debug("TCP-������ �� ���������, ������� �����������");
                //TcpClient t = Client;
                //t.BeginConnect(IPAddress, Port, ConnectCallback, t);
                try
                {
                    //Client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, );
                    Client.LingerState = new LingerOption(true, 3600);
                    Client.Connect(IPAddress, Port);
                }
                catch (InvalidOperationException ex)
                {
                    Log.Debug("���������� ��������� �������� ����������� � TCP-�������. ������� ����������� TCP-������", ex);
                    //����� ����� ��� �� ��������� �������������
                    //_client.Client.Shutdown();
                    _client.Client.Close();
                    _client = null;
                    _modbusAdapter = null;
                    modbusMaster = null;
                    CreateModbusMaster();
                }
                catch(Exception ex)
                {
                    throw;
                }
            }
            else
            {
                Log.Debug("TCP-������ ���������");
            }
        }

        ModbusIpMaster modbusMaster;
        private ModbusIpMaster GetModbusMaster()
        {
            //throw new NotImplementedException("InitializeModbusMaster");
            // ��������� ����������
            //_client = null;//todo: ModbusTcpIpAccessor.InitializeModbusMaster() - �� ����� �����������������?
            if (Client != null)
            {
                if (modbusMaster == null)
                {
                    Log.Debug("������������� ���������� �� Modbus...");
                    modbusMaster = ModbusIpMaster.CreateIp(Client);
                }

            }
            else
            {
                Log.Debug("Client is null");
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
                Log.Debug("�� ��������������� _modbusAdapter");
                return false;
            }
            if (_client == null)
            {
                Log.Debug("�� ��������������� _client");
                return false;
            }
            Log.Debug("������������ ����������� � {0}...", _client);
            bool[] coils = _modbusAdapter.ReadCoils(0, 0, 1);
            return coils != null && coils.Length > 0;
        }
    }
}
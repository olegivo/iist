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
        /// Клиент подключения по протоколу TCP
        ///</summary>
        private TcpClient Client
        {
            get
            {
                if (_client == null)
                    try
                    {
                        //IPEndPoint ipEndPoint = new IPEndPoint(IPAddress, Port);
                        //todo: ModbusTcpIpAccessor.Client - кэшировать доступ к TCP-IP каналу?
                        _client = new TcpClient(IPAddress.ToString(), Port);
                    }

                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        Debug.WriteLine(string.Format("Невозможно подключиться к TCP-IP каналу {0}:{1}", IPAddress, Port));
                    }

                return _client;
            }
        }
        #endregion


        #region Overrides of ModbusAccessor

        ///<summary>
        /// Имя порта
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
        /// Инициализировать управление по Modbus 
        ///</summary>
        public override void InitializeModbusMaster()
        {
            Console.WriteLine("Инициализация управления по Modbus/TCP");
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
                Debug.WriteLine("Инициализация управления по Modbus...");
                ModbusIpMaster modbusIpMaster = GetModbusMaster();
                _modbusAdapter = new NModbusAdapter(modbusIpMaster);
            }
#endif
            else
                Console.WriteLine("Не задан IPAddress");
        }

        /// <summary>
        /// Проверить подключен ли клиент, если возможно - переподключить
        /// </summary>
        private void CheckClient()
        {
            Console.WriteLine("Проверка подключенности TCP-клиента");
            var pollFailed = (Client.Client.Poll(10, SelectMode.SelectRead) && (Client.Available == 0));
            if (!Client.Connected || pollFailed)
            {
                Console.WriteLine("TCP-клиент не подключен, попытка подключения");
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
                    Console.WriteLine("Невозможно выполнить операцию подключения к TCP-клиенту. Попытка пересоздать TCP-клиент", ex);
                    //сброс полей для их повторной инициализации
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
                Console.WriteLine("TCP-клиент подключен");
            }
        }

        ModbusIpMaster modbusMaster = null;
        private ModbusIpMaster GetModbusMaster()
        {
            //throw new NotImplementedException("InitializeModbusMaster");
            // открываем соединение
            //_client = null;//todo: ModbusTcpIpAccessor.InitializeModbusMaster() - всё время переинициализация?
            if (Client != null)
            {
                if (modbusMaster == null)
                {
                    Debug.WriteLine("Инициализация управления по Modbus...");
                    modbusMaster = ModbusIpMaster.CreateIp(Client);
                }

            }
            else
            {
                Console.WriteLine("Client is null");
            }

            return modbusMaster;
        }

        ///<summary>
        /// Проверить доступность полевой шины
        ///</summary>
        ///<returns></returns>
        public override bool CheckOnline()
        {
            if (_modbusAdapter == null)
            {
                Console.WriteLine("Не инициализирован _modbusAdapter");
                return false;
            }
            if (_client == null)
            {
                Console.WriteLine("Не инициализирован _client");
                return false;
            }
            Console.WriteLine("Тестирование подключения к {0}...", _client);
            bool[] coils = _modbusAdapter.ReadCoils(0, 0, 1);
            return coils != null && coils.Length > 0;
        }
    }
}
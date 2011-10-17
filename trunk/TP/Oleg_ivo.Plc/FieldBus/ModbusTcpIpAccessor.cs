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
        #region fields
        private TcpClient _client;

        #endregion

        #region properties

        ///<summary>
        ///
        ///</summary>
        ///<param name="port"></param>
        ///<param name="ipAddress"></param>
        ///<param name="fieldBusType"></param>
        public ModbusTcpIpAccessor(int port, IPAddress ipAddress, FieldBusType fieldBusType) : base(port, ipAddress, fieldBusType)
        {
        }

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
                return string.Format("{0} : {1}. {2}",
                                     IPAddress,
                                     Port,
                                     Client != null && Client.Connected
                                         ? "Connected"
                                         : "Disconnected");
            }
        }

        #endregion

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component"></see> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only unmanaged resources. </param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && Client != null)
            {
                if (Client.Connected)
                {
                    Client.Client.Close();
                }
                Client.Close();
            }
        }


        ///<summary>
        /// Инициализировать управление по Modbus 
        ///</summary>
        public override void InitializeModbusMaster()
        {
            //throw new NotImplementedException("InitializeModbusMaster");
            // открываем соединение
            //_client = null;//todo: ModbusTcpIpAccessor.InitializeModbusMaster() - всё время переинициализация?
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
                        throw new InvalidOperationException("Невозможно выполнить операцию подключения к TCP-клиенту", ex);
                    } 
                }

                if (_modbusMaster == null)
                {
                    Debug.WriteLine("Инициализация управления по Modbus...");
                    _modbusMaster = ModbusIpMaster.CreateIp(Client);
                }

            }
        }

        ///<summary>
        /// Проверить доступность полевой шины
        ///</summary>
        ///<returns></returns>
        public override bool CheckOnline()
        {
            Console.WriteLine("Тестирование подключения к {0}...", _client);
            return Client!=null && Client.Connected;
        }
    }
}
using System;
using System.Net;
using System.Net.Sockets;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    ///<summary>
    /// Параметры подключения к полевой шине с поддержкой протокола TCP/IP
    ///</summary>
    public class TcpFieldBusPortParameters : FieldBusPortParameters
    {
        #region fields

        #endregion

        #region properties

        /// <summary>
        /// Порт подключения
        /// </summary>
        public int Port { get; set; }

        ///<summary>
        /// IP-адрес подключения
        ///</summary>
        public IPAddress IpAddress { get; set; }

        #endregion

        #region constructors

        ///<summary>
        ///
        ///</summary>
        ///<param name="ipAddress"></param>
        ///<param name="port"></param>
        public TcpFieldBusPortParameters(IPAddress ipAddress, int port) : base(FieldBusType.Ethernet)
        {
            //IPAddress = new IPAddress(new byte[]{127, 0, 0, 1});
            Port = port;
            IpAddress = ipAddress;
        }


        /// <summary>
        /// Параметры порта (по умолчанию)
        /// </summary>
        public TcpFieldBusPortParameters()
            : this(null, 502)
        {
        }

        #endregion


        #region methods
        ///<summary>
        /// Назначить порту данные параметры
        ///</summary>
        ///<param name="client">Порт</param>
        ///<exception cref="ArgumentNullException"></exception>
        public void Assign(TcpClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            //todo: назначить параметры?
            //client.Client.Blocking = Blocking;
            //client..DataBits = DataBits;
            //client.Parity = Parity;
            //client.StopBits = StopBits;
        }

        #endregion


    }
}
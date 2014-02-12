using System;
using System.Net;
using System.Net.Sockets;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    ///<summary>
    /// ��������� ����������� � ������� ���� � ���������� ��������� TCP/IP
    ///</summary>
    public class TcpFieldBusPortParameters : FieldBusPortParameters
    {
        #region fields

        #endregion

        #region properties

        /// <summary>
        /// ���� �����������
        /// </summary>
        public int Port { get; set; }

        ///<summary>
        /// IP-����� �����������
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
        /// ��������� ����� (�� ���������)
        /// </summary>
        public TcpFieldBusPortParameters()
            : this(null, 502)
        {
        }

        #endregion


        #region methods
        ///<summary>
        /// ��������� ����� ������ ���������
        ///</summary>
        ///<param name="client">����</param>
        ///<exception cref="ArgumentNullException"></exception>
        public void Assign(TcpClient client)
        {
            if (client == null) throw new ArgumentNullException("client");

            //todo: ��������� ���������?
            //client.Client.Blocking = Blocking;
            //client..DataBits = DataBits;
            //client.Parity = Parity;
            //client.StopBits = StopBits;
        }

        #endregion


    }
}
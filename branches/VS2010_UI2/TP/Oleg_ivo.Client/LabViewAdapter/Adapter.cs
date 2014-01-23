using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using DMS.Common.Messages;

namespace Oleg_ivo.HighLevelClient.LabViewAdapter
{
    /// <summary>
    /// ������� LabView
    /// </summary>
    public class Adapter
    {
        /// <summary>
        /// ������������ ������ ������� �� �������� � LabView
        /// </summary>
        public int QueueMaxSize { get; private set; }

        #region Singleton

        private static Adapter _instance;

        ///<summary>
        /// ������������ ���������
        ///</summary>
        public static Adapter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Adapter(5);//NOTE: ����� ������� ����� ������ ������� ���������
                }
                return _instance;
            }
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="Adapter" />.
        /// </summary>
        /// <param name="queueMaxSize">������������ ������ ������� �� �������� � LabView</param>
        private Adapter(int queueMaxSize)
        {
            QueueMaxSize = queueMaxSize;
            _server = new ThreadedServer(_port);
            _server.ConnectionProcessing += _server_ConnectionProcessing;
            _server.Start();
            //SetupServerSocket();
        }

        void _server_ConnectionProcessing(object sender, ConnectionInfoEventArgs e)
        {
            //����� ������������ ����������, ������ � ������� 5 ������ ���������� ������� TF_WHO_AM_I 
            if (ReceiveWHO_AM_I(e.ConnectionInfo.Socket))
            {
                //� �������� �� �� ����� (TF_WHO_AM_I_REPLY), � ������� ���������� � ����������� ���������� ������ � ��������
                SendWHO_AM_I_REPLY(e.ConnectionInfo.Socket);

                while (true)
                {
                    CheckNewData(e.ConnectionInfo.Socket);
                    return;
                }
            }
        }

        private void CheckNewData(Socket socket)
        {
            ClearExcessQueueElements();

            if(_queue.Count > 0)
            {
                Console.WriteLine("������� ������ ��� ��������\t[{0}]", DateTime.Now);

                byte[] bytes = _queue.Dequeue();
                Console.WriteLine("������:\t{0}", GetStringBytes(bytes));
                Console.WriteLine("�������� ��������� � ������� - {0} {1}\t[{1}]", _queue.Count, DateTime.Now);
                Send(socket, LabViewServerState.TF_NEW_DATA, bytes);
            }
        }

        /// <summary>
        /// �������� ������ �������� � �������
        /// </summary>
        private void ClearExcessQueueElements()
        {
            int counter = 0;
            while (_queue.Count > QueueMaxSize)
            {
                if(counter==0) 
                    Console.WriteLine("� ������� ���������� ������ ��������, ���������� �������. ��� ����� �������:");

                byte[] bytes = _queue.Dequeue();
                Console.WriteLine("������ �{1}:\t{0}", GetStringBytes(bytes), ++counter);
            }
            if(counter > 0)
                Console.WriteLine("�� ������� ������ ���������:\t{0}", counter);
        }

        private static string GetStringBytes(IEnumerable<byte> bytes)
        {
            return string.Format("[{0}]", bytes.Select(b => b.ToString()).Aggregate((left, result) => left + "\t" + result));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        private void AddToQueue(byte[] value)
        {
            ClearExcessQueueElements();

            Console.WriteLine("���������� �������� ������ � �������\t[{0}]\t", DateTime.Now);
            Console.WriteLine("������:\t{0}", GetStringBytes(value));
            _queue.Enqueue(value);
            Console.WriteLine("������ ��������� � �������, ��������� � ������� - {1}\t[{0}]", DateTime.Now, _queue.Count);
        }

        private readonly Queue<byte[]> _queue = new Queue<byte[]>();

        /// <summary>
        /// �������� ��������� ������� ������
        /// </summary>
        /// <param name="count">����������</param>
        /// <returns></returns>
        private static byte[] GetEmptyBytes(int count)
        {
            var bytes = new byte[count];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = 0;
            }
            return bytes;
        }

        private void SendWHO_AM_I_REPLY(Socket socket)
        {
            //� �������� �� �� ����� (TF_WHO_AM_I_REPLY), � ������� ���������� � ����������� ���������� ������ � ��������
            List<byte> bytes = new List<byte>(GetEmptyBytes(32));
            bytes[0] = 255;//��������� ������ ������ ������ ����
            Send(socket, LabViewServerState.TF_WHO_AM_I_REPLY, bytes);
        }

        private static void Send(Socket socket, LabViewServerState state, IEnumerable<byte> data)
        {
            Console.WriteLine("�������� ������ � LabView...\t[{0}]", DateTime.Now);

            List<byte> bytes = new List<byte>(GetEmptyBytes(2));
            bytes[0] = (byte)state;//��������� ��� ���������
            bytes.AddRange(data);//��������� ������
            if(bytes.Count!=34)
            {
                throw new ArgumentOutOfRangeException("��������� ������ ���� ������ 34 �����");
            }
            socket.Send(bytes.ToArray());

            Console.WriteLine("������ ����������\t[{0}]", DateTime.Now);
        }

        private static bool ReceiveWHO_AM_I(Socket socket)
        {
            //������ � ������� 5 ������ ���������� ������� TF_WHO_AM_I
            byte[] receivedBytes = ReceiveBytes(socket, LabViewClientState.STATE_SEND_WAI);

            Byte PV_Major = receivedBytes[0];
            Byte PV_Minor = receivedBytes[1];
            //������ ���������. ������������ ��� �������� �������������.
            //��� ������ 1.0, PV_Major = 1, PV_Minor = 0

            //Byte ClientCode = receivedBytes[2];
            //��� ���� �������. ���������������. �.�. ����� ����
            return (PV_Major == 1 && PV_Minor == 0);
        }

        private static byte[] ReceiveBytes(Socket socket, LabViewClientState expectedClientState)
        {
            byte[] receivedBytes = new byte[34];

            int bytesRead = socket.Receive(receivedBytes, 34, SocketFlags.None);
            if (bytesRead > 0)
            {
                LabViewClientState messageType = (LabViewClientState) BitConverter.ToInt16(receivedBytes.Take(2).ToArray(), 0);

                if (messageType==expectedClientState)
                {
                    return receivedBytes.Skip(2).ToArray();
                }

                string message =
                    string.Format(
                        "������� ����������� ������ �������. ��������� ������ = {0}, ���������� ������ = {1}",
                        expectedClientState, messageType);
                throw new InvalidOperationException(message);
            }

            throw new InvalidOperationException("�� ���� ��������� �� ������ �����!");
        }

        #endregion

        //private readonly ThreadedServer _server;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Send(InternalMessage message)
        {
            //TF_NEW_DATA - �������� ������������� ����������:
            // AIEntry_struct 
            // AIEntry 


            //IEntry_struct:
            // Byte EDate[3] 
            //  EDate[0] = (���-2000) 
            //  EDate[1] = ����� (����� � ��������� [1;12]) 
            //  EDate[2] = ���� [1;31] 
            // Byte ETime[3] 
            //  ETime[0] = ��� 
            //  ETime[1] = ������ 
            //  ETime[2] = ������� 
            // Long Result - (4 �����) - ��������� 
            // Byte XCS - ����������� ����� (�������� �� mod 2 ���� ���������� ������ �������������� ������)

            /*******************************************/
            
            byte[] bytes = GetAdaptedMessage(message);//new byte[256];
            if (bytes.Length!=32)
            {
                throw new ArgumentOutOfRangeException("������ ��������� ������ ���� ������ 32 �����");
            }

            AddToQueue(bytes);

            //try
            //{
            //    Socket s = new Socket(remoteEndpoint.Address.AddressFamily, SocketType.Raw, ProtocolType.Unspecified);
            //    Console.WriteLine("Sending data.");

            //    // This call blocks. 
            //    int i = s.SendTo(bytes, remoteEndpoint);
            //    s.Close();

            //    Console.WriteLine("Sent {0} bytes.", i);

            //    //// Get reply from the server.
            //    //i = _serverSocket.Receive(bytes);
            //    //Console.WriteLine(Encoding.UTF8.GetString(bytes));
            //}
            //catch (SocketException e)
            //{
            //    Console.WriteLine("{0} Error code: {1}.", e.Message, e.ErrorCode);
            //}

        }

        /// <summary>
        /// �������� �� ��������� �����, ������� ����� ������� � LabView
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static byte[] GetAdaptedMessage(InternalMessage message)
        {
            Console.WriteLine("��������� ������ ��� �������� � LabView...");

            byte[] bytes = GetEmptyBytes(32);
            //AIEntry_struct:
            // Unsigned Long Controller_ID (4 �����) 
            short shift = 4;
            // IEntry_struct IEntry (13 ����) 
            // Byte NA[15] 

            byte[] result = GetAdaptedData(message);
            if (result.Length!=4)
            {
                throw new ArgumentOutOfRangeException("������ ���������� ������ ���� ������ 4 �����");
            }

            //IEntry_struct:
            // Word ID - ������������� ������ (2 �����)
            Console.Write("����� �{0}\t", ((InternalLogicalChannelDataMessage) message).LogicalChannelId);
            byte[] id = BitConverter.GetBytes((Int16) ((InternalLogicalChannelDataMessage) message).LogicalChannelId);
            bytes[shift++] = id[0];//������� ����
            bytes[shift++] = id[1];//������� ����

            // Byte EDate[3] 
            Console.WriteLine("\t{0}", message.TimeStamp);
            //  EDate[0] = (���-2000) 
            bytes[shift++] = (byte)(message.TimeStamp.Year - 2000);
            //  EDate[1] = ����� (����� � ��������� [1;12]) 
            bytes[shift++] = (byte)(message.TimeStamp.Month);
            //  EDate[2] = ���� [1;31] 
            bytes[shift++] = (byte)(message.TimeStamp.Day);
            // Byte ETime[3] 
            //  ETime[0] = ��� 
            bytes[shift++] = (byte)(message.TimeStamp.Hour);
            //  ETime[1] = ������ 
            bytes[shift++] = (byte)(message.TimeStamp.Minute);
            //  ETime[2] = ������� 
            bytes[shift++] = (byte)(message.TimeStamp.Second);

            // Long Result - (4 �����) - ���������
            int upper = shift + 4;
            for (int i=0; shift < upper; shift++, i++)
            {
                bytes[shift] = result[i];
            }

            upper = shift;
            byte xcs = 0;
            // Byte XCS - ����������� ����� (�������� �� mod 2 ���� 12 ���������� ������ �������������� ������)
            for (int i = 4; i < upper; i++)
            {
                xcs = (byte)(xcs ^ bytes[i]);
            }

            bytes[shift] = xcs;

            Console.WriteLine("������ ��� �������� � LabView ��������");

            return bytes;
        }

        private static byte[] GetAdaptedData(InternalMessage message)
        {
            if (message is InternalLogicalChannelDataMessage)
            {
                var dataMessage = (InternalLogicalChannelDataMessage)message;
                Console.Write("������������ ������: {0}\t", dataMessage.Value);

                //�������������� ����������� �������� � ����� ��� �������� � LabView
                object value = Convert.ToInt32(Convert.ToDouble(dataMessage.Value) * 10);//NOTE: ��� �������� � LabView �������� �� 10
                byte[] adaptedData;
                if (value is double) adaptedData = BitConverter.GetBytes((double)value);
                else if (value is float)
                    adaptedData = BitConverter.GetBytes((float)value);
                else if (value is ulong)
                    adaptedData = BitConverter.GetBytes((ulong)value);
                else if (value is uint)
                    adaptedData = BitConverter.GetBytes((uint)value);
                else if (value is ushort)
                    adaptedData = BitConverter.GetBytes((ushort)value);
                else if (value is long)
                    adaptedData = BitConverter.GetBytes((long)value);
                else if (value is int)
                    adaptedData = BitConverter.GetBytes((int)value);
                else if (value is short)
                    adaptedData = BitConverter.GetBytes((short)value);
                else if (value is char)
                    adaptedData = BitConverter.GetBytes((char)value);
                else if (value is bool)
                    adaptedData = BitConverter.GetBytes((bool)value);
                else
                    throw new InvalidCastException(
                        "���������� ������������� ������������ �������� �� � ������ �� ����������� �����");

                if (dataMessage.LogicalChannelId == 0)
                    throw new ArgumentException("�� ������ ����� ������");

                //��������� � ��������� ������������ ��������
                return adaptedData;
            }
            
            throw new ArgumentOutOfRangeException("����������� ��� ���������" + message.GetType());
        }

        //private Socket _serverSocket;
        //private IPEndPoint remoteEndpoint;
        private readonly ThreadedServer _server;
        private const int _port = 26500;

/*
        private void SetupServerSocket()
        {
            // �������� ���������� � ��������� ����������
            IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint myEndpoint;
            myEndpoint = new IPEndPoint(localMachineInfo.AddressList[0], _port);
            remoteEndpoint = new IPEndPoint(localMachineInfo.AddressList[0], _port + 1);

            // ������� �����, ����������� ��� � ������
            // � �������� �������������
            //_serverSocket = new Socket(myEndpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //_serverSocket.Bind(myEndpoint);
            //_serverSocket.Listen((int)SocketOptionName.MaxConnections);
        }
*/
    }
}
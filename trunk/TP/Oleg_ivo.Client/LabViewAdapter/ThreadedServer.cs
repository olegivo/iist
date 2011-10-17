using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Oleg_ivo.HighLevelClient.LabViewAdapter
{
    /// <summary>
    /// 
    /// </summary>
    public class ThreadedServer
    {
        private Socket _serverSocket;
        private readonly int _port;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public ThreadedServer(int port)
        {
            _port = port;
        }

        private Thread _acceptThread;
        private readonly List<ConnectionInfo> _connections = new List<ConnectionInfo>();

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            SetupServerSocket();
            _acceptThread = new Thread(AcceptConnections) { IsBackground = true };
            _acceptThread.Start();
        }

        private void SetupServerSocket()
        {
            // Получаем информацию о локальном компьютере
            IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());

            IPAddress address = localMachineInfo.AddressList[0];
            IPEndPoint myEndpoint = new IPEndPoint(address, _port);

            // Создаем сокет, привязываем его к адресу
            // и начинаем прослушивание
            _serverSocket = new Socket(
                myEndpoint.Address.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(myEndpoint);
            _serverSocket.Listen((int)SocketOptionName.MaxConnections);
        }

        private void AcceptConnections()
        {
            while (true)
            {
                // Принимаем соединение
                Socket socket = _serverSocket.Accept();
                ConnectionInfo connection = new ConnectionInfo
                                                {
                                                    Socket = socket,
                                                    Thread = new Thread(ProcessConnection) { IsBackground = true }
                                                };

                // Создаем поток для получения данных
                connection.Thread.Start(connection);

                // Сохраняем сокет
                lock (_connections) _connections.Add(connection);
            }
// ReSharper disable FunctionNeverReturns
        }
// ReSharper restore FunctionNeverReturns

        ///<summary>
        ///
        ///</summary>
        public event EventHandler<ConnectionInfoEventArgs> ConnectionProcessing;

        private void InvokeConnectionProcessing(ConnectionInfoEventArgs e)
        {
            EventHandler<ConnectionInfoEventArgs> connectionProcessingHandler = ConnectionProcessing;
            if (connectionProcessingHandler != null) connectionProcessingHandler(this, e);
        }

        private void ProcessConnection(object state)
        {
            ConnectionInfo connection = (ConnectionInfo)state;

            //byte[] buffer = new byte[34];
            try
            {
                //запуск конечного автомата:
                InvokeConnectionProcessing(new ConnectionInfoEventArgs { ConnectionInfo = connection });

                //List<byte> bytes = new List<byte>();
                //while (true)
                //{
                //    int bytesRead = connection.Socket.Receive(buffer);
                //    if (bytesRead > 0)
                //    {
                //        lock (_connections)
                //        {
                //            bytes.AddRange(buffer);
                //            //foreach (ConnectionInfo connectionInfo in _connections.Where(conn => conn != connection))
                //            //{
                //            //    connectionInfo.Socket.Send(buffer, bytesRead, SocketFlags.None);
                //            //}
                //        }
                //    }
                //    else if (bytesRead == 0)
                //    {
                //        return;
                //    }
                //}
            }
            catch (SocketException exc)
            {
                Console.WriteLine("Socket exception: " + exc.SocketErrorCode);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception: " + exc);
            }
            finally
            {
                connection.Socket.Close();
                lock (_connections)
                    _connections.Remove(connection);
            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TcpDataEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public byte[] Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Socket Socket { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Thread Thread { get; set; }
    }

    ///<summary>
    ///
    ///</summary>
    public class ConnectionInfoEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public ConnectionInfo ConnectionInfo { get; set; }

    }
}
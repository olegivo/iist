using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using DMS.Common.Messages;

namespace Oleg_ivo.HighLevelClient.LabViewAdapter
{
    /// <summary>
    /// Адаптер LabView
    /// </summary>
    public class Adapter
    {
        /// <summary>
        /// Максимальный размер очереди на отправку в LabView
        /// </summary>
        public int QueueMaxSize { get; private set; }

        #region Singleton

        private static Adapter _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static Adapter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Adapter(5);//NOTE: здесь задаётся также размер очереди сообщений
                }
                return _instance;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Adapter" />.
        /// </summary>
        /// <param name="queueMaxSize">Максимальный размер очереди на отправку в LabView</param>
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
            //После установления соединения, клиент в течение 5 секунд отправляет посылку TF_WHO_AM_I 
            if (ReceiveWHO_AM_I(e.ConnectionInfo.Socket))
            {
                //и получает на неё ответ (TF_WHO_AM_I_REPLY), в котором сообщается о возможности дальнейшей работы с сервером
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
                Console.WriteLine("Найдены данные для отправки\t[{0}]", DateTime.Now);

                byte[] bytes = _queue.Dequeue();
                Console.WriteLine("Данные:\t{0}", GetStringBytes(bytes));
                Console.WriteLine("Осталось элементов в очереди - {0} {1}\t[{1}]", _queue.Count, DateTime.Now);
                Send(socket, LabViewServerState.TF_NEW_DATA, bytes);
            }
        }

        /// <summary>
        /// Очистить лишние элементы в очереди
        /// </summary>
        private void ClearExcessQueueElements()
        {
            int counter = 0;
            while (_queue.Count > QueueMaxSize)
            {
                if(counter==0) 
                    Console.WriteLine("В очереди обнаружены лишние элементы, тормозящие очередь. Они будут удалены:");

                byte[] bytes = _queue.Dequeue();
                Console.WriteLine("Данные №{1}:\t{0}", GetStringBytes(bytes), ++counter);
            }
            if(counter > 0)
                Console.WriteLine("Из очереди убрано элементов:\t{0}", counter);
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

            Console.WriteLine("Необходимо добавить данные в очередь\t[{0}]\t", DateTime.Now);
            Console.WriteLine("Данные:\t{0}", GetStringBytes(value));
            _queue.Enqueue(value);
            Console.WriteLine("Данные добавлены в очередь, элементов в очереди - {1}\t[{0}]", DateTime.Now, _queue.Count);
        }

        private readonly Queue<byte[]> _queue = new Queue<byte[]>();

        /// <summary>
        /// Получить несколько нулевых байтов
        /// </summary>
        /// <param name="count">Количество</param>
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
            //и получает на неё ответ (TF_WHO_AM_I_REPLY), в котором сообщается о возможности дальнейшей работы с сервером
            List<byte> bytes = new List<byte>(GetEmptyBytes(32));
            bytes[0] = 255;//заполняем даными только первый байт
            Send(socket, LabViewServerState.TF_WHO_AM_I_REPLY, bytes);
        }

        private static void Send(Socket socket, LabViewServerState state, IEnumerable<byte> data)
        {
            Console.WriteLine("Отправка данных в LabView...\t[{0}]", DateTime.Now);

            List<byte> bytes = new List<byte>(GetEmptyBytes(2));
            bytes[0] = (byte)state;//заполняем тип сообщения
            bytes.AddRange(data);//добавляем данные
            if(bytes.Count!=34)
            {
                throw new ArgumentOutOfRangeException("Сообщение должно быть длиной 34 байта");
            }
            socket.Send(bytes.ToArray());

            Console.WriteLine("Данные отправлены\t[{0}]", DateTime.Now);
        }

        private static bool ReceiveWHO_AM_I(Socket socket)
        {
            //клиент в течение 5 секунд отправляет посылку TF_WHO_AM_I
            byte[] receivedBytes = ReceiveBytes(socket, LabViewClientState.STATE_SEND_WAI);

            Byte PV_Major = receivedBytes[0];
            Byte PV_Minor = receivedBytes[1];
            //Версия протокола. Используется для проверки совместимости.
            //Для версии 1.0, PV_Major = 1, PV_Minor = 0

            //Byte ClientCode = receivedBytes[2];
            //Код типа клиента. Зарезервировано. Д.б. равно нулю
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
                        "Получен неожиданный статус клиента. Ожидаемый статус = {0}, полученный статус = {1}",
                        expectedClientState, messageType);
                throw new InvalidOperationException(message);
            }

            throw new InvalidOperationException("Не было прочитано ни одного байта!");
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
            //TF_NEW_DATA - передача измерительной информации:
            // AIEntry_struct 
            // AIEntry 


            //IEntry_struct:
            // Byte EDate[3] 
            //  EDate[0] = (год-2000) 
            //  EDate[1] = месяц (число в диапазоне [1;12]) 
            //  EDate[2] = день [1;31] 
            // Byte ETime[3] 
            //  ETime[0] = час 
            //  ETime[1] = минута 
            //  ETime[2] = секунда 
            // Long Result - (4 байта) - Результат 
            // Byte XCS - контрольная сумма (сложение по mod 2 всех предыдущих байтов информационной записи)

            /*******************************************/
            
            byte[] bytes = GetAdaptedMessage(message);//new byte[256];
            if (bytes.Length!=32)
            {
                throw new ArgumentOutOfRangeException("Данные сообщения должны быть длиной 32 байта");
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
        /// Получить из сообщения байты, которые можно послать в LabView
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static byte[] GetAdaptedMessage(InternalMessage message)
        {
            Console.WriteLine("Получение данных для отправки в LabView...");

            byte[] bytes = GetEmptyBytes(32);
            //AIEntry_struct:
            // Unsigned Long Controller_ID (4 байта) 
            short shift = 4;
            // IEntry_struct IEntry (13 байт) 
            // Byte NA[15] 

            byte[] result = GetAdaptedData(message);
            if (result.Length!=4)
            {
                throw new ArgumentOutOfRangeException("Данные результата должны быть длиной 4 байта");
            }

            //IEntry_struct:
            // Word ID - Идентификатор записи (2 байта)
            Console.Write("Канал №{0}\t", ((InternalLogicalChannelDataMessage) message).LogicalChannelId);
            byte[] id = BitConverter.GetBytes((Int16) ((InternalLogicalChannelDataMessage) message).LogicalChannelId);
            bytes[shift++] = id[0];//младший байт
            bytes[shift++] = id[1];//старший байт

            // Byte EDate[3] 
            Console.WriteLine("\t{0}", message.TimeStamp);
            //  EDate[0] = (год-2000) 
            bytes[shift++] = (byte)(message.TimeStamp.Year - 2000);
            //  EDate[1] = месяц (число в диапазоне [1;12]) 
            bytes[shift++] = (byte)(message.TimeStamp.Month);
            //  EDate[2] = день [1;31] 
            bytes[shift++] = (byte)(message.TimeStamp.Day);
            // Byte ETime[3] 
            //  ETime[0] = час 
            bytes[shift++] = (byte)(message.TimeStamp.Hour);
            //  ETime[1] = минута 
            bytes[shift++] = (byte)(message.TimeStamp.Minute);
            //  ETime[2] = секунда 
            bytes[shift++] = (byte)(message.TimeStamp.Second);

            // Long Result - (4 байта) - Результат
            int upper = shift + 4;
            for (int i=0; shift < upper; shift++, i++)
            {
                bytes[shift] = result[i];
            }

            upper = shift;
            byte xcs = 0;
            // Byte XCS - контрольная сумма (сложение по mod 2 всех 12 предыдущих байтов информационной записи)
            for (int i = 4; i < upper; i++)
            {
                xcs = (byte)(xcs ^ bytes[i]);
            }

            bytes[shift] = xcs;

            Console.WriteLine("Данных для отправки в LabView получены");

            return bytes;
        }

        private static byte[] GetAdaptedData(InternalMessage message)
        {
            if (message is InternalLogicalChannelDataMessage)
            {
                var dataMessage = (InternalLogicalChannelDataMessage)message;
                Console.Write("Оригинальные данные: {0}\t", dataMessage.Value);

                //Преобразование измеренного значения в байты для передачи в LabView
                object value = Convert.ToInt32(Convert.ToDouble(dataMessage.Value) * 10);//NOTE: для передачи в LabView умножаем на 10
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
                        "Невозможно преобразовать передаваемое значение ни к одному из стандартных типов");

                if (dataMessage.LogicalChannelId == 0)
                    throw new ArgumentException("Не указан номер канала");

                //добавляем в сообщение передаваемое значение
                return adaptedData;
            }
            
            throw new ArgumentOutOfRangeException("Неожиданный тип сообщения" + message.GetType());
        }

        //private Socket _serverSocket;
        //private IPEndPoint remoteEndpoint;
        private readonly ThreadedServer _server;
        private const int _port = 26500;

/*
        private void SetupServerSocket()
        {
            // Получаем информацию о локальном компьютере
            IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint myEndpoint;
            myEndpoint = new IPEndPoint(localMachineInfo.AddressList[0], _port);
            remoteEndpoint = new IPEndPoint(localMachineInfo.AddressList[0], _port + 1);

            // Создаем сокет, привязываем его к адресу
            // и начинаем прослушивание
            //_serverSocket = new Socket(myEndpoint.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //_serverSocket.Bind(myEndpoint);
            //_serverSocket.Listen((int)SocketOptionName.MaxConnections);
        }
*/
    }
}
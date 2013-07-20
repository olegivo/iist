using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Tools.ConnectionProvider;

namespace Oleg_ivo.MES.Logging
{
    ///<summary>
    /// Протоколирование сообщений
    ///</summary>
    public class InternalMessageLogger
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region Singleton

        private static InternalMessageLogger _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static InternalMessageLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InternalMessageLogger();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="InternalMessageLogger" />.
        /// </summary>
        private InternalMessageLogger()
        {
            _stopped = true;
        }

        #endregion

        #region Обработка очереди сообщений
        private class QueueElement
        {
            /// <summary>
            /// Сообщение
            /// </summary>
            public InternalMessage Message { get; internal set; }

            /// <summary>
            /// Временная метка прихода сообщения в очередь
            /// </summary>
            public DateTime IncomeTimeStamp { get; internal set; }
        }

        private readonly Queue<QueueElement> queue = new Queue<QueueElement>();

        /// <summary>
        /// Запустить очередь протоколирования
        /// </summary>
        public void Start()
        {
            if (_stopped)
            {
                _stopped = false;
                Thread thread = new Thread(MainLoop);
                thread.Start();
            }
        }

        /// <summary>
        /// Остановить очередь протоколирования
        /// </summary>
        /// <param name="forceInterruptProcessing">Остановить обработку оставшейся очереди</param>
        public void Stop(bool forceInterruptProcessing)
        {
            if (!_stopped)
                _stopped = true;

            // в режиме остановки обработки оставшейся очереди не ждём, когда очередь обработается, а очищаем её принудительно
            if (forceInterruptProcessing)
            {
                Log.Debug("Элементов в очереди - {0}", queue.Count);
                queue.Clear();
                Log.Debug("Очередь принудительно очищена");
            }
            else
            {
                Log.Debug("Ожидание обработки оставшейся очереди...");
                while (queue.Count > 0)
                {
                }
                Log.Debug("Очередь обработана");
            }
        }

        /// <summary>
        /// Добавить сообщение в очередь
        /// </summary>
        /// <param name="message"></param>
        private void AddMessageToQueue(InternalMessage message)
        {
            if (_stopped)
                throw new InvalidOperationException("Невозможно добавить сообщение в очередь, т.к. протоколирование остановлено");

            queue.Enqueue(new QueueElement { IncomeTimeStamp = DateTime.Now, Message = message });
        }

        private void MainLoop()
        {
            while (!_stopped || queue.Count > 0)
            {
                CheckNewData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _stopped;

        private void CheckNewData()
        {
            //ClearExcessQueueElements();

            if (queue.Count > 0)
            {
                Log.Debug("Найдены данные для отправки");

                QueueElement queueElement = queue.Dequeue();
                //Log.Debug("Данные:\t{0}", GetStringBytes(queueElement));
                Log.Debug("Осталось элементов в очереди - {0}", queue.Count);
                
                ProtocolMessage(queueElement.Message, queueElement.IncomeTimeStamp);
            }
        }

        /// <summary>
        /// Протоколировать сообщение
        /// </summary>
        /// <param name="message"></param>
        public void ProtocolMessage(InternalMessage message)
        {
            AddMessageToQueue(message);
        }

        #endregion

        /// <summary>
        /// Протоколировать сообщение
        /// </summary>
        /// <param name="message"></param>
        /// <param name="incomeTimeStamp"></param>
        private void ProtocolMessage(InternalMessage message, DateTime incomeTimeStamp)
        {
            var dataMessage = message as InternalLogicalChannelDataMessage;
            if (dataMessage == null)
                throw new ArgumentOutOfRangeException("Неожиданный тип сообщения" + message.GetType());
            var command = PrepareDataMessageCommand(dataMessage, incomeTimeStamp);

            if(command!=null)
                try
                {
                    DbConnectionProvider.Instance.OpenConnection(command);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("При протоколировании сообщения произошла ошибка", ex);
                }
                finally
                {
                    if(command.Connection.State != ConnectionState.Closed)
                        DbConnectionProvider.Instance.CloseConnection(command);
                }
            
        }

        /// <summary>
        /// Подготовка команды для вставки протокола сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="incomeTimeStamp"></param>
        private IDbCommand PrepareDataMessageCommand(InternalLogicalChannelDataMessage message, DateTime incomeTimeStamp)
        {
            StringBuilder builder = new StringBuilder("INSERT dbo.ProtocolData ( LogicalChannelId, TimeStamp, QueueTimeStamp, DataValue)");
            builder.AppendLine("VALUES (@LogicalChannelId, @TimeStamp, @QueueTimeStamp, @DataValue)");
            
            SqlCommand command = new SqlCommand(builder.ToString());
            command.Parameters.AddRange(new[]
                                            {
                                                new SqlParameter("@LogicalChannelId", message.LogicalChannelId),
                                                new SqlParameter("@TimeStamp", message.TimeStamp),
                                                new SqlParameter("@QueueTimeStamp", incomeTimeStamp),
                                                new SqlParameter("@DataValue", message.Value)
                                            });

            return command;
        }
    }
}

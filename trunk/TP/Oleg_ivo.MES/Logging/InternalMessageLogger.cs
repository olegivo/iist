using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Plc.Entities;
using Oleg_ivo.Tools.ConnectionProvider;

namespace Oleg_ivo.MES.Logging
{
    ///<summary>
    /// Протоколирование сообщений
    ///</summary>
    public class InternalMessageLogger
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="InternalMessageLogger" />.
        /// </summary>
        /// <param name="connectionProvider"></param>
        public InternalMessageLogger(DbConnectionProvider connectionProvider)
        {
            _stopped = true;
            this.connectionProvider = Enforce.ArgumentNotNull(connectionProvider, "connectionProvider");
        }

        #endregion

        [Dependency]
        public IComponentContext Context { get; set; }

        private PlcDataContext dataContext;

        protected PlcDataContext DataContext
        {
            get
            {
                return dataContext ??
                       (dataContext =
                           Context.Resolve<PlcDataContext>(new TypedParameter(typeof(string),
                               Context.Resolve<DbConnectionProvider>().DefaultConnectionString)));
            }
        }

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
            while (!_stopped || queue.Count > 0)//TODO:переделать на асинхронное взаимодействие очереди
            {
                CheckNewData();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _stopped;

        private DbConnectionProvider connectionProvider;

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
            var protocolData = CreateProtocolData(dataMessage, incomeTimeStamp);
            try
            {
                DataContext.ProtocolDatas.InsertOnSubmit(protocolData);
                DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("При протоколировании сообщения произошла ошибка", ex);
            }
        }

        /// <summary>
        /// Подготовка команды для вставки протокола сообщения
        /// </summary>
        /// <param name="message"></param>
        /// <param name="incomeTimeStamp"></param>
        private ProtocolData CreateProtocolData(InternalLogicalChannelDataMessage message, DateTime incomeTimeStamp)
        {
            var client = DataContext.Clients.Single(c=>c.ClientName==message.RegNameFrom);//TODO:move client data to message
            return new ProtocolData
            {
                LogicalChannelId = message.LogicalChannelId,
                TimeStamp = message.TimeStamp,
                QueueTimeStamp = incomeTimeStamp,
                DataValue = Convert.ToDecimal(message.Value),
                Client = client
            };
        }
    }
}

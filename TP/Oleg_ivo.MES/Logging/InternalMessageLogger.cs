using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using Autofac;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac;
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
        public InternalMessageLogger(IComponentContext context)
        {
            this.context = Enforce.ArgumentNotNull(context, "context");
            isStopped = true;
        }

        #endregion

        private readonly IComponentContext context;

        private PlcDataContext dataContext;

        protected PlcDataContext DataContext
        {
            get
            {
                return dataContext ??
                       (dataContext =
                           context.Resolve<PlcDataContext>(new TypedParameter(typeof(string),
                               context.Resolve<DbConnectionProvider>().DefaultConnectionString)));
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
            if (isStopped)
            {
                isStopped = false;
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
            if (!isStopped)
                isStopped = true;

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
            if (isStopped)
                throw new InvalidOperationException("Невозможно добавить сообщение в очередь, т.к. протоколирование остановлено");

            queue.Enqueue(new QueueElement { IncomeTimeStamp = DateTime.Now, Message = message });
        }

        private void MainLoop()
        {
            while (!isStopped || queue.Count > 0)//TODO:переделать на асинхронное взаимодействие очереди
            {
                CheckNewData();
            }
        }

        private bool isStopped;

        private void CheckNewData()
        {
            //ClearExcessQueueElements();

            if (queue.Count > 0)
            {
                Log.Debug("Найдены данные для отправки");

                var queueElement = queue.Dequeue();
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

        public void ProtocolError(InternalMessage message, FaultException faultException)
        {
            //TODO:записать ошибку в базу
            var ex = faultException.GetBaseException();
        }

        public void ProtocolEvent(InternalMessage message)
        {
            //TODO:записать событие в базу
/*
            Action<InternalLogicalChannelStateMessage> action =
                channelStateMessage =>
                {
                    /*TODO: событие об изменении состояния канала#1#
                };
            bool processed = ChainProcess(message, new []{action});
            var chain = new[]
            {
                CreateChainItem(message, action),
            };
            var hasResult = chain.Select(func => func()).FirstOrDefault(b=>b);
            if(!hasResult)
                throw new ArgumentOutOfRangeException("message",message, "Неожиданный тип сообщения");
*/
        }

/*
        private bool ChainProcess(InternalMessage message, IEnumerable<Action<InternalMessage>> actions)
        {
            return
                actions.Select(action => CreateChainItem(message, action))
                .Select(func => func())
                .FirstOrDefault(b => b);
        }

        /// <summary>
        /// Возвращает функтор, возвращающий false, если param не соответствует ожидаемому типу.
        /// Если параметр соответствует ожидаемому типу, функтор выполняет переданный action и возвращает true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TExpected"></typeparam>
        /// <param name="param"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private Func<bool> CreateChainItem<T, TExpected>(T param, Action<TExpected> action) where TExpected : class, T
        {
            if (param is TExpected)
            {
                return () =>
                {
                    action(param as TExpected);
                    return true;
                };
            }
            return () => false;
        }
*/
    }
}

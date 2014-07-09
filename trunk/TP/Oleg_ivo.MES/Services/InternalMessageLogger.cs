using System;
using System.Reactive.Subjects;
using Autofac;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Plc.Entities;
using Oleg_ivo.Tools.ConnectionProvider;
using ProtocolException = Oleg_ivo.Plc.Entities.ProtocolException;

namespace Oleg_ivo.MES.Services
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

        [Dependency(Required = true)]
        public ClientsProvider ClientsProvider { get; set; }


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

        /// <summary>
        /// Запустить очередь протоколирования
        /// </summary>
        public void Start()
        {
            lock (this)
            {
                if (!isStopped) return;
                subject = new Subject<QueueElement>();
                subject.Subscribe(queueElement => ProcessQueueItem(queueElement.Message, queueElement.IncomeTimeStamp));
                isStopped = false;
            }
        }

        /// <summary>
        /// Остановить очередь протоколирования
        /// </summary>
        /// <param name="forceInterruptProcessing">Остановить обработку оставшейся очереди</param>
        public void Stop(bool forceInterruptProcessing)
        {
            lock (this)
            {
                if (isStopped) return;
                isStopped = true;
                if (forceInterruptProcessing)
                {
                    subject.OnCompleted();//TODO:аварийное прерывание обработки?
                    Log.Debug("Очередь принудительно очищена");
                }
                else
                {
                    Log.Debug("Ожидание обработки оставшейся очереди");
                    subject.OnCompleted();
                    Log.Debug("Очередь обработана");
                }
                subject.Dispose();
                subject = null;
            }
        }

        /// <summary>
        /// Добавить сообщение в очередь
        /// </summary>
        /// <param name="message"></param>
        private void AddMessageToQueue(InternalMessage message)
        {
            lock (this)
            {
                if (isStopped)
                    throw new InvalidOperationException("Невозможно добавить сообщение в очередь, т.к. протоколирование остановлено");

                Log.Trace("Добавление сообщения в очередь ({0})", message.GetType().Name);
                subject.OnNext(new QueueElement { IncomeTimeStamp = DateTime.Now, Message = message });                
            }
        }

        private bool isStopped;
        private Subject<QueueElement> subject;

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
        private void ProcessQueueItem(InternalMessage message, DateTime incomeTimeStamp)
        {
            //Log.Debug("Найдены данные для отправки");
            //Log.Debug("Осталось элементов в очереди: {0}", subject.Count().First());

            var dataMessage = message as InternalLogicalChannelDataMessage;
            if (dataMessage != null)
            {
                WriteProtocolData(incomeTimeStamp, dataMessage);
                return;
            }

            var serviceMessage = message as InternalServiceMessage;
            if (serviceMessage != null)
            {
                var errorMessage = serviceMessage as InternalErrorMessage;
                var protocolException = errorMessage != null
                    ? new ProtocolException
                    {
                        Message = errorMessage.Error,
                        StackTrace = errorMessage.StackTrace
                    }
                    : null;
                WriteProtocolEvent(incomeTimeStamp, serviceMessage, protocolException);
                return;
            }
            throw new InvalidOperationException("Неожиданный тип сообщения " + message.GetType());
        }

        private void WriteProtocolData(DateTime incomeTimeStamp, InternalLogicalChannelDataMessage dataMessage)
        {
            var client = GetClient(dataMessage);
            var protocolData = dataMessage.IsDiscreteData
                ? (ProtocolData) new ProtocolDataDiscrete
                {
                    LogicalChannelId = dataMessage.LogicalChannelId,
                    TimeStamp = dataMessage.TimeStamp,
                    QueueTimeStamp = incomeTimeStamp,
                    DiscreteValue = (bool?) (dataMessage.Value),
                    Client = client
                }
                : new ProtocolDataAnalog
                {
                    LogicalChannelId = dataMessage.LogicalChannelId,
                    TimeStamp = dataMessage.TimeStamp,
                    QueueTimeStamp = incomeTimeStamp,
                    AnalogValue = Convert.ToDecimal(dataMessage.Value),
                    Client = client
                };
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

        private void WriteProtocolEvent(DateTime incomeTimeStamp, InternalServiceMessage serviceMessage, ProtocolException protocolException)
        {
            var client = GetClient(serviceMessage);
            var protocolEvent = new ProtocolEvent
            {
                TimeStamp = serviceMessage.TimeStamp,
                QueueTimeStamp = incomeTimeStamp,
                Client = client,
                EventTypeId = (short) serviceMessage.EventType,
                ProtocolException = protocolException
            };

            var channelStateMessage = serviceMessage as InternalLogicalChannelStateMessage;
            if (channelStateMessage != null)
            {
                protocolEvent.LogicalChannelId = channelStateMessage.LogicalChannelId;
                protocolEvent.Data = channelStateMessage.State.ToString();
            }

            try
            {
                DataContext.ProtocolEvents.InsertOnSubmit(protocolEvent);
                DataContext.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("При протоколировании сообщения произошла ошибка", ex);
            }
        }

        private Client GetClient(InternalMessage message)
        {
            //TODO:move client data to message?
            return ClientsProvider.GetClient(message.RegNameFrom);
        }

/*
        private void ProtocolEvent(InternalMessage message)
        {
            //TODO:записать событие в базу
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
        }
*/

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

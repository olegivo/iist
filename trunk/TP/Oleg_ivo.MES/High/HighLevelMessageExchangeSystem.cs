using System;
using System.Collections.Generic;
using System.ServiceModel;
using DMS.Common.MessageExchangeSystem.HighLevel;
using DMS.Common.Messages;
using Oleg_ivo.MES.Low;
using Oleg_ivo.MES.Registered;

using System.Linq;

namespace Oleg_ivo.MES.High
{
    ///<summary>
    /// Система обмена сообщениями c клиентами верхнего уровня
    ///</summary>
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single, 
        ConcurrencyMode = ConcurrencyMode.Reentrant, 
        AutomaticSessionShutdown = false, 
        IncludeExceptionDetailInFaults = true)]
    public class HighLevelMessageExchangeSystem : AbstractLevelMessageExchangeSystem<RegisteredHighLevelClient>, IHighLevelMessageExchangeSystem
    {
        #region Singleton

        private static HighLevelMessageExchangeSystem _instance;
        /// <summary>
        /// Если клиента нет в списке заинтересованных клиентов, то он ни разу не запрашивал информацию о зарегистрированных каналах.
        /// </summary>
        private readonly List<RegisteredHighLevelClient> InterestedRegisteredClients = new List<RegisteredHighLevelClient>();

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static HighLevelMessageExchangeSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HighLevelMessageExchangeSystem();
                    _instance.NotifySubscribeEvents();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="HighLevelMessageExchangeSystem" />.
        /// </summary>
        private HighLevelMessageExchangeSystem()
        {
        }

        #endregion

        private void Low_ChannelRegistered(object sender, LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            foreach (RegisteredHighLevelClient registeredHighLevelClient in InterestedRegisteredClients.Where(client => client.Interested))
                registeredHighLevelClient.ChannelRegister(e.ChannelSubscribeMessage);
        }

        private void Low_ChannelUnregistered(object sender, LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            foreach (RegisteredHighLevelClient registeredHighLevelClient in InterestedRegisteredClients.Where(client => client.Interested))
                registeredHighLevelClient.ChannelUnRegister(e.ChannelSubscribeMessage);
        }

        #region Вспомогательные классы и свойства для регистрации

/*
        internal void OnUpdate(double d)
        {
            EventHandler handler = BeforeUpdate;
            if (handler != null) handler(d, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler BeforeUpdate;
*/

        #endregion

        /// <summary>
        /// Получить зарегистрированные в системе каналы
        /// </summary>
        /// <param name="message">Сообщение с идентификацией запрашивающего</param>
        /// <returns></returns>
        public int[] GetRegisteredChannels(InternalMessage message)
        {
            bool success = true;
            var registeredHighLevelClient = GetRegisteredHighLevelClient(message);
            if (registeredHighLevelClient == null)
                throw new Exception(
                    string.Format(
                        "При попытке получить зарегистрированные в системе каналы произошла ошибка: клиент с регистрационным именем [{0}] не найден",
                        message.RegName));

            if (!InterestedRegisteredClients.Contains(registeredHighLevelClient))
                InterestedRegisteredClients.Add(registeredHighLevelClient);//добавляем к заинтересованным клиентам
            
            try
            {
                return
                    LowLevelMessageExchangeSystem.Instance.GetAllRegisteredChannels().Select(channel => channel.Id).ToArray();
            }
            catch(Exception)
            {
                success = false;
                throw;
            }
            finally
            {
                if (success)//если во время выполнения метода ничего не произошло, то ПОСЛЕ возврата метода добавляем клиента в доверенную зону
                {
                    registeredHighLevelClient.Interested = true;//ставим флаг заинтересованности
                }
            }
        }

        private delegate void ChannelSubscribeCaller(ChannelSubscribeMessage message);

        /// <summary>
        /// Начало подписки на чтение контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало подписки на чтение канала {0}", message.LogicalChannelId);

            var caller = new ChannelSubscribeCaller(ChannelSubscribe);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение подписки на чтение контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelSubscribe(ChannelSubscribeMessage message, IAsyncResult result)
        {
            Console.WriteLine("Клиент был подписан на канал");
        }

        /// <summary>
        /// Начало отписки от чтения контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginChannelUnSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало отписки от чтение канала {0}", message.LogicalChannelId);

            var caller = new ChannelSubscribeCaller(ChannelUnSubscribe);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение отписки от чтения контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndChannelUnSubscribe(ChannelSubscribeMessage message, IAsyncResult result)
        {
            Console.WriteLine("Клиент был отписан от канала");
        }

        private void OnRegistered(RegisteredHighLevelClient callback, InternalMessage message)
        {
            InvokeClientRegistered(new HighLevelClientEventArgs(callback, message));
        }

        private void OnUnregistered(RegisteredHighLevelClient callback, InternalMessage message)
        {
            InvokeClientUnregistered(new HighLevelClientEventArgs(callback, message));
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<HighLevelClientEventArgs> ClientRegistered;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<HighLevelClientEventArgs> ClientUnregistered;

        private void InvokeClientUnregistered(HighLevelClientEventArgs e)
        {
            var handler = ClientUnregistered;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void InvokeClientRegistered(HighLevelClientEventArgs e)
        {
            var handler = ClientRegistered;
            if (handler != null) handler(this, e);
        }

        #region Implementation of HighLevelIMessageExchangeSystem

        /// <summary>
        /// Подписка на чтение контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        private void ChannelSubscribe(ChannelSubscribeMessage message)
        {
            if (!message.Mode) throw new ArgumentException("Для подписки на канал в сообщении используется флаг отписки");

            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message);
            if (registeredHighLevelClient != null)
                registeredHighLevelClient.ChannelSubscribe(message);

            InvokeChannelSubscribed(new HighRegisteredLogicalChannelSubscribeEventArgs(registeredHighLevelClient, message));
        }

        /// <summary>
        /// Отписка от чтения контролируемого канала (посылка сообщения от клиента о том, что он отписывается от канала)
        /// </summary>
        /// <param name="message"></param>
        private void ChannelUnSubscribe(ChannelSubscribeMessage message)
        {
            if (message.Mode) throw new ArgumentException("Для отписки от канала в сообщении используется флаг подписки");

            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message);
            if (registeredHighLevelClient != null)
                registeredHighLevelClient.ChannelUnSubscribe(message);

            InvokeChannelUnSubscribed(new HighRegisteredLogicalChannelSubscribeEventArgs(registeredHighLevelClient, message));
        }

        /// <summary>
        /// Возможность записи в управляемый канал
        /// </summary>
        /// <param name="message"></param>
        public void IsAllowWriteChannel(IInternalMessage message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Начало записи данных в управляемый канал
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginWriteChannel(InternalLogicalChannelDataMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало записи канала {0}", message.LogicalChannelId);
            var caller = new WriteChannelCaller(WriteChannel);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение записи данных в управляемый канал
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndWriteChannel(InternalLogicalChannelDataMessage message, IAsyncResult result)
        {
            Console.WriteLine("Канал был записан");
        }

        /// <summary>
        /// Запись в управляемый канал
        /// </summary>
        /// <param name="message"></param>
        public void WriteChannel(InternalLogicalChannelDataMessage message)
        {
            //пришли новые данные по каналу. будем передавать вниз
            LowLevelMessageExchangeSystem.Instance.WriteChannel(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void SendMessage(InternalMessage message)
        {
            string s = string.Format("HighLevelClient -> MessageExchangeSystem : {0}{1}", message.TimeStamp, Environment.NewLine);
            InvokeMessageReceived(s);
        }


        /// <summary>
        /// 
        /// </summary>
        public event EventHandler MessageReceived;

        private void InvokeMessageReceived(object e)
        {
            EventHandler handler = MessageReceived;
            if (handler != null) handler(e, EventArgs.Empty);
        }

        #endregion

        #region Implementation of IMessageExchangeSystem

        /// <summary>
        /// Регистрация клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        private void Register(RegistrationMessage message, IHighLevelClientCallback clientCallback)
        {
            NotifySubscribeEvents();

            if (!message.Mode)
                throw new ArgumentException("Для регистрации клиента в сообщении используется флаг отмены регистрации");

            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message, false);
            // Это ПЛОХОЙ алгоритм оповещения, поскольку для каждого клиента
            // создаётся отдельный поток. Следовало бы в место этого создавать
            // по одному поток на каждый тикер

            //при необходимости создаём рабочий объект, добавляем его
            //в хэш-таблицу и запускаем в отдельном потоке

            //todo: подумать над регистрацией с того же тикера
            bool isRegistered = registeredHighLevelClient != null && registeredHighLevelClient.HasCallbacks;

            if (!isRegistered)
            {
                if (registeredHighLevelClient == null)
                {
                    registeredHighLevelClient = new RegisteredHighLevelClient {Ticker = message.RegName};
                    AddClient(message.RegName, registeredHighLevelClient);
                }
//                registeredHighLevelClient = (RegisteredHighLevelClient)_registeredClients[message.RegName];

                //Thread t = new Thread(w.RegisteredHighLevelClientProcess.SendUpdateToClient) { IsBackground = true };
                //t.Start();

                //получить рабочий объект из данного тикера и добавить
                //прокси клиента в список обратных вызовов

                registeredHighLevelClient.AddCallback(clientCallback);

                OnRegistered(registeredHighLevelClient, message);

                //todo: после регистрации сообщаем клиенту о всех зарегистрированных каналах (в дальнейшем может, через свойство?)
/*
                var registeredLogicalChannels = LowLevelMessageExchangeSystem.Instance.GetAllRegisteredChannels();
                foreach (var registeredLogicalChannel in registeredLogicalChannels)
                {
                    registeredHighLevelClient.ChannelRegister(new ChannelSubscribeMessage { LogicalChannelId = registeredLogicalChannel.Id });
                }
*/

            }
        }

        private void NotifySubscribeEvents()
        {
            LowLevelMessageExchangeSystem.Instance.ChannelRegistered += Low_ChannelRegistered;
            LowLevelMessageExchangeSystem.Instance.ChannelUnregistered += Low_ChannelUnregistered;
        }

        private RegisteredHighLevelClient GetRegisteredHighLevelClient(InternalMessage message)
        {
            return GetRegisteredHighLevelClient(message, true);
        }

        private RegisteredHighLevelClient GetRegisteredHighLevelClient(InternalMessage message, bool withCallbacks)
        {
            RegisteredHighLevelClient registeredHighLevelClient = this[message.RegName];
            if (withCallbacks && registeredHighLevelClient != null && !registeredHighLevelClient.HasCallbacks)
                registeredHighLevelClient = null;

            return registeredHighLevelClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SendMessageToClients(InternalMessage message)
        {
            foreach (RegisteredHighLevelClient value in RegisteredClients)
                value.SendMessageToClient(message);
        }

        /// <summary>
        /// Отмена регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientCallback"></param>
        private void Unregister(RegistrationMessage message, IHighLevelClientCallback clientCallback)
        {
            if (message.Mode) throw new ArgumentException("Для отмены регистрации клиента в сообщении используется флаг регистрации");

            //получить рабочий объект из данного тикера и удалить
            //прокси клиента из списка обратных вызовов
            RegisteredHighLevelClient registeredHighLevelClient = GetRegisteredHighLevelClient(message);

            if (registeredHighLevelClient != null)
            {
                registeredHighLevelClient.RemoveCallback(clientCallback);//лишаем клиента уведомлений
                
                OnUnregistered(registeredHighLevelClient, message);//уведомляем о том, что клиент отменил регистрацию

//                if (registeredHighLevelClient.HasCallbacks) 
                    RemoveClient(message.RegName);

            }
        }

        #endregion

        /// <summary>
        /// Получить зарегистрированный логический канал
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public RegisteredLogicalChannel GetRegisteredChannel(Func<RegisteredLogicalChannel, bool> predicate)
        {
            //ищем в верхней службе, если не находим - ищем в нижней службе
            RegisteredLogicalChannel channel = FindSubscribedChannel(predicate) ??
                                               LowLevelMessageExchangeSystem.Instance.GetRegisteredLogicalChannel(predicate);
            return channel;
        }

        /// <summary>
        /// Найти канал в верхней службе (если на него уже кто-то подписан)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private RegisteredLogicalChannel FindSubscribedChannel(Func<RegisteredLogicalChannel, bool> predicate)
        {
            RegisteredLogicalChannel registeredLogicalChannel =
                RegisteredClients
                    .SelectMany(client => client.RegisteredLogicalChannels.Values)//выбираем все каналы всех клиентов
                    .Where(predicate).Distinct().FirstOrDefault();//TODO: попробовать когда на один канал подписано несколько клиентов
            return registeredLogicalChannel;
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<HighRegisteredLogicalChannelSubscribeEventArgs> ChannelSubscribed;

        private void InvokeChannelSubscribed(HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            EventHandler<HighRegisteredLogicalChannelSubscribeEventArgs> handler = ChannelSubscribed;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<HighRegisteredLogicalChannelSubscribeEventArgs> ChannelUnSubscribed;

        private void InvokeChannelUnSubscribed(HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            EventHandler<HighRegisteredLogicalChannelSubscribeEventArgs> handler = ChannelUnSubscribed;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Прочитать канал
        /// </summary>
        /// <param name="message"></param>
        public void ReadChannel(InternalLogicalChannelDataMessage message)
        {
            RegisteredLogicalChannel subscribedChannel = FindSubscribedChannel(RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId));

            if (subscribedChannel!=null)
            {
                subscribedChannel.InvokeRead(message);
            }
            else
            {
                Console.WriteLine(
                    "Канал [{0}] извещает о приходе новых данных (чтение) от клиента [{1}], но на него никто не подписан",
                    message.LogicalChannelId, message.RegName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regName"></param>
        protected override void RemoveClient(string regName)
        {
            //убираем зарегистрированные каналы
            var registeredHighLevelClient = this[regName];

            foreach (int registeredLogicalChannelId in registeredHighLevelClient.RegisteredLogicalChannels.Keys.ToArray())
            {
                //имитируем посылку сообщения от клиента о том, что он отписывается от канала 
                //(тогда источники подписки будут об этом уведомлены)
                ChannelSubscribeMessage channelSubscribeMessage = new ChannelSubscribeMessage
                                                                      {
                                                                          LogicalChannelId = registeredLogicalChannelId,
                                                                          RegName = regName,
                                                                          Mode = false
                                                                      };
                ChannelUnSubscribe(channelSubscribeMessage);
            }

            base.RemoveClient(regName);

        }

        #region Implementation of IMessageExchangeSystem

        /// <summary>
        /// Начало регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginRegister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало регистрации клиента {0}", message.RegName);
            
            IHighLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<IHighLevelClientCallback>();
            
            var caller = new RigistrationCaller(Register);
            IAsyncResult result = caller.BeginInvoke(message, clientCallback, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndRegister(RegistrationMessage message, IAsyncResult result)
        {
            Console.WriteLine("Клиент был зарегистрирован");
        }

        /// <summary>
        /// Начало отмены регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginUnregister(RegistrationMessage message, AsyncCallback callback, object state)
        {
            Console.WriteLine("Начало отмены регистрации клиента {0}", message.RegName);

            IHighLevelClientCallback clientCallback = OperationContext.Current.GetCallbackChannel<IHighLevelClientCallback>();

            var caller = new RigistrationCaller(Unregister);
            IAsyncResult result = caller.BeginInvoke(message, clientCallback, callback, state);
            return result;
        }

        /// <summary>
        /// Завершение отмены регистрации клиента
        /// </summary>
        /// <param name="message"></param>
        /// <param name="result"></param>
        public void EndUnregister(RegistrationMessage message, IAsyncResult result)
        {
            Console.WriteLine("Регистрация клиента была отменена");
        }

        #endregion

        private delegate void RigistrationCaller(RegistrationMessage message, IHighLevelClientCallback clientCallback);
        private delegate void WriteChannelCaller(InternalLogicalChannelDataMessage message);
    }
}
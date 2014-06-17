using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Autofac;
using DMS.Common.MessageExchangeSystem;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.MES.Services;
using Oleg_ivo.Plc.Entities;
using Oleg_ivo.Tools.ConnectionProvider;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// Зарегистрированный клиент
    /// </summary>
    public abstract class RegisteredClient<TClientCallback> : IRegisteredChannelsHolder where TClientCallback : IClientCallback
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private PlcDataContext dataContext;

        #region fields

        #endregion

        #region constructors
        /// <summary>
        /// 
        /// </summary>
        protected RegisteredClient()
        {
            Callbacks = new List<TClientCallback>();
            RegisteredLogicalChannels = new Dictionary<int, RegisteredLogicalChannelExtended>();
        }

        #endregion

        #region properties
        /// <summary>
        /// Обратные вызовы
        /// </summary>
        protected List<TClientCallback> Callbacks { get; private set; }

        /// <summary>
        /// Есть обратные вызовы
        /// </summary>
        public bool HasCallbacks
        {
            get { return Callbacks.Count > 0; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string RegName { get; set; }

        /// <summary>
        /// Зарегистрированные логические каналы
        /// </summary>
        public Dictionary<int, RegisteredLogicalChannelExtended> RegisteredLogicalChannels { get; private set; }

        /// <summary>
        /// Есть зарегистрированные логические каналы
        /// </summary>
        public bool HasRegisteredLogicalChannels
        {
            get { return RegisteredLogicalChannels.Count > 0; }
        }

        [Dependency(Required = true)]
        public InternalMessageLogger InternalMessageLogger { get; set; }

        [Dependency(Required = true)]
        public IComponentContext Context { get; set; }

        public PlcDataContext DataContext { get
        {
            return dataContext ?? (dataContext = Context.Resolve<PlcDataContext>(new TypedParameter(typeof (string),
                                                                      Context
                                                                          .Resolve
                                                                          <DbConnectionProvider>()
                                                                          .DefaultConnectionString)));
        } }

        #endregion

        #region methods
        /// <summary>
        /// Добавить обратный вызов клиента
        /// </summary>
        /// <param name="callback"></param>
        public void AddCallback(TClientCallback callback)
        {
            lock (Callbacks)
                Callbacks.Add(callback);
        }

        /// <summary>
        /// Удалить обратный вызов клиента
        /// </summary>
        /// <param name="callback"></param>
        public void RemoveCallback(TClientCallback callback)
        {
            lock (Callbacks)
                Callbacks.Remove(callback);
        }

        /// <summary>
        /// Отправка сообщения клиенту
        /// </summary>
        /// <param name="message"></param>
        public void SendMessageToClient(InternalMessage message)
        {
            try
            {
                IterateCallbacks(c=> c.SendMessageToClient(message));
            }
            catch (FaultException<InvalidOperationException> ex)
            {
                throw;
            }
            catch (FaultException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                log.ErrorException("Ошибка при отправке кэшированного значения клиенту: {0}", ex);
                throw;
            }
        }

        #endregion

        /// <summary>
        /// Поиск зарегистрированного логического канала непосредственно в клиенте (не выходя за его рамки)
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public RegisteredLogicalChannelExtended GetRegisteredLogicalChannel(Func<RegisteredLogicalChannelExtended, bool> predicate)
        {
            return RegisteredLogicalChannels.Values.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Добавить канал в зарегистрированные для клиента
        /// </summary>
        /// <param name="registeredLogicalChannel"></param>
        protected void AddRegisteredChannel(RegisteredLogicalChannelExtended registeredLogicalChannel)
        {
            RegisteredLogicalChannels.Add(registeredLogicalChannel.Id, registeredLogicalChannel);
        }

        /// <summary>
        /// Удалить канал из зарегистрированные для клиента
        /// </summary>
        /// <param name="registeredLogicalChannel"></param>
        protected void RemoveRegisteredChannel(RegisteredLogicalChannelExtended registeredLogicalChannel)
        {
            RegisteredLogicalChannels.Remove(registeredLogicalChannel.Id);
        }

        protected void IterateCallbacks(Action<TClientCallback> callbackAction)
        {
            lock (Callbacks)
                foreach (var callback in Callbacks.ToList())
                    try
                    {
                        callbackAction(callback);
                    }
                    catch (CommunicationObjectFaultedException ex)
                    {
                        var error =
                            string.Format(
                                "Обратный вызов с клиентом [{0}] нарушен. Удаляем обратный вызов и ждём повторной регистрации клиента.",
                                RegName);
                        log.ErrorException(error, ex);
                        Callbacks.Remove(callback);
                    }
        }
    }
}
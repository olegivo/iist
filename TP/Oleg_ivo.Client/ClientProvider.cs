using System.Security;
using System.ServiceModel.Channels;
using DMS.Common;
using DMS.Common.Exceptions;
using Oleg_ivo.Base.Communication;
using Oleg_ivo.Tools.ExceptionCatcher;
using DMS.Common.Events;
using DMS.Common.MessageExchangeSystem.HighLevel;
using NLog;
using Oleg_ivo.HighLevelClient.ServiceReferenceHomeTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ServiceModel;
using DMS.Common.Messages;
namespace Oleg_ivo.HighLevelClient
{
    ///<summary>
    /// Провайдер для клиента
    ///</summary>
    public class ClientProvider : IClientBase
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientProvider" />.
        /// </summary>
        public ClientProvider()
        {
            CallbackHandler.ChannelRegistered += CallbackHandler_ChannelRegistered;
            CallbackHandler.ChannelUnRegistered += CallbackHandler_ChannelUnRegistered;
            CallbackHandler.ChannelStateChanged += CallbackHandler_ChannelStateChanged;
            CallbackHandler.HasReadChannel += CallbackHandler_HasReadChannel;

            reliableConnector = new ReliableConnector(this);
        }

        #region connectivity
        InstanceContext site;
        HighLevelMessageExchangeSystemClient proxy;
        CallbackHandler callbackHandler;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> ChannelSubscribeCompleted;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> ChannelUnSubscribeCompleted;

        public event EventHandler<AsyncCompletedEventArgs> SendErrorCompleted;

        private void proxy_SendErrorCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (SendErrorCompleted != null)
                SendErrorCompleted(this, e);
        }

        protected virtual void SubscribeProxy()
        {
            site.Faulted += site_Faulted;

            proxy.UnregisterCompleted += proxy_UnregisterCompleted  ;
            proxy.SendErrorCompleted += proxy_SendErrorCompleted;
            proxy.ChannelSubscribeCompleted += ChannelSubscribeCompleted;
            proxy.ChannelUnSubscribeCompleted += ChannelUnSubscribeCompleted;
            proxy.RegisterCompleted += Proxy_RegisterCompleted;
            proxy.UnregisterCompleted += UnregisterCompleted;
        }

        protected virtual void UnsubscribeProxy()
        {
            if (site != null)
            {
                site.Faulted -= site_Faulted;
            }
            if (proxy != null)
            {
                proxy.UnregisterCompleted -= proxy_UnregisterCompleted;
                proxy.SendErrorCompleted -= proxy_SendErrorCompleted;
                proxy.ChannelSubscribeCompleted -= ChannelSubscribeCompleted;
                proxy.ChannelUnSubscribeCompleted -= ChannelUnSubscribeCompleted;
                proxy.RegisterCompleted -= Proxy_RegisterCompleted;
                proxy.UnregisterCompleted -= UnregisterCompleted;
            }
        }

        void site_Faulted(object sender, EventArgs e)
        {
            Log.Error("Site faulted");//TODO:сюда добраться не ожидаем!
            var channel = sender as IChannel;
            if (channel != null)
            {
                channel.Abort();
                channel.Close();
            }

            //Disable the keep alive timer now that the channel is faulted
            //_keepAliveTimer.Stop();

            //The proxy channel should no longer be used
            AbortProxy();

            //Enable the try again timer and attempt to reconnect
            //reconnectTimer.Start();
        }

        public ICommunicationObject Proxy { get { return proxy; } }

        private readonly object registerLock = new object();

        public void Register()
        {
            lock (registerLock)
            {
                Log.Trace("Запуск синхронной регистрации");
                CreateProxy();
                var message = new RegistrationMessage(GetRegName(), null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
                proxy.Register(message);
                isRegistered = true;
                if (RegisterCompleted != null)
                    RegisterCompleted(this, new AsyncCompletedEventArgs(null, false, null));
            }
        }

        public void AbortProxy()
        {
            if (proxy != null)
            {
                Log.Debug("Прекращение канала связи");
                proxy.Abort();
                proxy.Close();
                proxy = null;
            }
        }

        public bool IsRegistered
        {
            get { lock (proxyLock) return isRegistered; }
        }

        private readonly object proxyLock = new object();

        /// <summary>
        /// Создать Proxy
        /// </summary>
        /// <remarks>
        /// По умолчанию параметры для инициализации канала берутся из App.Config. 
        /// В перегрузке можно использовать инициализацию дополнительными параметрами
        /// </remarks>
        private void CreateProxy()
        {
            lock (proxyLock)
            {
                UnsubscribeProxy();

                Log.Debug("Создание прокси");
                if (proxy != null)
                    proxy.SafeClose();
                site = new InstanceContext(CallbackHandler);
                proxy = new HighLevelMessageExchangeSystemClient(site);
                reliableConnector.SetProxy(proxy);

                SubscribeProxy();
            }
        }

        #endregion

        #region events
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler NeedProtocol
        {
            add { CallbackHandler.NeedProtocol += value; }
            remove { CallbackHandler.NeedProtocol -= value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs<ChannelRegistrationMessage>> ChannelRegistered;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs<ChannelRegistrationMessage>> ChannelUnRegistered;

        #endregion

        private void CallbackHandler_ChannelRegistered(object sender, MessageEventArgs<ChannelRegistrationMessage> e)
        {
            Log.Trace("CallbackHandler_ChannelRegistered");

            var message = e.Message;
            var newValue = new List<IRegisteredChannel> { new RegisteredLogicalChannel(message.LogicalChannelId) };
            if (RegisteredChannels != null)
                RegisteredChannels.AddRange(newValue);
            else 
                RegisteredChannels = newValue;

            if (actualValues.ContainsKey(message.LogicalChannelId))
                actualValues[message.LogicalChannelId] = default(double);
            else
                actualValues.Add(message.LogicalChannelId, default(double));

            if (ChannelRegistered != null) ChannelRegistered(this, e);
        }

        private void CallbackHandler_ChannelUnRegistered(object sender, MessageEventArgs<ChannelRegistrationMessage> e)
        {
            Log.Trace("CallbackHandler_ChannelUnRegistered");

            var message = e.Message;
            var removeValue = RegisteredChannels.FirstOrDefault(RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId ));

            if (removeValue != null)
                RegisteredChannels.Remove(removeValue);
            else
                throw new InvalidOperationException(string.Format("Невозможно найти зарегистрированный канал {0}", e.Message.LogicalChannelId));

            if (actualValues.ContainsKey(message.LogicalChannelId))
                actualValues[message.LogicalChannelId] = default(double);

            if (ChannelUnRegistered != null) ChannelUnRegistered(this, e);
        }

        //FileIOPermission f = new FileIOPermission(PermissionState.None);
        //f.AllLocalFiles = FileIOPermissionAccess.Read;

        //try
        //{
        //    f.Demand();
        //}
        //catch (SecurityException s)
        //{
        //    Log.Debug(s.Message);
        //}

        //try
        //{
        //    //System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
        //    string appConfigFileName = @"D:\Oleg\Мои документы\ЛЭТИ\ИИСТ\WAGO\Иващенко_Олег\Система\Программы в LV\LabViewClient\Oleg_ivo.Client.exe.config";
        //    FileIOPermission permission = new FileIOPermission(PermissionState.Unrestricted);
        //    permission.Assert();

        //    System.Configuration.ConfigurationManager.OpenExeConfiguration(appConfigFileName);

        //    CodeAccessPermission.RevertAssert();
        //}
        //catch (Exception ex)
        //{
        //    throw;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        protected CallbackHandler CallbackHandler
        {
            get { return callbackHandler ?? (callbackHandler = new CallbackHandler()); }
        }

        public bool IsCommunicationFailed
        {
            get
            {
                lock (registerLock)
                {
                    Log.Trace("Проверка коммуникаций");
                    if (proxy == null)
                    {
                        Log.Warn("Прокси не создан");
                        return true;
                    }
                    if (proxy.State == CommunicationState.Faulted)
                    {
                        Log.Warn("Канал связи нарушен");
                        return true;
                    }
                    return false;
                }
            }
        }

        public void SendErrorAsync(ExtendedThreadExceptionEventArgs e)
        {
            var exception = new TestException("У клиент обнаружена ошибка:\n" + e.Exception);
            proxy.SendErrorAsync(
                new InternalErrorMessage(GetRegName(), null, exception), e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init()//TODO: а нужен ли метод?
        {
            //if (proxy == null)
            //    CreateProxy();

            //TODO: загружать конфигурацию?
            //System.Configuration.ConfigurationManager.OpenExeConfiguration(appConfigFileName);
            //System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logicalChannelId"></param>
        /// <returns></returns>
        public object GetActualValue(int logicalChannelId)
        {
            return actualValues[logicalChannelId];
        }

        readonly SortedDictionary<int, object> actualValues = new SortedDictionary<int, object>();
        readonly SortedDictionary<int, LogicalChannelState> actualStates = new SortedDictionary<int, LogicalChannelState>();

        void CallbackHandler_HasReadChannel(object sender, MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            Log.Trace("CallbackHandler_HasReadChannel");

            if (RegisteredChannels != null)
            {
                var message = e.Message;
                if (actualValues.ContainsKey(message.LogicalChannelId))
                    actualValues[message.LogicalChannelId] = message.Value;
                else
                    actualValues.Add(message.LogicalChannelId, message.Value);

                InvokeHasReadChannel(e);
            }
        }

        void CallbackHandler_ChannelStateChanged(object sender, MessageEventArgs<InternalLogicalChannelStateMessage> e)
        {
            Log.Trace("CallbackHandler_ChannelStateChanged");

            if (RegisteredChannels != null)
            {
                var message = e.Message;
                var value = message.State;
                if (actualStates.ContainsKey(message.LogicalChannelId))
                    actualStates[message.LogicalChannelId] = value;
                else
                    actualStates.Add(message.LogicalChannelId, value);

                InvokeChannelStateChanged(e);
            }
        }

        /// <summary>
        /// Был прочтён канал
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelDataMessage>> HasReadChannel;

        private void InvokeHasReadChannel(MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            Log.Trace("InvokeHasReadChannel");

            var handler = HasReadChannel;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Был изменён статус канала
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelStateMessage>> ChannelStateChanged;

        private void InvokeChannelStateChanged(MessageEventArgs<InternalLogicalChannelStateMessage> e)
        {
            Log.Trace("InvokeChannelStateChanged");

            var handler = ChannelStateChanged;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Асинхронная регистрация
        /// </summary>
        public void RegisterAsync()
        {
            Log.Trace("Запуск асинхронной регистрации");
            CreateProxy();
            var message = new RegistrationMessage(GetRegName(), null, RegistrationMode.Register, DataMode.Read | DataMode.Write);
            proxy.RegisterAsync(message);
        }

        /// <summary>
        /// Разрешённый для клиента режим данных
        /// </summary>
        /// <value></value>
        public DataMode AllowedDataMode
        {
            get { return DataMode.Read | DataMode.Write; }
        }

        /// <summary>
        /// Регистрация завершена
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> RegisterCompleted;

        void Proxy_RegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                lock (registerLock) isRegistered = e.Error == null;

                if (isRegistered)
                    Log.Info("Регистрация на сервере завершилась успешно");
                else
                {
                    Log.Error("Регистрация на сервере завершилась неудачно:\n{0}", e.Error);
                    throw e.Error;
                }
            }
            finally
            {
                if (RegisterCompleted != null)
                    RegisterCompleted(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<IRegisteredChannel> RegisteredChannels { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Func<string> GetRegName { get; set; }

        private readonly ReliableConnector reliableConnector;
        private bool isRegistered;

        /// <summary>
        /// отмена регистрации
        /// </summary>
        public void Unregister()
        {
            Log.Trace("Unregister");

            proxy.UnregisterCompleted += proxy_UnregisterCompleted;
            proxy.UnregisterAsync(new RegistrationMessage(GetRegName(), null, RegistrationMode.Unregister, DataMode.Unknown));
        }

        void proxy_UnregisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Log.Trace("proxy_UnregisterCompleted");

            lock (proxyLock) isRegistered = e.Error != null;
            if (!isRegistered)
                Log.Info("Отмена регистрации на сервере завершилась успешно");
            else
                Log.Error("Отмена регистрации на сервере завершилась неудачно:\n{0}", e.Error);
            proxy.UnregisterCompleted -= proxy_UnregisterCompleted;
            if(RegisteredChannels!=null) RegisteredChannels.Clear();
            //TODO:Disconnect неужели вызывается до этого?
            //Task.Factory.StartNew(() => proxy.Disconnect(GetRegName()))
            //    .ContinueWith(task => log.Info("Disconnected"));
        }

        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted;

        /// <summary>
        /// Асинхронная подписка на канал. Номер канала запоминается в userState
        /// </summary>
        /// <param name="message"></param>
        public void SubscribeChannel(ChannelSubscribeMessage message)
        {
            Log.Trace("SubscribeChannel");

            proxy.ChannelSubscribeAsync(message, message.LogicalChannelId);
        }

        /// <summary>
        /// Асинхронная отписка от канала. Номер канала запоминается в userState
        /// </summary>
        /// <param name="message"></param>
        public void UnSubscribeChannel(ChannelSubscribeMessage message)
        {
            Log.Trace("UnSubscribeChannel");

            proxy.ChannelUnSubscribeAsync(message, message.LogicalChannelId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void WriteChannel(InternalLogicalChannelDataMessage message)
        {
            Log.Trace("WriteChannel");

            proxy.WriteChannelAsync(message);
        }

        [SecuritySafeCritical]
        public void Dispose()
        {
            if (proxy != null)
                proxy.SafeClose();
        }
    }
}

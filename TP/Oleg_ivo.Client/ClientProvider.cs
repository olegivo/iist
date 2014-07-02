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
        HighLevelMessageExchangeSystemClient highLevelMessageExchangeSystemClient;
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

        private void HighLevelMessageExchangeSystemClient_SendErrorCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (SendErrorCompleted != null)
                SendErrorCompleted(this, e);
        }

        protected virtual void SubscribeProxy()
        {
            site.Faulted += site_Faulted;

            highLevelMessageExchangeSystemClient.UnregisterCompleted += HighLevelMessageExchangeSystemClient_UnregisterCompleted  ;
            highLevelMessageExchangeSystemClient.SendErrorCompleted += HighLevelMessageExchangeSystemClient_SendErrorCompleted;
            highLevelMessageExchangeSystemClient.ChannelSubscribeCompleted += ChannelSubscribeCompleted;
            highLevelMessageExchangeSystemClient.ChannelUnSubscribeCompleted += ChannelUnSubscribeCompleted;
            highLevelMessageExchangeSystemClient.UnregisterCompleted += UnregisterCompleted;
        }

        protected virtual void UnsubscribeProxy()
        {
            if (site != null)
            {
                site.Faulted -= site_Faulted;
            }
            if (highLevelMessageExchangeSystemClient != null)
            {
                highLevelMessageExchangeSystemClient.UnregisterCompleted -= HighLevelMessageExchangeSystemClient_UnregisterCompleted;
                highLevelMessageExchangeSystemClient.SendErrorCompleted -= HighLevelMessageExchangeSystemClient_SendErrorCompleted;
                highLevelMessageExchangeSystemClient.ChannelSubscribeCompleted -= ChannelSubscribeCompleted;
                highLevelMessageExchangeSystemClient.ChannelUnSubscribeCompleted -= ChannelUnSubscribeCompleted;
                highLevelMessageExchangeSystemClient.UnregisterCompleted -= UnregisterCompleted;
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

        public ICommunicationObject Proxy { get { return highLevelMessageExchangeSystemClient; } }

        public void Register()
        {
            RegisterSync();
        }

        public void AbortProxy()
        {
            if (highLevelMessageExchangeSystemClient != null)
            {
                highLevelMessageExchangeSystemClient.Abort();
                highLevelMessageExchangeSystemClient.Close();
                highLevelMessageExchangeSystemClient = null;
            }
        }

        public bool IsRegistered
        {
            get { return true;/*TODO: Implement IsRegistered*/ }
        }


        /// <summary>
        /// Создать Proxy
        /// </summary>
        /// <remarks>
        /// По умолчанию параметры для инициализации канала берутся из App.Config. 
        /// В перегрузке можно использовать инициализацию дополнительными параметрами
        /// </remarks>
        private void CreateProxy()
        {
            UnsubscribeProxy();

            site = new InstanceContext(CallbackHandler);
            if(highLevelMessageExchangeSystemClient!=null)
                highLevelMessageExchangeSystemClient.SafeClose(TimeSpan.FromSeconds(5));
            highLevelMessageExchangeSystemClient = new HighLevelMessageExchangeSystemClient(site);

            reliableConnector.SetProxy(highLevelMessageExchangeSystemClient);

            SubscribeProxy();
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
            get { return highLevelMessageExchangeSystemClient == null || highLevelMessageExchangeSystemClient.State == CommunicationState.Faulted; }
        }

        public void SendErrorAsync(ExtendedThreadExceptionEventArgs e)
        {
            var exception = new TestException("У клиент обнаружена ошибка:\n" + e.Exception);
            highLevelMessageExchangeSystemClient.SendErrorAsync(
                new InternalErrorMessage(GetRegName(), null, exception), e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            if (highLevelMessageExchangeSystemClient == null)
                CreateProxy();

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
        /// Синхронная регистрация (используется для LabView)
        /// </summary>
        public void RegisterSync()
        {
            Log.Trace("RegisterSync");

            Register(false, null);
        }

        /// <summary>
        /// Асинхронная регистрация
        /// </summary>
        public void RegisterAsync(EventHandler<AsyncCompletedEventArgs> proxyRegisterCompleted)
        {
            Log.Trace("RegisterAsync");

            Register(true, proxyRegisterCompleted);
        }

        /// <summary>
        /// Универсальная регистрация
        /// </summary>
        /// <param name="async"></param>
        /// <param name="proxyRegisterCompleted"></param>
        public void Register(bool async, EventHandler<AsyncCompletedEventArgs> proxyRegisterCompleted)
        {
            CreateProxy();

            _proxyRegisterCompleted = proxyRegisterCompleted;
            highLevelMessageExchangeSystemClient.RegisterCompleted += Proxy_RegisterCompleted;
            var registrationMessage = new RegistrationMessage(GetRegName(), null, RegistrationMode.Register, AllowedDataMode);
            if (async)
                highLevelMessageExchangeSystemClient.RegisterAsync(registrationMessage, registrationMessage);
            else
            {
                highLevelMessageExchangeSystemClient.Register(registrationMessage);
                Proxy_RegisterCompleted(this, new AsyncCompletedEventArgs(null, false, null));
            }
        }

        /// <summary>
        /// Разрешённый для клиента режим данных
        /// </summary>
        /// <value></value>
        public DataMode AllowedDataMode
        {
            get { return DataMode.Read | DataMode.Write; }
        }

        void Proxy_RegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Log.Trace("Proxy_RegisterCompleted");

            if (e.Error != null)
                throw e.Error;
            highLevelMessageExchangeSystemClient.RegisterCompleted -= Proxy_RegisterCompleted;

            //object channels;
            //try
            //{
            //    channels = HighLevelMessageExchangeSystemClient.GetRegisteredChannels(new InternalMessage(RegName, null));
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}            //RegisteredChannels = HighLevelMessageExchangeSystemClient.GetRegisteredChannels(new InternalMessage(RegName, null));

            if (_proxyRegisterCompleted != null)
            {
                // чтобы снаружи знали, что регистрация завершена, каналы передаются сохраняются в UserState
                _proxyRegisterCompleted(this, e);
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

        private EventHandler<AsyncCompletedEventArgs> _proxyRegisterCompleted;
        private readonly ReliableConnector reliableConnector;

        /// <summary>
        /// отмена регистрации
        /// </summary>
        public void Unregister()
        {
            Log.Trace("Unregister");

            highLevelMessageExchangeSystemClient.UnregisterCompleted += HighLevelMessageExchangeSystemClient_UnregisterCompleted;
            highLevelMessageExchangeSystemClient.UnregisterAsync(new RegistrationMessage(GetRegName(), null, RegistrationMode.Unregister, DataMode.Unknown));
        }

        void HighLevelMessageExchangeSystemClient_UnregisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Log.Trace("HighLevelMessageExchangeSystemClient_UnregisterCompleted");

            highLevelMessageExchangeSystemClient.UnregisterCompleted -= HighLevelMessageExchangeSystemClient_UnregisterCompleted;
            if(RegisteredChannels!=null) RegisteredChannels.Clear();
            //TODO:Disconnect неужели вызывается до этого?
            //Task.Factory.StartNew(() => highLevelMessageExchangeSystemClient.Disconnect(GetRegName()))
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

            highLevelMessageExchangeSystemClient.ChannelSubscribeAsync(message, message.LogicalChannelId);
        }

        /// <summary>
        /// Асинхронная отписка от канала. Номер канала запоминается в userState
        /// </summary>
        /// <param name="message"></param>
        public void UnSubscribeChannel(ChannelSubscribeMessage message)
        {
            Log.Trace("UnSubscribeChannel");

            highLevelMessageExchangeSystemClient.ChannelUnSubscribeAsync(message, message.LogicalChannelId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void WriteChannel(InternalLogicalChannelDataMessage message)
        {
            Log.Trace("WriteChannel");

            highLevelMessageExchangeSystemClient.WriteChannelAsync(message);
        }

        [SecuritySafeCritical]
        public void Dispose()
        {
            if (highLevelMessageExchangeSystemClient != null)
                highLevelMessageExchangeSystemClient.SafeClose();
        }
    }
}

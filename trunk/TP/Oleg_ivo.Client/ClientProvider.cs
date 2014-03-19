using System.Security;
using System.ServiceModel.Channels;
using System.Timers;
using DMS.Common;
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
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientProvider" />.
        /// </summary>
        public ClientProvider()
        {
            reconnectTimer = new Timer(5000);
            reconnectTimer.Elapsed += reconnectTimer_Elapsed;

            CallbackHandler.ChannelRegistered += CallbackHandler_ChannelRegistered;
            CallbackHandler.ChannelUnRegistered += CallbackHandler_ChannelUnRegistered;
            CallbackHandler.HasReadChannel += CallbackHandler_HasReadChannel;
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
                highLevelMessageExchangeSystemClient.UnregisterCompleted -= HighLevelMessageExchangeSystemClient_UnregisterCompleted  ;
                highLevelMessageExchangeSystemClient.SendErrorCompleted += HighLevelMessageExchangeSystemClient_SendErrorCompleted;
                highLevelMessageExchangeSystemClient.ChannelSubscribeCompleted -= ChannelSubscribeCompleted;
                highLevelMessageExchangeSystemClient.ChannelUnSubscribeCompleted -= ChannelUnSubscribeCompleted;
                highLevelMessageExchangeSystemClient.UnregisterCompleted -= UnregisterCompleted;
            }
        }

        void site_Faulted(object sender, EventArgs e)
        {
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
            reconnectTimer.Start();
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

        private readonly Timer reconnectTimer;

        private void reconnectTimer_Elapsed(object sender, EventArgs e)
        {
            if (highLevelMessageExchangeSystemClient == null)
                CreateProxy();

            if (highLevelMessageExchangeSystemClient != null)
            {
                reconnectTimer.Stop();
                //_keepAliveTimer.Start();
            }
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
                highLevelMessageExchangeSystemClient.SafeClose();
            highLevelMessageExchangeSystemClient = new HighLevelMessageExchangeSystemClient(site);

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
        public event EventHandler<ChannelRegisterEventArgs> ChannelRegistered;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ChannelRegisterEventArgs> ChannelUnRegistered;

        #endregion

        private void CallbackHandler_ChannelRegistered(object sender, ChannelRegisterEventArgs e)
        {
            log.Debug("CallbackHandler_ChannelRegistered");

            var message = e.Message;
            var newValue = new List<IRegisteredChannel> { new RegisteredLogicalChannel(message.LogicalChannelId) };
            if (RegisteredChannels != null)
                RegisteredChannels.AddRange(newValue);
            else 
                RegisteredChannels = newValue;

            if (_actualValues.ContainsKey(message.LogicalChannelId))
                _actualValues[message.LogicalChannelId] = default(double);
            else
                _actualValues.Add(message.LogicalChannelId, default(double));

            if (ChannelRegistered != null) ChannelRegistered(this, e);
        }

        private void CallbackHandler_ChannelUnRegistered(object sender, ChannelRegisterEventArgs e)
        {
            log.Debug("CallbackHandler_ChannelUnRegistered");

            var message = e.Message;
            var removeValue = RegisteredChannels.FirstOrDefault(RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId ));

            if (removeValue != null)
                RegisteredChannels.Remove(removeValue);
            else
                throw new InvalidOperationException(string.Format("Невозможно найти зарегистрированный канал {0}", e.Message.LogicalChannelId));

            if (_actualValues.ContainsKey(message.LogicalChannelId))
                _actualValues[message.LogicalChannelId] = default(double);

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

        public void SendErrorAsync(ExtendedThreadExceptionEventArgs e)
        {
            highLevelMessageExchangeSystemClient.SendErrorAsync(
                new InternalErrorMessage(GetRegName(), null, e.Exception), e);
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
        public double GetActualValue(int logicalChannelId)
        {
            return _actualValues[logicalChannelId];
        }

        readonly SortedDictionary<int, double> _actualValues = new SortedDictionary<int, double>();

        void CallbackHandler_HasReadChannel(object sender, DataEventArgs e)
        {
            log.Debug("CallbackHandler_HasReadChannel");

            if (RegisteredChannels != null)
            {
                var message = e.Message;
                double value = (double)message.Value;
                if (_actualValues.ContainsKey(message.LogicalChannelId))
                    _actualValues[message.LogicalChannelId] = value;
                else
                    _actualValues.Add(message.LogicalChannelId, value);

                InvokeHasReadChannel(e);
            }
        }

        /// <summary>
        /// Был прочтён канал
        /// </summary>
        public event EventHandler<DataEventArgs> HasReadChannel;

        private void InvokeHasReadChannel(DataEventArgs e)
        {
            log.Debug("InvokeHasReadChannel");

            EventHandler<DataEventArgs> handler = HasReadChannel;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Синхронная регистрация (используется для LabView)
        /// </summary>
        public void RegisterSync()
        {
            log.Debug("RegisterSync");

            Register(false, null);
        }

        /// <summary>
        /// Асинхронная регистрация
        /// </summary>
        public void RegisterAsync(EventHandler<AsyncCompletedEventArgs> proxyRegisterCompleted)
        {
            log.Debug("RegisterAsync");

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
            log.Debug("Proxy_RegisterCompleted");

            highLevelMessageExchangeSystemClient.RegisterCompleted -= Proxy_RegisterCompleted;
            if (e.Error != null)
                throw new InvalidOperationException(e.Error.ToString(), e.Error);

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

        /// <summary>
        /// отмена регистрации
        /// </summary>
        public void Unregister()
        {
            log.Debug("Unregister");

            highLevelMessageExchangeSystemClient.UnregisterCompleted += HighLevelMessageExchangeSystemClient_UnregisterCompleted;
            highLevelMessageExchangeSystemClient.UnregisterAsync(new RegistrationMessage(GetRegName(), null, RegistrationMode.Unregister, DataMode.Unknown));
        }

        void HighLevelMessageExchangeSystemClient_UnregisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            log.Debug("HighLevelMessageExchangeSystemClient_UnregisterCompleted");

            highLevelMessageExchangeSystemClient.UnregisterCompleted -= HighLevelMessageExchangeSystemClient_UnregisterCompleted;
            if(RegisteredChannels!=null) RegisteredChannels.Clear();
        }

        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted;

        /// <summary>
        /// Асинхронная подписка на канал. Номер канала запоминается в userState
        /// </summary>
        /// <param name="message"></param>
        public void SubscribeChannel(ChannelSubscribeMessage message)
        {
            log.Debug("SubscribeChannel");

            highLevelMessageExchangeSystemClient.ChannelSubscribeAsync(message, message.LogicalChannelId);
        }

        /// <summary>
        /// Асинхронная отписка от канала. Номер канала запоминается в userState
        /// </summary>
        /// <param name="message"></param>
        public void UnSubscribeChannel(ChannelSubscribeMessage message)
        {
            log.Debug("UnSubscribeChannel");

            highLevelMessageExchangeSystemClient.ChannelUnSubscribeAsync(message, message.LogicalChannelId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void WriteChannel(InternalLogicalChannelDataMessage message)
        {
            log.Debug("WriteChannel");

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

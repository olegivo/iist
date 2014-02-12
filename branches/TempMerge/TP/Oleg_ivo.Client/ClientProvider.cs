#if IIST
#if WSHTTP_BINDING
using Oleg_ivo.HighLevelClient.ServiceReferenceIISTwsDualHttp;
#else
#if TCP_BINDING
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
#else
using Oleg_ivo.HighLevelClient.ServiceReferenceIIST;
#endif
#endif
#else
#if WSHTTP_BINDING
using Oleg_ivo.HighLevelClient.ServiceReferenceHomeWsDualHttp;
#else
using Oleg_ivo.HighLevelClient.ServiceReferenceHome;
#endif
#endif
namespace Oleg_ivo.HighLevelClient
{
    ///<summary>
    /// Провайдер для клиента
    ///</summary>
    public class ClientProvider
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        #region Singleton

        private static ClientProvider _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static ClientProvider Instance
        {
            get { return _instance ?? (_instance = new ClientProvider()); }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ClientProvider" />.
        /// </summary>
        protected ClientProvider()
        {
            try
            {
                if (Proxy == null)
                    throw new InvalidProgramException("Не удалось проинициализировать Proxy");
            }
            catch (Exception ex)
            {
                log.ErrorException("Ошибка при доступе к Proxy", ex);
                throw;
            }

            _callbackHandler.ChannelRegistered += CallbackHandler_ChannelRegistered;
            _callbackHandler.ChannelUnRegistered += CallbackHandler_ChannelUnRegistered;
            _callbackHandler.HasReadChannel += _callbackHandler_HasReadChannel;
        }

        #endregion

        #region connectivity
        InstanceContext _site;
        HighLevelMessageExchangeSystemClient _proxy;
        CallbackHandler _callbackHandler;

        /// <summary>
        /// 
        /// </summary>
        public HighLevelMessageExchangeSystemClient Proxy
        {
            get
            {
                if (_proxy == null)
                {
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

                    _callbackHandler = new CallbackHandler();
                    _site = new InstanceContext(_callbackHandler);

                    try
                    {
                        _proxy = CreateProxy(_site);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Произошла ошибка при создании Proxy", ex);
                    }
                }

                return _proxy;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> ChannelSubscribeCompleted
        {
            add { Proxy.ChannelSubscribeCompleted += value; }
            remove { Proxy.ChannelSubscribeCompleted -= value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> ChannelUnSubscribeCompleted
        {
            add { Proxy.ChannelUnSubscribeCompleted += value; }
            remove { Proxy.ChannelUnSubscribeCompleted -= value; }
        }

        /// <summary>
        /// Создать Proxy
        /// </summary>
        /// <remarks>
        /// По умолчанию параметры для инициализации канала берутся из App.Config. 
        /// В перегрузке можно использовать инициализацию дополнительными параметрами
        /// </remarks>
        protected virtual HighLevelMessageExchangeSystemClient CreateProxy(InstanceContext instanceContext)
        {
            var client = new HighLevelMessageExchangeSystemClient(instanceContext);
            return client;
        }

        #endregion

        #region events
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler NeedProtocol
        {
            add { _callbackHandler.NeedProtocol += value; }
            remove { _callbackHandler.NeedProtocol -= value; }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regName"></param>
        public void Init(string regName)
        {
            RegName = regName;
            //TODO: загружать конфигурацию?
            //System.Configuration.ConfigurationManager.OpenExeConfiguration(appConfigFileName);
            //System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
        }

        /// <summary>
        /// 
        /// </summary>
        private string RegName { get; set; }

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

        void _callbackHandler_HasReadChannel(object sender, DataEventArgs e)
        {
            log.Debug("_callbackHandler_HasReadChannel");

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
            _proxyRegisterCompleted = proxyRegisterCompleted;
            Proxy.RegisterCompleted += Proxy_RegisterCompleted;
            var registrationMessage = new RegistrationMessage(RegName, null, RegistrationMode.Register, AllowedDataMode);
            if (async)
                Proxy.RegisterAsync(registrationMessage, registrationMessage);
            else
            {
                Proxy.Register(registrationMessage);
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

            Proxy.RegisterCompleted -= Proxy_RegisterCompleted;
            if (e.Error != null)
                throw new InvalidOperationException(e.Error.ToString(), e.Error);

            //object channels;
            //try
            //{
            //    channels = Proxy.GetRegisteredChannels(new InternalMessage(RegName, null));
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}            //RegisteredChannels = Proxy.GetRegisteredChannels(new InternalMessage(RegName, null));

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

        private EventHandler<AsyncCompletedEventArgs> _proxyRegisterCompleted;

        /// <summary>
        /// отмена регистрации
        /// </summary>
        public void Unregister()
        {
            log.Debug("Unregister");

            Proxy.UnregisterCompleted += Proxy_UnregisterCompleted;
            Proxy.UnregisterAsync(new RegistrationMessage(RegName, null, RegistrationMode.Unregister, DataMode.Unknown));
        }

        void Proxy_UnregisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            log.Debug("Proxy_UnregisterCompleted");

            Proxy.UnregisterCompleted -= Proxy_UnregisterCompleted;
            RegisteredChannels.Clear();
        }

        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted
        {
            add { Proxy.UnregisterCompleted += value; }
            remove { Proxy.UnregisterCompleted -= value; }
        }

        /// <summary>
        /// Асинхронная подписка на канал. Номер канала запоминается в userState
        /// </summary>
        /// <param name="message"></param>
        public void SubscribeChannel(ChannelSubscribeMessage message)
        {
            log.Debug("SubscribeChannel");

            Proxy.ChannelSubscribeAsync(message, message.LogicalChannelId);
        }

        /// <summary>
        /// Асинхронная отписка от канала. Номер канала запоминается в userState
        /// </summary>
        /// <param name="message"></param>
        public void UnSubscribeChannel(ChannelSubscribeMessage message)
        {
            log.Debug("UnSubscribeChannel");

            Proxy.ChannelUnSubscribeAsync(message, message.LogicalChannelId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void WriteChannel(InternalLogicalChannelDataMessage message)
        {
            log.Debug("WriteChannel");

            Proxy.WriteChannelAsync(message);
        }
    }
}

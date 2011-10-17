#if IIST
#if WSHTTP_BINDING
using Oleg_ivo.HighLevelClient.ServiceReferenceIISTwsDualHttp;
#else
#if TCP_BINDING
using DMS.Common.Events;
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
        #region Singleton

        private static ClientProvider _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static ClientProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ClientProvider();
                }
                return _instance;
            }
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
                    //    Console.WriteLine(s.Message);
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
        public event EventHandler<ClientChannelSubscribeEventArgs> ChannelRegistered;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ClientChannelSubscribeEventArgs> ChannelUnRegistered;

        #endregion

        private void CallbackHandler_ChannelRegistered(object sender, ClientChannelSubscribeEventArgs e)
        {
            var message = e.Message;
            var newValue = new[] {message.LogicalChannelId};
            RegisteredChannels = RegisteredChannels != null ? RegisteredChannels.Union(newValue, EqualityComparer<int>.Default).ToArray() : newValue;

            if (_actualValues.ContainsKey(message.LogicalChannelId))
                _actualValues[message.LogicalChannelId] = default(double);
            else
                _actualValues.Add(message.LogicalChannelId, default(double));

            if (ChannelRegistered != null) ChannelRegistered(this, e);
        }

        private void CallbackHandler_ChannelUnRegistered(object sender, ClientChannelSubscribeEventArgs e)
        {
            var message = e.Message;
            var removeValue = new[] {message.LogicalChannelId};
            if (RegisteredChannels != null)
                RegisteredChannels = RegisteredChannels.Except(removeValue, EqualityComparer<int>.Default).ToArray();
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
            if (RegisteredChannels != null)
            {
                var message = e.Message;
                double value = (double) message.Value;
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
            EventHandler<DataEventArgs> handler = HasReadChannel;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Синхронная регистрация (используется для LabView)
        /// </summary>
        public void Register()
        {
            Proxy.Register(new RegistrationMessage { RegName = RegName, Mode = true });
            RegisteredChannels = Proxy.GetRegisteredChannels(new InternalMessage { RegName = RegName });
        }

        /// <summary>
        /// Асинхронная регистрация
        /// </summary>
        /// <param name="proxyRegisterCompleted"></param>
        public void Register(EventHandler<AsyncCompletedEventArgs> proxyRegisterCompleted)
        {
            _proxyRegisterCompleted = proxyRegisterCompleted;
            Proxy.RegisterAsync(new RegistrationMessage { RegName = RegName, Mode = true });
            Proxy.RegisterCompleted += Proxy_RegisterCompleted;
        }

        void Proxy_RegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Proxy.RegisterCompleted -= Proxy_RegisterCompleted;
            if (e.Error!=null)
                throw new InvalidOperationException(e.Error.ToString(), e.Error);
            RegisteredChannels = Proxy.GetRegisteredChannels(new InternalMessage { RegName = RegName });

            if (_proxyRegisterCompleted!=null)
            {
                // чтобы снаружи знали, что регистрация завершена, каналы передаются сохраняются в UserState
                _proxyRegisterCompleted(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int[] RegisteredChannels { get; private set; }

        private EventHandler<AsyncCompletedEventArgs> _proxyRegisterCompleted;

        /// <summary>
        /// отмена регистрации
        /// </summary>
        public void Unregister()
        {
            Proxy.UnregisterAsync(new RegistrationMessage { RegName = RegName });
        }


        /// <summary>
        /// Асинхронная подписка на канал. Номер канала запоминается в userState
        /// </summary>
        /// <param name="message"></param>
        public void SubscribeChannel(ChannelSubscribeMessage message)
        {
            Proxy.ChannelSubscribeAsync(message, message.LogicalChannelId);
        }

        /// <summary>
        /// Асинхронная отписка от канала. Номер канала запоминается в userState
        /// </summary>
        /// <param name="message"></param>
        public void UnSubscribeChannel(ChannelSubscribeMessage message)
        {
            Proxy.ChannelUnSubscribeAsync(message, message.LogicalChannelId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void WriteChannel(InternalLogicalChannelDataMessage message)
        {
            Proxy.WriteChannelAsync(message);
        }
    }
}

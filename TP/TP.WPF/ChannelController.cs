using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using DMS.Common.Events;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.HighLevelClient;
using TP.WPF.Properties;
using System.Linq;

namespace TP.WPF
{
    /// <summary>
    /// 
    /// </summary>
    public class ChannelController : Component
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private readonly IContainer components;
        /// <summary>
        /// 
        /// </summary>
        public ChannelController()
        {
            //InitializeComponent();
            components = new Container();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        public ChannelController(IContainer container)
        {
            container.Add(this);
            components = new Container();
            //InitializeComponent();
        }

        /// <summary>
        /// Автоматически подписываться на вновь зарегистрированные каналы
        /// </summary>
        [Description("Автоматически подписываться на вновь зарегистрированные каналы"), DefaultValue(false)]
        public bool AutoSubscribeChannels { get; set; }

        public LogicalChannelMappings LogicalChannelMappings
        {
            get { return logicalChannelMappings; }
            set
            {
                if(logicalChannelMappings == value) return;
                logicalChannelMappings = value;
                localDic = LogicalChannelMappings.ToDictionary(mapping => mapping.LogicalChannelId, mapping => mapping.LocalChannelId);
                realDic = LogicalChannelMappings.ToDictionary(mapping => mapping.LocalChannelId, mapping => mapping.LogicalChannelId);
            }
        }

        public int GetRealLogicalChannelId(int localChannelId)
        {
            return realDic != null && realDic.ContainsKey(localChannelId) 
                ? realDic[localChannelId] 
                : localChannelId;
        }

        public int GetLocalChannelId(int realChannelId)
        {
            return localDic != null && localDic.ContainsKey(realChannelId)
                ? localDic[realChannelId]
                : realChannelId;
        }

        private ClientProvider provider;

        /// <summary>
        /// 
        /// </summary>
        protected ClientProvider Provider
        {
            get { return provider ?? (provider = new ClientProvider()); }
        }

        /// <summary>
        /// регистрация
        /// </summary>
        public void Register()
        {
            log.Debug("Register");

            try
            {
                Provider.Register(true, RegisterCompleted);
            }
            catch (Exception ex)
            {
                CanRegister = true;
                throw;
            }
        }

        private bool _canRegister;

        /// <summary>
        /// 
        /// </summary>
        public bool CanRegister
        {
            get { return _canRegister; }
            set
            {
                if (_canRegister != value)
                {
                    _canRegister = value;

                    EventHandler handler = CanRegisterChanged;
                    if (handler != null) handler(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Событие изменения возможности регистрирации на сервере
        /// </summary>
        public event EventHandler CanRegisterChanged;
        /// <summary>
        /// Событие снятия с регистрации канала
        /// </summary>
        //public event EventHandler ChannelUnRegistered;
        /// <summary>
        /// Событие подписки на канал
        /// </summary>
        //public event EventHandler ChannelSubscribed;
        /// <summary>
        /// Событие снятия подписки на канал
        /// </summary>
        //public event EventHandler ChannelUnSubscribed;

        void RegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            log.Debug("RegisterCompleted");

            // регистрация завершена
            CanRegister = false;
            var registeredChannels = Provider.RegisteredChannels;
            if (registeredChannels == null || registeredChannels.Count == 0)
            {
                Protocol("Регистрация завершена успешно. На сервере не опубликовано ни одного канала");
            }
            else
                foreach (var registeredChannel in registeredChannels)
                {
                    //TODO: заполнить RegNameFrom
                    ChannelRegistrationMessage message = new ChannelRegistrationMessage(null, null,
                                                                                        RegistrationMode.Register,
                                                                                        DataMode.Read,
                                                                                        registeredChannel.LogicalChannelId);
                    AddRegisteredChannel(message);
                }
        }

        readonly List<int> registeredChannelsList = new List<int>();
        readonly List<int> subscribedChannelsList = new List<int>();

        private void AddRegisteredChannel(ChannelRegistrationMessage message)
        {
            var logicalChannelId = message.LogicalChannelId;
            if (!registeredChannelsList.Contains(logicalChannelId))
            {
                registeredChannelsList.Add(logicalChannelId);
                Protocol(string.Format("Канал [{0}] теперь доступен для подписки", logicalChannelId));
                
                if (!subscribedChannelsList.Contains(logicalChannelId))//TODO:проверить, можно ли без этого условия (не остаются ли каналы после отмены подписки или отмены регистрации)
                {
                    if (AutoSubscribeChannels)
                    {
                        SubscribeChannel(new ChannelSubscribeMessage(GetRegName(),
                            null,
                            SubscribeMode.Subscribe,
                            logicalChannelId));
                    }
                }
            }
        }

        /// <summary>
        /// отмена регистрации
        /// </summary>
        public void Unregister()
        {
            log.Debug("Unregister");

            try
            {
                //убираем все зарегистрированные и подписанные каналы:
                foreach (var channelId in subscribedChannelsList.ToArray())
                {
                    UnSubscribeChannel(new ChannelSubscribeMessage(GetRegName(),
                                                                   null,
                                                                   SubscribeMode.Unsubscribe,
                                                                   channelId)
                        );
                }
                //subscribedChannelsList.Clear();
                foreach (var channelId in registeredChannelsList.ToArray())
                {
                    RemoveRegisteredChannel(new ChannelRegistrationMessage(GetRegName(),
                                                                   null,
                                                                   RegistrationMode.Unregister, 
                                                                   DataMode.Unknown,                                                                    
                                                                   channelId));
                }
                //registeredChannelsList.Clear();

                Provider.Unregister();
                CanRegister = true;
                Protocol("Отмена регистрации на сервере завершилась успешно");
            }
            catch (Exception)
            {
                CanRegister = false;
                throw;
            }
        }

        /// <summary>
        /// Записать в канал
        /// </summary>
        /// <param name="localChannelId"></param>
        /// <param name="value"></param>
        public void WriteChannel(int localChannelId, object value)
        {
            log.Debug("WriteChannel");

            var realLogicalChannelId = GetRealLogicalChannelId(localChannelId);
            Provider.WriteChannel(new InternalLogicalChannelDataMessage(GetRegName(), null, DataMode.Write, realLogicalChannelId)
            {
                Value = value
            });
        }

        private bool isitialized;
        private LogicalChannelMappings logicalChannelMappings;
        /// <summary>
        /// Сопоставление realId -> localId
        /// </summary>
        private Dictionary<int, int> localDic;
        /// <summary>
        /// Сопоставление localId -> realId
        /// </summary>
        private Dictionary<int, int> realDic;


        /// <summary>
        /// 
        /// </summary>
        public void InitProvider()
        {
            //инициализация проводится только 1 раз
            if (isitialized) return;
            
            Provider.Init();

            Provider.NeedProtocol += Provider_NeedProtocol;
            Provider.ChannelUnRegistered += Provider_ChannelUnRegistered;
            Provider.ChannelRegistered += Provider_ChannelRegistered;
            Provider.ChannelSubscribeCompleted += Provider_ChannelSubscribeCompleted;
            Provider.ChannelUnSubscribeCompleted += Provider_ChannelUnSubscribeCompleted;

            isitialized = true;
        }

        /// <summary>
        /// Прочитан канал
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelDataMessage>> HasReadChannel
        {
            add { Provider.HasReadChannel += value; }
            remove { Provider.HasReadChannel -= value; }
        }

        /// <summary>
        /// Прочитан канал
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelStateMessage>> ChannelStateChanged
        {
            add { Provider.ChannelStateChanged += value; }
            remove { Provider.ChannelStateChanged -= value; }
        }


        /// <summary>
        /// Событие постановки канала на регистрацию
        /// </summary>
        public event EventHandler<MessageEventArgs<ChannelRegistrationMessage>> ChannelRegistered
        {
            add { Provider.ChannelRegistered += value; }
            remove { Provider.ChannelRegistered -= value; }
        }
        /// <summary>
        /// Событие снятия канала с регистрации
        /// </summary>
        public event EventHandler<MessageEventArgs<ChannelRegistrationMessage>> ChannelUnRegistered
        {
            add { Provider.ChannelUnRegistered += value; }
            remove { Provider.ChannelUnRegistered -= value; }
        }
        /// <summary>
        /// Событие подписки на канал
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> ChannelSubscribed
        {
            add { Provider.ChannelSubscribeCompleted += value; }
            remove { Provider.ChannelSubscribeCompleted -= value; }
        }

        /// <summary>
        /// Событие отписки от канала
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> ChannelUnSubscribed
        {
            add { Provider.ChannelUnSubscribeCompleted += value; }
            remove { Provider.ChannelUnSubscribeCompleted -= value; }

        }

        /// <summary>
        /// Событие отмены регистрации
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted
        {
            add { Provider.UnregisterCompleted += value; }
            remove { Provider.UnregisterCompleted -= value; }
        }

        private void Provider_NeedProtocol(object sender, EventArgs e)
        {
            Protocol(sender);
        }

        private void Protocol(object sender)
        {
            if (NeedProtocol != null) NeedProtocol(sender, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler NeedProtocol;

        private void Provider_ChannelUnRegistered(object sender, MessageEventArgs<ChannelRegistrationMessage> e)
        {
            log.Debug("Provider_ChannelUnRegistered");

            RemoveRegisteredChannel(e.Message);
            Protocol(string.Format("Канал [{0}] теперь недоступен для подписки", e.Message.LogicalChannelId));
        }

        private void RemoveRegisteredChannel(ChannelRegistrationMessage message)
        {
            log.Debug("RemoveRegisteredChannel");

            //EventHandler handler = ChannelUnRegistered;
            //if (handler != null) handler(this, EventArgs.Empty);    
            var channelId = message.LogicalChannelId;
            if (subscribedChannelsList.Contains(channelId))
            {
                ChannelSubscribeMessage unSubscribeMessage = new ChannelSubscribeMessage(GetRegName(), null,
                                                                                         SubscribeMode.Unsubscribe,
                                                                                         channelId);

                ParameterizedThreadStart thread = UnSubscribeUnregisteredChannelAsync;
                thread.Invoke(unSubscribeMessage);
            }
            if (registeredChannelsList.Contains(channelId))
                registeredChannelsList.Remove(channelId);
        }

        private void UnSubscribeUnregisteredChannelAsync(object message)
        {
            log.Debug("UnSubscribeUnregisteredChannelAsync");

            //Thread.Sleep(TimeSpan.FromSeconds(1));

            ChannelSubscribeMessage channelSubscribeMessage = (ChannelSubscribeMessage)message;
            subscribedChannelsList.Remove(channelSubscribeMessage.LogicalChannelId);
            string s = string.Format("Подписка на канал [{0}] была отменена из-за отмены регистрации канала",
                                          channelSubscribeMessage.LogicalChannelId);
            Protocol(s);
            //MessageBox.Show(s);
            //Program.Proxy.ChannelUnSubscribe(channelSubscribeMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Func<string> GetRegName
        {
            protected get { return Provider.GetRegName; }
            set { Provider.GetRegName = value; }
        }

        private void Provider_ChannelRegistered(object sender, MessageEventArgs<ChannelRegistrationMessage> e)
        {
            log.Debug("Provider_ChannelRegistered");
            AddRegisteredChannel(e.Message);
        }

        private void Provider_ChannelSubscribeCompleted(object sender, AsyncCompletedEventArgs e)
        {
            log.Debug("Provider_ChannelSubscribeCompleted");

            var channelId = Convert.ToInt32(e.UserState);
            Protocol(string.Format("Произошла подписка на канал [{0}]", channelId));
            if(!subscribedChannelsList.Contains(channelId))
                subscribedChannelsList.Add(channelId);
            //EventHandler handler = ChannelSubscribed;
            //if (handler != null) handler(this, EventArgs.Empty);   
        }

        private void Provider_ChannelUnSubscribeCompleted(object sender, AsyncCompletedEventArgs e)
        {
            log.Debug("Provider_ChannelUnSubscribeCompleted");

            var channelId = Convert.ToInt32(e.UserState);
            Protocol(string.Format("Произошла отписка от канала [{0}]", channelId));
            if (subscribedChannelsList.Contains(channelId))
                subscribedChannelsList.Remove(channelId);
            //EventHandler handler = ChannelUnSubscribed;
            //if (handler != null) handler(this, EventArgs.Empty);    
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> if managed resources should be disposed; otherwise, <see langword="false"/>.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (isitialized)
                {
                    Provider.NeedProtocol -= Provider_NeedProtocol;
                    Provider.ChannelUnRegistered -= Provider_ChannelUnRegistered;
                    Provider.ChannelRegistered -= Provider_ChannelRegistered;
                    Provider.ChannelSubscribeCompleted -= Provider_ChannelSubscribeCompleted;
                    Provider.ChannelUnSubscribeCompleted -= Provider_ChannelUnSubscribeCompleted;

                    Provider.Dispose();

                    isitialized = false;
                }

                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }


        private class DelayedSubscriber
        {
            private readonly ChannelSubscribeMessage _message;
            private readonly ClientProvider _provider;

            private DelayedSubscriber(ClientProvider provider, ChannelSubscribeMessage message)
            {
                _message = message;
                _provider = provider;
            }

            private void Subscribe()
            {
                Thread.Sleep(1000);
                _provider.SubscribeChannel(_message);
            }

            public static void Subscribe(ClientProvider provider, ChannelSubscribeMessage message)
            {
                DelayedSubscriber subscriber = new DelayedSubscriber(provider, message);
                Thread thread = new Thread(subscriber.Subscribe);
                thread.Start();
            }
        }

        private void SubscribeChannel(ChannelSubscribeMessage message)
        {
            log.Debug("SubscribeChannel");

            DelayedSubscriber.Subscribe(Provider, message);
            //Provider.SubscribeChannel(message);
        }

        private void UnSubscribeChannel(ChannelSubscribeMessage subscribeMessage)
        {
            log.Debug("UnSubscribeChannel");

            Provider.UnSubscribeChannel(subscribeMessage);
        }


    }
}

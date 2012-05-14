using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using DMS.Common.Events;
using DMS.Common.Messages;
using Oleg_ivo.HighLevelClient;

namespace TP.WPF
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ChannelController : Component
    {
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// 
        /// </summary>
        public ChannelController()
        {
            //InitializeComponent();
            components = new System.ComponentModel.Container();
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

        /// <summary>
        /// 
        /// </summary>
        protected ClientProvider Provider
        {
            get
            {
#if LABVIEW
                return LabViewClientProvider.Instance;
#else
                return ClientProvider.Instance;
#endif
            }
        }

        /// <summary>
        /// регистрация
        /// </summary>
        public void Register()
        {
            try
            {
                Provider.Register(false, RegisterCompleted);
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

        void RegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // регистрация завершена
            CanRegister = false;
            var registeredChannels = Provider.RegisteredChannels;
            if (registeredChannels == null || registeredChannels.Length == 0)
            {
                Protocol("Регистрация завершена успешно. На сервере не опубликовано ни одного канала");
            }
            else
                foreach (var registeredChannel in registeredChannels)
                {
                    //TODO: заполнить RegNameFrom
                    ChannelRegistrationMessage message = new ChannelRegistrationMessage(null, null,
                                                                                        RegistrationMode.Register,
                                                                                        DataMode.Read, registeredChannel);
                    AddRegisteredChannel(message);
                }
        }

        readonly IList registeredChannelsList = new List<int>();
        readonly IList subscribedChannelsList = new List<int>();

        private void AddRegisteredChannel(ChannelRegistrationMessage message)
        {
            if (!registeredChannelsList.Contains(message.LogicalChannelId) && !subscribedChannelsList.Contains(message.LogicalChannelId))
            {
                registeredChannelsList.Add(message.LogicalChannelId);
                Protocol(string.Format("Канал [{0}] теперь доступен для подписки", message.LogicalChannelId));
                if (AutoSubscribeChannels)
                {
                    SubscribeChannel(new ChannelSubscribeMessage(RegName, null, SubscribeMode.Subscribe,
                                                                 message.LogicalChannelId)
                        );
                }
            }

        }

        /// <summary>
        /// отмена регистрации
        /// </summary>
        public void Unregister()
        {
            try
            {
                Provider.Unregister();

                //убираем все зарегистрированные и подписанные каналы:
                registeredChannelsList.Clear();
                subscribedChannelsList.Clear();

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
        /// <param name="channelId"></param>
        /// <param name="value"></param>
        public void WriteChannel(int channelId, object value)
        {
            Provider.WriteChannel(new InternalLogicalChannelDataMessage(RegName, null, DataMode.Write, channelId)
            {
                Value = value
            });
        }

        private bool isitialized;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="regName"></param>
        public void InitProvider(string regName)
        {
            //инициализация проводится только 1 раз
            if (!isitialized)
            {
                RegName = regName;
                Provider.Init(regName);

                Provider.NeedProtocol += Provider_NeedProtocol;
                Provider.ChannelUnRegistered += Provider_ChannelUnRegistered;
                Provider.ChannelRegistered += Provider_ChannelRegistered;
                Provider.ChannelSubscribeCompleted += Provider_ChannelSubscribeCompleted;
                Provider.ChannelUnSubscribeCompleted += Provider_ChannelUnSubscribeCompleted;

                isitialized = true;
            }
        }

        /// <summary>
        /// Прочитан канал
        /// </summary>
        public event EventHandler<DataEventArgs> HasReadChannel
        {
            add { Provider.HasReadChannel += value; }
            remove { Provider.HasReadChannel -= value; }
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

        private void Provider_ChannelUnRegistered(object sender, ChannelRegisterEventArgs e)
        {
            RemoveRegisteredChannel(e.Message);
            Protocol(string.Format("Канал [{0}] теперь недоступен для подписки", e.Message.LogicalChannelId));
        }

        private void RemoveRegisteredChannel(ChannelRegistrationMessage message)
        {
            if (registeredChannelsList.Contains(message.LogicalChannelId))
            {
                registeredChannelsList.Remove(message.LogicalChannelId);
            }
            else if (subscribedChannelsList.Contains(message.LogicalChannelId))
            {
                ChannelSubscribeMessage unSubscribeMessage = new ChannelSubscribeMessage(RegName, null,
                                                                                         SubscribeMode.Unsubscribe,
                                                                                         message.LogicalChannelId);

                subscribedChannelsList.Remove(message.LogicalChannelId);

                ParameterizedThreadStart thread = UnSubscribeUnregisteredChannelAsync;
                thread.Invoke(unSubscribeMessage);
            }
        }

        private void UnSubscribeUnregisteredChannelAsync(object message)
        {
            //Thread.Sleep(TimeSpan.FromSeconds(1));

            ChannelSubscribeMessage channelSubscribeMessage = (ChannelSubscribeMessage)message;
            string s = string.Format("Подписка на канал [{0}] была отменена из-за отмены регистрации канала",
                                          channelSubscribeMessage.LogicalChannelId);
            Protocol(s);
            //            MessageBox.Show(s);
            //            Program.Proxy.ChannelUnSubscribe(channelSubscribeMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        protected string RegName { get; set; }

        private void Provider_ChannelRegistered(object sender, ChannelRegisterEventArgs e)
        {
            AddRegisteredChannel(e.Message);
        }

        private void Provider_ChannelSubscribeCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Protocol(string.Format("Произошла подписка на канал [{0}]", e.UserState));
        }

        private void Provider_ChannelUnSubscribeCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Protocol(string.Format("Произошла отписка от канала [{0}]", e.UserState));
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
            DelayedSubscriber.Subscribe(Provider, message);
            //Provider.SubscribeChannel(message);
        }

        private void UnSubscribeChannel(ChannelSubscribeMessage subscribeMessage)
        {
            Provider.UnSubscribeChannel(subscribeMessage);
        }


    }
}

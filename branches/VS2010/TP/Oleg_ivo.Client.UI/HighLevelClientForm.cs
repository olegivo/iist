#define SECURITY

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DMS.Common.Events;
using DMS.Common.MessageExchangeSystem.HighLevel;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Tools.UI;

namespace Oleg_ivo.HighLevelClient.UI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HighLevelClientForm : Form
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        public ClientProvider Provider
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
        /// 
        /// </summary>
        public HighLevelClientForm()
        {
            InitializeComponent();

            /*
            Binding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
            EndpointAddress endPoint = new EndpointAddress(@"net.pipe://localhost/Oleg_ivo.MES/High/high");
            */

            //режим клиента для LabView BEGIN
#if LABVIEW
            Console.WriteLine("Инициализация LabViewClientProvider...");
// ReSharper disable RedundantAssignment
            LabViewClientProvider.BindingType bindingType = LabViewClientProvider.BindingType.Unknown;
// ReSharper restore RedundantAssignment

            //определение типа соединения:
#if WSHTTP_BINDING
            bindingType = LabViewClientProvider.BindingType.WSDualHttpBinding;
#else
            bindingType = LabViewClientProvider.BindingType.NetNamePype;
#endif

            //определение защищённого соединения:
            bool security;
#if SECURITY
            security = true;
#else
            security = false;
#endif

            string uri;
            switch (bindingType)
            {
                case LabViewClientProvider.BindingType.NetNamePype:
                    uri = @"net.pipe://localhost/Oleg_ivo.MES/High/high";
                    break;
                case LabViewClientProvider.BindingType.WSDualHttpBinding:
                    uri = @"http://localhost:8081/Oleg_ivo.MES/High/high";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            LabViewClientProvider.InitBinding(bindingType, security);
            LabViewClientProvider.InitRemoteAddress(uri);
#endif
            //режим клиента для LabView END

            Provider.Init(GetRegName());

            Provider.NeedProtocol += Provider_NeedProtocol;
            Provider.ChannelUnRegistered += Provider_ChannelUnRegistered;
            Provider.ChannelRegistered += Provider_ChannelRegistered;
            Provider.ChannelSubscribeCompleted += Provider_ChannelSubscribeCompleted;
            Provider.ChannelUnSubscribeCompleted += Provider_ChannelUnSubscribeCompleted;
        }

        private void Provider_ChannelUnSubscribeCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Protocol(string.Format("Произошла отписка от канала [{0}]", e.UserState));
        }

        void Provider_ChannelSubscribeCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Protocol(string.Format("Произошла подписка на канал [{0}]", e.UserState));
        }

        void Provider_ChannelRegistered(object sender, ChannelRegisterEventArgs e)
        {
            AddRegisteredChannel(e.Message);
        }

        private void AddRegisteredChannel(ChannelRegistrationMessage message)
        {
            var sourceLeft = (List<RegisteredLogicalChannel>)doubleListBoxControl1.SourceLeft;
            var sourceRight = (List<RegisteredLogicalChannel>)doubleListBoxControl1.SourceRight;

            var channelId = message.LogicalChannelId;
            Func<RegisteredLogicalChannel, bool> func = c => c.LogicalChannelId == channelId;

            var hasLeft = sourceLeft.Any(func);
            var hasRight = sourceRight.Any(func);
            if (!hasLeft && !hasRight)
            {
                var channel = new RegisteredLogicalChannel(channelId)
                    {
                        Description = message.Description,
                        MinValue = message.MinValue,
                        MaxValue = message.MaxValue,
                        MinNormalValue = message.MinNormalValue,
                        MaxNormalValue = message.MaxNormalValue
                    };
                sourceLeft.Add(channel);
                doubleListBoxControl1.InitSources(sourceLeft, sourceRight);

                Protocol(string.Format("Канал [{0}] теперь доступен для подписки ({1}) [{2};{3}]",
                                       message.LogicalChannelId,
                                       message.Description,
                                       message.MinValue,
                                       message.MaxValue));
            }
            else if(hasLeft)
            {
                Protocol(string.Format("Канал [{0}] уже был доступен для подписки ({1}) [{2};{3}]",
                                       message.LogicalChannelId,
                                       message.Description,
                                       message.MinValue,
                                       message.MaxValue));
            }
            else
            {
                Protocol(string.Format("На канал [{0}] уже была подписка ({1}) [{2};{3}]",
                                       message.LogicalChannelId,
                                       message.Description,
                                       message.MinValue,
                                       message.MaxValue));
            }
        }

        void Provider_ChannelUnRegistered(object sender, ChannelRegisterEventArgs e)
        {
            RemoveRegisteredChannel(e.Message);
            Protocol(string.Format("Канал [{0}] теперь недоступен для подписки", e.Message.LogicalChannelId));
        }

        private void RemoveRegisteredChannel(ChannelRegistrationMessage message)
        {
            var sourceLeft = (List<RegisteredLogicalChannel>)doubleListBoxControl1.SourceLeft;
            var sourceRight = (List<RegisteredLogicalChannel>)doubleListBoxControl1.SourceRight;

            var channelId = message.LogicalChannelId;
            Func<RegisteredLogicalChannel, bool> func = c => c.LogicalChannelId == channelId;
            foreach (var source in sourceLeft.Where(func).ToList())
                sourceLeft.Remove(source);

            foreach (var source in sourceRight.Where(func).ToList())
            {
                var unSubscribeMessage = new ChannelSubscribeMessage(GetRegName(),
                                                                     null,
                                                                     SubscribeMode.Unsubscribe,
                                                                     channelId);
                sourceRight.Remove(source);

                ParameterizedThreadStart thread = UnSubscribeUnregisteredChannelAsync;
                thread.Invoke(unSubscribeMessage);
            }

            doubleListBoxControl1.InitSources(sourceLeft, sourceRight);
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

        void Provider_NeedProtocol(object sender, EventArgs e)
        {
            Protocol(sender);
        }

        private void Protocol(object sender)
        {
            if (sender is double || sender is string)
            {
                string s = string.Format("{0}\t{1}{2}", DateTime.Now, sender, Environment.NewLine);
                log.Debug(s);
                SetText(textBox1, s);
            }
        }

        private delegate void StDelegate(TextBox info, string s);
        private void SetText(TextBox info, string s)
        {
            if (info.InvokeRequired)
            {
                StDelegate ddd = SetText;
                info.Invoke(ddd, new object[] { info, s });
            }
            else
            {
                textBox1.Text += s;
            }
        }

        /// <summary>
        /// Вызывает событие <see cref="E:System.Windows.Forms.Form.Closing"/>.
        /// </summary>
        /// <param name="e">Объект <see cref="T:System.ComponentModel.CancelEventArgs"/>, содержащий данные события. </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Provider.NeedProtocol -= Provider_NeedProtocol;
            Provider.ChannelUnRegistered -= Provider_ChannelUnRegistered;
            Provider.ChannelRegistered -= Provider_ChannelRegistered;
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //Provider.SendMessage(new InternalMessage());
        }

        private bool CanRegister
        {
            set
            {
                btnRegister.Enabled = textBox2.Enabled = value;
                doubleListBoxControl1.Enabled = btnSendMessage.Enabled = btnSendError.Enabled = btnUnregister.Enabled = !value;
            }
        }

        internal string GetRegName()
        {
            return textBox2.Text;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // регистрация
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

        void RegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            CanRegister = false;
            // регистрация завершена
            /*
            var registeredChannels = Provider.RegisteredChannels;
            if (registeredChannels == null)
                Protocol("Регистрация завершена успешно. На сервере не опубликовано ни одного канала");
            else
                foreach (var registeredChannel in registeredChannels)
                {
                    var message = new ChannelRegistrationMessage(GetRegName(),
                                                                 null,
                                                                 RegistrationMode.Register,
                                                                 DataMode.Read,
                                                                 registeredChannel.LogicalChannelId);
                    AddRegisteredChannel(message);
                }
            */
        }



        private void btnUnregister_Click(object sender, EventArgs e)
        {
            // отмена регистрации
            try
            {
                Provider.Unregister();
                doubleListBoxControl1.InitSources(new List<RegisteredLogicalChannel>(), new List<RegisteredLogicalChannel>());//убираем все зарегистрированные и подписанные каналы
                CanRegister = true;
            }
            catch (Exception)
            {
                CanRegister = false;
                throw;
            }
        }

        private void doubleListBoxControl1_ItemMoved(object sender, MovedEventArgs e)
        {
            SubscribeMode regMode = e.MoveDirection == DoubleListBoxControl.Direction.LeftToRight
                                        ? SubscribeMode.Subscribe
                                        : SubscribeMode.Unsubscribe;
            RegisteredLogicalChannel logicalChannel = (RegisteredLogicalChannel) e.MovingObject;
            ChannelSubscribeMessage subscribeMessage = new ChannelSubscribeMessage(GetRegName(), null, regMode,
                                                                                   logicalChannel.LogicalChannelId);

            switch (subscribeMessage.Mode)
            {
                case SubscribeMode.Subscribe:
                    SubscribeChannel(subscribeMessage);
                    break;
                case SubscribeMode.Unsubscribe:
                    UnSubscribeChannel(subscribeMessage);
                    break;
            }
        }

        private void SubscribeChannel(ChannelSubscribeMessage message)
        {
            Provider.SubscribeChannel(message);
        }

        private void UnSubscribeChannel(ChannelSubscribeMessage subscribeMessage)
        {
            Provider.UnSubscribeChannel(subscribeMessage);
        }

        private void HighLevelClientForm_Load(object sender, EventArgs e)
        {
            var left = new List<RegisteredLogicalChannel>();
            var right = new List<RegisteredLogicalChannel>();

            doubleListBoxControl1.InitDisplayMember("Id");
            doubleListBoxControl1.InitSources(left, right);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();
        }

        private void btnSendError_Click(object sender, EventArgs e)
        {
            var channelId = doubleListBoxControl1.SelectionRight.Count == 1 ? (int)doubleListBoxControl1.SelectionRight[0] : 0;
            var message = new InternalLogicalChannelDataMessage(GetRegName(), null, DataMode.Write, channelId) { Value = GetValueToWrite() };
            Provider.WriteChannel(message);
            //var exception = new TestException("Произошла тестовая ошибка :(");
            //throw exception;
            //Provider.Proxy.SendErrorAsync(new InternalErrorMessage(exception));
        }

        private object GetValueToWrite()
        {
            var form = new GetValueForm() { Value = "0" };
            DialogResult result = form.ShowDialog();
            return result == DialogResult.OK ? Convert.ToUInt16(form.Value) : 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Provider.Init(GetRegName());
        }
    }
}
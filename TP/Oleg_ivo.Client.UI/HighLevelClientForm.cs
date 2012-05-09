#define SECURITY

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DMS.Common.Events;
using DMS.Common.Exceptions;
using DMS.Common.Messages;
using Oleg_ivo.Tools.UI;

namespace Oleg_ivo.HighLevelClient.UI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HighLevelClientForm : Form
    {
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

            //����� ������� ��� LabView BEGIN
#if LABVIEW
            Console.WriteLine("������������� LabViewClientProvider...");
// ReSharper disable RedundantAssignment
            LabViewClientProvider.BindingType bindingType = LabViewClientProvider.BindingType.Unknown;
// ReSharper restore RedundantAssignment

            //����������� ���� ����������:
#if WSHTTP_BINDING
            bindingType = LabViewClientProvider.BindingType.WSDualHttpBinding;
#else
            bindingType = LabViewClientProvider.BindingType.NetNamePype;
#endif

            //����������� ����������� ����������:
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
            //����� ������� ��� LabView END

            Provider.Init(GetRegName());

            Provider.NeedProtocol += Provider_NeedProtocol;
            Provider.ChannelUnRegistered += Provider_ChannelUnRegistered;
            Provider.ChannelRegistered += Provider_ChannelRegistered;
            Provider.ChannelSubscribeCompleted += Provider_ChannelSubscribeCompleted;
            Provider.ChannelUnSubscribeCompleted += Provider_ChannelUnSubscribeCompleted;
        }

        private void Provider_ChannelUnSubscribeCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Protocol(string.Format("��������� ������� �� ������ [{0}]", e.UserState));
        }

        void Provider_ChannelSubscribeCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Protocol(string.Format("��������� �������� �� ����� [{0}]", e.UserState));
        }

        void Provider_ChannelRegistered(object sender, ChannelRegisterEventArgs e)
        {
            AddRegisteredChannel(e.Message);
        }

        private void AddRegisteredChannel(ChannelRegistrationMessage message)
        {
            IList sourceLeft = doubleListBoxControl1.SourceLeft;
            IList sourceRight = doubleListBoxControl1.SourceRight;

            if (!sourceLeft.Contains(message.LogicalChannelId) && !sourceRight.Contains(message.LogicalChannelId))
            {
                sourceLeft.Add(message.LogicalChannelId);
                doubleListBoxControl1.InitSources(sourceLeft, sourceRight);
            }

            Protocol(string.Format("����� [{0}] ������ �������� ��� ��������", message.LogicalChannelId));
        }

        void Provider_ChannelUnRegistered(object sender, ChannelRegisterEventArgs e)
        {
            RemoveRegisteredChannel(e.Message);
            Protocol(string.Format("����� [{0}] ������ ���������� ��� ��������", e.Message.LogicalChannelId));
        }

        private void RemoveRegisteredChannel(ChannelRegistrationMessage message)
        {
            IList sourceLeft = doubleListBoxControl1.SourceLeft;
            IList sourceRight = doubleListBoxControl1.SourceRight;

            if (sourceLeft.Contains(message.LogicalChannelId))
            {
                sourceLeft.Remove(message.LogicalChannelId);
            }
            else if (sourceRight.Contains(message.LogicalChannelId))
            {
                ChannelSubscribeMessage unSubscribeMessage = new ChannelSubscribeMessage(GetRegName(), null,
                                                                                         SubscribeMode.Unsubscribe,
                                                                                         message.LogicalChannelId);
                sourceRight.Remove(message.LogicalChannelId);

                ParameterizedThreadStart thread = UnSubscribeUnregisteredChannelAsync;
                thread.Invoke(unSubscribeMessage);
            }

            doubleListBoxControl1.InitSources(sourceLeft, sourceRight);
        }

        private void UnSubscribeUnregisteredChannelAsync(object message)
        {
            //Thread.Sleep(TimeSpan.FromSeconds(1));

            ChannelSubscribeMessage channelSubscribeMessage = (ChannelSubscribeMessage)message;
            string s = string.Format("�������� �� ����� [{0}] ���� �������� ��-�� ������ ����������� ������",
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
        /// �������� ������� <see cref="E:System.Windows.Forms.Form.Closing"/>.
        /// </summary>
        /// <param name="e">������ <see cref="T:System.ComponentModel.CancelEventArgs"/>, ���������� ������ �������. </param>
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
            // �����������
            try
            {
                Provider.Register(RegisterCompleted);
            }
            catch (Exception)
            {
                CanRegister = true;
                throw;
            }
        }

        void RegisterCompleted(object sender, AsyncCompletedEventArgs e)
        {
            CanRegister = false;
            // ����������� ���������
            var registeredChannels = Provider.RegisteredChannels;
            if (registeredChannels == null)
                Protocol("����������� ��������� �������. �� ������� �� ������������ �� ������ ������");
            else
                foreach (var registeredChannel in registeredChannels)
                {
                    ChannelRegistrationMessage message = new ChannelRegistrationMessage(GetRegName(), null,
                                                                                        RegistrationMode.Register,
                                                                                        DataMode.Read, registeredChannel);
                    AddRegisteredChannel(message);
                }
        }



        private void btnUnregister_Click(object sender, EventArgs e)
        {
            // ������ �����������
            try
            {
                Provider.Unregister();
                doubleListBoxControl1.InitSources(new List<int>(), new List<int>());//������� ��� ������������������ � ����������� ������
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
            ChannelSubscribeMessage subscribeMessage = new ChannelSubscribeMessage(GetRegName(), null, regMode,
                                                                                   (int)e.MovingObject);

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
            List<int> _left = new List<int>();
            List<int> _right = new List<int>();

            //            _left.AddRange(Enumerable.Range(28, 10));
            //            _right.AddRange(Enumerable.Range(11, 10));

            doubleListBoxControl1.InitSources(_left, _right);
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
            //var exception = new TestException("��������� �������� ������ :(");
            //throw exception;
            //Provider.Proxy.SendErrorAsync(new InternalErrorMessage(exception));
        }

        private object GetValueToWrite()
        {
            var form = new GetValueForm(){Value = "0"};
            DialogResult result = form.ShowDialog();
            return result == DialogResult.OK ? Convert.ToUInt16(form.Value) : 0;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Provider.Init(GetRegName());
        }
    }
}
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using DMS.Common.Messages;
using Oleg_ivo.MES.High;
using Oleg_ivo.MES.Low;
using Oleg_ivo.MES.Registered;
using Oleg_ivo.Tools.ConnectionProvider;

namespace Oleg_ivo.MES
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MesForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public MesForm()
        {
            InitializeComponent();

/*
            HighLevelMessageExchangeSystem.Instance.BeforeUpdate += High_BeforeUpdate;
            LowLevelMessageExchangeSystem.Instance.BeforeUpdate += High_BeforeUpdate;
*/

            HighLevelMessageExchangeSystem.Instance.ClientRegistered += High_ClientRegistered;
            LowLevelMessageExchangeSystem.Instance.ClientRegistered += Low_ClientRegistered;

            HighLevelMessageExchangeSystem.Instance.ClientUnregistered += High_ClientUnregistered;
            LowLevelMessageExchangeSystem.Instance.ClientUnregistered += Low_ClientUnregistered;

            HighLevelMessageExchangeSystem.Instance.MessageReceived += Instance_MessageReceived;
            LowLevelMessageExchangeSystem.Instance.MessageReceived += Instance_MessageReceived;

            HighLevelMessageExchangeSystem.Instance.ErrorReceived += Instance_ErrorReceived;
            LowLevelMessageExchangeSystem.Instance.ErrorReceived += Instance_ErrorReceived;

            LowLevelMessageExchangeSystem.Instance.ChannelRegistered += Low_ChannelRegistered;
            LowLevelMessageExchangeSystem.Instance.ChannelUnregistered += Low_ChannelUnregistered;

            HighLevelMessageExchangeSystem.Instance.ChannelSubscribed += High_ChannelSubscribed;
            HighLevelMessageExchangeSystem.Instance.ChannelUnSubscribed += High_UnSubscribed;
        }

        void Instance_ErrorReceived(object sender, ErrorReceivedEventArgs e)
        {
            InternalErrorMessage internalErrorMessage = e.Message;
            string s = String.Format("{0}\tКлиент {1} сообщает об ошибке:{2}{3}", internalErrorMessage.TimeStamp,
                                     internalErrorMessage.RegNameFrom,
                                     Environment.NewLine, internalErrorMessage.Error);
            Protocol(s);
            
        }

        private void Protocol(string s)
        {
            SetText(textBox1, s);
        }

        void Instance_MessageReceived(object sender, EventArgs e)
        {
            string s = sender as string;
            if (s != null)
                Protocol(s);
        }

        private void Low_ClientUnregistered(object sender, LowLevelClientEventArgs e)
        {
            if (e.RegisteredLowLevelClient != null)
            {
                RegistrationMessage registrationMessage = e.Message as RegistrationMessage;
                if (registrationMessage != null)
                {
                    var client = new { Name = registrationMessage.RegNameFrom, Value = e.RegisteredLowLevelClient };
                    RemoveItem(lbRegisteredLow, client);
                    Protocol(String.Format("{0}\tКлиент нижнего уровня [{1}] отменил регистрацию на сервере{2}",
                                           DateTime.Now, registrationMessage.RegNameFrom, Environment.NewLine));
                }
            }
        }

        void High_ClientUnregistered(object sender, HighLevelClientEventArgs e)
        {
            if (e.RegisteredHighLevelClient != null)
            {
                RegistrationMessage registrationMessage = e.Message as RegistrationMessage;
                if (registrationMessage != null)
                {
                    var client = new { Name = registrationMessage.RegNameFrom, Value = e.RegisteredHighLevelClient };
                    RemoveItem(lbRegisteredHigh, client);
                    Protocol(String.Format("{0}\tКлиент верхнего уровня [{1}] отменил регистрацию на сервере{2}",
                                           DateTime.Now, registrationMessage.RegNameFrom, Environment.NewLine));
                }
            }
        }

        private void Low_ClientRegistered(object sender, LowLevelClientEventArgs e)
        {
            if (e.RegisteredLowLevelClient != null)
            {
                RegistrationMessage registrationMessage = e.Message as RegistrationMessage;
                if (registrationMessage != null)
                {
                    var client = new {Name = registrationMessage.RegNameFrom, Value = e.RegisteredLowLevelClient};
                    AddItem(lbRegisteredLow, client);
                    Protocol(String.Format("{0}\tКлиент нижнего уровня [{1}] зарегистрировался на сервере{2}",
                                           DateTime.Now, registrationMessage.RegNameFrom, Environment.NewLine));
                }
            }
        }

        void High_ClientRegistered(object sender, HighLevelClientEventArgs e)
        {
            if (e.RegisteredHighLevelClient != null)
            {
                RegistrationMessage registrationMessage = e.Message as RegistrationMessage;
                if (registrationMessage != null)
                {
                    var client = new {Name = registrationMessage.RegNameFrom, Value = e.RegisteredHighLevelClient};
                    AddItem(lbRegisteredHigh, client);
                    Protocol(String.Format("{0}\tКлиент верхнего уровня [{1}] зарегистрировался на сервере{2}",
                                           DateTime.Now, registrationMessage.RegNameFrom, Environment.NewLine));
                }
            }
        }

        private void Low_ChannelRegistered(object sender, LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            if (e.RegisteredLowLevelClient != null && e.ChannelRegistrationMessage != null)
            {
                string s = String.Format("{0}\tКлиент нижнего уровня [{1}] зарегистрировал на сервере канал [{2}]{3}",
                                              DateTime.Now, 
                                              e.ChannelRegistrationMessage.RegNameFrom,
                                              e.ChannelRegistrationMessage.LogicalChannelId,
                                              Environment.NewLine);
                Protocol(s);
            }
        }

        private void Low_ChannelUnregistered(object sender, LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            if (e.RegisteredLowLevelClient != null && e.ChannelRegistrationMessage != null)
            {
                string s = String.Format("{0}\tКлиент нижнего уровня [{1}] отменил регистрацию на сервере канала [{2}]{3}",
                                              DateTime.Now, 
                                              e.ChannelRegistrationMessage.RegNameFrom,
                                              e.ChannelRegistrationMessage.LogicalChannelId,
                                              Environment.NewLine);
                Protocol(s);
            }
        }

        private void High_ChannelSubscribed(object sender, HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            if (e.RegisteredHighLevelClient != null && e.ChannelSubscribeMessage != null)
            {
                string s = String.Format("{0}\tКлиент верхнего уровня [{1}] подписался канал [{2}]{3}",
                                              DateTime.Now,
                                              e.ChannelSubscribeMessage.RegNameFrom,
                                              e.ChannelSubscribeMessage.LogicalChannelId,
                                              Environment.NewLine);
                Protocol(s);
            }
        }

        private void High_UnSubscribed(object sender, HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            if (e.RegisteredHighLevelClient != null && e.ChannelSubscribeMessage != null)
            {
                string s = String.Format("{0}\tКлиент верхнего уровня [{1}] отписался от канала [{2}]{3}",
                                              DateTime.Now,
                                              e.ChannelSubscribeMessage.RegNameFrom,
                                              e.ChannelSubscribeMessage.LogicalChannelId,
                                              Environment.NewLine);
                Protocol(s);
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


        private delegate void ListBoxOperationDelegate(ListBox info, object s);

        private void AddItem(ListBox info, object s)
        {
            if (info.InvokeRequired)
            {
                ListBoxOperationDelegate ddd = AddItem;
                info.Invoke(ddd, new[] { info, s });
            }
            else
            {
                info.Items.Add(s);
                if (info.Items.Count == 1) info.SelectedItem = info.Items[0];
            }
        }

        private void RemoveItem(ListBox info, object s)
        {
            if (info.InvokeRequired)
            {
                ListBoxOperationDelegate ddd = RemoveItem;
                info.Invoke(ddd, new[] { info, s });
            }
            else
            {
                info.Items.Remove(s);
            }
        }


        /// <summary>
        /// Вызывает событие <see cref="E:System.Windows.Forms.Form.Closing"/>.
        /// </summary>
        /// <param name="e">Объект <see cref="T:System.ComponentModel.CancelEventArgs"/>, содержащий данные события. </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

/*
            HighLevelMessageExchangeSystem.Instance.BeforeUpdate -= High_BeforeUpdate;
            LowLevelMessageExchangeSystem.Instance.BeforeUpdate -= High_BeforeUpdate;
*/

            HighLevelMessageExchangeSystem.Instance.ClientRegistered -= High_ClientRegistered;
            LowLevelMessageExchangeSystem.Instance.ClientRegistered -= Low_ClientRegistered;

            HighLevelMessageExchangeSystem.Instance.ClientUnregistered -= High_ClientUnregistered;
            LowLevelMessageExchangeSystem.Instance.ClientUnregistered -= Low_ClientUnregistered;

            HighLevelMessageExchangeSystem.Instance.MessageReceived -= Instance_MessageReceived;
            LowLevelMessageExchangeSystem.Instance.MessageReceived -= Instance_MessageReceived;

            LowLevelMessageExchangeSystem.Instance.ChannelRegistered -= Low_ChannelRegistered;
            LowLevelMessageExchangeSystem.Instance.ChannelUnregistered -= Low_ChannelUnregistered;

            HighLevelMessageExchangeSystem.Instance.ChannelSubscribed -= High_ChannelSubscribed;
            HighLevelMessageExchangeSystem.Instance.ChannelUnSubscribed -= High_UnSubscribed;
        }

        private void btnSendMessageHigh_Click(object sender, EventArgs e)
        {
            foreach (var selectedItem in lbRegisteredHigh.SelectedItems)
            {
                RegisteredHighLevelClient registeredHighLevelClient =
                    GetRegisteredClient(selectedItem, lbRegisteredHigh.ValueMember) as RegisteredHighLevelClient;
                string clientRegName =
                    GetClientRegName(selectedItem, lbRegisteredLow.DisplayMember);
                if (registeredHighLevelClient != null)
                    registeredHighLevelClient.SendMessageToClient(new InternalMessage(HighLevelMessageExchangeSystem.Instance.RegName, clientRegName));
            }
        }

        private object GetRegisteredClient(object selectedItem, string valueMember)
        {
            return selectedItem.GetType().GetProperty(valueMember).GetGetMethod().Invoke(
                selectedItem, null);
        }

        private string GetClientRegName(object selectedItem, string displayMember)
        {
            return selectedItem.GetType().GetProperty(displayMember).GetGetMethod().Invoke(
                selectedItem, null) as string;
        }

        private void btnSendMessageLow_Click(object sender, EventArgs e)
        {
            foreach (var selectedItem in lbRegisteredLow.SelectedItems)
            {
                RegisteredLowLevelClient registeredHighLevelClient =
                    GetRegisteredClient(selectedItem, lbRegisteredLow.ValueMember) as RegisteredLowLevelClient;
                string clientRegName =
                    GetClientRegName(selectedItem, lbRegisteredHigh.DisplayMember);
                if (registeredHighLevelClient != null)
                    registeredHighLevelClient.SendMessageToClient(new InternalMessage(LowLevelMessageExchangeSystem.Instance.RegName, clientRegName));
            }
        }

        private void lbRegisteredHigh_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSendMessageHigh.Enabled = lbRegisteredHigh.SelectedItems.Count > 0;
        }

        private void lbRegisteredLow_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSendMessageLow.Enabled = lbRegisteredLow.SelectedItems.Count > 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();
            textBox1.Refresh();
        }

        private void MesForm_Load(object sender, EventArgs e)
        {
            TestConnection(DbConnectionProvider.Instance);
        }


        private void TestConnection(DbConnectionProvider dbConnectionProvider)
        {
            var command = new SqlCommand("select 1");
            dbConnectionProvider.OpenConnection(command);
            try
            {
                var result = command.ExecuteScalar();
            }
            finally
            {
                dbConnectionProvider.CloseConnection(command);
            }
        }
    }
}
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using DMS.Common.Messages;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.MES.High;
using Oleg_ivo.MES.Low;
using Oleg_ivo.MES.Registered;
using Oleg_ivo.Tools.ConnectionProvider;

namespace Oleg_ivo.MES
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MesForm : Form//TODO: to WPF
    {
        private readonly DbConnectionProvider connectionProvider;
        private readonly HighLevelMessageExchangeSystem highLevelMessageExchangeSystem;
        private readonly LowLevelMessageExchangeSystem lowLevelMessageExchangeSystem;

        /// <summary>
        /// 
        /// </summary>
        protected MesForm()
        {
            InitializeComponent();

        }

        public MesForm(DbConnectionProvider connectionProvider, HighLevelMessageExchangeSystem highLevelMessageExchangeSystem, LowLevelMessageExchangeSystem lowLevelMessageExchangeSystem)
            : this()
        {
            this.connectionProvider = Enforce.ArgumentNotNull(connectionProvider, "connectionProvider");
            this.highLevelMessageExchangeSystem = Enforce.ArgumentNotNull(highLevelMessageExchangeSystem,
                "highLevelMessageExchangeSystem");
            this.lowLevelMessageExchangeSystem = Enforce.ArgumentNotNull(lowLevelMessageExchangeSystem,
                "lowLevelMessageExchangeSystem");

            /*
            highLevelMessageExchangeSystem.BeforeUpdate += High_BeforeUpdate;
            lowLevelMessageExchangeSystem.BeforeUpdate += High_BeforeUpdate;
            */

            this.highLevelMessageExchangeSystem.ClientRegistered += High_ClientRegistered;
            this.lowLevelMessageExchangeSystem.ClientRegistered += Low_ClientRegistered;

            this.highLevelMessageExchangeSystem.ClientUnregistered += High_ClientUnregistered;
            this.lowLevelMessageExchangeSystem.ClientUnregistered += Low_ClientUnregistered;

            this.highLevelMessageExchangeSystem.MessageReceived += Instance_MessageReceived;
            this.lowLevelMessageExchangeSystem.MessageReceived += Instance_MessageReceived;

            this.highLevelMessageExchangeSystem.ErrorReceived += Instance_ErrorReceived;
            this.lowLevelMessageExchangeSystem.ErrorReceived += Instance_ErrorReceived;

            this.lowLevelMessageExchangeSystem.ChannelRegistered += Low_ChannelRegistered;
            this.lowLevelMessageExchangeSystem.ChannelUnregistered += Low_ChannelUnregistered;

            this.highLevelMessageExchangeSystem.ChannelSubscribed += High_ChannelSubscribed;
            this.highLevelMessageExchangeSystem.ChannelUnSubscribed += High_UnSubscribed;
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
                    var client = new { Name = registrationMessage.RegNameFrom, Value = e.RegisteredLowLevelClient };
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
                    var client = new { Name = registrationMessage.RegNameFrom, Value = e.RegisteredHighLevelClient };
                    AddItem(lbRegisteredHigh, client);
                    Protocol(String.Format("{0}\tКлиент верхнего уровня [{1}] зарегистрировался на сервере{2}",
                                           DateTime.Now, registrationMessage.RegNameFrom, Environment.NewLine));
                }
            }
        }

        private void Low_ChannelRegistered(object sender, LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            if (e.RegisteredLowLevelClient != null && e.Message != null)
            {
                string s = String.Format("{0}\tКлиент нижнего уровня [{1}] зарегистрировал на сервере канал [{2}]{3}",
                                              DateTime.Now,
                                              e.Message.RegNameFrom,
                                              e.Message.LogicalChannelId,
                                              Environment.NewLine);
                Protocol(s);
            }
        }

        private void Low_ChannelUnregistered(object sender, LowRegisteredLogicalChannelSubscribeEventArgs e)
        {
            if (e.RegisteredLowLevelClient != null && e.Message != null)
            {
                string s = String.Format("{0}\tКлиент нижнего уровня [{1}] отменил регистрацию на сервере канала [{2}]{3}",
                                              DateTime.Now,
                                              e.Message.RegNameFrom,
                                              e.Message.LogicalChannelId,
                                              Environment.NewLine);
                Protocol(s);
            }
        }

        private void High_ChannelSubscribed(object sender, HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            if (e.RegisteredHighLevelClient != null && e.Message != null)
            {
                string s = String.Format("{0}\tКлиент верхнего уровня [{1}] подписался канал [{2}]{3}",
                                              DateTime.Now,
                                              e.Message.RegNameFrom,
                                              e.Message.LogicalChannelId,
                                              Environment.NewLine);
                Protocol(s);
            }
        }

        private void High_UnSubscribed(object sender, HighRegisteredLogicalChannelSubscribeEventArgs e)
        {
            if (e.RegisteredHighLevelClient != null && e.Message != null)
            {
                string s = String.Format("{0}\tКлиент верхнего уровня [{1}] отписался от канала [{2}]{3}",
                                              DateTime.Now,
                                              e.Message.RegNameFrom,
                                              e.Message.LogicalChannelId,
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

        private void btnSendMessageHigh_Click(object sender, EventArgs e)
        {
            foreach (var selectedItem in lbRegisteredHigh.SelectedItems)
            {
                RegisteredHighLevelClient registeredHighLevelClient =
                    GetRegisteredClient(selectedItem, lbRegisteredHigh.ValueMember) as RegisteredHighLevelClient;
                string clientRegName =
                    GetClientRegName(selectedItem, lbRegisteredLow.DisplayMember);
                if (registeredHighLevelClient != null)
                    registeredHighLevelClient.SendMessageToClient(new InternalMessage(highLevelMessageExchangeSystem.RegName, clientRegName));
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
                    registeredHighLevelClient.SendMessageToClient(new InternalMessage(lowLevelMessageExchangeSystem.RegName, clientRegName));
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
            TestConnection();
        }


        private void TestConnection()
        {
            var command = new SqlCommand("select 1");
            connectionProvider.OpenConnection(command);
            try
            {
                var result = command.ExecuteScalar();
            }
            finally
            {
                connectionProvider.CloseConnection(command);
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                /*
                highLevelMessageExchangeSystem.BeforeUpdate -= High_BeforeUpdate;
                lowLevelMessageExchangeSystem.BeforeUpdate -= High_BeforeUpdate;
                */

                highLevelMessageExchangeSystem.ClientRegistered -= High_ClientRegistered;
                lowLevelMessageExchangeSystem.ClientRegistered -= Low_ClientRegistered;

                highLevelMessageExchangeSystem.ClientUnregistered -= High_ClientUnregistered;
                lowLevelMessageExchangeSystem.ClientUnregistered -= Low_ClientUnregistered;

                highLevelMessageExchangeSystem.MessageReceived -= Instance_MessageReceived;
                lowLevelMessageExchangeSystem.MessageReceived -= Instance_MessageReceived;

                lowLevelMessageExchangeSystem.ChannelRegistered -= Low_ChannelRegistered;
                lowLevelMessageExchangeSystem.ChannelUnregistered -= Low_ChannelUnregistered;

                highLevelMessageExchangeSystem.ChannelSubscribed -= High_ChannelSubscribed;
                highLevelMessageExchangeSystem.ChannelUnSubscribed -= High_UnSubscribed;

                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
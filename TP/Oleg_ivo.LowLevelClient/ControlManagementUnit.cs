#if BINDING_TCP
using System.ServiceModel;
using Oleg_ivo.CMU.ServiceReferenceHomeTcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DMS.Common.Messages;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Tools.UI;
#else
using Oleg_ivo.CMU.ServiceReferenceHome;
#endif

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// ������ �������� � ����������
    ///</summary>
    public class ControlManagementUnit
    {
        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="ControlManagementUnit" />.
        /// ��� ����, ����� ��������� ������������ �������, ������� ������ ������� <see cref="GetDistributedMeasurementInformationSystem"/> 
        /// � ������� ������ <see cref="BuildSystemConfiguration"/>
        /// </summary>
        public ControlManagementUnit()
        {
            CallbackHandler.NeedProtocol += CallbackHandler_NeedProtocol;
            CallbackHandler.ChannelSubscribed += CallbackHandler_ChannelSubscribed;
            CallbackHandler.ChannelUnSubscribed += CallbackHandler_ChannelUnSubscribed;
            
            Planner = Planner.Instance;
            Planner.NewDadaReceived += Instance_NewDadaReceived;
        }

        /// <summary>
        /// 
        /// </summary>
        public void BuildSystemConfiguration()
        {
            if (DistributedMeasurementInformationSystem != null)
                DistributedMeasurementInformationSystem.BuildSystemConfiguration();
        }

        /// <summary>
        /// ����������� ������ �������
        /// </summary>
        private Planner Planner { get; set; }

        ///<summary>
        /// ������������� �������������-������������� ������� (�����)
        ///</summary>
        private DistributedMeasurementInformationSystemBase DistributedMeasurementInformationSystem
        {
            get
            {
                if (GetDistributedMeasurementInformationSystem == null)
                    throw new NullReferenceException(
                        "�� ������ ������� ��� ��������� ������ ������������� �������������-������������� �������");
                return GetDistributedMeasurementInformationSystem();
            }
        }

        /// <summary>
        /// ������� ��� ��������� ������ ������������� �������������-������������� �������
        /// </summary>
        public GetDistributedMeasurementInformationSystemDelegate GetDistributedMeasurementInformationSystem { private get; set; }

        /// <summary>
        /// ������� ��� ��������� ������ ������������� �������������-������������� �������
        /// </summary>
        /// <returns></returns>
        public delegate DistributedMeasurementInformationSystemBase GetDistributedMeasurementInformationSystemDelegate();

        ///<summary>
        /// ���������� ������
        ///</summary>
        protected virtual IEnumerable<LogicalChannel> LogicalChannels
        {
            get
            {
                return DistributedMeasurementInformationSystem.PlcManagerBase.LogicalChannels;
            }
        }



        void Instance_NewDadaReceived(object sender, NewDataReceivedEventArgs e)
        {
            InternalLogicalChannelDataMessage message = new InternalLogicalChannelDataMessage
            {
                DataMode = DataMode.Read,
                LogicalChannelId = e.LogicalChannel.Id,
                RegName = RegName,
                Value = e.Value
            };

            //���������� ������� ������ ����������� ����������� �� ����� ������:
            ReadChannel(message);
            //                var s = string.Format("{0} �������� � ����� ������ [{1}]", measurementPoll.LogicalChannel, e.SignalTime);
            //                Protocol(s);
            //System.Windows.Forms.MessageBox.Show();
        }

        private LowLevelMessageExchangeSystemClient LowLevelMessageExchangeSystemClient
        {
            get { return Proxy; }
        }

        static InstanceContext site;
        static LowLevelMessageExchangeSystemClient proxy;

        /// <summary>
        /// 
        /// </summary>
        public static LowLevelMessageExchangeSystemClient Proxy
        {
            get
            {
                if (proxy == null)
                {
                    CallbackHandler callbackHandler = new CallbackHandler();
                    site = new InstanceContext(callbackHandler);
                    proxy = new LowLevelMessageExchangeSystemClient(site);
                }
                return proxy;
            }
        }

        private string RegName
        {
            get
            {
                if (GetRegName == null) 
                    throw new NullReferenceException("�� ����� ������� GetRegName");
                
                return GetRegName();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GetRegNameDelegate GetRegName { private get; set; }

        void CallbackHandler_ChannelSubscribed(object sender, ChannelSubscribeEventArgs e)
        {
            LogicalChannel channel =
                LogicalChannels.AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));


            Protocol(string.Format("{0} ��� �������� �� ��������� ����� ������", channel));
            //��������� ������ ������ ��� ������������������� ������ ����� �������� �� ����
            Planner.StartPoll(channel);
        }

        void CallbackHandler_ChannelUnSubscribed(object sender, ChannelSubscribeEventArgs e)
        {
            LogicalChannel channel =
                LogicalChannels.AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(e.Message.LogicalChannelId));

            Protocol(string.Format("{0} ��� ������� �� ��������� ����� ������", channel));

            Planner.StopPoll(channel);
        }

        void CallbackHandler_NeedProtocol(object sender, EventArgs e)
        {
            if (sender is double || sender is string)
                Protocol(sender);
        }

        /// <summary>
        /// ��������������� ������
        /// (������������ ������� NeedProtocol)
        /// </summary>
        /// <param name="sender"></param>
        protected void Protocol(object sender)
        {
            if (NeedProtocol != null)
                NeedProtocol(sender, EventArgs.Empty);
        }

        /// <summary>
        /// ��������� ����������������
        /// </summary>
        public event EventHandler<EventArgs> NeedProtocol;

        /// <summary>
        /// ������� � ������� ������ ����������� ���������
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(InternalMessage message)
        {
            LowLevelMessageExchangeSystemClient.SendMessageAsync(message);
        }

        /// <summary>
        /// ������� � ������� ������ ����������� ��������� � ����� ������
        /// </summary>
        /// <param name="message"></param>
        public void ReadChannel(InternalLogicalChannelDataMessage message)
        {
            LowLevelMessageExchangeSystemClient.ReadChannelAsync(message);
        }

        /// <summary>
        /// ������� � ������� ������ ����������� ��������� � �����������
        /// </summary>
        public void Register()
        {
            LowLevelMessageExchangeSystemClient.RegisterAsync(new RegistrationMessage { RegName = RegName, Mode = true });
        }

        /// <summary>
        /// ������� � ������� ������ ����������� ��������� �� ������ �����������
        /// </summary>
        public void Unregister()
        {
            LowLevelMessageExchangeSystemClient.UnregisterAsync(new RegistrationMessage { RegName = GetRegName(), Mode = false });
        }

        /// <summary>
        /// ������ ����������� ���������
        /// </summary>
        public event EventHandler<AsyncCompletedEventArgs> UnregisterCompleted
        {
            add { LowLevelMessageExchangeSystemClient.UnregisterCompleted += value; }
            remove { LowLevelMessageExchangeSystemClient.UnregisterCompleted -= value; }
        }

        /// <summary>
        /// �������� ��������� ���������� ������
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> GetLogicalChannels()
        {
            //��������� ������ ��������������������� ������ (Id > 0):
            return 
                LogicalChannels
                    .Select(channel => channel.Id > 0 ? channel.Id : 0)
                        .Where(i => i > 0);

        }


        /// <summary>
        /// �������� ����� ������
        /// </summary>
        /// <param name="e"></param>
        /// <param name="synchronizingObject"></param>
        /// <exception cref="Exception"></exception>
        public void TryAddPoll(MovingEventArgs e, ISynchronizeInvoke synchronizingObject)
        {
            LogicalChannel channel =
                LogicalChannels.AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate((int)e.MovingObject));

            if (channel == null)
                throw new Exception("����� �� ������");

            double interval;
            if (channel.PollPeriod == TimeSpan.Zero)
                channel.PollPeriod = TimeSpan.FromMilliseconds(1000);

            string s;

            s = channel.PollPeriod.TotalMilliseconds.ToString();

            /*
                        s = InputBox.Show("������� �������� ������ ������ (� �������������)",
                                          string.Format("�������� ������ ������ [{0}]", channel),
                                          "1000");
            */


            if (double.TryParse(s, out interval))
            {
                Planner.AddPoll(channel, interval, synchronizingObject);
            }
            else
            {
                MessageBox.Show("��������� �������� �� �������� ����������", "������ �������� ��������� ������ ������",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        public void TryRemovePoll(MovingEventArgs e)
        {
            LogicalChannel channel =
                LogicalChannels.AsEnumerable()
                        .FirstOrDefault(LogicalChannel.GetFindChannelPredicate((int)e.MovingObject));

            if (channel == null)
                throw new Exception("����� �� ������");

            Planner.RemovePoll(channel);
        }

        /// <summary>
        /// ���������������� ����� � ������� ������ �����������
        /// </summary>
        /// <param name="subscribeMessage"></param>
        public void RegisterChannel(ChannelSubscribeMessage subscribeMessage)
        {
            LowLevelMessageExchangeSystemClient.ChannelRegisterAsync(subscribeMessage);
            LogicalChannel channel =
                LogicalChannels.AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(subscribeMessage.LogicalChannelId));

            Protocol(string.Format("{0} ��������������� � ������� ������ �����������", channel));
        }

        /// <summary>
        /// �������� ����������� ������ � ������� ������ �����������
        /// </summary>
        /// <param name="subscribeMessage"></param>
        public void UnregisterChannel(ChannelSubscribeMessage subscribeMessage)
        {
            LowLevelMessageExchangeSystemClient.ChannelUnRegisterAsync(subscribeMessage);
            LogicalChannel channel =
                LogicalChannels.AsEnumerable().FirstOrDefault(
                    LogicalChannel.GetFindChannelPredicate(subscribeMessage.LogicalChannelId));

            Protocol(string.Format("{0} ������ ����������� � ������� ������ �����������", channel));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public delegate string GetRegNameDelegate();
}
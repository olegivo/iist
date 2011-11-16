using System;
using DMS.Common.Messages;
using Oleg_ivo.MES.Logging;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// ������������������ ���������� �����
    /// </summary>
    public class RegisteredLogicalChannel
    {
        /// <summary>
        /// ������������� ������
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// ������������������ ���������� �����
        /// </summary>
        /// <param name="id">������������� ������</param>
        /// <param name="dataMode">����� ������ ������</param>
        public RegisteredLogicalChannel(int id, DataMode dataMode)
        {
            Id = id;
            DataMode = dataMode;
            Read += RegisteredLogicalChannel_Read;
            Write += RegisteredLogicalChannel_Write;
        }

        void RegisteredLogicalChannel_Write(object sender, InternalLogicalChannelDataMessageEventArgs e)
        {
            //��������������� ��������� �� ������ ������
            InternalMessageLogger.Instance.ProtocolMessage(e.Message);
        }

        private void RegisteredLogicalChannel_Read(object sender, InternalLogicalChannelDataMessageEventArgs e)
        {
            //��������������� ��������� �� ������ ������
            InternalMessageLogger.Instance.ProtocolMessage(e.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataMode">���� <see cref="DataMode.Unknown"/>, �������� �� ����������� ��� ����������</param>
        /// <returns></returns>
        public static Func<RegisteredLogicalChannel, bool> GetFindChannelPredicate(int id, DataMode dataMode)
        {
            //TODO:��������� ����� ������ ������ ��� �������� �� ����, ��� ������ � ��� ������ (�������� �������� - ����� ������)
            return
                (channel =>
                 channel.Id == id && (dataMode == DataMode.Unknown || (channel.DataMode & dataMode) == dataMode));
        }

        /// <summary>
        /// ����� ������ ������
        /// </summary>
        public DataMode DataMode { get; set; }

        /// <summary>
        /// ������ ������
        /// </summary>
        public event EventHandler<InternalLogicalChannelDataMessageEventArgs> Read;
        
        /// <summary>
        /// ������ ������
        /// </summary>
        public event EventHandler<InternalLogicalChannelDataMessageEventArgs> Write;

        private void InvokeRead(InternalLogicalChannelDataMessageEventArgs e)
        {
            EventHandler<InternalLogicalChannelDataMessageEventArgs> handler = Read;
            if (handler != null) handler(this, e);
        }

        private void InvokeWrite(InternalLogicalChannelDataMessageEventArgs e)
        {
            EventHandler<InternalLogicalChannelDataMessageEventArgs> handler = Write;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// ������� ���������� ������ ��������� � ����� ������
        /// </summary>
        /// <param name="message"></param>
        public void InvokeRead(InternalLogicalChannelDataMessage message)
        {
            InvokeRead(new InternalLogicalChannelDataMessageEventArgs(message));
        }

        /// <summary>
        /// ������� ���������� ������ ��������� � ����� ������
        /// </summary>
        /// <param name="message"></param>
        public void InvokeWrite(InternalLogicalChannelDataMessage message)
        {
            InvokeWrite(new InternalLogicalChannelDataMessageEventArgs(message));
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ChannelSubscribeMessageEventArgs> Subscribed;

        private int _subscribedCount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void InvokeSubscribed(ChannelSubscribeMessage message)
        {
            //���� ��� ������ ��������, ����� �������� ���������� (�������� ������),
            //��� ���-�� ���������� � ����� ������������ ����� ������
            if (_subscribedCount == 0)
                InvokeSubscribed(new ChannelSubscribeMessageEventArgs(message)); 
            
            _subscribedCount++;
        }

        private void InvokeSubscribed(ChannelSubscribeMessageEventArgs e)
        {
            EventHandler<ChannelSubscribeMessageEventArgs> handler = Subscribed;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ChannelSubscribeMessageEventArgs> UnSubscribed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void InvokeUnSubscribed(ChannelSubscribeMessage message)
        {
            _subscribedCount--;

            //���� ��� ���� ��������� �������, ����� �������� ���������� (�������� ������),
            //��� ������ ����������� ��� � ����� �������������� ����� ������
            if (_subscribedCount == 0)
                InvokeUnSubscribed(new ChannelSubscribeMessageEventArgs(message));
        }

        private void InvokeUnSubscribed(ChannelSubscribeMessageEventArgs e)
        {
            EventHandler<ChannelSubscribeMessageEventArgs> handler = UnSubscribed;
            if (handler != null) handler(this, e);
        }

        
    }
}
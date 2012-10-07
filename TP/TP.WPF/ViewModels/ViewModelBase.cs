using System;
using DMS.Common.Messages;
using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels
{
    public abstract class ViewModelBase : ViewModel
    {
        /// <summary>
        /// ����� ����������� ������
        /// </summary>
        /// <param name="message"></param>
        public virtual void OnChannelRegistered(ChannelRegistrationMessage message)
        {
            //TODO:��������� ������ View �� ��� �������, ����� ����������� ����������� ����������
            if (NeedInitIndicator != null)
                NeedInitIndicator(this, new IndicatorInitEventArgs(message));
        }

        /// <summary>
        /// ����� ������ ����������� ������
        /// </summary>
        /// <param name="channelId"></param>
        public virtual void OnChannelUnRegistered(int channelId)
        {
            
        }

        /// <summary>
        /// ����� ��������� ���������� ������
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="isActive"></param>
        public virtual void OnChannelIsActiveChanged(int channelId, bool isActive)
        {
            
        }

        /// <summary>
        /// ����� ������ ������
        /// </summary>
        /// <param name="message"></param>
        public virtual void OnReadChannel(InternalLogicalChannelDataMessage message)
        {
        }

        /// <summary>
        /// ����� ������ ����������� ������
        /// </summary>
        /// <param name="message"></param>
        public virtual void OnChannelUnRegistered(ChannelRegistrationMessage message)
        {
            
        }

        protected void RaiseSendMessage(int channelId, object value)
        {
            if (SendControlMessage != null)
                SendControlMessage(this, new SendControlMessageEventArgs(channelId, value));
        }

        /// <summary>
        /// ������� ������������� ���������������� ���������
        /// </summary>
        public event EventHandler<IndicatorInitEventArgs> NeedInitIndicator;

        /// <summary>
        /// ������� ������������� ������� ����������� ���������
        /// </summary>
        public event EventHandler<SendControlMessageEventArgs> SendControlMessage;
    }
}
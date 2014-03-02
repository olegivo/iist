using System;
using DMS.Common.Messages;

namespace TP.WPF.ViewModels
{
    public abstract class ViewModelBase : GalaSoft.MvvmLight.ViewModelBase
    {
        private ObservableDictionary<int, IndicatorViewModel> indicatorViewModels;
        public ObservableDictionary<int, IndicatorViewModel> IndicatorViewModels
        {
            get { return indicatorViewModels; }
            set
            {
                indicatorViewModels = value;
                RaisePropertyChanged("IndicatorViewModels");
                if (indicatorViewModels != null)
                    indicatorViewModels.CollectionChanged += (sender, e) => RaisePropertyChanged("IndicatorViewModels");
            }
        }

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
            var channelId = message.LogicalChannelId;
            if(IndicatorViewModels.ContainsKey(channelId))
            {
                var indicatorViewModel = IndicatorViewModels[channelId];
                indicatorViewModel.CurrentValue = message.Value as double?;
            }
        }

        /// <summary>
        /// ����� ������ ����������� ������
        /// </summary>
        /// <param name="message"></param>
        public virtual void OnChannelUnRegistered(ChannelRegistrationMessage message)
        {
            indicatorViewModels[message.LogicalChannelId].CurrentValue = null;

        }

        /// <summary>
        /// ����� ������ �����������.
        /// � ������ ��������� �������� �� ������ �����������, �� ������ ������������������� ������ ���.
        /// ���������� ��������� ������� ������, ����������� � ������������������ ��� ����������� �������.
        /// </summary>
        public virtual void OnUnregistered()
        {
            foreach (var indicatorViewModel in IndicatorViewModels)
            {
                indicatorViewModel.Value.CurrentValue = null;
            }
            
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
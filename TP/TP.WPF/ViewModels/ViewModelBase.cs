using System;
using DMS.Common.Messages;
using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels
{
    public abstract class ViewModelBase : ViewModel
    {
        private ObservableDictionary<int, IndicatorViewModel> indicatorViewModels;
        public ObservableDictionary<int, IndicatorViewModel> IndicatorViewModels
        {
            get { return indicatorViewModels; }
            set
            {
                indicatorViewModels = value;
                OnPropertyChanged("IndicatorViewModels");
                if (indicatorViewModels != null)
                    indicatorViewModels.CollectionChanged += (sender, e) => OnPropertyChanged("IndicatorViewModels");
            }
        }

        /// <summary>
        /// После регистрации канала
        /// </summary>
        /// <param name="message"></param>
        public virtual void OnChannelRegistered(ChannelRegistrationMessage message)
        {
            //TODO:подписать каждое View на это событие, чтобы динамически настраивать индикаторы
            if (NeedInitIndicator != null)
                NeedInitIndicator(this, new IndicatorInitEventArgs(message));
        }

        /// <summary>
        /// После отмены регистрации канала
        /// </summary>
        /// <param name="channelId"></param>
        public virtual void OnChannelUnRegistered(int channelId)
        {
            
        }

        /// <summary>
        /// После изменения активности канала
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="isActive"></param>
        public virtual void OnChannelIsActiveChanged(int channelId, bool isActive)
        {
            
        }

        /// <summary>
        /// После чтения канала
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
        /// После отмены регистрации канала
        /// </summary>
        /// <param name="message"></param>
        public virtual void OnChannelUnRegistered(ChannelRegistrationMessage message)
        {
            
        }

        /// <summary>
        /// После отмены регистрации.
        /// В данном состоянии подписка на каналы отсутствует, ни одного зарегистрированного канала нет.
        /// Необходимо почистить текущие данные, относящиеся к зарегисртированным или подписанным каналам.
        /// </summary>
        public virtual void OnUnregistered()
        {
            
        }

        protected void RaiseSendMessage(int channelId, object value)
        {
            if (SendControlMessage != null)
                SendControlMessage(this, new SendControlMessageEventArgs(channelId, value));
        }

        /// <summary>
        /// Событие необходимости инициализировать индикатор
        /// </summary>
        public event EventHandler<IndicatorInitEventArgs> NeedInitIndicator;

        /// <summary>
        /// Событие необходимости послать управляющее сообщение
        /// </summary>
        public event EventHandler<SendControlMessageEventArgs> SendControlMessage;
    }
}
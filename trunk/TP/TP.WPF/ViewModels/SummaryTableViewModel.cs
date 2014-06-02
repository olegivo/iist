using System;
using DMS.Common.Messages;

namespace TP.WPF.ViewModels
{

    public class SummaryTableViewModel : ViewModelBase
    {

        private readonly DataSetChannels summarySet = new DataSetChannels();

        public DataSetChannels SummarySet
        {
            get { return summarySet; }
        }

        public DataSetChannels.ChannelsDataTable SummaryTable
        {
            get { return SummarySet.Channels; }
        }

        private static double GetValue(double? d)
        {
            return d.HasValue ? d.Value : default(double);
        }

        private void AddChannel(ChannelRegistrationMessage message)
        {
            var row = SummaryTable.FindById(message.LogicalChannelId);
            if (row == null)
                SummaryTable.AddChannelsRow(message.LogicalChannelId,
                                            message.Description,
                                            default(double),
                                            false,
                                            GetValue(message.MinValue),
                                            GetValue(message.MaxValue),
                                            GetValue(message.MinNormalValue),
                                            GetValue(message.MaxNormalValue));
        }

        private void RemoveChannel(int channelId)
        {
            var row = SummaryTable.FindById(channelId);
            if (row != null)
                SummaryTable.RemoveChannelsRow(row);
        }

        private void SetActive(int channelId, bool isActive)
        {
            //throw new NotImplementedException();
            if (isActive)
            {
                SummaryTable.FindById(channelId).IsActive = true;
            }
            else
            {
                SummaryTable.FindById(channelId).IsActive = false;
            }
        }


        private void ActualizeChannelValue(int channelId, object chanelValue)
        {
            var row = SummaryTable.FindById(channelId);
            if (row != null)
                row.CurrentValue = chanelValue;

            RaisePropertyChanged("SummaryTable");
        }

        /// <summary>
        /// После регистрации канала
        /// </summary>
        /// <param name="message"></param>
        public override void OnChannelRegistered(ChannelRegistrationMessage message)
        {
            base.OnChannelRegistered(message);
            AddChannel(message);
        }

        public override void OnChannelIsActiveChanged(int channelId, bool isActive)
        {
            base.OnChannelIsActiveChanged(channelId, isActive);
            SetActive(channelId, isActive);
        }

        /// <summary>
        /// После чтения канала
        /// </summary>
        /// <param name="message"></param>
        public override void OnReadChannel(InternalLogicalChannelDataMessage message)
        {
            base.OnReadChannel(message);
            object chanelValue;
            chanelValue = message.Value is double ? Math.Round((double) message.Value, 2) : message.Value;
            ActualizeChannelValue(message.LogicalChannelId, chanelValue); //согласно требованию представления данных в ИИС
        }

        /// <summary>
        /// После отмены регистрации канала
        /// </summary>
        /// <param name="message"></param>
        public override void OnChannelUnRegistered(ChannelRegistrationMessage message)
        {
            base.OnChannelUnRegistered(message);
            RemoveChannel(message.LogicalChannelId);
        }

        /// <summary>
        /// После отмены регистрации.
        /// В данном состоянии подписка на каналы отсутствует, ни одного зарегистрированного канала нет.
        /// Необходимо почистить текущие данные, относящиеся к зарегисртированным или подписанным каналам.
        /// </summary>
        public override void OnUnregistered()
        {
            base.OnUnregistered();
            SummarySet.Clear();
        }
    }

}

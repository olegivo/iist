using DMS.Common.Messages;

namespace TP.WPF.ViewModels
{
    /*
     * Обеспечить следующую функциональность (каждый пункт оформить методом или 
     * парой методов в модели представления, и вызывать их из главной модели представления):

-при обнаружении зарегистрированных каналов добавлять записи в таблицу параметров
-при обнаружении отсутствия зарегистрированных каналов удалять записи из таблицы
-при подписке/отписки канала на подучение новых данных отображать это состояние в таблице (флаг "Активен" или "Подписка")
-при получении новых данных от канала отображать текущее (актуальное) значение параметра в таблице параметров
     */

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

        public void RemoveChannel(int channelId)
        {
            var row = SummaryTable.FindById(channelId);
            if (row == null)
                SummaryTable.FindById(channelId).Delete();
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

        private void ActualizeChannelValue(int channelId, double chanelValue)
        {
            var row = SummaryTable.FindById(channelId);
            if (row != null)
                row.CurrentValue = chanelValue;

            OnPropertyChanged("SummaryTable");
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
            ActualizeChannelValue(message.LogicalChannelId, (double) message.Value);
        }
    }

}

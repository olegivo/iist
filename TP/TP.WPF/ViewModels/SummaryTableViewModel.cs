using DMS.Common.Messages;
using JulMar.Windows.Mvvm;

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

    public class SummaryTableViewModel : ViewModel
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

        public void AddChannel(ChannelRegistrationMessage message)
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

        public void SetActive(int channelId, bool isActive)
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

        public void ActualizeChannelValue(int channelId, float chanelValue)
        {
            var row = SummaryTable.FindById(channelId);
            if (row != null)
                row.CurrentValue = chanelValue;

            OnPropertyChanged("SummaryTable");
        }
        /*
                public void CreateDataTable()
                {
                    DataTable _summaryTable = new DataTable("SummaryTable");
                    UniqueConstraint custUnique = new UniqueConstraint(new DataColumn[] { _summaryTable.Columns["ChanelId"] });
            
                    summarySet.Tables["SummaryTable"].Constraints.Add(custUnique);
                    _summaryTable.Columns.Add("ChanelValue");
                    _summaryTable.Columns.Add("ChanelName");

                    SetChannelsNames();
                }
        */


        //private void ucCommonParentChanged(object sender, EventArgs e)
        //{
        //    ActualizeChannelValue();
        //}
    }

}

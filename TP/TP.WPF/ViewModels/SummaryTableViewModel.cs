using System;
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

        public void AddChannel(int channelId, string description, float defaultValue, bool isActive, float minValue, float maxValue, float minNormalValue, float maxNormalValue)
        {
            var row = SummaryTable.FindById(channelId);
            if (row == null)
                SummaryTable.AddChannelsRow(channelId, description, defaultValue, isActive, minValue, maxValue, minNormalValue, maxNormalValue);
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
            if(isActive)
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
        public void SetChannelsNames()
        {
            AddChannel(1, "Канал №1 - (ТП1)", 0, true, 0, 0, 0, 0);
            AddChannel(2, "Канал №2 - (ТП2)", 0, true, 0, 0, 0, 0);
            AddChannel(3, "Канал №3 - (ТП3)", 0, true, 0, 0, 0, 0);
            AddChannel(4, "Канал №4 - (ТР4)", 0, true, 0, 0, 0, 0);
            AddChannel(5, "Канал №5 - (ТР5)", 0, true, 0, 0, 0, 0);
            AddChannel(6, "Канал №6 - (ТС6)", 0, true, 0, 0, 0, 0);
            AddChannel(7, "Канал №7 - (ТС7)", 0, true, 0, 0, 0, 0);
            AddChannel(8, "Канал №8 - (ТС8)", 0, true, 0, 0, 0, 0);
            AddChannel(9, "Канал №9 - (P)", 0, true, 0, 0, 0, 0);
            AddChannel(10, "Канал №10 - (PH1)", 0, true, 0, 0, 0, 0);
            AddChannel(11, "Канал №11 - (PH2)", 0, true, 0, 0, 0, 0);
            AddChannel(12, "Канал №12 - (S)", 0, true, 0, 0, 0, 0);
            AddChannel(13, "Канал №13 - (ДУ-9)", 0, true, 0, 0, 0, 0);
            AddChannel(14, "Канал №14 - (ДУ-11)", 0, true, 0, 0, 0, 0);
            AddChannel(15, "Канал №15 - (ДУ-1)", 0, true, 0, 0, 0, 0);
            AddChannel(16, "Канал №16 - (ДУ-4)", 0, true, 0, 0, 0, 0);
            AddChannel(17, "Канал №17 - (ДУ-10)", 0, true, 0, 0, 0, 0);
            AddChannel(18, "Канал №18 - (Г-O2)", 0, true, 0, 0, 0, 0);
            AddChannel(19, "Канал №19 - (Г-СО)", 0, true, 0, 0, 0, 0);
            AddChannel(20, "Канал №20 - (Г-O2)", 0, true, 0, 0, 0, 0);
            AddChannel(21, "Канал №21 - (Г-СО)", 0, true, 0, 0, 0, 0);
            AddChannel(22, "Канал №22 - (Г-SO2)", 0, true, 0, 0, 0, 0);
            AddChannel(23, "Канал №23 - (Г-NO)", 0, true, 0, 0, 0, 0);
            AddChannel(24, "Канал №24 - (Г-NO2)", 0, true, 0, 0, 0, 0);
        }



        //private void ucCommonParentChanged(object sender, EventArgs e)
        //{
        //    ActualizeChannelValue();
        //}


    }

}

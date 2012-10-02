using System;
using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels
{

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
            //TODO:if already exists?
            SummaryTable.AddChannelsRow(channelId, description, defaultValue, isActive, minValue, maxValue, minNormalValue, maxNormalValue);
        }

        public void RemoveChannel()
        {
            throw new NotImplementedException();
        }

        public void SetActive(int channelId, bool isActive)
        {
            throw new NotImplementedException();
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
        
        private void SetChannelsNames()
        {
            _summaryTable.Rows.Add(1, 0, "ТП1");
            _summaryTable.Rows.Add(2, 0, "ТП2");
            _summaryTable.Rows.Add(3, 0, "ТП3");
            _summaryTable.Rows.Add(4, 0, "ТР4");
            _summaryTable.Rows.Add(5, 0, "ТР5");
            _summaryTable.Rows.Add(6, 0, "ТС6");
            _summaryTable.Rows.Add(7, 0, "ТС7");
            _summaryTable.Rows.Add(8, 0, "ТС8");
            _summaryTable.Rows.Add(9, 0, "P");
            _summaryTable.Rows.Add(10, 0, "PH1");
            _summaryTable.Rows.Add(11, 0, "PH2");
            _summaryTable.Rows.Add(12, 0, "S");
            _summaryTable.Rows.Add(13, 0, "ДУ-9");
            _summaryTable.Rows.Add(14, 0, "ДУ-11");
            _summaryTable.Rows.Add(15, 0, "ДУ-1");
            _summaryTable.Rows.Add(16, 0, "ДУ-4");
            _summaryTable.Rows.Add(17, 0, "ДУ-10");
            _summaryTable.Rows.Add(18, 0, "Г-O2");
            _summaryTable.Rows.Add(19, 0, "Г-СО");
            _summaryTable.Rows.Add(20, 0, "Г-O2");
            _summaryTable.Rows.Add(21, 0, "Г-СО");
            _summaryTable.Rows.Add(22, 0, "Г-SO2");
            _summaryTable.Rows.Add(23, 0, "Г-NO");
            _summaryTable.Rows.Add(24, 0, "Г-NO2");
        }
*/


        //private void ucCommonParentChanged(object sender, EventArgs e)
        //{
        //    ActualizeChannelValue();
        //}


    }

}

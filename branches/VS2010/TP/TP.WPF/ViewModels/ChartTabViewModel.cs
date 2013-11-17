using System;
using System.Collections.ObjectModel;
using System.Windows;
using AmCharts.Windows.QuickCharts;
using DMS.Common.Messages;

namespace TP.WPF.ViewModels
{

    public class ChartTabViewModel : ViewModelBase
    {
        //public DiscreetClearObservableCollection<SerialGraph> ChannelCharts;
        public ChartTabViewModel() 
        {
            //ChannelCharts = new DiscreetClearObservableCollection<SerialGraph>();
            
        }


        public class ChartDataItem
        {
            public int ChannelId { get; set; }
            public double ChannelValue { get; set; }
            public string ChannelTime { get; set; }
            public DiscreetClearObservableCollection<SerialGraph> ChannelCharts { get; set; }
        }

        public ObservableCollection<ChartDataItem> ChartBindingData { get { return _data; } }
        private ObservableCollection<ChartDataItem> _data = new ObservableCollection<ChartDataItem>();

        
        
        
        /// <summary>
        /// После чтения канала
        /// </summary>
        /// <param name="message"></param>
        public override void OnReadChannel(InternalLogicalChannelDataMessage message)
        {
            //base.OnReadChannel(message);
            var newChartDataItem = new ChartDataItem
                {
                    ChannelId = message.LogicalChannelId,
                    ChannelTime = message.TimeStamp.ToString("mm:ss"),
                    ChannelValue = (double) message.Value
                };
            if (!_data.Contains(new ChartDataItem()))
            {
                _data.Add(newChartDataItem);
            }
            if (_data.Count>100)
            {
                _data.RemoveAt(0);
            }
            

            //ActualizeChannelValue(message.LogicalChannelId, Math.Round((double)message.Value,2)); //согласно требованию представления данных в ИИС
        }

        public override void OnChannelRegistered(ChannelRegistrationMessage message)
        {
            //base.OnChannelRegistered(message);
            //ChannelCharts.Add(new LineGraph()
            //    {
            //        Title = String.Format("Канал №{0}",message.LogicalChannelId)
            //    });
        }

    }

}

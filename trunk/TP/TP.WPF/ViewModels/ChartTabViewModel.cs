using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using AmCharts.Windows.QuickCharts;
using DMS.Common.Messages;

namespace TP.WPF.ViewModels
{

    public class ChartTabViewModel : ViewModelBase
    {

        public ObservableCollection<ChartDataItem> ChartBindingData { get { return _data; } }
        private ObservableCollection<ChartDataItem> _data = new ObservableCollection<ChartDataItem>()
            {
           //TEST_CHART_DATA
           // new ChartDataItem()
           //     {
           //         ChannelId = 1,
           //         ChannelTime = "2",
           //         ChannelValue = 0.1
           //     },
           // new ChartDataItem()
           //     {
           //         ChannelId = 1,
           //         ChannelTime = "3",
           //         ChannelValue = 0.2
                    
           //     },
           //new ChartDataItem()
           //     {
           //         ChannelId = 1,
           //         ChannelTime = "4",
           //         ChannelValue = 0.15
                    
           //     },
            };

        public DiscreetClearObservableCollection<SerialGraph> _chartCollection = new DiscreetClearObservableCollection
            <SerialGraph>()
            {
                //TEST_CHART
                //new LineGraph()
                //    {
                //        Title = "TestChart",
                //        ValueMemberPath = "ChannelValue",
                //        ChannelId = "Channel1"

                //    }


            };
        public DiscreetClearObservableCollection<SerialGraph> ChartCollection
        {
            set { _chartCollection = value; }
            get
            {
                return _chartCollection;
            }
        }
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
                    ChannelValue = (double)message.Value
                };
            if (!ChartBindingData.Contains(new ChartDataItem()))
            {
                ChartBindingData.Add(newChartDataItem);
            }
            if (ChartBindingData.Count > 100)
            {
                ChartBindingData.RemoveAt(0);
            }

        }

        public override void OnChannelRegistered(ChannelRegistrationMessage message)
        {
            ChartCollection.Add(new LineGraph()
                {
                    Title = "Канал №" + message.LogicalChannelId,
                    ValueMemberPath = "ChannelValue",
                    ChannelId = "Channel" + message.LogicalChannelId.ToString()
                }
                );
        }
        public override void OnChannelUnRegistered(ChannelRegistrationMessage message)
        {
            var gi = SelectSerialGraphItemByChannelId(message.LogicalChannelId);
            if (gi != null)
            {
                //Удалить график из коллекции
                ChartCollection.Remove(gi);

                //Удалить все даные удаленого графика, а то завалится в методе SetPointLocations
                ClearUnregisteredChannelData(gi.ChannelId);


            }
        }

        private SerialGraph SelectSerialGraphItemByChannelId(int channelId)
        {
            foreach (var serialGraph in ChartCollection)
            {
                string gn = "Channel" + channelId;
                if (serialGraph.ChannelId == gn)
                    return serialGraph;

            }
            return null;
        }

        private void ClearUnregisteredChannelData(string channelId)
        {
            foreach (ChartDataItem chartDataItem in ChartBindingData)
            {
                if (chartDataItem.ChannelId == Int32.Parse(channelId.Replace("Channel","")))
                {
                    ChartBindingData.Remove(chartDataItem);
                }
            }

        }

    }

}

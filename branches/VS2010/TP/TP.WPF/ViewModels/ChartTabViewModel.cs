using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<ChartDataItem> ChartBindingData { get { return _data; } }
        private ObservableCollection<ChartDataItem> _data = new ObservableCollection<ChartDataItem>()
            {new ChartDataItem()
                {
                    ChannelId = 1,
                    ChannelTime = "2",
                    ChannelValue = 22
                },
            new ChartDataItem()
                {
                    ChannelId = 1,
                    ChannelTime = "3",
                    ChannelValue = 12
                    
                },
            new ChartDataItem()
                {
                    ChannelId = 1,
                    ChannelTime = "4",
                    ChannelValue = 15
                    
                },
           new ChartDataItem()
                {
                    ChannelId = 1,
                    ChannelTime = "5",
                    ChannelValue = 14
                    
                }
            };
        public class ChartDataItem
        {
            public int ChannelId { get; set; }
            public double ChannelValue { get; set; }
            public string ChannelTime { get; set; }
        }

        public DiscreetClearObservableCollection<SerialGraph> _chartCollection = new DiscreetClearObservableCollection
            <SerialGraph>()
            {
                new LineGraph()
                    {
                        Title = "TestChart",
                        ValueMemberPath = "ChannelValue"
                        
                        
                    }
            };
        public DiscreetClearObservableCollection<SerialGraph> ChartCollection {
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
            ChartCollection.Add(new LineGraph()
                {
                    Title = "Канал №" + message.LogicalChannelId,
                    ValueMemberPath = "val" + message.LogicalChannelId,
                    Name = "graph" + message.LogicalChannelId
                }
                );
        }
        public override void OnChannelUnRegistered(ChannelRegistrationMessage message)
        {
            var gi = SelectSerialGraphItemByChannelId(message.LogicalChannelId);
            if (gi!=null)
            {
                ChartCollection.Remove(gi);
            }
        }

        private SerialGraph SelectSerialGraphItemByChannelId(int channelId)
        {
            foreach (var serialGraph in ChartCollection)
            {
                string gn = "graph" + channelId;
                if (serialGraph.Name == gn)
                    return serialGraph;

            }
            return null;
        }

    }

}

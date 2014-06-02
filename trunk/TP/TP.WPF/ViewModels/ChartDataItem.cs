using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP.WPF.ViewModels
{
    public class ChartDataItem
    {
        public int ChannelId { get; set; }
        public object ChannelValue { get; set; }
        public string ChannelTime { get; set; }
    }
}

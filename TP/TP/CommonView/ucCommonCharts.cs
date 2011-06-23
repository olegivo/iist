using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UICommon;

namespace TP.CommonView
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucCommonCharts : XtraUserControl
    {
        private Dictionary<int, ucChart> _chartsMapping;
        private Dictionary<int, string> _channelsNameDic;

        /// <summary>
        /// 
        /// </summary>
        public ucCommonCharts()
        {
            InitializeComponent();
        }

        private IEnumerable<ucChart> GetCharts(Control сontrol)
        {
            List<ucChart> charts = new List<ucChart>();
            if (сontrol != null)
                foreach (Control childControl in сontrol.Controls)
                {
                    if (childControl.GetType() == typeof(ucChart))
                        charts.Add((ucChart)childControl);
                    else
                        charts.AddRange(GetCharts(childControl));
                }

            return charts;
        }

        private void MapCharts()
        {
            _chartsMapping = new Dictionary<int, ucChart>();

            try
            {
                IEnumerable<ucChart> charts = GetCharts(this);
                
                SetChannelsNames();

                foreach (var channelIdChart in
                    charts.SelectMany(chart => chart.ChannelsToDisplay.ToDictionary(item => item, item => chart)))
                {
                    int channelId = channelIdChart.Key;
                    _chartsMapping.Add(channelId, channelIdChart.Value);
                    if (!_channelsNameDic.ContainsKey(channelId))
                        throw new ArgumentOutOfRangeException("channelId", 
                                                              channelId,
                                                              "Словарь названий каналов не содержит такой номер канала");
                    channelIdChart.Value.SetChannelsName(channelId, _channelsNameDic[channelId]);
                }

                foreach (ucChart chart in charts)
                {
                    chart.InitializeChart();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Неправильно настроены каналы для графиков", ex);
            }
        }

        /// <summary>
        /// Добавить данные на график
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <param name="newValue"></param>
        public void AddChartData(int channelNumber, double newValue)
        {
            if (_chartsMapping.ContainsKey(channelNumber))
                _chartsMapping[channelNumber].AddChartData(channelNumber, newValue);
        }

        /// <summary>
        /// Задать соответствие номеров и названий каналов
        /// </summary>
        private void SetChannelsNames()
        {
            _channelsNameDic = new Dictionary<int, string>();
            _channelsNameDic.Add(1, "ТП1");
            _channelsNameDic.Add(2, "ТП2");
            _channelsNameDic.Add(3, "ТП3");
            _channelsNameDic.Add(4, "ТР4");
            _channelsNameDic.Add(5, "ТР5");
            _channelsNameDic.Add(6, "ТС6");
            _channelsNameDic.Add(7, "ТС7");
            _channelsNameDic.Add(8, "ТС8");
            _channelsNameDic.Add(9, "P");
            _channelsNameDic.Add(10, "PH1");
            _channelsNameDic.Add(11, "PH2");
            _channelsNameDic.Add(12, "S");
            _channelsNameDic.Add(13, "ДУ-9");
            _channelsNameDic.Add(14, "ДУ-11");
            _channelsNameDic.Add(15, "ДУ-1");
            _channelsNameDic.Add(16, "ДУ-4");
            _channelsNameDic.Add(17, "ДУ-10");
            _channelsNameDic.Add(18, "Г-O2");
            _channelsNameDic.Add(19, "Г-СО");
            _channelsNameDic.Add(20, "Г-O2");
            _channelsNameDic.Add(21, "Г-СО");
            _channelsNameDic.Add(22, "Г-SO2");
            _channelsNameDic.Add(23, "Г-NO");
            _channelsNameDic.Add(24, "Г-NO2");
        }


        private void ucCommonParentChanged(object sender, EventArgs e)
        {
            MapCharts();
        }
    }
}

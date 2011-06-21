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
                foreach (var keyValuePair in
                    GetCharts(this).SelectMany(chart => chart.ChannelsToDisplay.ToDictionary(item => item, item => chart)))
                {
                    _chartsMapping.Add(keyValuePair.Key, keyValuePair.Value);
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

        private void ucCommonParentChanged(object sender, EventArgs e)
        {
            MapCharts();
        }
    }
}

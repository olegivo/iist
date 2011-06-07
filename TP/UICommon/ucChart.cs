using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using DevExpress.XtraCharts;

namespace UICommon
{
    public partial class ucChart : DevExpress.XtraEditors.XtraUserControl
    {
        public ucChart()
        {
            InitializeComponent();


        }

        /// <summary>
        /// Список каналов, Отображаемых на графике
        /// </summary>

        List<int> _ChannelList = new List<int>();

        public List<int> ChannelsToDisplay
        {
            get { return _ChannelList; }
            set
            {
                if (_ChannelList != value)
                {
                    _ChannelList = value;
                }

            }
        }
        /// <summary>
        /// Заголовок графика
        /// </summary>
        private String _ChartTitle = "";
        public string ChartTitle
        {
            get { return _ChartTitle; }
            set { if (_ChartTitle != value) { _ChartTitle = value; } }
        }




        Series[] series = new Series[10]; //TODO: непонятно сколько памяти выделять под массив (ChannelsToDisplay.Count)
        int[] ChannelChartNumbers = new int[10];
        /// <summary>
        /// инициализация графика и его серий, параметров, итп.
        /// </summary>
        public void InitializeChart()
        {

            for (int i = 0; i < ChannelsToDisplay.Count; i++)
            {
                int channelNumber = ChannelsToDisplay[i];
                ChannelChartNumbers[i] = channelNumber;
                Series series1 = new Series("Channel #" + Convert.ToString(channelNumber), ViewType.Area);

                string tableName = GetTableName(channelNumber);
                dtsChart1.Tables.Add(new dtsChart.ChartDataDataTable {TableName = tableName});
                series1.DataSource = dtsChart1.Tables[tableName];
                series[i] = series1;
                series1.ArgumentDataMember = "TimeStamp";
                series1.ValueDataMembers.AddRange("Value");

                chartControl1.Series.Add(series[i]);

                series[i].Label.Visible = false;

            }
            if (ChartTitle != "")
            {
                ChartTitle chartTitle1 = new ChartTitle();
                chartTitle1.Antialiasing = true;
                chartTitle1.Font = new Font("Tahoma", 12, FontStyle.Bold);
                chartTitle1.Text = ChartTitle;
                chartControl1.Titles.Add(chartTitle1);

            }

            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;
            diagram.AxisX.Label.Angle = -45;
            diagram.AxisX.DateTimeOptions.Format = DateTimeFormat.ShortTime; //DateTimeFormat.MonthAndDay;
            //diagram.AxisX.DateTimeOptions.FormatString = "hh:mm:ss"; //DateTimeFormat.Custom;

            diagram.EnableAxisXScrolling = true;
            diagram.AxisX.Range.Auto = false;
            diagram.AxisX.Range.SetInternalMinMaxValues(0, 30);
            diagram.ScrollingOptions.UseScrollBars = true;
            


        }

        private string GetTableName(int i)
        {
            return string.Format("ChartData{0}", i);
        }


        public void AddDataChart(int ChannelNumber, double NewValue)
        {
            dtsChart.ChartDataDataTable dataTable = dtsChart1.Tables[GetTableName(ChannelNumber)] as dtsChart.ChartDataDataTable;
            if (dataTable != null)
            {
                DateTime now = DateTime.Now.AddSeconds(-3);//todo: настраиваемый временной буфер
                foreach (var dr in dataTable.Rows.Cast<dtsChart.ChartDataRow>().Where(dr => dr.TimeStamp < now).ToList())
                {
                    dataTable.RemoveChartDataRow(dr);
                }

                dataTable.AddChartDataRow(DateTime.Now, NewValue);
            }
        }
    }

}

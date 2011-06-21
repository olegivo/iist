using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private List<int> _channelList = new List<int>();
        private String _chartTitle = "";
        private int _chartDataStorageTime = 30;
        Series[] series = new Series[10]; //TODO: непонятно сколько памяти выделять под массив (ChannelsToDisplay.Count)

        /// <summary>
        /// Список каналов, отображаемых на графике
        /// </summary>
        public List<int> ChannelsToDisplay
        {
            get { return _channelList; }
            set { _channelList = value; }
        }
        

        /// <summary>
        /// Заголовок графика
        /// </summary>
        [DefaultValue("")]
        public string ChartTitle
        {
            get { return _chartTitle; }
            set
            {
                if (_chartTitle != value)
                {
                    _chartTitle = value;
                }
            }
        }

        /// <summary>
        /// Настраиваемый временной буфер [сек.]
        /// </summary>
        [DefaultValue(30)]
        public int ChartDataStorageTime
        {
            get { return _chartDataStorageTime; }
            set
            {
                if (_chartDataStorageTime != value)
                {
                    _chartDataStorageTime = value;
                }
            }
        }





        /// <summary>
        /// инициализация графика и его серий, параметров, итп.
        /// </summary>
        public void InitializeChart()
        {

            for (int i = 0; i < ChannelsToDisplay.Count; i++)
            {
                int channelNumber = ChannelsToDisplay[i];
                Series series1 = new Series("Channel #" + Convert.ToString(channelNumber), ViewType.Line);
                string tableName = GetTableName(channelNumber);
                dtsChart1.Tables.Add(new dtsChart.ChartDataDataTable { TableName = tableName });
                series1.DataSource = dtsChart1.Tables[tableName];
                series[i] = series1;
                series1.ArgumentDataMember = "TimeStamp";
                series1.ValueDataMembers.AddRange("Value");
            

                chartControl1.Series.Add(series[i]);

                series[i].Label.Visible = false;

            }
            if (ChartTitle != "")
            {
                ChartTitle chartTitle1 = new ChartTitle { 
                    Antialiasing = true, 
                    Font = new Font("Tahoma", 12, FontStyle.Bold), 
                    Text = ChartTitle };
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

        private static string GetTableName(int i)
        {
            return string.Format("ChartData{0}", i);
        }


        /// <summary>
        /// Добавить данные на график
        /// </summary>
        /// <param name="channelNumber"></param>
        /// <param name="newValue"></param>
        public void AddChartData(int channelNumber, double newValue)
        {
            dtsChart.ChartDataDataTable dataTable = dtsChart1.Tables[GetTableName(channelNumber)] as dtsChart.ChartDataDataTable;
            if (dataTable != null)
            {
                DateTime now = DateTime.Now.AddSeconds(-ChartDataStorageTime);//настраиваемый временной буфер
                foreach (var dr in dataTable.Rows.Cast<dtsChart.ChartDataRow>().Where(dr => dr.TimeStamp < now).ToList())
                {
                    dataTable.RemoveChartDataRow(dr);
                }

                dataTable.AddChartDataRow(DateTime.Now, newValue);//добавление ряда в таблицу датасета
                chartControl1.Refresh();
            }
        }

        private void ucChart_Load(object sender, EventArgs e)
        {
            InitializeChart();
        }

    }

}

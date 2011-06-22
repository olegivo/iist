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
            SetChannelsNames();


        }

        private List<int> _ChannelsToDisplay = new List<int>();
        private List<string> _ChannelsNameList = new List<string>();
        private String _chartTitle = "";
        private int _chartDataStorageTime = 30;
        Series[] series = new Series[10]; //TODO: непонятно сколько памяти выделять под массив (ChannelsToDisplay.Count)

        /// <summary>
        /// Список каналов, отображаемых на графике
        /// </summary>
        public List<int> ChannelsToDisplay
        {
            get { return _ChannelsToDisplay; }
            set { _ChannelsToDisplay = value; }
        }
        /// <summary>
        /// Названия каналов (верней, за какой параметр они отвечают)
        /// </summary>
        private void SetChannelsNames() {
            _ChannelsNameList.Add("ТП1");
            _ChannelsNameList.Add("ТП2");
            _ChannelsNameList.Add("ТП3");
            _ChannelsNameList.Add("ТР4");
            _ChannelsNameList.Add("ТР5");
            _ChannelsNameList.Add("ТС6");
            _ChannelsNameList.Add("ТС7");
            _ChannelsNameList.Add("ТС8");
            _ChannelsNameList.Add("P");
            _ChannelsNameList.Add("PH1");
            _ChannelsNameList.Add("PH2");
            _ChannelsNameList.Add("S");
            _ChannelsNameList.Add("ДУ-9");
            _ChannelsNameList.Add("ДУ-11");
            _ChannelsNameList.Add("ДУ-1");
            _ChannelsNameList.Add("ДУ-4");
            _ChannelsNameList.Add("ДУ-10");
            _ChannelsNameList.Add("Г-O2");
            _ChannelsNameList.Add("Г-СО");
            _ChannelsNameList.Add("Г-O2");
            _ChannelsNameList.Add("Г-СО");
            _ChannelsNameList.Add("Г-SO2");
            _ChannelsNameList.Add("Г-NO");
            _ChannelsNameList.Add("Г-NO2");
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
                Series series1 = new Series(_ChannelsNameList[ChannelsToDisplay[i]-1]+" #" + Convert.ToString(channelNumber), ViewType.Line);
                string tableName = GetTableName(channelNumber);
                dtsChart1.Tables.Add(new dtsChart.ChartDataDataTable { TableName = tableName });
                series1.DataSource = dtsChart1.Tables[tableName];
                series[i] = series1;
                series1.ArgumentDataMember = "TimeStamp";
                series1.ValueDataMembers.AddRange("Value");

                
                series1.ValueScaleType = ScaleType.Numerical;
                series1.ArgumentScaleType = ScaleType.DateTime;         //BUG:  ошибка при отображении
            

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
            //diagram.AxisX.Label.Angle = -45;
            //diagram.AxisX.DateTimeMeasureUnit = DateTimeMeasurementUnit.Second;
            diagram.AxisX.DateTimeOptions.Format = DateTimeFormat.ShortTime;
            //diagram.AxisX.DateTimeOptions.FormatString = "hh:mm:ss"; //DateTimeFormat.Custom;
            
            //настройка полосы прокрутки
            diagram.EnableAxisXScrolling = true;
            diagram.AxisX.Range.Auto = false;
            diagram.AxisX.Range.SetInternalMinMaxValues(0, 30);
            diagram.ScrollingOptions.UseScrollBars = true;
            


        }

        private static string GetTableName(int i)
        {
            return string.Format("ChartData{0}", i);
        }

        DateTime now = DateTime.Today;
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
                
                now = now.AddSeconds(1);
                now = now.AddMinutes(1);
                dataTable.AddChartDataRow(now, newValue);  //добавление ряда в таблицу датасета
                chartControl1.Refresh();
            }
        }

        private void ucChart_Load(object sender, EventArgs e)
        {
            InitializeChart();
        }

    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using DevExpress.XtraCharts;

namespace UICommon
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucChart : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucChart()
        {
            ChartDataStorageTime = 30;
            ChartTitle = "";
            InitializeComponent();
        }

        private List<int> _channelsToDisplay = new List<int>();
        private readonly Dictionary<int, string> _channelsNamedic = new Dictionary<int, string>();

        /// <summary>
        /// Список каналов, отображаемых на графике
        /// </summary>
        public List<int> ChannelsToDisplay
        {
            get { return _channelsToDisplay; }
            set { _channelsToDisplay = value; }
        }
        /// <summary>
        /// Задать соответствие номера и названия канала
        /// </summary>
        public void SetChannelsName(int channelId, string name)
        {
            _channelsNamedic.Add(channelId, name);
        }

        /// <summary>
        /// Заголовок графика
        /// </summary>
        [DefaultValue("")]
        public string ChartTitle { get; set; }

        /// <summary>
        /// Настраиваемый временной буфер [сек.]
        /// </summary>
        [DefaultValue(30)]
        public int ChartDataStorageTime { get; set; }


        /// <summary>
        /// инициализация графика и его серий, параметров, и т.п..
        /// </summary>
        public void InitializeChart()
        {

            foreach (int channelNumber in ChannelsToDisplay)
            {
                Series series1 = new Series(_channelsNamedic[channelNumber]+" #" + Convert.ToString(channelNumber), ViewType.Line);
                string tableName = GetTableName(channelNumber);
                dtsChart1.Tables.Add(new dtsChart.ChartDataDataTable { TableName = tableName });
                series1.DataSource = dtsChart1.Tables[tableName];
                series1.ArgumentDataMember = "TimeStamp";
                series1.ValueDataMembers.AddRange("Value");

                
                //series1.ValueScaleType = ScaleType.Numerical;
                //series1.ArgumentScaleType = ScaleType.DateTime;         //BUG:  ошибка при отображении


                chartControl1.Series.Add(series1);

                series1.Label.Visible = false;
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
            diagram.AxisX.DateTimeMeasureUnit = DateTimeMeasurementUnit.Second;
            diagram.AxisX.DateTimeOptions.Format = DateTimeFormat.ShortTime;//DateTimeFormat.Custom;
            diagram.AxisX.DateTimeOptions.FormatString = "hh:mm:ss";
            
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


    }

}

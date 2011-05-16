using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
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
                ChannelChartNumbers[i] = ChannelsToDisplay[i];
                series[i] = new Series("Channel #" + Convert.ToString(ChannelsToDisplay[i]), ViewType.Area);
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


        public void AddDataChart(int ChannelNumber, int NewValue)
        {

            for (int i = 0; i < ChannelsToDisplay.Count; i++)
            {
                if (ChannelsToDisplay[i] == ChannelNumber)
                {
                    series[i].Points.Add(new SeriesPoint((DateTime.Now.ToString("hh:mm:ss")), NewValue));
                    break;
                }
            }

        }





    }

}

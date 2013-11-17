using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using AmCharts.Windows.QuickCharts;
using Microsoft.Windows.Controls;
using TP.WPF.ViewModels;

namespace TP.WPF.Views
{
    /// <summary>
    /// Interaction logic for ucChartTab.xaml
    /// </summary>
    public partial class ucChartTab : UserControl
    {
        //public SerialChart ChannelChart = new SerialChart();
        public ucChartTab()
        {
            InitializeComponent();
            //visibleColumnNames = new StringCollection { "Id", "Description", "CurrentValue", "IsActive" };
            //grid.AutoGeneratingColumn += grid_AutoGeneratingColumn;
            //this.Chart1.Graphs.Add(new LineGraph(){});

            Chart1.DataContextChanged += new DependencyPropertyChangedEventHandler(Chart1_DataContextChanged);
        }

        void Chart1_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            //throw new System.NotImplementedException();
        }



        //void grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        //{
        //    var table = grid.ItemsSource as DataSetChannels.ChannelsDataTable;
        //    if (table == null) return;
            
        //    var column = e.Column;
        //    var header = (string) e.Column.Header;
        //    var col = table.Columns[header];
        //    column.Visibility = visibleColumnNames.Contains(header) ? Visibility.Visible : Visibility.Hidden;
        //    column.Header = col.Caption;
        //}
    }
}

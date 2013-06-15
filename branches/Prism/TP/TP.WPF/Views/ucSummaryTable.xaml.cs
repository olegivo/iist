using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Windows.Controls;
using TP.WPF.ViewModels;

namespace TP.WPF.Views
{
    /// <summary>
    /// Interaction logic for ucSummaryTable.xaml
    /// </summary>
    public partial class ucSummaryTable : UserControl
    {
        private readonly StringCollection visibleColumnNames;

        public ucSummaryTable()
        {
            InitializeComponent();
            visibleColumnNames = new StringCollection { "Id", "Description", "CurrentValue", "IsActive" };
            grid.AutoGeneratingColumn += grid_AutoGeneratingColumn;
        }

        void grid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var table = grid.ItemsSource as DataSetChannels.ChannelsDataTable;
            if (table == null) return;
            
            var column = e.Column;
            var header = (string) e.Column.Header;
            var col = table.Columns[header];
            column.Visibility = visibleColumnNames.Contains(header) ? Visibility.Visible : Visibility.Hidden;
            column.Header = col.Caption;
        }
    }
}

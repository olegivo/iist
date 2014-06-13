using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Oleg_ivo.CMU.WPF.ViewModels;

namespace Oleg_ivo.CMU.WPF.Views
{
    /// <summary>
    /// Interaction logic for ChannelsListView.xaml
    /// </summary>
    public partial class ChannelsListView : UserControl
    {
        public ChannelsListView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SelectedChannelsProperty =
            DependencyProperty.Register("SelectedChannels", typeof (ObservableCollection<LogicalChannelViewModel>),
                typeof (ChannelsListView), new PropertyMetadata(default(ObservableCollection<LogicalChannelViewModel>)));

        public ObservableCollection<LogicalChannelViewModel> SelectedChannels
        {
            get { return (ObservableCollection<LogicalChannelViewModel>) GetValue(SelectedChannelsProperty); }
            set { SetValue(SelectedChannelsProperty, value); }
        }

        public static readonly DependencyProperty MainViewModelProperty = DependencyProperty.Register("MainViewModel",
            typeof (MainViewModel), typeof (ChannelsListView), new PropertyMetadata(default(MainViewModel)));

        public MainViewModel MainViewModel
        {
            get { return (MainViewModel) GetValue(MainViewModelProperty); }
            set { SetValue(MainViewModelProperty, value); }
        }

        public static readonly DependencyProperty AvialableChannelsProperty =
            DependencyProperty.Register("AvialableChannels", typeof (ObservableCollection<LogicalChannelViewModel>),
                typeof (ChannelsListView), new PropertyMetadata(default(ObservableCollection<LogicalChannelViewModel>)));

        public ObservableCollection<LogicalChannelViewModel> AvialableChannels
        {
            get { return (ObservableCollection<LogicalChannelViewModel>) GetValue(AvialableChannelsProperty); }
            set { SetValue(AvialableChannelsProperty, value); }
        }

    }

    
}

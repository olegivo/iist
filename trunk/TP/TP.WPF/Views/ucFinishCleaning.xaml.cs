using System.Windows.Controls;
using DMS.Common.Messages;
using TP.WPF.ViewModels;

namespace TP.WPF.Views
{
    public class BaseView : UserControl
    {
        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="T:System.Windows.Controls.UserControl"/>.
        /// </summary>
        public BaseView()
        {
            DataContextChanged += ucFinishCleaning_DataContextChanged;
        }

        private void ucFinishCleaning_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            var viewModelBase = DataContext as ViewModelBase;
            if (viewModelBase != null)
                viewModelBase.NeedInitIndicator += (sender1, e1) => OnInitIndicator(e1.Message);
        }

        /// <summary>
        /// Инициализация индикатора
        /// </summary>
        /// <param name="message"></param>
        protected virtual void OnInitIndicator(ChannelRegistrationMessage message)
        {
            
        }
    }

    /// <summary>
    /// Interaction logic for ucFinishCleaning.xaml
    /// </summary>
    public partial class ucFinishCleaning : BaseView
    {
        /// <summary>
        /// Инициализация нового экземпляра класса <see cref="T:System.Windows.Controls.UserControl"/>.
        /// </summary>
        public ucFinishCleaning()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Инициализация индикатора
        /// </summary>
        /// <param name="message"></param>
        protected override void OnInitIndicator(ChannelRegistrationMessage message)
        {
            base.OnInitIndicator(message);
            switch (message.LogicalChannelId)
            {
                case 6:
                    indicatorT6.Init(message);
                    break;
            }
        }
    }
}
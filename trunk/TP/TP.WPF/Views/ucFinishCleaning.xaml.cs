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
                /*
                    indicatorT6.Init(message);
                    indicatorV.Init(message);
                    indicatorCO.Init(message);
                    indicatorО2.Init(message);
                    indicatorSO2.Init(message);
                    indicatorNO.Init(message);
                    indicatorNO2.Init(message);
                    indicatorT7.Init(message);
                    indicatorMCO.Init(message);
                    indicatorMSO2.Init(message);
                    indicatorMNO.Init(message);
                    indicatorMNO2.Init(message);
                */
                case 6:
                    indicatorT6.Init(message);
                    break;
                case 7:
                    indicatorT7.Init(message);
                    break; //TС7	температура перед дымососом
                case 20:
                    indicatorО2.Init(message);
                    break; //Г-О2	концентрация газа О2
                case 21:
                    indicatorCO.Init(message);
                    break; //Г-СО	концентрация газа СО
                case 22:
                    indicatorSO2.Init(message);
                    break; //Г-SО2	концентрация газа SО2
                case 23:
                    indicatorNO.Init(message);
                    break; //Г-NО	концентрация газа NО
                case 24:
                    indicatorNO2.Init(message);
                    break; //Г-NО2	концентрация газа NО2
            }
        }
    }
}
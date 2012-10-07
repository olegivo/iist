using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using DMS.Common.Messages;

namespace UICommon.WPF.UIComponents
{
    /// <summary>
    /// Interaction logic for ucIndicator.xaml
    /// </summary>
    public partial class ucIndicator : UserControl, INotifyPropertyChanged
    {
        private double? minValue;
        private double? maxValue;
        private double? minNormalValue;
        private double? maxNormalValue;

        public ucIndicator()
        {
            InitializeComponent();
            PropertyChanged += ucIndicator_PropertyChanged;
        }

        void ucIndicator_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(e.PropertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Init(ChannelRegistrationMessage message)
        {
            MinValue = message.MinValue;
            MaxValue = message.MaxValue;
            MinNormalValue = message.MinValue;
            MaxNormalValue = message.MaxValue;
            Caption = message.Description;
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(ucIndicator));

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public double? MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                OnPropertyChanged("MinValue");
            }
        }

        public double? MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                OnPropertyChanged("MaxValue");
            }
        }

        public double? MinNormalValue
        {
            get { return minNormalValue; }
            set
            {
                minNormalValue = value;
                OnPropertyChanged("MinNormalValue");
            }
        }

        public double? MaxNormalValue
        {
            get { return maxNormalValue; }
            set
            {
                maxNormalValue = value;
                OnPropertyChanged("MaxNormalValue");
            }
        }

        private static void CurrentValueChagedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ucIndicator control = sender as ucIndicator;
            if (control == null) return;

            control.CurrentValue = (double)e.NewValue;
        }

        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue",
                                        typeof(double),
                                        typeof(ucIndicator),
                                        new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, CurrentValueChagedCallback));

        public double CurrentValue
        {
            get { return (double)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        //TODO: разные стили всех вариантов нахождения CurrentValue в различных диапазонах
    }
}

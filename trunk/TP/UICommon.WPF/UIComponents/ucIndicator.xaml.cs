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
            //Console.WriteLine(e.PropertyName);
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
            MinNormalValue = message.MinNormalValue;
            MaxNormalValue = message.MaxNormalValue;
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

            var propertyNames = new[]
                    {
                        "IsValueHigherNormal",
                        "IsValueLowerNormal",
                        "IsValueHigherCritycal",
                        "IsValueLowerCritycal"
                    };
            foreach (var propertyName in propertyNames)
                control.OnPropertyChanged(propertyName);

        }

        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue",
                                        typeof(object),
                                        typeof(ucIndicator),
                                        new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, CurrentValueChagedCallback));

        /// <summary>
        /// Текущее значение
        /// </summary>
        public object CurrentValue
        {
            get { return GetValue(CurrentValueProperty); }
            set
            {
                SetValue(CurrentValueProperty, value);
                var propertyNames = new[]
                    {
                        "IsValueHigherNormal",
                        "IsValueLowerNormal",
                        "IsValueHigherCritycal",
                        "IsValueLowerCritycal"
                    };
                foreach (var propertyName in propertyNames)
                    OnPropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Текущее значение больше нормального
        /// </summary>
        public bool IsValueHigherNormal
        {
            get
            {
                return CompareCurrentValueWith(MaxNormalValue) > 0;
            }
        }

        /// <summary>
        /// Текущее значение больше максимально возможного
        /// </summary>
        public bool IsValueHigherCritycal
        {
            get
            {
                return CompareCurrentValueWith(MaxValue) > 0;
            }
        }

        /// <summary>
        /// Текущее значение меньше нормального
        /// </summary>
        public bool IsValueLowerNormal
        {
            get
            {
                return CompareCurrentValueWith(MinNormalValue) < 0;
            }
        }

        /// <summary>
        /// Текущее значение меньше минимально возможного
        /// </summary>
        public bool IsValueLowerCritycal
        {
            get
            {
                return CompareCurrentValueWith(MinValue) < 0;
            }
        }

        private int CompareCurrentValueWith(double? compareValue)
        {
            var result = 0;
            var value = CurrentValue as double?;
            if (value.HasValue && compareValue.HasValue)
                result = value.Value.CompareTo(compareValue.Value);
            return result;
        }
    }
}

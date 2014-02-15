using System;
using DMS.Common.Messages;
using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels
{
    public class IndicatorViewModel : ViewModel
    {
        private double? minValue;
        private double? maxValue;
        private double? minNormalValue;
        private double? maxNormalValue;
        private string caption;
        private double? currentValue;
        private bool discreteOnState;

        public void Init(ChannelRegistrationMessage message)
        {
            MinValue = message.MinValue;
            MaxValue = message.MaxValue;
            MinNormalValue = message.MinNormalValue;
            MaxNormalValue = message.MaxNormalValue;
            Caption = message.Description;
        }

        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                OnPropertyChanged("Caption");
            }
        }

        public bool IsOn
        {
            get { return currentValue != null ? true : false; }
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

        /// <summary>
        /// Текущее значение
        /// </summary>
        public double? CurrentValue
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
                OnPropertyChanged("CurrentValue");
                var propertyNames = new[]
                    {
                        "IsValueHigherNormal",
                        "IsValueLowerNormal",
                        "IsValueHigherCritycal",
                        "IsValueLowerCritycal",
                        "ShortCurrentValue",
                        "DiscreteOnState",
                        "IsOn",
                        "CurrentState"
                    };
                foreach (var propertyName in propertyNames)
                    OnPropertyChanged(propertyName);
            }
        }

        public Decimal ShortCurrentValue {
            get
            {
                return currentValue==null?0:Decimal.Round((Decimal) CurrentValue, 2);
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
            return CurrentValue.HasValue && compareValue.HasValue
                       ? CurrentValue.Value.CompareTo(compareValue.Value)
                       : 0;
        }


        //TODO: создать отдельную модель представления для дискретных индикаторов
        public bool DiscreteOnState
        {
            get
            {
                return (CurrentValue != null && CurrentValue > 0.9) ? true : false;
                //discreteOnState;
            }
            set
            {
                if (CurrentValue != null && CurrentValue > 0.9)
                    discreteOnState = true;//Convert.ToBoolean(value);
                else
                    discreteOnState = false;
                OnPropertyChanged("DiscreteOnState");

            }
        }


        public string CurrentState
        {
            get
            {
                if (IsValueHigherNormal || IsValueLowerNormal)
                    return "WarningState";
                if (IsValueLowerCritycal || IsValueHigherCritycal)
                    return "AlarmState";
                if (CurrentValue == 0)
                    return "OffState";
                if (CurrentValue != null)
                    return "WorkingState";
                return "NoSignal";


    }
}
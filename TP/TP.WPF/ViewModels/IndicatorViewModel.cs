using System;
using DMS.Common.Messages;
using Oleg_ivo.Base.Extensions;

namespace TP.WPF.ViewModels
{
    public class IndicatorViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private double? minValue;
        private double? maxValue;
        private double? minNormalValue;
        private double? maxNormalValue;
        private string caption;
        private IComparable currentValue;
        private LogicalChannelState? channelState;
        private bool isRegistered;
        //private bool discreteOnState;

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
                RaisePropertyChanged("Caption");
            }
        }

        public LogicalChannelState? ChannelState
        {
            get { return channelState; }
            set
            {
                channelState = value;
                RaisePropertyChanged("ChannelState");
                RaisePropertyChanged("CurrentState");
            }
        }

        public bool IsRegistered
        {
            get { return isRegistered; }
            set
            {
                isRegistered = value;
                RaisePropertyChanged("IsRegistered");
                RaisePropertyChanged("CurrentState");
            }
        }

        public double? MinValue
        {
            get { return minValue; }
            set
            {
                minValue = value;
                RaisePropertyChanged("MinValue");
            }
        }

        public double? MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;
                RaisePropertyChanged("MaxValue");
            }
        }

        public double? MinNormalValue
        {
            get { return minNormalValue; }
            set
            {
                minNormalValue = value;
                RaisePropertyChanged("MinNormalValue");
            }
        }

        public double? MaxNormalValue
        {
            get { return maxNormalValue; }
            set
            {
                maxNormalValue = value;
                RaisePropertyChanged("MaxNormalValue");
            }
        }

        /// <summary>
        /// Текущее значение
        /// </summary>
        public IComparable CurrentValue
        {
            get { return currentValue; }
            set
            {
                if (currentValue == value) return;
                currentValue = value;
                RaisePropertyChanged("CurrentValue");
                var propertyNames = new[]
                {
                    "IsValueHigherNormal",
                    "IsValueLowerNormal",
                    "IsValueHigherCritycal",
                    "IsValueLowerCritycal",
                    "ShortCurrentValue",
                    "CurrentValueDouble",
                    "CurrentValueBool",
                    "CurrentState"
                };
                foreach (var propertyName in propertyNames)
                    RaisePropertyChanged(propertyName);
            }
        }

        public Decimal ShortCurrentValue
        {
            get
            {
                var d = CurrentValueDouble;
                return d.HasValue ? Decimal.Round((Decimal) d, 2) : 0;
            }
        }

        /// <summary>
        /// Текущее значение больше нормального
        /// </summary>
        public bool IsValueHigherNormal
        {
            get { return CompareCurrentValueWith(MaxNormalValue) > 0; }
        }

        /// <summary>
        /// Текущее значение больше максимально возможного
        /// </summary>
        public bool IsValueHigherCritycal
        {
            get { return CompareCurrentValueWith(MaxValue) > 0; }
        }

        /// <summary>
        /// Текущее значение меньше нормального
        /// </summary>
        public bool IsValueLowerNormal
        {
            get { return CompareCurrentValueWith(MinNormalValue) < 0; }
        }

        /// <summary>
        /// Текущее значение меньше минимально возможного
        /// </summary>
        public bool IsValueLowerCritycal
        {
            get { return CompareCurrentValueWith(MinValue) < 0; }
        }

        private int CompareCurrentValueWith(double? compareValue)
        {
            var d = CurrentValueDouble;
            return d.HasValue && compareValue.HasValue
                ? d.Value.CompareTo(compareValue.Value)
                : 0;
        }

        public const string NotRegistered = "NotRegistered";
        public const string Undefined = "Undefined";
        public const string BreakState = "BreakState";
        public const string AlarmState = "AlarmState";
        public const string WorkingState = "WorkingState";
        public const string DiscreteOff = "DiscreteOff";
        /// <summary>
        /// NotRegistered - канал не подписан либо поступает значение False (0) с дискретного модуля
        /// BreakState - сигнал об обрыве связи (только для аналоговых модулей 4...20мА)
        /// AlarmState - значение аналоговой величины превышает максимально допустимую
        /// WorkingState - данные поступают, канал исправен
        /// DiscreteOff - данные поступают, канал исправен, дискретный канал в состоянии "выключен"
        /// </summary>
        public string CurrentState
        {
            get
            {
                var d = CurrentValueDouble;
                var b = CurrentValueBool;

                if (!IsRegistered)
                    return NotRegistered;
                if (ChannelState.HasValue)
                {
                    if (ChannelState == LogicalChannelState.Break)
                        return BreakState;
                    if (IsValueHigherNormal || IsValueLowerNormal)
                        return AlarmState;
                    if (d.HasValue)
                        return WorkingState;
                    if (b.HasValue)
                        return b.Value ? WorkingState : DiscreteOff;
                }
                return Undefined;
            }
        }

        public double? CurrentValueDouble
        {
            get { return (double?) (CurrentValue != null && CurrentValue.IsNumeric() ? CurrentValue : null); }
            set { CurrentValue = value; }
        }

        public bool? CurrentValueBool
        {
            get { return (bool?) (CurrentValue != null && CurrentValue.IsBool() ? CurrentValue : null); }
            set { CurrentValue = value; }
        }




    }
}
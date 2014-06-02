﻿using System;
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
        private bool isOn;
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

        public bool IsOn
        {
            get { return isOn; }
            set
            {
                isOn = value;
                RaisePropertyChanged("IsOn");
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
                    "DiscreteOnState",
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


        public string CurrentState
        {
            get
            {
                string state;
                if (!IsOn /*|| CurrentValue == null*/)
                    state = "NoSignal";
                else
                {
                    var d = CurrentValueDouble;
                    var b = CurrentValueBool;
                    if ((d.HasValue && !(d.Value > 0)) || !(b.HasValue && b.Value))
                        state = "OffState";
                    else
                        state = IsValueHigherNormal || IsValueLowerNormal
                            ? "AlarmState"
                            : "WorkingState";
                }

                Console.WriteLine(state);
                return state;
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

        //TODO: создать отдельную модель представления для дискретных индикаторов
        public bool DiscreteOnState
        {
            get
            {
                var b = CurrentValueBool;
                return b.HasValue && b.Value;
                //discreteOnState;
            }
/*
            set
            {
                if (CurrentValue != null && CurrentValue > 0.9)
                    discreteOnState = true;//Convert.ToBoolean(value);
                else
                    discreteOnState = false;
                RaisePropertyChanged("DiscreteOnState");

            }
*/
        }




    }
}
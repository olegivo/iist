using System;
using System.Globalization;
using System.Windows.Input;
using JulMar.Windows.Mvvm;
using TP.WPF.ViewModels.AutoControl;

namespace TP.WPF.ViewModels
{
    public class FinishCleaningViewModel : ViewModel
    {
        private readonly AutoControl<FinishCleaningViewModel>  autoControl;

        public FinishCleaningViewModel()
        {
            autoControl = new FinishCleaningAutoControl(this);
            //TEST: для тестового изменения значения
            IncreaseCommand = new DelegatingCommand(OnIncrease);
        }

        //TEST: метод для тестового изменения значения
        private void OnIncrease()
        {
            Temperature_TC6++;
        }

        //TEST: команда для тестового изменения значения
        public ICommand IncreaseCommand { get; private set; }

        private double temperature6;
        private double temperature7;
        private double concentration_CO;
        private double concentration_O2;
        private double concentration_SO2;
        private double concentration_NO2;
        private double concentration_NO;
        private bool burnerStatus;
        private double v;

        public double Temperature_TC6
        {
            get { return temperature6; }
            set
            {
                if (temperature6 != value)
                {
                    temperature6 = value;
                    OnPropertyChanged("Temperature_TC6");
                    OnPropertyChanged("Temperature_TC6S");
                }
            }
        }

        public string Temperature_TC6S { get { return Temperature_TC6.ToString(CultureInfo.InvariantCulture); } }

        public double Temperature_TC7
        {
            get { return temperature7; }
            set
            {
                if (temperature7 != value)
                {
                    temperature7 = value;
                    OnPropertyChanged("Temperature_TC7");
                }
            }
        }

        public double GasConcentration_CO
        {
            get { return concentration_CO; }
            set
            {
                if (concentration_CO != value)
                {
                    concentration_CO = value;
                    OnPropertyChanged("GasConcentration_CO");
                    OnPropertyChanged("Massa_CO");
                }
            }
        }

        public double GasConcentration_O2
        {
            get { return concentration_O2; }
            set
            {
                if (concentration_O2 != value)
                {
                    concentration_O2 = value;
                    OnPropertyChanged("GasConcentration_O2");
                }
            }
        }

        public double GasConcentration_SO2
        {
            get { return concentration_SO2; }
            set
            {
                if (concentration_SO2 != value)
                {
                    concentration_SO2 = value;
                    OnPropertyChanged("GasConcentration_SO2");
                    OnPropertyChanged("Massa_SO2");
                }
            }
        }

        public double GasConcentration_NO2
        {
            get { return concentration_NO2; }
            set
            {
                if (concentration_NO2 != value)
                {
                    concentration_NO2 = value;
                    OnPropertyChanged("GasConcentration_NO2");
                    OnPropertyChanged("Massa_NO2");
                }
            }
        }

        public double GasConcentration_NO
        {
            get { return concentration_NO; }
            set
            {
                if (concentration_NO != value)
                {
                    concentration_NO = value;
                    OnPropertyChanged("GasConcentration_NO");
                    OnPropertyChanged("Massa_NO");
                }
            }
        }

#region Расчётные значения массы веществ
        public double Massa_SO2
        {
            get { return GasConcentration_SO2 * Math.PI * 0.36 * 4.96 / 10000; }

        }

        public double Massa_CO
        {
            get { return GasConcentration_CO * Math.PI * 0.36 * 4.96 / 10000; }

        }

        public double Massa_NO2
        {
            get { return GasConcentration_NO2 * Math.PI * 0.36 * 4.96 / 10000; }

        }

        public double Massa_NO
        {
            get { return GasConcentration_NO * Math.PI * 0.36 * 4.96 / 10000; }

        }
#endregion

        /// <summary>
        /// Состояние горелки
        /// </summary>
        public bool BurnerStatus
        {
            get { return burnerStatus; }
            set
            {
                if (burnerStatus != value)
                {
                    burnerStatus = value;
                    OnPropertyChanged("BurnerStatus");
                    RaiseSendMessage(10001, BurnerStatus);
                }
            }
        }

        /// <summary>
        /// Количество оборотов дымососа в минуту
        /// </summary>
        public double V
        {
            get { return v; }
            set
            {
                if (v != value)
                {
                    v = value;
                    RaiseSendMessage(10002, V);
                    OnPropertyChanged("V");
                }
            }
        }

        public bool IsAutomaticControl
        {
            get { return autoControl.IsAutomaticControl; }
            set
            {
                if (autoControl.IsAutomaticControl != value)
                {
                    autoControl.IsAutomaticControl = value;
                    OnPropertyChanged("IsAutomaticControl");
                }
            }
        }

        private void RaiseSendMessage(int channelId, object value)
        {
            if (SendControlMessage != null)
                SendControlMessage(this, new SendControlMessageEventArgs(channelId, value));
        }

        /// <summary>
        /// Событие необходимости послать управляющее сообщение
        /// </summary>
        public event EventHandler<SendControlMessageEventArgs> SendControlMessage;

    }
}
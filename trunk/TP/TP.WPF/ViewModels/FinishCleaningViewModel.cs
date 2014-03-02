using System;
using System.Windows.Input;
using DMS.Common.Messages;
using GalaSoft.MvvmLight.Command;
using TP.WPF.ViewModels.AutoControl;

namespace TP.WPF.ViewModels
{
    public class FinishCleaningViewModel : ViewModelBase
    {
        private readonly AutoControl<FinishCleaningViewModel> autoControl;

        public FinishCleaningViewModel()
        {
            autoControl = new FinishCleaningAutoControl(this);
            //TEST: для тестового изменения значения
            IncreaseCommand = new RelayCommand(OnIncrease);
        }

        //TEST: метод для тестового изменения значения
        private void OnIncrease()
        {
            OnChannelRegistered(new ChannelRegistrationMessage("", "", RegistrationMode.Register, DataMode.Read, 6)
                {
                    MinValue = 0,
                    MinNormalValue = 20,
                    MaxNormalValue = 40,
                    MaxValue = 50
                });
            if (!Temperature_TC6.HasValue) Temperature_TC6 = 0;
            Temperature_TC6 += (Temperature_TC6 > 50 ? -70 : 5);
        }

        //TEST: команда для тестового изменения значения
        public ICommand IncreaseCommand { get; private set; }

        private double? temperature6;
        private double temperature7;
        private double concentration_CO;
        private double concentration_O2;
        private double concentration_SO2;
        private double concentration_NO2;
        private double concentration_NO;
        private bool burnerStatus;
        private double v;

        #region "Контрольные параметры"

        public double? Temperature_TC6
        {
            get { return temperature6; }
            set
            {
                if (temperature6 != value)
                {
                    temperature6 = value;
                    RaisePropertyChanged("Temperature_TC6");
                }
            }
        }

        public double Temperature_TC7
        {
            get { return temperature7; }
            set
            {
                if (temperature7 != value)
                {
                    temperature7 = value;
                    RaisePropertyChanged("Temperature_TC7");
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
                    RaisePropertyChanged("GasConcentration_CO");
                    RaisePropertyChanged("Massa_CO");
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
                    RaisePropertyChanged("GasConcentration_O2");
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
                    RaisePropertyChanged("GasConcentration_SO2");
                    RaisePropertyChanged("Massa_SO2");
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
                    RaisePropertyChanged("GasConcentration_NO2");
                    RaisePropertyChanged("Massa_NO2");
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
                    RaisePropertyChanged("GasConcentration_NO");
                    RaisePropertyChanged("Massa_NO");
                }
            }
        }
        #endregion

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

        #region Управляемые параметры
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
                    RaisePropertyChanged("BurnerStatus");
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
                    RaisePropertyChanged("V");
                }
            }
        }

        #endregion

        public bool IsAutomaticControl
        {
            get { return autoControl.IsAutomaticControl; }
            set
            {
                if (autoControl.IsAutomaticControl != value)
                {
                    autoControl.IsAutomaticControl = value;
                    RaisePropertyChanged("IsAutomaticControl");
                }
            }
        }

        /// <summary>
        /// После чтения канала
        /// </summary>
        /// <param name="message"></param>
        public override void OnReadChannel(InternalLogicalChannelDataMessage message)
        {
            base.OnReadChannel(message);
            double value = Convert.ToDouble(message.Value);
            int channelId = message.LogicalChannelId;

            switch (channelId)
            {
                case 6:
                    Temperature_TC6 = value;
                    break; //TС6	температура перед рукавным фильтром
                case 7:
                    Temperature_TC7 = value;
                    break; //TС7	температура перед дымососом
                case 20:
                    GasConcentration_O2 = value;
                    break; //Г-О2	концентрация газа О2
                case 21:
                    GasConcentration_CO = value;
                    break; //Г-СО	концентрация газа СО
                case 22:
                    GasConcentration_SO2 = value;
                    break; //Г-SО2	концентрация газа SО2
                case 23:
                    GasConcentration_NO = value;
                    break; //Г-NО	концентрация газа NО
                case 24:
                    GasConcentration_NO2 = value;
                    break; //Г-NО2	концентрация газа NО2
            }
        }
    }
}
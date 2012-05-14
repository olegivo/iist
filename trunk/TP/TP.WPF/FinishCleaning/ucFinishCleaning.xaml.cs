using System;
using System.ComponentModel;

namespace TP.WPF.FinishCleaning
{
    /// <summary>
    /// Interaction logic for ucFinishCleaning.xaml
    /// </summary>
    public partial class ucFinishCleaning : INotifyPropertyChanged
    {
        public ucFinishCleaning()
        {
            this.InitializeComponent();
        }

        private float _temperature6;

        public float Temperature_TC6
        {
            get { return _temperature6; }
            set
            {
                if (_temperature6 != value)
                {
                    _temperature6 = value;
                    OnPropertyChanged("Temperature_TC6");
                }
            }
        }

        private float _temperature7;

        public float Temperature_TC7
        {
            get { return _temperature7; }
            set
            {
                if (_temperature7 != value)
                {
                    _temperature7 = value;
                    OnPropertyChanged("Temperature_TC7");
                }
            }
        }

        private float _concentration_CO;

        public float GasConcentration_CO
        {
            get { return _concentration_CO; }
            set
            {
                if (_concentration_CO != value)
                {
                    _concentration_CO = value;
                    OnPropertyChanged("GasConcentration_CO");
                }
            }
        }

        private float _concentration_O2;

        public float GasConcentration_O2
        {
            get { return _concentration_O2; }
            set
            {
                if (_concentration_O2 != value)
                {
                    _concentration_O2 = value;
                    OnPropertyChanged("GasConcentration_O2");
                }
            }
        }

        private float _concentration_SO2;

        public float GasConcentration_SO2
        {
            get { return _concentration_SO2; }
            set
            {
                if (_concentration_SO2 != value)
                {
                    _concentration_SO2 = value;
                    OnPropertyChanged("GasConcentration_SO2");
                }
            }
        }

        private float _concentration_NO2;

        public float GasConcentration_NO2
        {
            get { return _concentration_NO2; }
            set
            {
                if (_concentration_NO2 != value)
                {
                    _concentration_NO2 = value;
                    OnPropertyChanged("GasConcentration_NO2");
                }
            }
        }

        private float _concentration_NO;

        public float GasConcentration_NO
        {
            get { return _concentration_NO; }
            set
            {
                if (_concentration_NO != value)
                {
                    _concentration_NO = value;
                    OnPropertyChanged("GasConcentration_NO");
                }
            }
        }
        private bool burnerStatus;

        /// <summary>
        /// Включеное состояние
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

        private double v;
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


        private void RaiseSendMessage(int channelId, object value)
        {
            if (SendControlMessage != null)
                SendControlMessage(this, new SendControlMessageEventArgs(channelId, value));
        }

        /// <summary>
        /// Событие необходимости послать управляющее сообщение
        /// </summary>
        public event EventHandler<SendControlMessageEventArgs> SendControlMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChangedEventArgs args = new PropertyChangedEventArgs(propertyName);
                handler(this, args);
            }
        }
    }
}
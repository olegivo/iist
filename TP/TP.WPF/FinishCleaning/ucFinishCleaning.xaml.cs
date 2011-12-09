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

        private float _temperature;

        public float Temperature_TC6
        {
            get { return _temperature; }
            set
            {
                if (_temperature != value)
                {
                    _temperature = value;
                    OnPropertyChanged("Temperature_TC6");
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
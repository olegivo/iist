using System;
using System.ComponentModel;
using System.Timers;

namespace TP.WPF.Views
{
    /// <summary>
    /// Interaction logic for ucFinishCleaning.xaml
    /// </summary>
    public partial class ucFinishCleaning : INotifyPropertyChanged
    {
        private Timer tmrAutoControlMode;
        public ucFinishCleaning()
        {
            this.InitializeComponent();
            tmrAutoControlMode = new Timer {Interval = 5000};
            tmrAutoControlMode.Elapsed += tmrAutoControlMode_Elapsed;
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
                    OnPropertyChanged("Massa_CO");
                }
            }
        }

    public float Massa_CO
        {
            get { return (float)(GasConcentration_CO * Math.PI * 0.36 * 4.96 / 10000); }
            
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
                    OnPropertyChanged("Massa_SO2");
                }
            }
        }

        public float Massa_SO2
        {
            get { return (float)(GasConcentration_SO2 * Math.PI * 0.36 * 4.96 / 10000); }

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
                    OnPropertyChanged("Massa_NO2");
                }
            }
        }

        public float Massa_NO2
        {
            get { return (float)(GasConcentration_NO2 * Math.PI * 0.36 * 4.96 / 10000); }

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
                    OnPropertyChanged("Massa_NO");
                }
            }
        }

        public float Massa_NO
        {
            get { return (float)(GasConcentration_NO * Math.PI * 0.36 * 4.96 / 10000); }

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

        private bool isAutomaticControl;
        public bool IsAutomaticControl
        {
            get { return isAutomaticControl; }
            set
            {
                if (isAutomaticControl != value)
                {
                    isAutomaticControl = value;
                    ApplyAutoControlMode();
                    OnPropertyChanged("IsAutomaticControl");
                }
            }
        }
     
        private void ApplyAutoControlMode()
        {
            if (IsAutomaticControl)
            {
               
               tmrAutoControlMode.Start();
               
            }
            else
            {
                tmrAutoControlMode.Stop();
            }
            
        }

        private void tmrAutoControlMode_Elapsed(object sender, ElapsedEventArgs e)
        {
            DoAutoControl();
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



        public void DoAutoControl()
        {
            int oborot = 0;
            int gorelka = 0;
            double delta_v = 10;

            if (GasConcentration_CO > 3000)
            {oborot = oborot + 1;}

            if (GasConcentration_O2 < 5)
            {oborot = oborot + 1;}

            if (GasConcentration_O2 > 21)
            {oborot = oborot - 1;}

            if (GasConcentration_SO2 > 600)
            {oborot = oborot + 1;}

            if (GasConcentration_NO > 700)
            {oborot = oborot + 1;}

            if (GasConcentration_NO2 > 750)
            {oborot = oborot + 1;}

            if (Temperature_TC6 < 120)
            { gorelka = gorelka + 1;}

            if (Temperature_TC6 > 180)
            { gorelka = gorelka - 1; }

            if (Temperature_TC7 < 160)
            { gorelka = gorelka + 1; }

            if (oborot > 0)
            {V = V + delta_v;}

            if (oborot < 0)
            { V = V - delta_v; }

            if (gorelka > 0)
            {BurnerStatus = true;}
            
            if (gorelka < 0)
            {BurnerStatus = false;}
         
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
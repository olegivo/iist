using System;
using DMS.Common.Messages;

namespace TP.WPF.ViewModels 
{
    public class AllHeatExchangerViewModel : ViewModelBase
    {
        /// <summary>
        /// TП3	температура в камере дожигания
        /// </summary>
        private double temperature3;
        public double Temperature_TP3
        {
            get { return temperature3; }
            set
            {
                if (temperature3 != value)
                {
                    temperature3 = value;
                    OnPropertyChanged("Temperature_TP3");
                }
            }
        }
        /// <summary>
        /// TР4	температура в теплообменнике ТО1
        /// </summary>
        private double temperature4;
        public double Temperature_TR4
        {
            get { return temperature4; }
            set
            {
                if (temperature4 != value)
                {
                    temperature4 = value;
                    OnPropertyChanged("Temperature_TR4");
                }
            }
        }
        /// <summary>
        /// TР5	температура в теплообменнике ТО2
        /// </summary>
        private double temperature5;
        public double Temperature_TR5
        {
            get { return temperature5; }
            set
            {
                if (temperature5 != value)
                {
                    temperature5 = value;
                    OnPropertyChanged("Temperature_TR5");
                }
            }
        }
        /// <summary>
        /// Г-О2	концентрация газа О2
        /// </summary>
        private double concentration_O2;
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
        /// <summary>
        /// Г-СО	концентрация газа СО
        /// </summary>
        private double concentration_CO;
        public double GasConcentration_CO
        {
            get { return concentration_CO; }
            set
            {
                if (concentration_CO != value)
                {
                    concentration_CO = value;
                    OnPropertyChanged("GasConcentration_CO");
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
            var value = Convert.ToDouble(message.Value);
            var channelId = message.LogicalChannelId;

            switch (channelId)
            {
                case 3:
                    Temperature_TP3 = value;
                    break; //TП3	температура в камере дожигания
                case 4:
                    Temperature_TR4 = value;
                    break; //TР4	температура в теплообменнике ТО1
                case 5:
                    Temperature_TR5 = value;
                    break; //TР5	температура в теплообменнике ТО2
                    //BUG: канал не реализован
                    //    case 9:
                    //    break; //Р	разрежение в камере дожигания
                case 18:
                    GasConcentration_O2 = value;
                    break; //Г-О2	концентрация газа О2
                case 19:
                    GasConcentration_CO = value;
                    break; //Г-СО	концентрация газа СО
            }
        }
    }
}

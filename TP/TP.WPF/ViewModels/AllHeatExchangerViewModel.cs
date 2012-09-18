using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels 
{
    public class AllHeatExchangerViewModel: ViewModel
    {
        /// <summary>
        /// TП3	температура в камере дожигания
        /// </summary>
        private float _temperature3;
        public float Temperature_TP3
        {
            get { return _temperature3; }
            set
            {
                if (_temperature3 != value)
                {
                    _temperature3 = value;
                    OnPropertyChanged("Temperature_TP3");
                }
            }
        }
        /// <summary>
        /// TР4	температура в теплообменнике ТО1
        /// </summary>
        private float _temperature4;
        public float Temperature_TR4
        {
            get { return _temperature4; }
            set
            {
                if (_temperature4 != value)
                {
                    _temperature4 = value;
                    OnPropertyChanged("Temperature_TR4");
                }
            }
        }
        /// <summary>
        /// TР5	температура в теплообменнике ТО2
        /// </summary>
        private float _temperature5;
        public float Temperature_TR5
        {
            get { return _temperature5; }
            set
            {
                if (_temperature5 != value)
                {
                    _temperature5 = value;
                    OnPropertyChanged("Temperature_TR5");
                }
            }
        }
        /// <summary>
        /// Г-О2	концентрация газа О2
        /// </summary>
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
        /// <summary>
        /// Г-СО	концентрация газа СО
        /// </summary>
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels
{
    public class DrumTypeFurnaceViewModel : ViewModel
    {
        /// <summary>
        /// TП1	температура в циклонной вихревой топке
        /// </summary>
        private float _temperature1;
        public float Temperature_TC1
        {
            get { return _temperature1; }
            set
            {
                if (_temperature1 != value)
                {
                    _temperature1 = value;
                    OnPropertyChanged("Temperature_TC1");
                }
            }
        }
        /// <summary>
        /// TП2	температура в загрузочной системе
        /// </summary>
        private float _temperature2;
        public float Temperature_TC2
        {
            get { return _temperature2; }
            set
            {
                if (_temperature2 != value)
                {
                    _temperature2 = value;
                    OnPropertyChanged("Temperature_TC2");
                }
            }
        }
        /// <summary>
        /// S	скорость вращения печи
        /// </summary>
        private float _speed;
        public float Speed_S
        {
            get { return _speed; }
            set
            {
                if (_speed != value)
                {
                    _speed = value;
                    OnPropertyChanged("Speed_S");
                }
            }
        }
        /// <summary>
        /// TС8	температура воды в системе охлаждения
        /// </summary>
        private float _temperature8;
        public float Temperature_TC8
        {
            get { return _temperature8; }
            set
            {
                if (_temperature8 != value)
                {
                    _temperature8 = value;
                    OnPropertyChanged("Temperature_TC8");
                }
            }
        }
        /// <summary>
        /// ДУ-9	уровень отходов в бункере
        /// </summary>
        private float _level9;
        public float Level_DU9
        {
            get { return _level9; }
            set
            {
                if (_level9 != value)
                {
                    _level9 = value;
                    OnPropertyChanged("Level_DU9");
                }
            }
        }

    }
}

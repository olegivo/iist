using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels
{
    public class CycloneAndScrubberViewModel : ViewModel
    {
        /// <summary>
        /// рН1	уровень рН в СФ1
        /// </summary>
        private float _phLevel1;
        public float PhLevel_CF1
        {
            get { return _phLevel1; }
            set
            {
                if (_phLevel1 != value)
                {
                    _phLevel1 = value;
                    OnPropertyChanged("PhLevel_CF1");
                }
            }
        }

        /// <summary>
        /// рН2	уровень рН в СФ2
        /// </summary>
        private float _phLevel2;
        public float PhLevel_CF2
        {
            get { return _phLevel2; }
            set
            {
                if (_phLevel2 != value)
                {
                    _phLevel2 = value;
                    OnPropertyChanged("PhLevel_CF2");
                }
            }
        }
        /// <summary>
        /// ДУ-10	уровень в СБ
        /// </summary>
        private float _level10;
        public float Level_DU10
        {
            get { return _level10; }
            set
            {
                if (_level10 != value)
                {
                    _level10 = value;
                    OnPropertyChanged("Level_DU10");
                }
            }
        }
    }
}

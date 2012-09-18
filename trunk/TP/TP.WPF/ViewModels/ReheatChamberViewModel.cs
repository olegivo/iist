using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels
{
    public class ReheatChamberViewModel : ViewModel
    {
        /// <summary>
        /// ДУ-1	уровень в НЕ
        /// </summary>
        private float _level1;
        public float Level_DU1
        {
            get { return _level1; }
            set
            {
                if (_level1 != value)
                {
                    _level1 = value;
                    OnPropertyChanged("Level_DU1");
                }
            }
        }
        /// <summary>
        /// ДУ-4	уровень в РЕ
        /// </summary>
        private float _level4;
        public float Level_DU4
        {
            get { return _level4; }
            set
            {
                if (_level4 != value)
                {
                    _level4 = value;
                    OnPropertyChanged("Level_DU4");
                }
            }
        }

        /// <summary>
        /// ДУ-11	уровень в РТ
        /// </summary>
        private float _level11;
        public float Level_DU11
        {
            get { return _level11; }
            set
            {
                if (_level11 != value)
                {
                    _level11 = value;
                    OnPropertyChanged("Level_DU11");
                }
            }
        }


    }
}
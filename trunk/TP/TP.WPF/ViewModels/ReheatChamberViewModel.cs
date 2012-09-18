using JulMar.Windows.Mvvm;

namespace TP.WPF.ViewModels
{
    public class ReheatChamberViewModel : ViewModel
    {
        /// <summary>
        /// ��-1	������� � ��
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
        /// ��-4	������� � ��
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
        /// ��-11	������� � ��
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
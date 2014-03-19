using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Autofac;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Base.Extensions;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.WAGO.CMS.Dialogs
{
    public class PolynomDialogViewModel : ViewModelBase
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public PolynomDialogViewModel()
        {
            AddCoefficientCommand = new RelayCommand(AddCoefficient);
            DeleteCoefficientCommand = new RelayCommand<PolynomCoefficient>(DeleteCoefficient);
        }

        private Polynom polynom;
        private ObservableCollection<PolynomCoefficient> coefficients;

        [Dependency]
        public IComponentContext Context { get; set; }

        public Polynom Polynom
        {
            get
            {
                polynom.Dictionary = Coefficients.ToDictionary(item => item.Key, item => item.Value);
                return polynom;
            }
            set
            {
                polynom = value ?? new Polynom {Dictionary = new Dictionary<short, double>()};
                Coefficients = new ObservableCollection<PolynomCoefficient>(polynom.Dictionary.Select(item => new PolynomCoefficient { Key = item.Key, Value = item.Value}));
            }
        }

        public RelayCommand<PolynomCoefficient> DeleteCoefficientCommand { get; private set; }
        public void DeleteCoefficient(PolynomCoefficient coefficient)
        {
            try
            {
                Coefficients.Remove(coefficient);
            }
            catch (Exception ex)
            {
                log.ErrorException("Ошибка при удалении коэффициента", ex);
            }
        }

        public RelayCommand AddCoefficientCommand { get; private set; }
        public void AddCoefficient()
        {
            try
            {
                var next = Coefficients.MaxOrDefault(c => c.Key);
                if (Coefficients.Any()) next++;
                Coefficients.Add(new PolynomCoefficient {Key = next});
            }
            catch (Exception ex)
            {
                log.ErrorException("Ошибка при добавлении коэффициента", ex);
            }
        }

        public class PolynomCoefficient : ViewModelBase
        {
            private short key;
            private double value;

            public short Key
            {
                get { return key; }
                set
                {
                    key = value;
                    RaisePropertyChanged("Key");
                }
            }

            public double Value
            {
                get { return value; }
                set
                {
                    this.value = value;
                    RaisePropertyChanged("Value");
                }
            }
        }

        public ObservableCollection<PolynomCoefficient> Coefficients
        {
            get { return coefficients; }
            set
            {
                coefficients = value;
                RaisePropertyChanged("Coefficients");
            }
        }
    }
}

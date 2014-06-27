using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Linq;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;
using Oleg_ivo.Plc.Entities;

namespace Oleg_ivo.WAGO.CMS.Dialogs
{
    public class ParametersEditDialogViewModel : ViewModelBase
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public ParametersEditDialogViewModel()
        {
            AddParameterCommand = new RelayCommand(AddParameter);
            DeleteParameterCommand = new RelayCommand<Parameter>(DeleteParameter);
        }

        private Table<Parameter> parametersSource;
        public void SetSource(PlcDataContext dataContext)
        {
            MeasurementUnits = dataContext.MeasurementUnits;
            parametersSource = dataContext.Parameters;
            if (Parameters != null) 
                Parameters.CollectionChanged -= Parameters_CollectionChanged;
            Parameters = new ObservableCollection<Parameter>(parametersSource);
            if (Parameters != null) 
                Parameters.CollectionChanged += Parameters_CollectionChanged;
        }

        public Table<MeasurementUnit> MeasurementUnits
        {
            get { return measurementUnits; }
            set
            {
                measurementUnits = value;
                RaisePropertyChanged(() => MeasurementUnits);
            }
        }

        void Parameters_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    parametersSource.InsertAllOnSubmit(e.NewItems.Cast<Parameter>());
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var newItems = e.NewItems==null ? Enumerable.Empty<Parameter>()  : e.NewItems.Cast<Parameter>();
                    parametersSource.DeleteAllOnSubmit(e.OldItems.Cast<Parameter>().Except(newItems));
                    break;
            }
        }

        private ObservableCollection<Parameter> parameters;
        private Table<MeasurementUnit> measurementUnits;

        public ObservableCollection<Parameter> Parameters
        {
            get { return parameters; }
            set
            {
                parameters = value;
                RaisePropertyChanged("Parameters");
            }
        }

        public RelayCommand<Parameter> DeleteParameterCommand { get; private set; }
        public void DeleteParameter(Parameter parameter)
        {
            try
            {
                Parameters.Remove(parameter);
                RaisePropertyChanged("Parameters");
            }
            catch (Exception ex)
            {
                log.Error("Ошибка при удалении параметра", ex);
            }
        }

        public RelayCommand AddParameterCommand { get; private set; }
        public void AddParameter()
        {
            try
            {
                Parameters.Add(new Parameter());
            }
            catch (Exception ex)
            {
                log.Error("Ошибка при добавлении параметра", ex);
            }
        }
    }
}
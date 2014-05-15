using System;
using System.Collections.Generic;
using System.Data.Linq;
using Autofac;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NLog;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Entities;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.Tools.ConnectionProvider;
using Oleg_ivo.WAGO.CMS.Dialogs;
using UICommon.WPF.Dialogs;
using LogicalChannel = Oleg_ivo.Plc.Channels.LogicalChannel;

namespace Oleg_ivo.WAGO.CMS.ViewModel
{
    public class DeviceConfigurationViewModel : ViewModelBase
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private object selectedItem;

        public DeviceConfigurationViewModel()
        {
            SaveCommand = new RelayCommand(Save);
            EditDirectPolynomCommand = new RelayCommand<LogicalChannel>(EditDirectPolynom);
            EditReversePolynomCommand = new RelayCommand<LogicalChannel>(EditReversePolynom);
            EditParametersCommand = new RelayCommand(EditParameters);
        }


        [Dependency]
        public IDistributedMeasurementInformationSystem DMIS { get; set; }

        [Dependency]
        public IModalDialogService ModalDialogService { get; set; }

        [Dependency]
        public IComponentContext Context { get; set; }

        private PlcDataContext dataContext;

        protected PlcDataContext DataContext
        {
            get
            {
                return dataContext ??
                       (dataContext =
                           Context.Resolve<PlcDataContext>(new TypedParameter(typeof(string),
                               Context.Resolve<DbConnectionProvider>().DefaultConnectionString)));
            }
        }

        public Table<Parameter> Parameters
        {
            get { return DataContext.Parameters; }
        }
        
        public List<FieldBusManager> FieldBusManagers
        {
            get
            {
                return DMIS.PlcManager.FieldBusManagers;
            }
        }

        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }

        public RelayCommand SaveCommand { get; private set; }
        public void Save()
        {
            try
            {
                DMIS.PlcManager.Save();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public RelayCommand EditParametersCommand { get; private set; }
        public void EditParameters()
        {
            try
            {
                ModalDialogService.CreateAndShowDialog<ParametersEditDialogViewModel>(
                    modalWindow => modalWindow.ViewModel.SetSource(DataContext));
            }
            catch (Exception ex)
            {
                log.ErrorException("Ошибка при редактировании параметров", ex);
            }
        }

        public RelayCommand<LogicalChannel> EditDirectPolynomCommand { get; private set; }
        public void EditDirectPolynom(LogicalChannel logicalChannel)
        {
            try
            {
                ModalDialogService.CreateAndShowDialog<PolynomDialogViewModel>(
                    modalWindow =>
                    {
                        modalWindow.Title = "Редактирование прямого преобразования";
                        modalWindow.ViewModel.Polynom = logicalChannel.DirectTransform;
                    },
                    (model, dialogResult) =>
                    {
                        if (dialogResult.HasValue && dialogResult.Value) 
                            logicalChannel.DirectTransform = model.Polynom;
                    });
            }
            catch (Exception ex)
            {
                log.ErrorException("Ошибка при редактировании прямого преобразования", ex);
            }
        }

        public RelayCommand<LogicalChannel> EditReversePolynomCommand { get; private set; }
        public void EditReversePolynom(LogicalChannel logicalChannel)
        {
            try
            {
                ModalDialogService.CreateAndShowDialog<PolynomDialogViewModel>(
                    modalWindow =>
                    {
                        modalWindow.Title = "Редактирование обратного преобразования";
                        modalWindow.ViewModel.Polynom = logicalChannel.ReverseTransform;
                    },
                    (model, dialogResult) =>
                    {
                        if (dialogResult.HasValue && dialogResult.Value)
                            logicalChannel.ReverseTransform = model.Polynom;
                    });
            }
            catch (Exception ex)
            {
                log.ErrorException("Ошибка при редактировании обратного преобразования", ex);
            }
        }
    }
}
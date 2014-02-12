using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;

namespace Oleg_ivo.WAGO.CMS.ViewModel
{
    public class DeviceConfigurationViewModel : ViewModelBase
    {
        private object selectedItem;

        public DeviceConfigurationViewModel()
        {
            SaveCommand = new RelayCommand(Save);
        }


        [Dependency]
        public IDistributedMeasurementInformationSystem DMIS { get; set; }

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
    }
}
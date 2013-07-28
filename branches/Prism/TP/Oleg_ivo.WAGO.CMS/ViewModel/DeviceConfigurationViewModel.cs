using System.Collections.Generic;
using GalaSoft.MvvmLight;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;
using Oleg_ivo.PrismExtensions.Autofac.DependencyInjection;

namespace Oleg_ivo.WAGO.CMS.ViewModel
{
    public class DeviceConfigurationViewModel : ViewModelBase
    {
        private object selectedItem;

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
    }
}
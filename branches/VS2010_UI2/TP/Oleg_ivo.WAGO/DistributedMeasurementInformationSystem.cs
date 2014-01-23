using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.WAGO.Forms;

namespace Oleg_ivo.WAGO
{
    ///<summary>
    /// Распределённая информационно-измерительная система (фасад)
    ///</summary>
    public class DistributedMeasurementInformationSystem : DistributedMeasurementInformationSystemBase
    {
        #region fields

        #endregion

        #region properties

        #endregion

        #region constructors

        #region Singleton


        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public new static DistributedMeasurementInformationSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DistributedMeasurementInformationSystem();
                }
                return _instance as DistributedMeasurementInformationSystem;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DistributedMeasurementInformationSystem" />.
        /// </summary>
        private DistributedMeasurementInformationSystem()
        {
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        protected override DistributedSystemSettingsBase CreateSettings()
        {
            return new DistributedSystemSettings();
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        protected override PlcManagerBase CreatePlcManager()
        {
            return new WagoPlcManager();
        }

        ///<summary>
        /// Построить конфигурацию системы
        ///</summary>
        public override void BuildSystemConfiguration()
        {
            base.BuildSystemConfiguration();

            PlcManagerBase.BuildFieldBuses(true, FieldBusType.RS485);
            PlcManagerBase.BuildFieldBuses(false, FieldBusType.Ethernet);
            //WagoPlcManager.BuildPhysicalChannels();
        }

        #endregion

        #endregion

        ///<summary>
        ///
        ///</summary>
        public override void ShowConfiguration()
        {
            DeviceConfigurationForm form = new DeviceConfigurationForm();
            form.ShowDialog();
        }
    }
}
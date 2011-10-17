using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.WAGO.Forms;

namespace Oleg_ivo.WAGO
{
    ///<summary>
    /// ������������� �������������-������������� ������� (�����)
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
        /// ������������ ���������
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
        /// �������������� ����� ��������� ������ <see cref="DistributedMeasurementInformationSystem" />.
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
        /// ��������� ������������ �������
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
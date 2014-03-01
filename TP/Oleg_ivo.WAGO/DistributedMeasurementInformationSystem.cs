using Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.WAGO.Configuration;
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

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="DistributedMeasurementInformationSystem" />.
        /// </summary>
        public DistributedMeasurementInformationSystem(IComponentContext context):base(context){}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IDistributedSystemSettings CreateSettings()
        {
            var configurationManager = Context.Resolve<ConfigurationManager>();
            return configurationManager.LoadConfig();
        }

        ///<summary>
        /// ��������� ������������ �������
        ///</summary>
        public override void BuildSystemConfiguration()
        {
            base.BuildSystemConfiguration();

            //TODO:
            //PlcManager.BuildFieldBuses(true, FieldBusType.RS232);
            //PlcManager.BuildFieldBuses(true, FieldBusType.RS485);
            PlcManager.BuildFieldBuses(false, FieldBusType.Ethernet);
            //WagoPlcManager.BuildPhysicalChannels();
        }

        #endregion

        ///<summary>
        ///
        ///</summary>
        public override void ShowConfiguration()
        {
            var form = new DeviceConfigurationForm();
            Context.InjectAttributedProperties(form);
            form.ShowDialog();
        }
    }
}
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.WAGO.Devices;
using Oleg_ivo.WAGO.Factory;

namespace Oleg_ivo.WAGO
{
    ///<summary>
    /// Диспетчер всех ПЛК, включенных в конфигурацию системы (на всех портах)
    ///</summary>
    public class WagoPlcManager : PlcManagerBase
    {
        #region fields

        #endregion

        #region properties

        #endregion

        #region constructors

        #endregion


        #region methods


        #endregion


        #region Фабрики по умолчанию

        #endregion

        ///<summary>
        ///
        ///</summary>
        protected override IPlcFactory GetPlcFactory()
        {
            return WagoPlcFactory.Instance;
        }

        ///<summary>
        ///
        ///</summary>
        protected override IFieldBusNodeFactory GetFieldBusNodeFactory()
        {
            return WagoFieldBusNodeFactory.Instance;
        }

        ///<summary>
        ///
        ///</summary>
        protected override IPhysicalChannelsFactory GetPhysicalChannelsFactory()
        {
            return WagoPhysicalChannelsFactory.Instance;
        }

        ///<summary>
        ///
        ///</summary>
        protected override ILogicalChannelsFactory GetLogicalChannelsFactory()
        {
            return Factory.LogicalChannelsFactory.Instance;
        }
    }
}

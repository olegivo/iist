using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// Фабрика ПЛК
    ///</summary>
    public interface IPlcFactory
    {
        ///<summary>
        /// Создать ПЛК на основе узла полевой шины
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        PLC CreatePLC(FieldBusNode fieldBusNode);
    }
}
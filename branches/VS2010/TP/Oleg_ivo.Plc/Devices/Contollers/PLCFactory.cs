using System;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// Фабрика ПЛК
    ///</summary>
    public abstract class PlcFactory : IPlcFactory
    {
        ///<summary>
        /// Создать ПЛК на основе узла полевой шины
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        public abstract PLC CreatePLC(FieldBusNode fieldBusNode);

        /// <summary>
        /// Инициализировать ПЛК.
        /// Связывает Узел и ПЛК
        /// </summary>
        /// <param name="plc"></param>
        protected virtual void InitPLC(PLC plc)
        {
            if (plc == null) throw new ArgumentNullException("plc");
            plc.FieldBusNode.InitPlc(plc);
        }
    }
}
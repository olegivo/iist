using System;
using Autofac;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;
using Oleg_ivo.PrismExtensions.Autofac;
using Oleg_ivo.PrismExtensions.Autofac.DependencyInjection;
using Oleg_ivo.WAGO.Meta;

namespace Oleg_ivo.WAGO.Devices
{
    ///<summary>
    /// Фабрика ПЛК WAGO
    ///</summary>
    public class WagoPlcFactory : PlcFactory
    {
        private readonly IPhysicalChannelsFactory physicalChannelsFactory;
        private readonly WagoMetaFactory wagoMetaFactory;
        private IDistributedMeasurementInformationSystem dmis;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="WagoPlcFactory" />.
        /// </summary>
        /// <param name="context"></param>
        public WagoPlcFactory(IComponentContext context)
        {
            var componentContext = Enforce.ArgumentNotNull(context, "context");
            dmis = componentContext.Resolve<IDistributedMeasurementInformationSystem>();
            physicalChannelsFactory = componentContext.Resolve<IPhysicalChannelsFactory>();
            wagoMetaFactory = componentContext.ResolveUnregistered<WagoMetaFactory>();
        }

        ///<summary>
        /// Создать ПЛК на основе узла полевой шины
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        public override PLC CreatePLC(FieldBusNode fieldBusNode)
        {
            if (fieldBusNode == null) throw new ArgumentNullException("fieldBusNode");

            WagoPlc plc = new WagoPlc(fieldBusNode, physicalChannelsFactory, wagoMetaFactory);
            InitPLC(plc);
            return plc;
        }

        #region Overrides of PlcFactory

        /// <summary>
        /// Инициализировать ПЛК
        /// </summary>
        /// <param name="plc"></param>
        protected override void InitPLC(PLC plc)
        {
            base.InitPLC(plc);
            if (dmis.Settings.IsEmulationMode)
            {
                Console.WriteLine(
                    "В настройках системы IsEmulationMode = true, поэтому инициализация объекта PLC и управления по Modbus не требуется. Объект {0} будет в состоянии Offline",
                    plc);
                return;
            }

            WagoPlc wagoPlc = plc as WagoPlc;
            if (wagoPlc!=null)
            {
                if (!wagoPlc.FieldBusNode.IsOnline)//если не онлайн
                {
                    //инициализируем
                    wagoPlc.FieldBusNode.InitializeModbusMaster();
                    //вычисляем доступность узла методом CheckOnline (последний результат результат также удет доступен из свойства IsOnline):
                    wagoPlc.FieldBusNode.CheckOnline(); 
                }

                //todo: Oleg_ivo.WAGO.Devices.WagoPlcFactory.InitPLC(PLC plc) инициализация, специфичная для WAGO
            }
        }

        #endregion
    }
}
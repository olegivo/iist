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
    /// ������� ��� WAGO
    ///</summary>
    public class WagoPlcFactory : PlcFactory
    {
        private readonly IPhysicalChannelsFactory physicalChannelsFactory;
        private readonly WagoMetaFactory wagoMetaFactory;
        private IDistributedMeasurementInformationSystem dmis;

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="WagoPlcFactory" />.
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
        /// ������� ��� �� ������ ���� ������� ����
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
        /// ���������������� ���
        /// </summary>
        /// <param name="plc"></param>
        protected override void InitPLC(PLC plc)
        {
            base.InitPLC(plc);
            if (dmis.Settings.IsEmulationMode)
            {
                Console.WriteLine(
                    "� ���������� ������� IsEmulationMode = true, ������� ������������� ������� PLC � ���������� �� Modbus �� ���������. ������ {0} ����� � ��������� Offline",
                    plc);
                return;
            }

            WagoPlc wagoPlc = plc as WagoPlc;
            if (wagoPlc!=null)
            {
                if (!wagoPlc.FieldBusNode.IsOnline)//���� �� ������
                {
                    //��������������
                    wagoPlc.FieldBusNode.InitializeModbusMaster();
                    //��������� ����������� ���� ������� CheckOnline (��������� ��������� ��������� ����� ���� �������� �� �������� IsOnline):
                    wagoPlc.FieldBusNode.CheckOnline(); 
                }

                //todo: Oleg_ivo.WAGO.Devices.WagoPlcFactory.InitPLC(PLC plc) �������������, ����������� ��� WAGO
            }
        }

        #endregion
    }
}
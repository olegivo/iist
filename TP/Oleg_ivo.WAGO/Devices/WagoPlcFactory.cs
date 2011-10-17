using System;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.WAGO.Devices
{
    ///<summary>
    /// ������� ��� WAGO
    ///</summary>
    public class WagoPlcFactory : PlcFactory
    {
        #region Singleton

        private static WagoPlcFactory _instance;

        ///<summary>
        /// ������������ ���������
        ///</summary>
        public static WagoPlcFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WagoPlcFactory();
                }
                return _instance;
            }
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="WagoPlcFactory" />.
        /// </summary>
        private WagoPlcFactory()
        {
        }

        #endregion

        ///<summary>
        /// ������� ��� �� ������ ���� ������� ����
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        public override PLC CreatePLC(FieldBusNode fieldBusNode)
        {
            if (fieldBusNode == null) throw new ArgumentNullException("fieldBusNode");

            WagoPlc plc = new WagoPlc(fieldBusNode);
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
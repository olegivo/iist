using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc.Ports
{
    ///<summary>
    /// ��������� ����������� � ������� ����
    ///</summary>
    public abstract class FieldBusPortParameters
    {
        private readonly FieldBusType _fieldBusType;

        ///<summary>
        ///��������� ����������� � ������� ����
        ///</summary>
        ///<param name="fieldBusType"></param>
        protected FieldBusPortParameters(FieldBusType fieldBusType)
        {
            _fieldBusType = fieldBusType;
        }

        ///<summary>
        /// �������� ���������� ������� ����
        ///</summary>
        public FieldBusNodeAddressCollection PhysicalAddressRange//todo:rename OR ��� ��� ����?
        {
            get { return DistributedMeasurementInformationSystemBase.Instance.Settings.FieldBusLoadOptions[FieldBusType].FieldBusNodeAddresses; }
        }

        ///<summary>
        /// ��� ������� ����
        ///</summary>
        public FieldBusType FieldBusType
        {
            get { return _fieldBusType; }
        }
    }
}
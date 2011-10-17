using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc
{
    /// <summary>
    /// ����� �������� ������� ����
    /// </summary>
    public class FieldBusLoadOptions
    {
        /// <summary>
        /// ����� �������� ������� ����
        /// </summary>
        /// <param name="fieldBusType"></param>
        public FieldBusLoadOptions(FieldBusType fieldBusType)
        {
            FieldBusType = fieldBusType;
        }

        /// <summary>
        /// ��� ������� ����
        /// </summary>
        public FieldBusType FieldBusType { get; private set; }

        /// <summary>
        /// ����� �������� ����� ������� ��� 
        /// </summary>
        public MeasurementSystemLevelLoadOptions FieldNodesLevel { get; set; }

        /// <summary>
        /// ����� �������� ���������� �������
        /// </summary>
        public MeasurementSystemLevelLoadOptions PhysicalChannelsLevel { get; set; }

        /// <summary>
        /// ����� �������� ����� ���������� �������
        /// </summary>
        public MeasurementSystemLevelLoadOptions LogicalChannelsLevel { get; set; }

        /// <summary>
        /// ��������� ������� ��� �� ����
        /// </summary>
        public FieldBusNodeAddressCollection FieldBusNodeAddresses { get; set; }
 
    }
}
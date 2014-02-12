using System.Collections.Generic;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc
{
    ///<summary>
    /// ��������� ������������� �������
    ///</summary>
    public class DistributedSystemSettings : IDistributedSystemSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"></see> class.
        /// </summary>
        public DistributedSystemSettings()
        {
            FieldBusLoadOptions = new Dictionary<FieldBusType, FieldBusLoadOptions>();
            //InitLoadOptions();
        }

        /// <summary>
        /// ����� �������� ������� ���
        /// </summary>
        public Dictionary<FieldBusType, FieldBusLoadOptions> FieldBusLoadOptions { get; set; }

        /// <summary>
        /// �������� �������� (������������ ��� ��������� � ���������������� �����)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ����� �������� (��������� � �������� ����������� �� �����)
        /// </summary>
        public bool IsEmulationMode { get; set; }
    }
}
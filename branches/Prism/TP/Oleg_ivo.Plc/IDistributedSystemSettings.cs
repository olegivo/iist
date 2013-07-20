using System.Collections.Generic;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc
{
    public interface IDistributedSystemSettings
    {
        /// <summary>
        /// ����� �������� ������� ���
        /// </summary>
        Dictionary<FieldBusType, FieldBusLoadOptions> FieldBusLoadOptions { get; }

        /// <summary>
        /// ����� �������� (��������� � �������� ����������� �� �����)
        /// </summary>
        bool IsEmulationMode { get; set; }
    }
}
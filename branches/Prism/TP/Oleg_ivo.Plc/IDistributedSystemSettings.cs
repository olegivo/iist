using System.Collections.Generic;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc
{
    public interface IDistributedSystemSettings
    {
        /// <summary>
        /// �������� �������� (������������ ��� ��������� � ���������������� �����)
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// ����� �������� (��������� � �������� ����������� �� �����)
        /// </summary>
        bool IsEmulationMode { get; set; }

        /// <summary>
        /// ����� �������� ������� ���
        /// </summary>
        Dictionary<FieldBusType, FieldBusLoadOptions> FieldBusLoadOptions { get; set; }
    }
}
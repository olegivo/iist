using System.ComponentModel;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    /// ��� ������� ����
    ///</summary>
    public enum FieldBusType
    {
        [Description("����������� ��� ������� ����")]
        Unknown = 0,
        [Description("������� ���� RS-232")]
        RS232 = 1,
        [Description("������� ���� RS-485")]
        RS485 = 2,
        [Description("������� ���� Ethernet")]
        Ethernet = 3
    }
}
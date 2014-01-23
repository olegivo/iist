using System.ComponentModel;

namespace Oleg_ivo.Plc.FieldBus
{
    ///<summary>
    /// Тип полевой шины
    ///</summary>
    public enum FieldBusType
    {
        [Description("Неизвестный тип полевой шины")]
        Unknown = 0,
        [Description("Полевая шина RS-232")]
        RS232 = 1,
        [Description("Полевая шина RS-485")]
        RS485 = 2,
        [Description("Полевая шина Ethernet")]
        Ethernet = 3
    }
}
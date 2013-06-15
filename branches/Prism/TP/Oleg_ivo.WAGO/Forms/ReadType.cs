using System.ComponentModel;

namespace Oleg_ivo.WAGO.Forms
{
    enum ReadType
    {
        [Description("ReadCoils")]
        ReadCoils=1,
        [Description("ReadInputRegisters")]
        ReadInputRegisters=2,
        [Description("ReadHoldingRegisters")]
        ReadHoldingRegister=3
    }
}
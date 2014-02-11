using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// ������� ���
    ///</summary>
    public interface IPlcFactory
    {
        ///<summary>
        /// ������� ��� �� ������ ���� ������� ����
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        PLC CreatePLC(FieldBusNode fieldBusNode);
    }
}
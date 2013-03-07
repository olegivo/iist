using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Contollers;
using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// ������� ���������� �������
    ///</summary>
    public interface IPhysicalChannelsFactory
    {
        ///<summary>
        /// ��������� ���������� ������
        ///</summary>
        ///<param name="plc"></param>
        ///<returns></returns>
        PhysicalChannelCollection BuildPhysicalChannels(PLC plc);

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ���� ������� ����
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        PhysicalChannelCollection LoadPhysicalChannels(FieldBusNode fieldBusNode);

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ����������� ������
        ///</summary>
        ///<param name="fieldBusNode"></param>
        ///<returns></returns>
        void SavePhysicalChannels(FieldBusNode fieldBusNode);
    }
}
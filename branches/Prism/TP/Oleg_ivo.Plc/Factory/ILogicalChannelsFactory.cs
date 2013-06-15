using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// ������� ���������� �������
    ///</summary>
    public interface ILogicalChannelsFactory
    {
        ///<summary>
        ///
        ///</summary>
        ///<param name="physicalChannel"></param>
        ///<returns></returns>
        LogicalChannelCollection BuildLogicalChannel(PhysicalChannel physicalChannel);

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ����������� ������
        ///</summary>
        ///<returns></returns>
        LogicalChannelCollection LoadLogicalChannels(PhysicalChannel physicalChannel);

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ����������� ������
        ///</summary>
        ///<returns></returns>
        void SaveLogicalChannels(PhysicalChannel physicalChannel);
    }
}
namespace Oleg_ivo.Plc.Channels
{
    ///<summary>
    /// ������� ���������� ����� (������ ��� ������)
    ///</summary>
    public class InputLogicalChannel : LogicalChannel
    {
        ///<summary>
        ///
        ///</summary>
        ///<param name="physicalChannel"></param>
        ///<param name="addressShift"></param>
        ///<param name="channelSize"></param>
        public InputLogicalChannel(PhysicalChannel physicalChannel, ushort addressShift, ushort channelSize)
            : base(physicalChannel, addressShift, channelSize)
        {
        }
    }
}
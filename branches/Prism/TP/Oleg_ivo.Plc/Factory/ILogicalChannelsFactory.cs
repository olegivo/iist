using System;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.Plc.Factory
{
    ///<summary>
    /// ������� ���������� �������
    ///</summary>
    public interface ILogicalChannelsFactory
    {
        /// <summary>
        ///  ��������� ��������� ���������� ������� �� ��������� ��� ������� ����������� ������ (�.�. ��������������� ����������� ������ �� �����������)
        /// </summary>
        /// <param name="physicalChannel"></param>
        /// <returns></returns>
        LogicalChannelCollection BuildDefaultLogicalChannels(PhysicalChannel physicalChannel);

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ����������� ������
        ///</summary>
        ///<returns></returns>
        LogicalChannelCollection LoadLogicalChannels(PhysicalChannel physicalChannel);

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ����������� ������
        ///</summary>
        ///<returns></returns>
        [Obsolete("������ ������� ������ ������� ������������ DataContext.SubmitChanges")]
        void SaveLogicalChannels(PhysicalChannel physicalChannel);

        /// <summary>
        /// ������� ������ ����������� ������ ��� ����������� ������ (� ������� ������� ������ ������ ����������� ������)
        /// </summary>
        /// <param name="physicalChannel"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        LogicalChannel CreateLogicalChannelTemplate(PhysicalChannel physicalChannel, Entities.LogicalChannel entity = null);
    }
}
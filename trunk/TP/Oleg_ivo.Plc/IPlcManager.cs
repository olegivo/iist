using System.Collections.Generic;
using System.ComponentModel;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.Plc.FieldBus;
using Oleg_ivo.Plc.FieldBus.FieldBusManagers;

namespace Oleg_ivo.Plc
{
    public interface IPlcManager
    {
        ///<summary>
        /// ���������� ������� ���
        ///</summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(true)]
        List<FieldBusManager> FieldBusManagers { get; }

        ///<summary>
        ///
        ///</summary>
        LogicalChannelCollection LogicalChannels { get; }

        ///<summary>
        /// ������� ����� ������� ����
        ///</summary>
        ///<returns></returns>
        IFieldBusNodeFactory FieldBusNodesFactory { get; }

        ///<summary>
        /// ������� ������� ���
        ///</summary>
        ///<returns></returns>
        IFieldBusFactory FieldBusFactory { get; }

        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        IPhysicalChannelsFactory PhysicalChannelsFactory { get; }

        /// <summary>
        /// ������� ���������� �������
        /// </summary>
        ILogicalChannelsFactory LogicalChannelsFactory { get; }

        ///<summary>
        /// ��������� ������������ ������� ��� �������
        ///</summary>
        ///<param name="cleanBeforeBuild">������� ����� �����������</param>
        ///<param name="fieldBusType">��� ������� ����</param>
        void BuildFieldBuses(bool cleanBeforeBuild, FieldBusType fieldBusType);

        void Dispose();
        string ToString();
        
        void Save();
    }
}
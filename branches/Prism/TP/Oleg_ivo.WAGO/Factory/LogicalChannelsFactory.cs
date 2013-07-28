using System;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Modules;
using Oleg_ivo.Plc.Factory;

namespace Oleg_ivo.WAGO.Factory
{
    ///<summary>
    /// ������� ���������� �������
    ///</summary>
    public class LogicalChannelsFactory : ILogicalChannelsFactory
    {
        private readonly IDistributedMeasurementInformationSystem dmis;

        public LogicalChannelsFactory(IDistributedMeasurementInformationSystem dmis)
        {
            this.dmis = Enforce.ArgumentNotNull(dmis, "dmis");
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="physicalChannel"></param>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        ///<returns></returns>
        public LogicalChannelCollection BuildLogicalChannel(PhysicalChannel physicalChannel)
        {
            LogicalChannelCollection channelCollection = new LogicalChannelCollection();
            IOModule ioModule = physicalChannel.IOModule;

            if (ioModule!=null)
            {
                bool isAnalog = ioModule.IsAnalog;
                bool isDiscrete = ioModule.IsDiscrete;
                ushort size = ioModule.Size;
                ushort shift = 0;//������� �������� ������

                ushort count = (ushort)(isAnalog && !isDiscrete ? size / 16 : size);//���������� ��
                ushort logicalChannelSize = (ushort)(size / count);//������ ������� ������

                for (int i = 0; i < count; i++, shift+=1/*logicalChannelSize*/)
                {
                    //todo:����� ���� ������� ���������� �����
                    LogicalChannel logicalChannel;
                    if (ioModule.IsInput && ioModule.IsOutput)
                        logicalChannel = new InputOutputLogicalChannel(physicalChannel, shift, logicalChannelSize);
                    else if (ioModule.IsInput)
                        logicalChannel = new InputLogicalChannel(physicalChannel, shift, logicalChannelSize);
                    else if (ioModule.IsOutput)
                        logicalChannel = new OutputLogicalChannel(physicalChannel, shift, logicalChannelSize);
                    else
                        throw new ArgumentOutOfRangeException("physicalChannel",
                                                              "������ �����-������ ����������� ������ �� �������� �� �������, �� ��������");
                    logicalChannel.IsEmulationMode = dmis.Settings.IsEmulationMode;
                    channelCollection.Add(logicalChannel);
                }
            }

            return channelCollection;
        }

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ����������� ������
        ///</summary>
        ///<returns></returns>
        public LogicalChannelCollection LoadLogicalChannels(PhysicalChannel physicalChannel)
        {
            LogicalChannelsDAC logicalChannelsDAC = new LogicalChannelsDAC {LogicalChannelsFactory = this};
            return logicalChannelsDAC.GetChannels(physicalChannel);
        }

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ����������� ������
        ///</summary>
        ///<returns></returns>
        public void SaveLogicalChannels(PhysicalChannel physicalChannel)
        {
            LogicalChannelsDAC logicalChannelsDAC = new LogicalChannelsDAC {LogicalChannelsFactory = this};
            logicalChannelsDAC.SaveChannels(physicalChannel);
        }
    }
}
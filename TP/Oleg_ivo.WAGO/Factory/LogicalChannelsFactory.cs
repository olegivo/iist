using System;
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
        #region fields
        private static LogicalChannelsFactory instance;

        #endregion

        #region properties

        ///<summary>
        ///
        ///</summary>
        public static LogicalChannelsFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogicalChannelsFactory();
                }
                return instance;
            }
        }

        #endregion

        #region constructors

        ///<summary>
        ///
        ///</summary>
        protected LogicalChannelsFactory()
        {
            
        }
        #endregion

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
                    if (ioModule.IsInput && ioModule.IsOutput)
                    {
                        channelCollection.Add(new InputOutputLogicalChannel(physicalChannel, shift, logicalChannelSize));
                    }
                    else if (ioModule.IsInput)
                    {
                        channelCollection.Add(new InputLogicalChannel(physicalChannel, shift, logicalChannelSize));
                    }
                    else if (ioModule.IsOutput)
                    {
                        channelCollection.Add(new OutputLogicalChannel(physicalChannel, shift, logicalChannelSize));
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("physicalChannel", "������ �����-������ ����������� ������ �� �������� �� �������, �� ��������");
                    }
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
            LogicalChannelsDAC logicalChannelsDAC = new LogicalChannelsDAC();
            return logicalChannelsDAC.GetChannels(physicalChannel);
        }

        ///<summary>
        /// ��������� ����������� ���������� ������ ��� ����������� ������
        ///</summary>
        ///<returns></returns>
        public void SaveLogicalChannels(PhysicalChannel physicalChannel)
        {
            LogicalChannelsDAC logicalChannelsDAC = new LogicalChannelsDAC();
            logicalChannelsDAC.SaveChannels(physicalChannel);
        }
    }
}
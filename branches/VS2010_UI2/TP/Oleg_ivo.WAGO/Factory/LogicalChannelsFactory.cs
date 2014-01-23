using System;
using Oleg_ivo.Plc.Channels;
using Oleg_ivo.Plc.Devices.Modules;
using Oleg_ivo.Plc.Factory;

namespace Oleg_ivo.WAGO.Factory
{
    ///<summary>
    /// ‘абрика логических каналов
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
                ushort shift = 0;//текущее смещение адреса

                ushort count = (ushort)(isAnalog && !isDiscrete ? size / 16 : size);//количество Ћ 
                ushort logicalChannelSize = (ushort)(size / count);//размер каждого канала

                for (int i = 0; i < count; i++, shift+=1/*logicalChannelSize*/)
                {
                    //todo:«десь надо строить логический канал
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
                        throw new ArgumentOutOfRangeException("physicalChannel", "ћодуль ввода-вывода физического канала не €вл€етс€ ни входным, ни выходным");
                    }
                }                
            }

            return channelCollection;
        }

        ///<summary>
        /// «агрузить настроенные логические каналы дл€ физического канала
        ///</summary>
        ///<returns></returns>
        public LogicalChannelCollection LoadLogicalChannels(PhysicalChannel physicalChannel)
        {
            LogicalChannelsDAC logicalChannelsDAC = new LogicalChannelsDAC();
            return logicalChannelsDAC.GetChannels(physicalChannel);
        }

        ///<summary>
        /// «агрузить настроенные логические каналы дл€ физического канала
        ///</summary>
        ///<returns></returns>
        public void SaveLogicalChannels(PhysicalChannel physicalChannel)
        {
            LogicalChannelsDAC logicalChannelsDAC = new LogicalChannelsDAC();
            logicalChannelsDAC.SaveChannels(physicalChannel);
        }
    }
}
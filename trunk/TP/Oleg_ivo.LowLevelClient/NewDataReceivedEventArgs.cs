using System;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    /// <summary>
    /// 
    /// </summary>
    public class NewDataReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public LogicalChannel LogicalChannel { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logicalChannel"></param>
        /// <param name="value"></param>
        public NewDataReceivedEventArgs(LogicalChannel logicalChannel, object value)
        {
            LogicalChannel = logicalChannel;
            Value = value;
        }
    }
}
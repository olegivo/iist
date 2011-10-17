using System;
using DMS.Common.Messages;

namespace DMS.Common.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class DataEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public InternalLogicalChannelDataMessage Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public DataEventArgs(InternalLogicalChannelDataMessage message)
        {
            Message = message;
        }
    }
}
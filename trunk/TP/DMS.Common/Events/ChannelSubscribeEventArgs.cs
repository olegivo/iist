using System;
using DMS.Common.Messages;

namespace DMS.Common.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class ChannelSubscribeEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public ChannelSubscribeMessage Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ChannelSubscribeEventArgs(ChannelSubscribeMessage message)
        {
            Message = message;
        }
    }
}
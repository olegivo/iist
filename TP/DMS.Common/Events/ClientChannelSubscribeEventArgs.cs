using System;
using DMS.Common.Messages;

namespace DMS.Common.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientChannelSubscribeEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public ChannelSubscribeMessage Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ClientChannelSubscribeEventArgs(ChannelSubscribeMessage message)
        {
            Message = message;
        }
    }
}
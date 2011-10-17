using System;
using DMS.Common.Messages;

namespace Oleg_ivo.LowLevelClient
{
    /// <summary>
    /// 
    /// </summary>
    public class ChannelSubscribeEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public ChannelSubscribeMessage Message { get; set; }

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
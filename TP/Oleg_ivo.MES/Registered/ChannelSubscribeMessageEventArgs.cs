using System;
using DMS.Common.Messages;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// 
    /// </summary>
    public class ChannelSubscribeMessageEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public ChannelSubscribeMessage Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ChannelSubscribeMessageEventArgs(ChannelSubscribeMessage message)
        {
            Message = message;
        }
    }
}
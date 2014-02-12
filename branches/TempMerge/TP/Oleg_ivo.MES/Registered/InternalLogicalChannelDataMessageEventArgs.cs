using System;
using DMS.Common.Messages;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// 
    /// </summary>
    public class InternalLogicalChannelDataMessageEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public InternalLogicalChannelDataMessage Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public InternalLogicalChannelDataMessageEventArgs(InternalLogicalChannelDataMessage message)
        {
            Message = message;
        }
    }
}
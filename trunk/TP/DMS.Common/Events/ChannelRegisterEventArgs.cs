using System;
using DMS.Common.Messages;

namespace DMS.Common.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class ChannelRegisterEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public ChannelRegistrationMessage Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ChannelRegisterEventArgs(ChannelRegistrationMessage message)
        {
            Message = message;
        }
    }
}
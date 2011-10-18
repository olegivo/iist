using System;
using DMS.Common.Messages;
using Oleg_ivo.MES.Registered;

namespace Oleg_ivo.MES.Low
{
    /// <summary>
    /// 
    /// </summary>
    public class LowRegisteredLogicalChannelSubscribeEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public RegisteredLowLevelClient RegisteredLowLevelClient { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ChannelRegistrationMessage ChannelRegistrationMessage { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredLowLevelClient"></param>
        /// <param name="message"></param>
        public LowRegisteredLogicalChannelSubscribeEventArgs(RegisteredLowLevelClient registeredLowLevelClient, ChannelRegistrationMessage message)
        {
            RegisteredLowLevelClient = registeredLowLevelClient;
            ChannelRegistrationMessage = message;
        }
    }
}
using System;
using DMS.Common.Messages;
using Oleg_ivo.MES.Registered;

namespace Oleg_ivo.MES.High
{
    /// <summary>
    /// 
    /// </summary>
    public class HighRegisteredLogicalChannelSubscribeEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public RegisteredHighLevelClient RegisteredHighLevelClient { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ChannelSubscribeMessage ChannelSubscribeMessage { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredHighLevelClient"></param>
        /// <param name="message"></param>
        public HighRegisteredLogicalChannelSubscribeEventArgs(RegisteredHighLevelClient registeredHighLevelClient, ChannelSubscribeMessage message)
        {
            RegisteredHighLevelClient = registeredHighLevelClient;
            ChannelSubscribeMessage = message;
        }
    }
}
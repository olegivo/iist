using DMS.Common.Events;
using DMS.Common.Messages;
using Oleg_ivo.MES.Registered;

namespace Oleg_ivo.MES.High
{
    /// <summary>
    /// 
    /// </summary>
    public class HighRegisteredLogicalChannelSubscribeEventArgs : MessageEventArgs<ChannelSubscribeMessage>
    {
        /// <summary>
        /// 
        /// </summary>
        public RegisteredHighLevelClient RegisteredHighLevelClient { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredHighLevelClient"></param>
        /// <param name="message"></param>
        public HighRegisteredLogicalChannelSubscribeEventArgs(RegisteredHighLevelClient registeredHighLevelClient, ChannelSubscribeMessage message) : base(message)
        {
            RegisteredHighLevelClient = registeredHighLevelClient;
        }
    }
}
using System;

namespace TP.WPF.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class SendControlMessageEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="value"></param>
        public SendControlMessageEventArgs(int channelId, object value)
        {
            ChannelId = channelId;
            Value = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ChannelId { get; private set; }
    }
}
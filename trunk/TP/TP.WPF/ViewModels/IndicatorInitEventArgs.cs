using System;
using DMS.Common.Messages;

namespace TP.WPF.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class IndicatorInitEventArgs : EventArgs
    {
        public ChannelRegistrationMessage Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"> </param>
        public IndicatorInitEventArgs(ChannelRegistrationMessage message)
        {
            Message = message;
        }
    }
}
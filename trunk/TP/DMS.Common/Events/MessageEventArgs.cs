using System;
using DMS.Common.Messages;

namespace DMS.Common.Events
{
    public class MessageEventArgs<TMessage> : EventArgs where TMessage : InternalMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public TMessage Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MessageEventArgs(TMessage message)
        {
            Message = message;
        }
    }
}
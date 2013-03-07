using System;
using DMS.Common.Messages;

namespace Oleg_ivo.MES
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public InternalErrorMessage Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ErrorReceivedEventArgs(InternalErrorMessage message)
        {
            Message = message;
        }
    }
}
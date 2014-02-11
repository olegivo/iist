using System;
using DMS.Common.Messages;
using Oleg_ivo.MES.Registered;

namespace Oleg_ivo.MES.Low
{
    /// <summary>
    /// 
    /// </summary>
    public class LowLevelClientEventArgs : EventArgs
    {
        /// <summary>
        /// Обратный вызов для клиента
        /// </summary>
        public RegisteredLowLevelClient RegisteredLowLevelClient { get; private set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public InternalMessage Message { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredLowLevelClient"></param>
        /// <param name="message"></param>
        public LowLevelClientEventArgs(RegisteredLowLevelClient registeredLowLevelClient, InternalMessage message)
        {
            RegisteredLowLevelClient = registeredLowLevelClient;
            Message = message;
        }
    }
}
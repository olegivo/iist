using System;
using DMS.Common.Messages;
using Oleg_ivo.MES.Registered;

namespace Oleg_ivo.MES.High
{
    /// <summary>
    /// 
    /// </summary>
    public class HighLevelClientEventArgs : EventArgs
    {
        /// <summary>
        /// Обратный вызов для клиента
        /// </summary>
        public RegisteredHighLevelClient RegisteredHighLevelClient { get; private set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public InternalMessage Message { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredHighLevelClient"></param>
        /// <param name="message"></param>
        public HighLevelClientEventArgs(RegisteredHighLevelClient registeredHighLevelClient, InternalMessage message)
        {
            RegisteredHighLevelClient = registeredHighLevelClient;
            Message = message;
        }
    }
}
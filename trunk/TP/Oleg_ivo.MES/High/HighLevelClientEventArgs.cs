using DMS.Common.Events;
using DMS.Common.Messages;
using Oleg_ivo.MES.Registered;

namespace Oleg_ivo.MES.High
{
    /// <summary>
    /// 
    /// </summary>
    public class HighLevelClientEventArgs : MessageEventArgs<InternalMessage>
    {
        /// <summary>
        /// Обратный вызов для клиента
        /// </summary>
        public RegisteredHighLevelClient RegisteredHighLevelClient { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="registeredHighLevelClient"></param>
        /// <param name="message"></param>
        public HighLevelClientEventArgs(RegisteredHighLevelClient registeredHighLevelClient, InternalMessage message) : base(message)
        {
            RegisteredHighLevelClient = registeredHighLevelClient;
        }
    }
}
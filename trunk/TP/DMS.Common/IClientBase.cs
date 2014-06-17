using System;
using System.ServiceModel;

namespace DMS.Common
{
    public interface IClientBase : IDisposable, IErrorSender
    {
        /// <summary></summary>
        Func<string> GetRegName { get; set; }

        ICommunicationObject Proxy { get; }

        /// <summary>
        /// Послать в систему обмена сообщениями сообщение о регистрации
        /// </summary>
        void Register();

        void AbortProxy();
    }
}
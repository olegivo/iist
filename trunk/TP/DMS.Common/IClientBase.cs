using System;

namespace DMS.Common
{
    public interface IClientBase : IDisposable, IErrorSender
    {
        /// <summary></summary>
        Func<string> GetRegName { get; set; }
    }
}
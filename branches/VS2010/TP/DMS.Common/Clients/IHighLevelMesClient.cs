using System.ServiceModel;
using DMS.Common.Messages;

namespace DMS.Common.Clients
{
    /// <summary>
    /// Клиент (верхнего уровня) системы обмена сообщениями
    /// </summary>
    public interface IHighLevelMesClient : IMesClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="price"></param>
        [OperationContract(IsOneWay = true)]
        void PriceUpdate(string ticker, double price);

        /// <summary>
        /// Чтение данных из контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        void ReadChannel(IInternalMessage message);

    }
}
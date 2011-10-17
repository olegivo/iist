﻿using System.ServiceModel;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem.LowLevel
{
    /// <summary>
    /// Контракт обратного вызова от службы к клиенту нижнего уровня.
    /// Представляет операции, доступные для вызова с серверной стороны.
    /// </summary>
    public interface ILowLevelClientCallback : ILowLevelMessageExchangeSystem, IClientCallback
    {
        /// <summary>
        /// Подписка на чтение контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void ChannelSubscribe(ChannelSubscribeMessage message);

        /// <summary>
        /// Отписка от чтения контролируемого канала
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void ChannelUnSubscribe(ChannelSubscribeMessage message);

    }
}
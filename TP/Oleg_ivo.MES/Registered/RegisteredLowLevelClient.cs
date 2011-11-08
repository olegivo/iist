using System;
using DMS.Common.MessageExchangeSystem.LowLevel;
using DMS.Common.Messages;
using NLog;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// Зарегистрированный клиент нижнего уровня
    /// </summary>
    public class RegisteredLowLevelClient : RegisteredClient<ILowLevelClientCallback>
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Зарегистрировать канал
        /// </summary>
        /// <param name="message"></param>
        public void ChannelRegister(ChannelRegistrationMessage message)
        {
            RegisteredLogicalChannel registeredLogicalChannel = new RegisteredLogicalChannel(message.LogicalChannelId, message.DataMode);
            AddRegisteredChannel(registeredLogicalChannel);

            registeredLogicalChannel.Subscribed += registeredLogicalChannel_Subscribed;
            registeredLogicalChannel.UnSubscribed += registeredLogicalChannel_UnSubscribed;

            //подписка на событие записи канала
            registeredLogicalChannel.Write += registeredLogicalChannel_Write;
        }
                

        private void registeredLogicalChannel_Write(object sender, InternalLogicalChannelDataMessageEventArgs e)
        {
            SendWriteToClient(e.Message);
        }

        private void SendWriteToClient(InternalLogicalChannelDataMessage message)
        {
            lock (Callbacks)
                foreach (ILowLevelClientCallback callback in Callbacks)
                    try
                    {
                        callback.SendWriteToClient(message);
                    }
                    catch (Exception ex)
                    {
                        log.ErrorException("Ошибка при отправке новых данных клиенту: {0}",
                                          ex);
                        throw;
                    }
        }

        void registeredLogicalChannel_Subscribed(object sender, ChannelSubscribeMessageEventArgs e)
        {
            //канал сообщает, что появились подписчики на канал. Уведомляем об этом клиент нижнего уровня, пусть активирует канал
            lock (Callbacks)
                foreach (ILowLevelClientCallback callback in Callbacks)
                    try
                    {
                        callback.ChannelSubscribe(e.Message);
                    }
                    catch (Exception ex)
                    {
                        log.ErrorException("Ошибка при уведомлении клиента о первой подписке на канал: {0}",
                                          ex);
                        throw;
                    }

        }

        private void registeredLogicalChannel_UnSubscribed(object sender, ChannelSubscribeMessageEventArgs e)
        {
            //канал сообщает, что все подписчики на канал отписаны. Уведомляем об этом клиент нижнего уровня, пусть деактивирует канал
            lock (Callbacks)
                foreach (ILowLevelClientCallback callback in Callbacks)
                    try
                    {
                        callback.ChannelUnSubscribe(e.Message);
                    }
                    catch (Exception ex)
                    {
                        log.ErrorException("Ошибка при уведомлении клиента о последней отписке от канала: {0}",
                                          ex);
                        throw;
                    }
        }

        /// <summary>
        /// Отменить регистрацию канала
        /// </summary>
        /// <param name="message"></param>
        public void ChannelUnRegister(ChannelRegistrationMessage message)
        {
            RegisteredLogicalChannel registeredLogicalChannel =
                GetRegisteredLogicalChannel(RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId,
                                                                                             message.DataMode));RemoveRegisteredChannel(registeredLogicalChannel);

            registeredLogicalChannel.Subscribed -= registeredLogicalChannel_Subscribed;
            registeredLogicalChannel.UnSubscribed -= registeredLogicalChannel_UnSubscribed;

            //отписка на событие чтения канала
            registeredLogicalChannel.Write -= registeredLogicalChannel_Write;
        }
    }
}
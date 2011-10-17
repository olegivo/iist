using System;
using DMS.Common.MessageExchangeSystem.LowLevel;
using DMS.Common.Messages;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// Зарегистрированный клиент нижнего уровня
    /// </summary>
    public class RegisteredLowLevelClient : RegisteredClient<ILowLevelClientCallback>
    {
        /// <summary>
        /// Зарегистрировать канал
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ChannelRegister(ChannelSubscribeMessage message)
        {
            RegisteredLogicalChannel registeredLogicalChannel = new RegisteredLogicalChannel(message.LogicalChannelId);
            AddRegisteredChannel(registeredLogicalChannel);

            registeredLogicalChannel.Subscribed += registeredLogicalChannel_Subscribed;
            registeredLogicalChannel.UnSubscribed += registeredLogicalChannel_UnSubscribed;
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
                        Console.WriteLine("Ошибка при уведомлении клиента о первой подписке на канал: {0}",
                                          ex.Message);
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
                        Console.WriteLine("Ошибка при уведомлении клиента о последней отписке от канала: {0}",
                                          ex.Message);
                        throw;
                    }
        }

        /// <summary>
        /// Отменить регистрацию канала
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ChannelUnRegister(ChannelSubscribeMessage message)
        {
            RegisteredLogicalChannel registeredLogicalChannel = GetRegisteredLogicalChannel(RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId));
            RemoveRegisteredChannel(registeredLogicalChannel);

            registeredLogicalChannel.Subscribed -= registeredLogicalChannel_Subscribed;
            registeredLogicalChannel.UnSubscribed -= registeredLogicalChannel_UnSubscribed;
        }
    }
}
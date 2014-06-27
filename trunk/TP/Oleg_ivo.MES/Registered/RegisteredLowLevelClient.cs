using System;
using Autofac;
using DMS.Common.Events;
using DMS.Common.MessageExchangeSystem.LowLevel;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.Base.Autofac.DependencyInjection;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// Зарегистрированный клиент нижнего уровня
    /// </summary>
    public class RegisteredLowLevelClient : RegisteredClient<ILowLevelClientCallback>
    {
        private readonly IComponentContext context;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public RegisteredLowLevelClient(IComponentContext context)
        {
            this.context = Enforce.ArgumentNotNull(context, "context");
        }

        /// <summary>
        /// Зарегистрировать канал
        /// </summary>
        /// <param name="message"></param>
        public void ChannelRegister(ChannelRegistrationMessage message)
        {
            var registeredLogicalChannel = new RegisteredLogicalChannelExtended
                (
                    message.LogicalChannelId, 
                    message.DataMode, message.IsDiscrete ? LogicalChannelState.Work : LogicalChannelState.Break/*дискретные каналы не имеют состояния => всегда Work*/)
                {
                    MinValue = message.MinValue,
                    MaxValue = message.MaxValue,
                    MinNormalValue = message.MinNormalValue,
                    MaxNormalValue = message.MaxNormalValue,
                    Description = message.Description
                };
            context.InjectAttributedProperties(registeredLogicalChannel);
            AddRegisteredChannel(registeredLogicalChannel);

            registeredLogicalChannel.Subscribed += registeredLogicalChannel_Subscribed;
            registeredLogicalChannel.UnSubscribed += registeredLogicalChannel_UnSubscribed;

            //подписка на событие записи канала
            registeredLogicalChannel.Write += registeredLogicalChannel_Write;
        }
                

        private void registeredLogicalChannel_Write(object sender, MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            SendWriteToClient(e.Message);
        }

        private void SendWriteToClient(InternalLogicalChannelDataMessage message)
        {
            try
            {
                IterateCallbacks(c=> c.SendWriteToClient(message));
            }
            catch (Exception ex)
            {
                log.Error("Ошибка при отправке новых данных клиенту: {0}", ex);
                throw;
            }
        }

        void registeredLogicalChannel_Subscribed(object sender, MessageEventArgs<ChannelSubscribeMessage> e)
        {
            //канал сообщает, что появились подписчики на канал. Уведомляем об этом клиент нижнего уровня, пусть активирует канал
            try
            {
                IterateCallbacks(c=> c.ChannelSubscribe(e.Message));
            }
            catch (Exception ex)
            {
                log.Error("Ошибка при уведомлении клиента о первой подписке на канал: {0}", ex);
                throw;
            }
        }

        private void registeredLogicalChannel_UnSubscribed(object sender, MessageEventArgs<ChannelSubscribeMessage> e)
        {
            //канал сообщает, что все подписчики на канал отписаны. Уведомляем об этом клиент нижнего уровня, пусть деактивирует канал
            try
            {
                IterateCallbacks(c=> c.ChannelUnSubscribe(e.Message));
            }
            catch (Exception ex)
            {
                log.Error("Ошибка при уведомлении клиента о последней отписке от канала: {0}", ex);
                throw;
            }
        }

        /// <summary>
        /// Отменить регистрацию канала
        /// </summary>
        /// <param name="message"></param>
        public void ChannelUnRegister(ChannelRegistrationMessage message)
        {
            RegisteredLogicalChannelExtended registeredLogicalChannel =
                GetRegisteredLogicalChannel(RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId,
                                                                                             message.DataMode));RemoveRegisteredChannel(registeredLogicalChannel);

            registeredLogicalChannel.Subscribed -= registeredLogicalChannel_Subscribed;
            registeredLogicalChannel.UnSubscribed -= registeredLogicalChannel_UnSubscribed;

            //отписка на событие чтения канала
            registeredLogicalChannel.Write -= registeredLogicalChannel_Write;
        }
    }
}
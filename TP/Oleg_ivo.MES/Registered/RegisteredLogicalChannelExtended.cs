using System;
using DMS.Common.Events;
using DMS.Common.MessageExchangeSystem.HighLevel;
using DMS.Common.Messages;
using Oleg_ivo.Base.Autofac.DependencyInjection;
using Oleg_ivo.MES.Logging;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// «арегистрированный логический канал
    /// </summary>
    public class RegisteredLogicalChannelExtended : RegisteredLogicalChannel
    {
        /// <summary>
        /// «арегистрированный логический канал
        /// </summary>
        /// <param name="id">»дентификатор канала</param>
        /// <param name="dataMode">–ежим данных канала</param>
        public RegisteredLogicalChannelExtended(int id, DataMode dataMode)
            : base(id)
        {
            DataMode = dataMode;
            State = LogicalChannelState.Off;
            ChangeState += RegisteredLogicalChannel_ChangeState;
            Read += RegisteredLogicalChannel_Read;
            Write += RegisteredLogicalChannel_Write;
        }

        [Dependency(Required = true)]
        public InternalMessageLogger InternalMessageLogger { get; set; }

        public LogicalChannelState State { get; private set; }

        void RegisteredLogicalChannel_Write(object sender, MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            //протоколировать записанные в канал данные
            InternalMessageLogger.ProtocolMessage(e.Message);
        }

        private void RegisteredLogicalChannel_Read(object sender, MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            //протоколировать прочтЄнные из канала данные
            InternalMessageLogger.ProtocolMessage(e.Message);
        }

        private void RegisteredLogicalChannel_ChangeState(object sender, MessageEventArgs<InternalLogicalChannelStateMessage> e)
        {
            //протоколировать состо€ние канала
            InternalMessageLogger.ProtocolEvent(e.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataMode">≈сли <see cref="DataMode.Unknown"/>, параметр не учитываетс€ при фильтрации</param>
        /// <returns></returns>
        public static Func<RegisteredLogicalChannelExtended, bool> GetFindChannelPredicate(int id, DataMode dataMode)
        {
            return
                (channel =>
                 channel.Id == id && (dataMode == DataMode.Unknown || (channel.DataMode & dataMode) == dataMode));
        }

        /// <summary>
        /// »зменение статуса канала
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelStateMessage>> ChangeState;

        /// <summary>
        /// „тение канала
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelDataMessage>> Read;

        /// <summary>
        /// «апись канала
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelDataMessage>> Write;

        private void InvokeChangeState(MessageEventArgs<InternalLogicalChannelStateMessage> e)
        {
            var handler = ChangeState;
            if (handler != null) handler(this, e);
        }

        private void InvokeRead(MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            var handler = Read;
            if (handler != null) handler(this, e);
        }

        private void InvokeWrite(MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            var handler = Write;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// ѕослать слушател€м канала сообщение о новых данных
        /// </summary>
        /// <param name="message"></param>
        public void InvokeRead(InternalLogicalChannelDataMessage message)
        {
            InvokeRead(new MessageEventArgs<InternalLogicalChannelDataMessage>(message));
        }

        /// <summary>
        /// ѕослать слушател€м канала сообщение о новых данных
        /// </summary>
        /// <param name="message"></param>
        public void InvokeChangeState(InternalLogicalChannelStateMessage message)
        {
            State = message.State;
            InvokeChangeState(new MessageEventArgs<InternalLogicalChannelStateMessage>(message));
        }

        /// <summary>
        /// ѕослать слушател€м канала сообщение о новых данных
        /// </summary>
        /// <param name="message"></param>
        public void InvokeWrite(InternalLogicalChannelDataMessage message)
        {
            InvokeWrite(new MessageEventArgs<InternalLogicalChannelDataMessage>(message));
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs<ChannelSubscribeMessage>> Subscribed;

        private int subscribedCount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void InvokeSubscribed(ChannelSubscribeMessage message)
        {
            //если это перва€ подписка, нужно сообщить слушател€м (владелец канала),
            //что кто-то подписалс€ и нужно активировать опрос канала
            if (subscribedCount == 0)
                InvokeSubscribed(new MessageEventArgs<ChannelSubscribeMessage>(message));

            subscribedCount++;
        }

        private void InvokeSubscribed(MessageEventArgs<ChannelSubscribeMessage> e)
        {
            var handler = Subscribed;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs<ChannelSubscribeMessage>> UnSubscribed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void InvokeUnSubscribed(ChannelSubscribeMessage message)
        {
            subscribedCount--;

            //если это была последн€€ отписка, нужно сообщить слушател€м (владелец канала),
            //что больше подписчиков нет и нужно деактивировать опрос канала
            if (subscribedCount == 0)
                InvokeUnSubscribed(new MessageEventArgs<ChannelSubscribeMessage>(message));
        }

        private void InvokeUnSubscribed(MessageEventArgs<ChannelSubscribeMessage> e)
        {
            var handler = UnSubscribed;
            if (handler != null) handler(this, e);
        }

    }
}
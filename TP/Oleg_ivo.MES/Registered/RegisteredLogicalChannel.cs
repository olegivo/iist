using System;
using DMS.Common.Messages;
using Oleg_ivo.MES.Logging;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// «арегистрированный логический канал
    /// </summary>
    public class RegisteredLogicalChannel
    {
        /// <summary>
        /// »дентификатор канала
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// «арегистрированный логический канал
        /// </summary>
        /// <param name="id">»дентификатор канала</param>
        public RegisteredLogicalChannel(int id)
        {
            Id = id;
            Read += RegisteredLogicalChannel_Read;
        }

        private void RegisteredLogicalChannel_Read(object sender, InternalLogicalChannelDataMessageEventArgs e)
        {
            //протоколировать прочтЄнные из канала данные
            InternalMessageLogger.Instance.ProtocolMessage(e.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Func<RegisteredLogicalChannel, bool> GetFindChannelPredicate(int id)
        {
            return (channel => channel.Id == id);
        }

        /// <summary>
        /// „тение канала
        /// </summary>
        public event EventHandler<InternalLogicalChannelDataMessageEventArgs> Read;

        private void InvokeRead(InternalLogicalChannelDataMessageEventArgs e)
        {
            EventHandler<InternalLogicalChannelDataMessageEventArgs> handler = Read;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// ѕослать слушател€м канала сообщение о новых данных
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void InvokeRead(InternalLogicalChannelDataMessage message)
        {
            InvokeRead(new InternalLogicalChannelDataMessageEventArgs(message));
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ChannelSubscribeMessageEventArgs> Subscribed;

        private int _subscribedCount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void InvokeSubscribed(ChannelSubscribeMessage message)
        {
            //если это перва€ подписка, нужно сообщить слушател€м (владелец канала),
            //что кто-то подписалс€ и нужно активировать опрос канала
            if (_subscribedCount == 0)
                InvokeSubscribed(new ChannelSubscribeMessageEventArgs(message)); 
            
            _subscribedCount++;
        }

        private void InvokeSubscribed(ChannelSubscribeMessageEventArgs e)
        {
            EventHandler<ChannelSubscribeMessageEventArgs> handler = Subscribed;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ChannelSubscribeMessageEventArgs> UnSubscribed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void InvokeUnSubscribed(ChannelSubscribeMessage message)
        {
            _subscribedCount--;

            //если это была последн€€ отписка, нужно сообщить слушател€м (владелец канала),
            //что больше подписчиков нет и нужно деактивировать опрос канала
            if (_subscribedCount == 0)
                InvokeUnSubscribed(new ChannelSubscribeMessageEventArgs(message));
        }

        private void InvokeUnSubscribed(ChannelSubscribeMessageEventArgs e)
        {
            EventHandler<ChannelSubscribeMessageEventArgs> handler = UnSubscribed;
            if (handler != null) handler(this, e);
        }

        
    }
}
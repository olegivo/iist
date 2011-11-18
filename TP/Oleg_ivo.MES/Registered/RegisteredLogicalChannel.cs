using System;
using DMS.Common.Messages;
using Oleg_ivo.MES.Logging;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// Зарегистрированный логический канал
    /// </summary>
    public class RegisteredLogicalChannel
    {
        /// <summary>
        /// Идентификатор канала
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Зарегистрированный логический канал
        /// </summary>
        /// <param name="id">Идентификатор канала</param>
        /// <param name="dataMode">Режим данных канала</param>
        public RegisteredLogicalChannel(int id, DataMode dataMode)
        {
            Id = id;
            DataMode = dataMode;
            Read += RegisteredLogicalChannel_Read;
            Write += RegisteredLogicalChannel_Write;
        }

        void RegisteredLogicalChannel_Write(object sender, InternalLogicalChannelDataMessageEventArgs e)
        {
            //протоколировать прочтённые из канала данные
            InternalMessageLogger.Instance.ProtocolMessage(e.Message);
        }

        private void RegisteredLogicalChannel_Read(object sender, InternalLogicalChannelDataMessageEventArgs e)
        {
            //протоколировать прочтённые из канала данные
            InternalMessageLogger.Instance.ProtocolMessage(e.Message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataMode">Если <see cref="DataMode.Unknown"/>, параметр не учитывается при фильтрации</param>
        /// <returns></returns>
        public static Func<RegisteredLogicalChannel, bool> GetFindChannelPredicate(int id, DataMode dataMode)
        {
            return
                (channel =>
                 channel.Id == id && (dataMode == DataMode.Unknown || (channel.DataMode & dataMode) == dataMode));
        }

        /// <summary>
        /// Режим данных канала
        /// </summary>
        public DataMode DataMode { get; set; }

        /// <summary>
        /// Чтение канала
        /// </summary>
        public event EventHandler<InternalLogicalChannelDataMessageEventArgs> Read;
        
        /// <summary>
        /// Запись канала
        /// </summary>
        public event EventHandler<InternalLogicalChannelDataMessageEventArgs> Write;

        private void InvokeRead(InternalLogicalChannelDataMessageEventArgs e)
        {
            EventHandler<InternalLogicalChannelDataMessageEventArgs> handler = Read;
            if (handler != null) handler(this, e);
        }

        private void InvokeWrite(InternalLogicalChannelDataMessageEventArgs e)
        {
            EventHandler<InternalLogicalChannelDataMessageEventArgs> handler = Write;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Послать слушателям канала сообщение о новых данных
        /// </summary>
        /// <param name="message"></param>
        public void InvokeRead(InternalLogicalChannelDataMessage message)
        {
            InvokeRead(new InternalLogicalChannelDataMessageEventArgs(message));
        }

        /// <summary>
        /// Послать слушателям канала сообщение о новых данных
        /// </summary>
        /// <param name="message"></param>
        public void InvokeWrite(InternalLogicalChannelDataMessage message)
        {
            InvokeWrite(new InternalLogicalChannelDataMessageEventArgs(message));
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
            //если это первая подписка, нужно сообщить слушателям (владелец канала),
            //что кто-то подписался и нужно активировать опрос канала
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

            //если это была последняя отписка, нужно сообщить слушателям (владелец канала),
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
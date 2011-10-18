using System;
using System.ServiceModel;
using DMS.Common.Events;
using DMS.Common.Messages;
#if IIST
using Oleg_ivo.CMU.ServiceReferenceIIST;
#else
#if BINDING_TCP
using Oleg_ivo.LowLevelClient.ServiceReferenceHomeTcp;
#else

#endif
#endif

namespace Oleg_ivo.LowLevelClient
{

    /// <summary>
    /// Реализация 
    /// </summary>
    [CallbackBehavior(IncludeExceptionDetailInFaults = true)]
    public class CallbackHandler : ILowLevelMessageExchangeSystemCallback// IClientCallback
    {

        private void OnNeedProtocol(object d)
        {
            EventHandler handler = NeedProtocol;
            if (handler != null) handler(d, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler NeedProtocol;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SendMessageToClient(InternalMessage message)
        {
            string s = string.Format("MessageExchangeSystem -> Client : {0}{1}", message.TimeStamp, Environment.NewLine);
            OnNeedProtocol(s);

            //todo:Oleg_ivo.CMU.CallbackHandler.SendMessageToClient - для проверки проброса исключений на сервер
/*
            TestException exception = new TestException("Мне прислали сообщение, но произошла тестовая ошибка");
            ExceptionDetail detail = new ExceptionDetail(exception);
            throw new FaultException<ExceptionDetail>(detail, exception.Message);
*/
        }

        #region SendMessageToClient

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncResult BeginSendMessageToClient(InternalMessage message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void EndSendMessageToClient(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ChannelSubscribe(ChannelSubscribeMessage message)
        {
            InvokeChannelSubsrcibe(new ChannelSubscribeEventArgs(message));
        }

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler<ChannelSubscribeEventArgs> ChannelSubscribed;

        private void InvokeChannelSubsrcibe(ChannelSubscribeEventArgs e)
        {
            EventHandler<ChannelSubscribeEventArgs> handler = ChannelSubscribed;
            if (handler != null) handler(this, e);
        }

        #region ChannelSubscribe

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncResult BeginChannelSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void EndChannelSubscribe(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ChannelUnSubscribe(ChannelSubscribeMessage message)
        {
            InvokeChannelUnSubsrcibe(new ChannelSubscribeEventArgs(message));
        }

        /// <summary>
        /// 
        /// </summary>
        public static event EventHandler<ChannelSubscribeEventArgs> ChannelUnSubscribed;

        private void InvokeChannelUnSubsrcibe(ChannelSubscribeEventArgs e)
        {
            EventHandler<ChannelSubscribeEventArgs> handler = ChannelUnSubscribed;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// Прочитан канал
        /// </summary>
        public static event EventHandler<DataEventArgs> HasWriteChannel;

        private void OnSendWriteToClient(InternalLogicalChannelDataMessage message)
        {
            if (HasWriteChannel != null) HasWriteChannel(this, new DataEventArgs(message));
        }

        #region ChannelUnSubscribe

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncResult BeginChannelUnSubscribe(ChannelSubscribeMessage message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void EndChannelUnSubscribe(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SendWriteToClient(InternalLogicalChannelDataMessage message)
        {
            OnSendWriteToClient(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncResult BeginSendWriteToClient(InternalLogicalChannelDataMessage message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void EndSendWriteToClient(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="price"></param>
        public void PriceUpdate(string ticker, double price)
        {
            OnNeedProtocol(price);

            Console.WriteLine("Получено извещение в : {0}. {1}:{2}", DateTime.Now, ticker, price);
        }

        #region PriceUpdate
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticker"></param>
        /// <param name="price"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncResult BeginPriceUpdate(string ticker, double price, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void EndPriceUpdate(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion

/*
        #region Члены IHighLevelMessageExchangeSystemCallback


        public void SendReadToClient(InternalLogicalChannelDataMessage message)
        {
            string s = string.Format("MessageExchangeSystem -> Client : Из канала [{0}] пришли новые данные [{1}]. {2}{3}", 
                message.LogicalChannelId,
                message.Value,
                message.TimeStamp, 
                Environment.NewLine);
            OnNeedProtocol(s);
        }

        public IAsyncResult BeginSendReadToClient(InternalLogicalChannelDataMessage message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndSendReadToClient(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion
*/
    }
}
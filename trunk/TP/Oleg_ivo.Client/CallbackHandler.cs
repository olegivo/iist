using System;
using System.ServiceModel;
using DMS.Common.Messages;
using Oleg_ivo.HighLevelClient.ServiceReferenceHomeTcp;
#if IIST
#if WSHTTP_BINDING
using Oleg_ivo.HighLevelClient.ServiceReferenceIISTwsDualHttp;
#else
#if TCP_BINDING

#else
using Oleg_ivo.HighLevelClient.ServiceReferenceIIST;
#endif
#endif
#else
#if WSHTTP_BINDING
using Oleg_ivo.HighLevelClient.ServiceReferenceHomeWsDualHttp;
#else
using Oleg_ivo.HighLevelClient.ServiceReferenceHome;
#endif
#endif

namespace Oleg_ivo.HighLevelClient
{

    /// <summary>
    /// Реализация 
    /// </summary>
    [CallbackBehavior(IncludeExceptionDetailInFaults = true)]
    [CallbackErrorHandlerBehavior(typeof(CustomErrorHandler))]
    public class CallbackHandler : IHighLevelMessageExchangeSystemCallback// IClientCallback
    {

        private void OnNeedProtocol(object d)
        {
            EventHandler handler = NeedProtocol;
            if (handler != null) handler(d, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler NeedProtocol;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ClientChannelSubscribeEventArgs> ChannelRegistered;

        private void OnChannelRegistered(ClientChannelSubscribeEventArgs e)
        {
            EventHandler<ClientChannelSubscribeEventArgs> handler = ChannelRegistered;
            if (handler != null) handler(null, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ClientChannelSubscribeEventArgs> ChannelUnRegistered;

        private void OnChannelUnRegistered(ClientChannelSubscribeEventArgs e)
        {
            EventHandler<ClientChannelSubscribeEventArgs> handler = ChannelUnRegistered;
            if (handler != null) handler(null, e);
        }

        #region Члены IHighLevelMessageExchangeSystemCallback
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SendMessageToClient(InternalMessage message)
        {
            string s = string.Format("MessageExchangeSystem -> Client : {0}", message.TimeStamp);
            OnNeedProtocol(s);

            //todo:Oleg_ivo.Client.CallbackHandler.SendMessageToClient - для проверки проброса исключений на сервер
            /*
                        TestException exception = new TestException("Мне прислали сообщение, но произошла тестовая ошибка");
                        ExceptionDetail detail = new ExceptionDetail(exception);
                        throw new FaultException<ExceptionDetail>(detail, exception.Message);
            */
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ChannelRegister(ChannelSubscribeMessage message)
        {
            OnChannelRegistered(new ClientChannelSubscribeEventArgs(message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncResult BeginChannelRegister(ChannelSubscribeMessage message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void EndChannelRegister(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ChannelUnRegister(ChannelSubscribeMessage message)
        {
            OnChannelUnRegistered(new ClientChannelSubscribeEventArgs(message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncResult BeginChannelUnRegister(ChannelSubscribeMessage message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void EndChannelUnRegister(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SendReadToClient(InternalLogicalChannelDataMessage message)
        {
            string s = string.Format("MessageExchangeSystem -> Client : Из канала [{0}] пришли новые данные [{1}]. {2}{3}",
                message.LogicalChannelId,
                message.Value,
                message.TimeStamp,
                Environment.NewLine);

            OnSendReadToClient(message);
            OnNeedProtocol(s);

#if LABVIEW
            Adapter.Instance.Send(message);
#endif
        }

        /// <summary>
        /// Прочитан канал
        /// </summary>
        public event EventHandler<DataEventArgs> HasReadChannel;

        private void OnSendReadToClient(InternalLogicalChannelDataMessage message)
        {
            EventHandler<DataEventArgs> handler = HasReadChannel;
            if (handler != null) handler(this, new DataEventArgs(message));
        }

        /// <summary>
        /// 
        /// </summary>
        public class DataEventArgs : EventArgs
        {
            /// <summary>
            /// 
            /// </summary>
            public InternalLogicalChannelDataMessage Message { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="message"></param>
            public DataEventArgs(InternalLogicalChannelDataMessage message)
            {
                Message = message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncResult BeginSendReadToClient(InternalLogicalChannelDataMessage message, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void EndSendReadToClient(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class ClientChannelSubscribeEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public ChannelSubscribeMessage Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ClientChannelSubscribeEventArgs(ChannelSubscribeMessage message)
        {
            Message = message;
        }
    }
}
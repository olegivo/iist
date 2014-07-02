using System;
using System.ServiceModel;
using DMS.Common.Events;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Communication;
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
    [ErrorBehavior(typeof(ServiceErrorHandler))]
    public class CallbackHandler : IHighLevelMessageExchangeSystemCallback// IClientCallback
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        [Obsolete]
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
        public event EventHandler<MessageEventArgs<ChannelRegistrationMessage>> ChannelRegistered;

        private void OnChannelRegistered(MessageEventArgs<ChannelRegistrationMessage> e)
        {
            var handler = ChannelRegistered;
            if (handler != null) handler(null, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MessageEventArgs<ChannelRegistrationMessage>> ChannelUnRegistered;

        private void OnChannelUnRegistered(MessageEventArgs<ChannelRegistrationMessage> e)
        {
            var handler = ChannelUnRegistered;
            if (handler != null) handler(null, e);
        }

        #region Члены IHighLevelMessageExchangeSystemCallback
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void SendMessageToClient(DMS.Common.Messages.InternalMessage message)
        {
            string s = string.Format("MessageExchangeSystem -> Client : {0}", message.TimeStamp);
            log.Debug(s);
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
        public void ChannelRegister(ChannelRegistrationMessage message)
        {
            OnChannelRegistered(new MessageEventArgs<ChannelRegistrationMessage>(message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncResult BeginChannelRegister(ChannelRegistrationMessage message, AsyncCallback callback, object asyncState)
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
        public void ChannelUnRegister(ChannelRegistrationMessage message)
        {
            OnChannelUnRegistered(new MessageEventArgs<ChannelRegistrationMessage>(message));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IAsyncResult BeginChannelUnRegister(ChannelRegistrationMessage message, AsyncCallback callback, object asyncState)
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
            log.Debug(s);
            OnNeedProtocol(s);

#if LABVIEW
            Adapter.Instance.Send(message);
#endif
        }

        /// <summary>
        /// Прочитан канал
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelDataMessage>> HasReadChannel;

        private void OnSendReadToClient(InternalLogicalChannelDataMessage message)
        {
            var handler = HasReadChannel;
            if (handler != null) handler(this, new MessageEventArgs<InternalLogicalChannelDataMessage>(message));
        }

        /// <summary>
        /// Изменилось состояние канала
        /// </summary>
        public event EventHandler<MessageEventArgs<InternalLogicalChannelStateMessage>> ChannelStateChanged;

        private void OnSendChannelStateToClient(InternalLogicalChannelStateMessage message)
        {
            var handler = ChannelStateChanged;
            if (handler != null) handler(this, new MessageEventArgs<InternalLogicalChannelStateMessage>(message));
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

        public void SendChannelStateToClient(InternalLogicalChannelStateMessage message)
        {
            string s = string.Format("MessageExchangeSystem -> Client : канала [{0}] поменял состояние на [{1}]. {2}{3}",
                message.LogicalChannelId,
                message.State,
                message.TimeStamp,
                Environment.NewLine);

            OnSendChannelStateToClient(message);
            log.Debug(s);
            OnNeedProtocol(s);
        }

        public IAsyncResult BeginSendChannelStateToClient(InternalLogicalChannelStateMessage message, AsyncCallback callback,
            object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndSendChannelStateToClient(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DMS.Common.Events;
using DMS.Common.MessageExchangeSystem.HighLevel;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.Base.Autofac;
using Oleg_ivo.MES.High;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// Зарегистрированный клиент верхнего уровня
    /// </summary>
    public class RegisteredHighLevelClient : RegisteredClient<IHighLevelClientCallback>
    {
        private readonly HighLevelMessageExchangeSystem highLevelMessageExchangeSystem;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public RegisteredHighLevelClient(HighLevelMessageExchangeSystem highLevelMessageExchangeSystem,
            DataMode dataMode, int clientId)
        {
            this.highLevelMessageExchangeSystem = Enforce.ArgumentNotNull(highLevelMessageExchangeSystem,
                "highLevelMessageExchangeSystem");
            DataMode = dataMode;
            this.clientId = clientId;
        }

        #region fields

        #endregion

        #region properties

        #endregion

        #region constructors

        #endregion

        #region methods

        #endregion

        /// <summary>
        /// Добавление канала к зарегистрированным
        /// и подписка на 
        /// событие чтения канала (<see cref="RegisteredLogicalChannelExtended.Read"/>)
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException">Если Клиент уже подписан на данный канал 
        /// или Искомый канал недоступен для подписки</exception>
        public void ChannelSubscribe(ChannelSubscribeMessage message)
        {
            if (!AllowedLogicalChannels.Contains(message.LogicalChannelId))
                throw new InvalidOperationException(string.Format("Клиенту не разрешено подписываться на канал {0}",
                    message.LogicalChannelId));

            //поиск ЛК, где Id - заданный:
            var predicate =
                RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId, DataMode.Unknown);

            var logicalChannel = GetRegisteredLogicalChannel(predicate);
            if (logicalChannel != null)
                throw new ArgumentException("Клиент уже подписан на данный канал");

            var registeredLogicalChannel = highLevelMessageExchangeSystem.GetRegisteredChannel(predicate);

            if (registeredLogicalChannel == null) 
                throw new ArgumentException("Искомый канал недоступен для подписки");
            
            if((DataMode & registeredLogicalChannel.DataMode) != DataMode.Unknown)
            {
                AddRegisteredChannel(registeredLogicalChannel);
                
                //подписка на событие чтения канала
                registeredLogicalChannel.Read += registeredLogicalChannel_Read;
                registeredLogicalChannel.ChangeState += registeredLogicalChannel_ChangeState;
            }
            else
            {
                var s = String.Format(
                    "Разрешённый режим данных для клиента [{0}] ({1}) не совпадает с режимом данных канала [{2}] ({3}). Подписка не состоится.",
                    message.RegNameFrom, DataMode, registeredLogicalChannel.Id, registeredLogicalChannel.DataMode);
                log.Warn(s);
                throw new Exception(s);
            }
        }

        /// <summary>
        /// Разрешённый режим данных клиента
        /// </summary>
        public DataMode DataMode { get; private set; }

        /// <summary>
        /// Отписка от 
        /// событие чтения канала (<see cref="RegisteredLogicalChannelExtended.Read"/>)
        /// и удаление канала из зарегистрированных
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException">Если Клиент не подписан на данный канал</exception>
        public void ChannelUnSubscribe(ChannelSubscribeMessage message)
        {
            //поиск ЛК, где Id - заданный:
            RegisteredLogicalChannelExtended registeredLogicalChannel =
                GetRegisteredLogicalChannel(RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId,
                                                                                             DataMode.Unknown));
            if (registeredLogicalChannel == null)
                throw new ArgumentException("Клиент не подписан на данный канал");

            RemoveRegisteredChannel(registeredLogicalChannel);

            //отписка на событие чтения канала
            registeredLogicalChannel.Read -= registeredLogicalChannel_Read;
            registeredLogicalChannel.ChangeState -= registeredLogicalChannel_ChangeState;
        }

        private void registeredLogicalChannel_Read(object sender, MessageEventArgs<InternalLogicalChannelDataMessage> e)
        {
            SendReadToClient(e.Message);
        }

        void registeredLogicalChannel_ChangeState(object sender, MessageEventArgs<InternalLogicalChannelStateMessage> e)
        {
            SendChannelStateToClient(e.Message);
        }

        /// <summary>
        /// Отправка сообщения клиенту
        /// </summary>
        /// <param name="message"></param>
        public void SendChannelStateToClient(InternalLogicalChannelStateMessage message)
        {
            try
            {
                IterateCallbacks(c=> c.SendChannelStateToClient(message));
            }
            catch (Exception ex)
            {
                log.Error("Ошибка при отправке новых данных клиенту: {0}", ex);
                throw;
            }
        }

        /// <summary>
        /// Отправка сообщения клиенту
        /// </summary>
        /// <param name="message"></param>
        public void SendReadToClient(InternalLogicalChannelDataMessage message)
        {
            try
            {
                IterateCallbacks(c=> c.SendReadToClient(message));
            }
            catch (Exception ex)
            {
                log.Error("Ошибка при отправке новых данных клиенту: {0}", ex);
                throw;
            }
        }

        public void ChannelRegisterAsync(ChannelRegistrationMessage message)
        {
            AsyncCallback callback = EndChannelRegister;
            object state = null;
            var caller = new Action<ChannelRegistrationMessage>(ChannelRegister);
            IAsyncResult result = caller.BeginInvoke(message, callback, state);
        }

        private void EndChannelRegister(IAsyncResult result)
        {
            
        }

        private List<int> allowedLogicalChannels;
        private readonly int clientId;

        public List<int> AllowedLogicalChannels
        {
            get
            {
                lock (DataContext)
                {
                    return allowedLogicalChannels ??
                   (allowedLogicalChannels =
                       DataContext.LogicalChannelClients
                           .Where(lcc => lcc.ClientId == ClientId)
                           .Select(lcc => lcc.LogicalChannnelId)
                           .ToList());
                    
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ChannelRegister(ChannelRegistrationMessage message)
        {
            if (!AllowedLogicalChannels.Contains(message.LogicalChannelId))
            {
                log.Warn("Клиенту {0} не разрешено видеть регистрацию канала {1}", ClientId, message.LogicalChannelId);
                return;
            }

            try
            {
                IterateCallbacks(c=> c.ChannelRegister(message));
            }
            catch (Exception ex)
            {
                log.Error("Ошибка при сообщении клиенту о регистрации канала: {0}", ex);
                throw;
            }
        }

        /// <summary>
        /// Отменить регистрацию канала на клиенте верхнего уровня
        /// </summary>
        /// <param name="message"></param>
        public void ChannelUnRegister(ChannelRegistrationMessage message)
        {
            if (!AllowedLogicalChannels.Contains(message.LogicalChannelId))
            {
                log.Warn("Клиенту {0} не разрешено видеть отмену регистрации канала {1}", ClientId, message.LogicalChannelId);
                return;
            }

            //удаляем канал из коллекции зарегистрированных канало данного клиента
            var registeredLogicalChannel =
                GetRegisteredLogicalChannel(RegisteredLogicalChannelExtended.GetFindChannelPredicate(message.LogicalChannelId,
                                                                                             message.DataMode));
            if (registeredLogicalChannel != null)
                RemoveRegisteredChannel(registeredLogicalChannel);

            try
            {
                IterateCallbacks(c=> c.ChannelUnRegister(message));
            }
            catch (Exception ex)
            {
                log.Error("Ошибка при сообщении клиенту об отмене регистрации канала: {0}", ex);
                throw;
            }
        }

        /// <summary>
        /// Заинтересованность клиента в зарегистрированных каналах.
        /// Если true, то клиент уже успешно запросил о зарегистрированных каналах и был получен ответ.
        /// Если false, но он есть в списке заинтересованных клиентов, 
        /// то клиент уже запросил о зарегистрированных каналах, но ответа пока не дождался.
        /// Если клиента нет в списке заинтересованных клиентов, то он ни разу не запрашивал информацию о зарегистрированных каналах.
        /// </summary>
        public bool Interested { get; set; }

        public int ClientId
        {
            get { return clientId; }
        }
    }
}
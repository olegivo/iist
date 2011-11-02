using System;
using DMS.Common.MessageExchangeSystem.HighLevel;
using DMS.Common.Messages;
using NLog;
using Oleg_ivo.MES.High;

namespace Oleg_ivo.MES.Registered
{
    /// <summary>
    /// Зарегистрированный клиент верхнего уровня
    /// </summary>
    public class RegisteredHighLevelClient : RegisteredClient<IHighLevelClientCallback>
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        #region fields

        #endregion

        #region properties

        #endregion

        #region constructors

        #endregion

        #region methods

        #endregion

/*
        /// <summary>
        /// Отправка сообщения клиенту об обновлении цены
        /// </summary>
        public void SendUpdateToClient()
        {
            throw new NotImplementedException("Метод не будет использоваться, т.к. он тестовый");
            Random w = new Random();
            Random p = new Random();
            while (true)
            {
//                    Thread.Sleep(w.Next(5000)); // Откуда-то получаем обновления
                lock (Callbacks)
                    foreach (IHighLevelClientCallback c in Callbacks)
                        try
                        {
                            double d = 100.00 + p.NextDouble() * 10;
                            HighLevelMessageExchangeSystem.Instance.OnUpdate(d);
                            c.PriceUpdate(Ticker, d);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка при отправке кэшированного значения клиенту: {0}",
                                              ex.Message);
                        }
            }
        }
*/


        /// <summary>
        /// Добавление канала к зарегистрированным
        /// и подписка на 
        /// событие чтения канала (<see cref="RegisteredLogicalChannel.Read"/>)
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException">Если Клиент уже подписан на данный канал 
        /// или Искомый канал недоступен для подписки</exception>
        public void ChannelSubscribe(ChannelSubscribeMessage message)
        {
            //поиск ЛК, где Id - заданный:
            Func<RegisteredLogicalChannel, bool> predicate =
                RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId, DataMode.Read);//TODO:для подписки только чтение?

            RegisteredLogicalChannel logicalChannel = GetRegisteredLogicalChannel(predicate);
            if (logicalChannel != null)
                throw new ArgumentException("Клиент уже подписан на данный канал");

            //TODO:проверять режим данных канала при подписке на него
            RegisteredLogicalChannel registeredLogicalChannel =
                HighLevelMessageExchangeSystem.Instance.GetRegisteredChannel(predicate);

            if (registeredLogicalChannel == null) 
                throw new ArgumentException("Искомый канал недоступен для подписки");

            AddRegisteredChannel(registeredLogicalChannel);
            
            //подписка на событие чтения канала
            registeredLogicalChannel.Read += registeredLogicalChannel_Read;
        }

        /// <summary>
        /// Отписка от 
        /// событие чтения канала (<see cref="RegisteredLogicalChannel.Read"/>)
        /// и удаление канала из зарегистрированных
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="ArgumentException">Если Клиент не подписан на данный канал</exception>
        public void ChannelUnSubscribe(ChannelSubscribeMessage message)
        {
            //поиск ЛК, где Id - заданный:
            RegisteredLogicalChannel registeredLogicalChannel =
                GetRegisteredLogicalChannel(RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId,
                                                                                             DataMode.Read));//TODO:для подписки только чтение?
            if (registeredLogicalChannel == null)
                throw new ArgumentException("Клиент не подписан на данный канал");

            RemoveRegisteredChannel(registeredLogicalChannel);

            //отписка на событие чтения канала
            registeredLogicalChannel.Read -= registeredLogicalChannel_Read;
        }

        private void registeredLogicalChannel_Read(object sender, InternalLogicalChannelDataMessageEventArgs e)
        {
            SendReadToClient(e.Message);
        }

        /// <summary>
        /// Отправка сообщения клиенту
        /// </summary>
        /// <param name="message"></param>
        public void SendReadToClient(InternalLogicalChannelDataMessage message)
        {
            lock (Callbacks)
                foreach (IHighLevelClientCallback callback in Callbacks)
                    try
                    {
                        callback.SendReadToClient(message);
                    }
                    catch (Exception ex)
                    {
                        log.ErrorException("Ошибка при отправке новых данных клиенту: {0}",
                                          ex);
                        throw;
                    }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ChannelRegister(ChannelRegistrationMessage message)
        {
            lock (Callbacks)
                foreach (IHighLevelClientCallback callback in Callbacks)
                    try
                    {
                        callback.ChannelRegister(message);
                    }
                    catch (Exception ex)
                    {
                        log.ErrorException("Ошибка при сообщении клиенту о регистрации канала: {0}",
                                          ex);
                        throw;
                    }
        }

        /// <summary>
        /// Отменить регистрацию канала на клиенте верхнего уровня
        /// </summary>
        /// <param name="message"></param>
        public void ChannelUnRegister(ChannelRegistrationMessage message)
        {
            //удаляем канал из коллекции зарегистрированных канало данного клиента
            var registeredLogicalChannel =
                GetRegisteredLogicalChannel(RegisteredLogicalChannel.GetFindChannelPredicate(message.LogicalChannelId,
                                                                                             message.DataMode));
            if (registeredLogicalChannel != null)
                RemoveRegisteredChannel(registeredLogicalChannel);

            lock (Callbacks)
                foreach (IHighLevelClientCallback callback in Callbacks)
                    try
                    {
                        callback.ChannelUnRegister(message);
                    }
                    catch (Exception ex)
                    {
                        log.ErrorException("Ошибка при сообщении клиенту об отмене регистрации канала: {0}",
                                          ex);
                        throw;
                    }
        }

        /// <summary>
        /// Заинтересованность клиента в зарегистрированных каналах.
        /// Если <see langword="true"/>, то клиент уже успешно запросил о зарегистрированных каналах и был получен ответ.
        /// Если <see langword="false"/>, но он есть в списке заинтересованных клиентов, 
        /// то клиент уже запросил о зарегистрированных каналах, но ответа пока не дождался.
        /// Если клиента нет в списке заинтересованных клиентов, то он ни разу не запрашивал информацию о зарегистрированных каналах.
        /// </summary>
        public bool Interested { get; set; }
    }
}
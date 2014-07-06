using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NLog;
using Oleg_ivo.Base.Extensions;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// Планировщик опроса каналов
    ///</summary>
    public class Planner
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Опросы каналов
        /// </summary>
        private readonly ConcurrentDictionary<LogicalChannel, MeasurementPoll> measurementPolls = new ConcurrentDictionary<LogicalChannel, MeasurementPoll>();

        /// <summary>
        /// Добавить опрос канала
        /// </summary>
        /// <param name="channel"></param>
        public void AddPoll(LogicalChannel channel)
        {
            var poll = new MeasurementPoll(channel);
            measurementPolls.AddOrUpdate(channel, logicalChannel => poll, (logicalChannel, existsPoll) =>
            {
                log.Trace("Опрос для канала №{0} уже существует в расписании. Перед добалвением необходима его остановка и удаление.", logicalChannel.Id);
                existsPoll.StopPoll();
                MeasurementPoll oldPoll;
                var removed = measurementPolls.TryRemove(channel, out oldPoll);
                log.Trace("Удаление предыдущего опроса для канала №{0}: {1}", logicalChannel.Id, removed ? "успешно, добавление новой версии" : "неудачно, новая версия не добавлена");
                return removed ? poll : existsPoll;
            });
        }

        private MeasurementPoll GetMeasurementPoll(LogicalChannel channel)
        {
            return measurementPolls.GetValueOrDefault(channel);
        }

        /// <summary>
        /// Удалить опрос канала
        /// </summary>
        /// <param name="channel"></param>
        public void RemovePoll(LogicalChannel channel)
        {
            log.Trace("Удаление опроса канала №{0}", channel.Id);
            MeasurementPoll poll;
            if (measurementPolls.TryRemove(channel, out poll))
            {
                poll.StopPoll(); //обязательно остановить, иначе будет тикать
                log.Trace("Опрос канала №{0} удалён из расписания и остановлен", channel.Id);
            }
            else
                log.Warn("Для канала №{0} не найден опрос", channel.Id);
            //throw new NotImplementedException("Удалить опрос");
        }

        /// <summary>
        /// запустить таймер опроса канала
        /// </summary>
        /// <param name="channel"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void StartPoll(LogicalChannel channel)
        {
            var measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("Не найден опрос для данного канала");

            measurementPoll.Elapsed += measurementPoll_Elapsed;
            measurementPoll.StartPoll();
        }

        void measurementPoll_Elapsed(object sender, NewDataReceivedEventArgs e)
        {
            var handler = NewDadaReceived;
            if (handler != null)
                handler(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<NewDataReceivedEventArgs> NewDadaReceived;

        /// <summary>
        /// остановить таймер опроса канала
        /// </summary>
        /// <param name="channel"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void StopPoll(LogicalChannel channel)
        {
            var measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("Не найден опрос для данного канала");

            measurementPoll.Elapsed -= measurementPoll_Elapsed;
            measurementPoll.StopPoll();
        }

        /// <summary>
        /// Остановить все таймеры опроса каналов
        /// </summary>
        public void StopAllPolls()
        {
            foreach (var measurementPoll in measurementPolls)
                measurementPoll.Value.StopPoll();
        }
    }
}
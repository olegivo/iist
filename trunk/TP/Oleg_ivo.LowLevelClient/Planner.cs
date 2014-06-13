using System;
using System.Collections.Generic;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// Планировщик опроса каналов
    ///</summary>
    public class Planner
    {
        /// <summary>
        /// Опросы каналов
        /// </summary>
        private readonly Dictionary<LogicalChannel, MeasurementPoll> measurementPolls = new Dictionary<LogicalChannel, MeasurementPoll>();

        /// <summary>
        /// Добавить опрос канала
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="interval"></param>
        public void AddPoll(LogicalChannel channel, double interval)
        {
            var measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll != null)
                throw new Exception("Уже есть опрос для данного канала");

            var poll = new MeasurementPoll(channel);
            measurementPolls.Add(channel, poll);
            //throw new NotImplementedException("Учесть настройки опроса");
        }

        private MeasurementPoll GetMeasurementPoll(LogicalChannel channel)
        {
            return measurementPolls.ContainsKey(channel) ? measurementPolls[channel] : null;
        }

        /// <summary>
        /// Удалить опрос канала
        /// </summary>
        /// <param name="channel"></param>
        public void RemovePoll(LogicalChannel channel)
        {
            var measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("Не найден опрос для данного канала");

            measurementPoll.StopPoll();//обязательно остановить, иначе будет тикать
            measurementPolls.Remove(channel);
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
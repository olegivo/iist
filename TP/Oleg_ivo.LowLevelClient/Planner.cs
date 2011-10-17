using System;
using System.Collections.Generic;
using System.ComponentModel;
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.LowLevelClient
{
    ///<summary>
    /// Планировщик опроса каналов
    ///</summary>
    public class Planner
    {
        #region Singleton

        private static Planner _instance;

        ///<summary>
        /// Единственный экземпляр
        ///</summary>
        public static Planner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Planner();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Planner" />.
        /// </summary>
        private Planner()
        {
        }

        #endregion

        /// <summary>
        /// Опросы каналов
        /// </summary>
        private readonly Dictionary<LogicalChannel, MeasurementPoll> MeasurementPolls = new Dictionary<LogicalChannel, MeasurementPoll>();

        /// <summary>
        /// Добавить опрос канала
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="interval"></param>
        /// <param name="synchronizingObject"></param>
        public void AddPoll(LogicalChannel channel, double interval, ISynchronizeInvoke synchronizingObject)
        {
            MeasurementPoll measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll != null)
                throw new Exception("Уже есть опрос для данного канала");

            MeasurementPoll poll = new MeasurementPoll(channel, interval, synchronizingObject);
            MeasurementPolls.Add(channel, poll);
            //throw new NotImplementedException("Учесть настройки опроса");
        }

        private MeasurementPoll GetMeasurementPoll(LogicalChannel channel)
        {
            return MeasurementPolls.ContainsKey(channel) ? MeasurementPolls[channel] : null;
        }

        /// <summary>
        /// Удалить опрос канала
        /// </summary>
        /// <param name="channel"></param>
        public void RemovePoll(LogicalChannel channel)
        {
            MeasurementPoll measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("Не найден опрос для данного канала");

            measurementPoll.StopPoll();//обязательно остановить, иначе будет тикать
            MeasurementPolls.Remove(channel);
            //throw new NotImplementedException("Удалить опрос");
        }

        /// <summary>
        /// запустить таймер опроса канала
        /// </summary>
        /// <param name="channel"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void StartPoll(LogicalChannel channel)
        {
            MeasurementPoll measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("Не найден опрос для данного канала");

            measurementPoll.Elapsed += measurementPoll_Elapsed;
            measurementPoll.StartPoll();
        }

        void measurementPoll_Elapsed(object sender, NewDataReceivedEventArgs e)
        {
            EventHandler<NewDataReceivedEventArgs> handler = NewDadaReceived;
            if (handler != null)
            {
                handler(sender, e);
            }
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
            MeasurementPoll measurementPoll = GetMeasurementPoll(channel);
            if (measurementPoll == null)
                throw new Exception("Не найден опрос для данного канала");

            measurementPoll.Elapsed -= measurementPoll_Elapsed;
            measurementPoll.StopPoll();
        }
    }
}
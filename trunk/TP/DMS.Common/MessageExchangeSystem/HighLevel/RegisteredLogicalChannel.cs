using System;
using System.Runtime.Serialization;
using DMS.Common.Messages;

namespace DMS.Common.MessageExchangeSystem.HighLevel
{
    [DataContract]
    public class RegisteredLogicalChannel : IRegisteredChannel
    {
        public RegisteredLogicalChannel()
        {
        }

        public RegisteredLogicalChannel(int id)
        {
            Id = id;
        }

        public static Func<IRegisteredChannel, bool> GetFindChannelPredicate(int id)
        {
            return (channel => channel.LogicalChannelId == id);
        }


        /// <summary>
        /// Идентификатор канала
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Номер канала
        /// </summary>
        [DataMember]
        public int LogicalChannelId
        {
            get { return Id; }
            set { Id = value; }
        }

        /// <summary>
        /// Минимальное допустимое значение для канала
        /// </summary>
        [DataMember]
        public double? MinValue { get; set; }

        /// <summary>
        /// Максимальное допустимое значение для канала
        /// </summary>
        [DataMember]
        public double? MaxValue { get; set; }

        /// <summary>
        /// Минимальное нормальное значение для канала
        /// </summary>
        [DataMember]
        public double? MinNormalValue { get; set; }

        /// <summary>
        /// Максимальное нормальное значение для канала
        /// </summary>
        [DataMember]
        public double? MaxNormalValue { get; set; }

        ///<summary>
        /// Описание канала
        ///</summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Режим данных канала
        /// </summary>
        [DataMember]
        public DataMode DataMode { get; set; }
    }
}
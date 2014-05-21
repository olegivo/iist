using System;
using Oleg_ivo.Plc.Common;

namespace Oleg_ivo.Plc.Channels
{
    ///<summary>
    /// Логический канал.
    /// Реализует уточнённую адресацию к памяти ПЛК, получение и задание значений
    ///</summary>
    public class LogicalChannel : IIdentified
    {
        public Entities.LogicalChannel Entity { get; set; }

        public delegate double GetLogicalChannelValueDelegate();

        #region fields

        private ushort channelSize;
        private double? previousValue;//todo:очередь
        private string description;
        private ushort addressShift;
        private int id;
        private double deltaChangeLimit;
        private double? minValue;
        private double? maxValue;
        private double? minNormalValue;
        private double? maxNormalValue;
        private TimeSpan? pollPeriod;
        private Polynom directTransform;
        private Polynom reverseTransform;

        #endregion

        #region properties

        ///<summary>
        /// Физический канал
        ///</summary>
        public PhysicalChannel PhysicalChannel { get; private set; }

        ///<summary>
        /// Описание канала
        ///</summary>
        public string Description
        {
            get { return Entity != null ? Entity.Description : description; }
            set
            {
                if (Description == value) return;
                description = value;
                if (Entity != null) Entity.Description = value;
            }
        }

        ///<summary>
        /// Адрес в физическом канале
        ///</summary>
        public ushort AddressShift
        {
            get
            {
                return
                    (ushort)(Entity != null && Entity.AddressShift.HasValue ? Entity.AddressShift.Value : addressShift);
            }
            set
            {
                if (AddressShift == value) return;
                addressShift = value;
                if (Entity != null) Entity.AddressShift = value;
            }
        }

        ///<summary>
        /// Разрядность физического канала
        ///</summary>
        public ushort ChannelSize
        {
            get
            {
                return
                    (ushort)
                        (Entity != null && Entity.Size.HasValue
                            ? Entity.Size.Value
                            : (channelSize > 0
                                ? channelSize
                                : (PhysicalChannel != null ? PhysicalChannel.ChannelSize : 0)));
            }
            set
            {
                if (ChannelSize == value) return;
                channelSize = value;
                if (Entity != null) Entity.Size = value;
            }
        }

        ///<summary>
        /// Адрес на шине для записи
        ///</summary>
        public int WriteAddress
        {
            get
            {
                return (PhysicalChannel != null ? PhysicalChannel.WriteAddress : 0) + AddressShift;
            }
        }

        ///<summary>
        /// Адрес на шине для чтения
        ///</summary>
        public int ReadAddress
        {
            get { return PhysicalChannel.ReadAddress + AddressShift; }
        }

        public bool IsInput { get { return PhysicalChannel != null && PhysicalChannel.IOModule.IsInput; } }

        public bool IsOutput { get { return PhysicalChannel != null && PhysicalChannel.IOModule.IsOutput; } }

        ///<summary>
        /// Идентификатор
        ///</summary>
        public int Id
        {
            get { return Entity != null ? Entity.Id : id; }
            set
            {
                if (Id == value) return;
                id = value;
                if (Entity != null) Entity.Id = value;
            }
        }

        /// <summary>
        /// Порог изменения величины
        /// </summary>
        public double DeltaChangeLimit
        {
            get
            {
                return Entity != null && Entity.Parameter != null && Entity.Parameter.SensivityDelta.HasValue
                    ? (double)Entity.Parameter.SensivityDelta.Value
                    : deltaChangeLimit;
            }
            set
            {
                if (DeltaChangeLimit == value) return;
                deltaChangeLimit = value;
                if (Entity != null && Entity.Parameter != null)
                    Entity.Parameter.SensivityDelta = (decimal?)value;
            }
        }

        /// <summary>
        /// Минимальное допустимое значение для канала
        /// </summary>
        public double? MinValue
        {
            get { return Entity != null && Entity.Parameter != null ? (double?)Entity.Parameter.MinValue : minValue; }
            set
            {
                if (MinValue == value) return;
                minValue = value;
                if (Entity != null && Entity.Parameter != null) Entity.Parameter.MinValue = (decimal?)value;
            }
        }

        /// <summary>
        /// Максимальное допустимое значение для канала
        /// </summary>
        public double? MaxValue
        {
            get { return Entity != null && Entity.Parameter != null ? (double?)Entity.Parameter.MaxValue : maxValue; }
            set
            {
                if (MaxValue == value) return;
                maxValue = value;
                if (Entity != null && Entity.Parameter != null) Entity.Parameter.MaxValue = (decimal?)value;
            }
        }

        /// <summary>
        /// Минимальное нормальное значение для канала
        /// </summary>
        public double? MinNormalValue
        {
            get { return Entity != null && Entity.Parameter != null ? (double?)Entity.Parameter.MinNormalValue : maxValue; }
            set
            {
                if (MinNormalValue == value) return;
                maxValue = value;
                if (Entity != null && Entity.Parameter != null) Entity.Parameter.MinNormalValue = (decimal?)value;
            }
        }

        /// <summary>
        /// Максимальное нормальное значение для канала
        /// </summary>
        public double? MaxNormalValue
        {
            get { return Entity != null && Entity.Parameter != null ? (double?)Entity.Parameter.MinNormalValue : maxValue; }
            set
            {
                if (MinNormalValue == value) return;
                maxValue = value;
                if (Entity != null && Entity.Parameter != null) Entity.Parameter.MinNormalValue = (decimal?)value;
            }
        }

        /// <summary>
        /// Полином прямого преобразования
        /// </summary>
        public Polynom DirectTransform
        {
            get
            {
                return directTransform ??
                       (directTransform =
                           (Entity != null && Entity.Parameter != null && Entity.Parameter.DirectPolynom != null
                               ? Polynom.DeSerializePolynom(Entity.Parameter.DirectPolynom)
                               : null));
            }
            set
            {
                if (DirectTransform == value) return;
                directTransform = value;
                if (Entity != null && Entity.Parameter != null) Entity.Parameter.DirectPolynom = Polynom.SerializePolynom(value);
            }
        }

        /// <summary>
        /// Полином обратного преобразования
        /// </summary>
        public Polynom ReverseTransform
        {
            get
            {
                return reverseTransform ??
                       (reverseTransform =
                           (Entity != null && Entity.Parameter != null && Entity.Parameter.ReversePolynom != null
                               ? Polynom.DeSerializePolynom(Entity.Parameter.ReversePolynom)
                               : null));
            }
            set
            {
                if (ReverseTransform == value) return;
                reverseTransform = value;
                if (Entity != null && Entity.Parameter != null) Entity.Parameter.ReversePolynom = Polynom.SerializePolynom(value);
            }
        }

        /// <summary>
        /// Период опроса канала
        /// </summary>
        public TimeSpan? PollPeriod
        {
            get
            {
                return pollPeriod == null && Entity != null && Entity.Parameter != null && Entity.Parameter.PollPeriod.HasValue
                    ? pollPeriod = TimeSpan.FromMilliseconds((double)Entity.Parameter.PollPeriod.Value)
                    : pollPeriod;
            }
            set
            {
                if (PollPeriod == value) return;
                pollPeriod = value;
                if (Entity != null && Entity.Parameter != null && value.HasValue) 
                    Entity.Parameter.PollPeriod = (decimal?)value.Value.TotalMilliseconds;
            }
        }

        /// <summary>
        /// Делегат альтернативного получения данных из канала.
        /// Если задан, используется в методе <see cref="GetValue"/>
        /// </summary>
        public GetLogicalChannelValueDelegate GetValueAltDelegate { protected get; set; }

        /// <summary>
        /// Делегат эмуляции альтернативного получения значения из канала.
        /// Если задан, используется в методе <see cref="GetValueEmulation"/>
        /// </summary>
        public GetLogicalChannelValueDelegate GetValueEmulationAltDelegate { get; set; }

        /// <summary>
        /// Режим эмуляции (обращения к реальным устройствам не будет)
        /// </summary>
        public bool IsEmulationMode { get; set; }

        #endregion

        #region constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="physicalChannel"></param>
        /// <param name="entity"></param>
        /// <param name="addressShift"></param>
        /// <param name="channelSize"></param>
        public LogicalChannel(PhysicalChannel physicalChannel, Entities.LogicalChannel entity, ushort addressShift, ushort channelSize)
        {
            PhysicalChannel = physicalChannel;
            Entity = entity;
            AddressShift = addressShift;
            this.channelSize = channelSize;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("Логический канал {0}[{1}-{2}] Id = {3}", Description, WriteAddress, WriteAddress + ChannelSize - 1, Id);
        }


        #region methods

        ///<summary>
        /// Записать значение в логический канал
        ///</summary>
        ///<param name="value"></param>
        public void SetValue(double value)
        {
            if (ReverseTransform != null) value = ReverseTransform.GetValue(value);
            object v = Math.Abs(Math.Round(value) - value) < 0.001
                ? Convert.ToInt32(value)
                : value;
            PhysicalChannel.SetValue(AddressShift, ChannelSize, v);
        }

        ///<summary>
        /// Получить значение из логического канала.
        /// Если задан делегат альтернативного получения данных из канала, используется он,
        /// иначе из физического канала получается значение, 
        /// проводится прямое преобразование (если задан полином прямого преобразвания),
        /// затем производится проверка полученного значения по диапазону изменения величины.
        /// Если установлено свойство <see cref="IsEmulationMode"/>=true, происходит эмуляция возвращения значения из канала.
        /// При этом если задан делегат эмуляции альтернативного получения данных из канала, используется он,
        /// иначе производится получения функции синуса от аргумента времени, 
        /// периодом одна минута и диапазоном значений, равным диапазону изменения величины,
        /// проводится прямое преобразование (если задан полином прямого преобразвания),
        /// затем производится проверка полученного значения по диапазону изменения величины.
        ///</summary>
        ///<exception cref="ArgumentOutOfRangeException"></exception>
        ///<returns></returns>
        public double GetValue()
        {
            if (IsEmulationMode)
                return GetValueEmulation();

            if (GetValueAltDelegate != null)
                return GetValueAltDelegate();

            object obj = PhysicalChannel.GetValue(AddressShift, ChannelSize);
            if (obj is Array)
            {
                Array ar = (Array)obj;
                if (PhysicalChannel.IOModule.IsDiscrete && ChannelSize == ar.Length)
                {
                    Int64 result = 0;
                    for (int i = 0; i < ChannelSize; i++)
                    {
                        var b = (bool)ar.GetValue(i);
                        if (b) result += Convert.ToInt64(Math.Pow(2, i));
                    }
                    obj = result;
                }
                else
                {
                    if (ar.Length != 1)
                        throw new ArgumentOutOfRangeException("ChannelSize", ChannelSize, "Для логического канала неправильно задан размер (при попытке прочитать одно значение был прочитан массив значений)");
                    obj = ar.GetValue(0);
                }
            }

            double value = Convert.ToDouble(obj);
            if (DirectTransform != null)
                value = DirectTransform.GetValue(value);

            CheckValueRange(value);

            return value;
        }

        /// <summary>
        /// Получить новое значение из логического канала.
        /// </summary>
        /// <returns>Если новое значение мало отличается от предыдущего, возвращается <see langword="null"/>. 
        /// Иначе новое значение перезаписывает предыдущее</returns>
        public double? GetNewValue()
        {
            double? value = GetValue();

            if (previousValue == null || IsNewData(previousValue, value))
                previousValue = value;
            else
                value = null;

            return value;
        }

        /// <summary>
        /// Проверка попадения значения в диапазон
        /// </summary>
        /// <param name="value"></param>
        private void CheckValueRange(double value)
        {
            double limitValue;
            string message = "";

            limitValue = MinValue ?? value;
            if (value < limitValue)
                message += string.Format("минимальное разрешённое значение ({0})", limitValue);

            limitValue = MaxValue ?? value;
            if (value > limitValue)
                message +=
                    message.Length > 0 ? Environment.NewLine : ""
                    + string.Format("максимальное разрешённое значение ({0})", limitValue);

            if (message.Length > 0)
            {
                throw new ArgumentOutOfRangeException("value", value, message);
            }

        }

        /// <summary>
        /// Эмуляция возвращения значения из канала.
        /// Если задан делегат эмуляции альтернативного получения данных из канала, используется он,
        /// иначе производится получения функции синуса от аргумента времени, 
        /// периодом одна минута и диапазоном значений, равным диапазону изменения величины,
        /// проводится прямое преобразование (если задан полином прямого преобразвания),
        /// затем производится проверка полученного значения по диапазону изменения величины.
        /// </summary>
        /// <returns></returns>
        private double GetValueEmulation()
        {
            if (GetValueEmulationAltDelegate != null)
                return GetValueEmulationAltDelegate();

            int second = DateTime.Now.Second;
            double value = Math.Sin(second * 6 * (Math.PI / 180));//синус [-1;+1]
            value = ((value + 1) / 2);//сдвиг - синус [0;+1]
            if (MinValue != null && MaxValue != null)
                value = (double)(MinValue + (MaxValue - MinValue) * value);//масштабирование до диапазона

            //NOTE: при эмуляции полином прямого преобразования можно и не использовать, тогда нужно закомментить следующие строки
            if (DirectTransform != null)
                value = DirectTransform.GetValue(value);

            CheckValueRange(value);

            return value;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Func<LogicalChannel, bool> GetFindChannelPredicate(int id)
        {
            return (channel => channel.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Если > 0</param>
        /// <param name="physicalChannelId">Если > 0</param>
        /// <param name="addressShift"></param>
        /// <param name="channelSize">Если > 0</param>
        /// <returns></returns>
        public static Func<LogicalChannel, bool> GetEqualsPredicate(int id, int physicalChannelId, ushort addressShift, ushort channelSize)
        {
            return
                (channel =>
                 (id == 0 || channel.Id == id)
                 && (physicalChannelId == 0
                        || channel.PhysicalChannel == null
                        || channel.PhysicalChannel.Id == 0
                        || channel.PhysicalChannel.Id == physicalChannelId)
                 && (channel.AddressShift == addressShift)
                 && (channelSize == 0 || channel.ChannelSize == channelSize)
                );
        }

        /// <summary>
        /// Проверка на изменение величины (используется <see cref="DeltaChangeLimit"/>)
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsNewData(double? oldValue, double? value)
        {
            bool result = true;
            if (DeltaChangeLimit > 0 && oldValue != null && value != null)
            {
                //если хотя бы одна из границ диапазона не задана, принимаем, что DeltaChangeLimit - абсолютное изменение, иначе - относительное (в % от размера диапазона)
                double delta;
                if (MinValue != null && MaxValue != null)
                {
                    //относительное различие
                    delta = DeltaChangeLimit * 100 / Math.Abs((double)(MaxValue - MinValue));
                }
                else
                {
                    //абсолютное различие
                    delta = DeltaChangeLimit;
                }

                result = (oldValue == null || Math.Abs((double)(oldValue - value)) >= delta);
            }

            return result;
        }
    }
}
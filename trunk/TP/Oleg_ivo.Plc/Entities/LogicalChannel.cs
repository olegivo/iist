using Oleg_ivo.Plc.Common;

namespace Oleg_ivo.Plc.Entities
{
    public partial class LogicalChannel : IIdentified
    {
        //TODO:часть 1. перенести в данный класс часть логики из Oleg_ivo.Plc.Channels.LogicalChannel
        //TODO:часть 2. впоследствии использовать данный класс вместо Oleg_ivo.Plc.Channels.LogicalChannel
/*
        public delegate double GetLogicalChannelValueDelegate();

        #region fields

        private ushort _channelSize;
        private double? previousValue;

        #endregion

        #region properties

        /// <summary>
        /// ѕорог изменени€ величины
        /// </summary>
        public double DeltaChangeLimit { get; set; }

        /// <summary>
        /// ѕолином пр€мого преобразовани€
        /// </summary>
        public Polynom DirectTransform { get; set; }

        /// <summary>
        /// ѕолином обратного преобразовани€
        /// </summary>
        public Polynom ReverseTransform { get; set; }

        /// <summary>
        /// ƒелегат альтернативного получени€ данных из канала.
        /// ≈сли задан, используетс€ в методе <see cref="GetValue"/>
        /// </summary>
        public Channels.LogicalChannel.GetLogicalChannelValueDelegate GetValueAltDelegate { protected get; set; }

        /// <summary>
        /// ƒелегат эмул€ции альтернативного получени€ значени€ из канала.
        /// ≈сли задан, используетс€ в методе <see cref="GetValueEmulation"/>
        /// </summary>
        public Channels.LogicalChannel.GetLogicalChannelValueDelegate GetValueEmulationAltDelegate { get; set; }

        /// <summary>
        /// –ежим эмул€ции (обращени€ к реальным устройствам не будет)
        /// </summary>
        public bool IsEmulationMode { get; set; }

        #endregion


        #region methods

        /// <summary>
        /// ѕроверка попадени€ значени€ в диапазон
        /// </summary>
        /// <param name="value"></param>
        private void CheckValueRange(double value)
        {
            double limitValue;
            string message = "";

            limitValue = (double?) MinValue ?? value;
            if (value < limitValue)
                message += string.Format("минимальное разрешЄнное значение ({0})", limitValue);

            limitValue = (double?) MaxValue ?? value;
            if (value > limitValue)
                message +=
                    message.Length > 0 ? Environment.NewLine : ""
                    + string.Format("максимальное разрешЄнное значение ({0})", limitValue);

            if (message.Length > 0)
            {
                throw new ArgumentOutOfRangeException("value", value, message);
            }

        }

        /// <summary>
        /// Ёмул€ци€ возвращени€ значени€ из канала.
        /// ≈сли задан делегат эмул€ции альтернативного получени€ данных из канала, используетс€ он,
        /// иначе производитс€ получени€ функции синуса от аргумента времени, 
        /// периодом одна минута и диапазоном значений, равным диапазону изменени€ величины,
        /// проводитс€ пр€мое преобразование (если задан полином пр€мого преобразвани€),
        /// затем производитс€ проверка полученного значени€ по диапазону изменени€ величины.
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
                value = (double)MinValue + (double)(MaxValue - MinValue).Value * value;//масштабирование до диапазона

            //NOTE: при эмул€ции полином пр€мого преобразовани€ можно и не использовать, тогда нужно закомментить следующие строки
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
        public static Func<Channels.LogicalChannel, bool> GetFindChannelPredicate(int id)
        {
            return (channel => channel.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">≈сли > 0</param>
        /// <param name="physicalChannelId">≈сли > 0</param>
        /// <param name="addressShift"></param>
        /// <param name="channelSize">≈сли > 0</param>
        /// <returns></returns>
        public static Func<Channels.LogicalChannel, bool> GetEqualsPredicate(int id, int physicalChannelId, ushort addressShift, ushort channelSize)
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
        /// ѕроверка на изменение величины (используетс€ <see cref="DeltaChangeLimit"/>)
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsNewData(double? oldValue, double? value)
        {
            bool result = true;
            if (DeltaChangeLimit > 0 && oldValue != null && value != null)
            {
                //если хот€ бы одна из границ диапазона не задана, принимаем, что DeltaChangeLimit - абсолютное изменение, иначе - относительное (в % от размера диапазона)
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
*/

    }
}
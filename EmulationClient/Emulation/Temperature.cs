using System;

namespace EmulationClient.Emulation
{
    /// <summary>
    /// Температура
    /// </summary>
    public class Temperature : CPBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Temperature()
        {
            startTime = DateTime.Now;
        }

        private DateTime startTime;

        private int GetPassedSeconds()
        {
            DateTime now = DateTime.Now;
            return now.Subtract(startTime).Seconds;
        }

        /// <summary>
        /// Значение контролируемого параметра
        /// </summary>
        public override double OutputValue { get; set; }

        /// <summary>
        /// Входной параметр: Горелка Вкл.\Выкл.
        /// </summary>
        public  bool IsBurnerOn { get; set; }

       /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public override void Refresh()
        {
            double delta = (IsBurnerOn ? 1/2 : -1/2)
                            * Math.Exp(GetPassedSeconds());
            OutputValue += delta;
            startTime = DateTime.Now;
        }
    }
}
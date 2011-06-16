using System;

namespace EmulationClient.Emulation
{
    /// <summary>
    /// Температура
    /// </summary>
    public class Temperature : IControlledParameter
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
        public double OutputValue { get; set; }

        /// <summary>
        /// Входной параметр: Горелка Вкл.\Выкл.
        /// </summary>
        public bool IsBurnerOn { get; set; }

        //if (IsBurnerOn = true);
        //{
        //Temperature = Math.Exp(1/2*DateTime.Now.Second);
        //}
        //else          
        //{
        //Temperature = Math.Exp(-1/2*DateTime.Now.Second);
        //}

        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public void Refresh()
        {
            double delta = (IsBurnerOn ? 1 : -1)
                            * Math.Exp(GetPassedSeconds());
            OutputValue += delta;
            startTime = DateTime.Now;
        }
    }
}
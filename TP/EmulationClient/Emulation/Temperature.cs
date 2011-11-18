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
        /// Входной параметр: Горелка (включено / выключено)
        /// </summary>
        public  bool IsBurnerOn { get; set; }

       /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public override void Refresh()
       {
           double delta = (IsBurnerOn ? 1 : -1)
                          *Math.Exp(Math.Sqrt(GetPassedSeconds())/100);
           if (_outputValue > 20 || delta > 0) _outputValue += delta;
            startTime = DateTime.Now;
        }
    }
}
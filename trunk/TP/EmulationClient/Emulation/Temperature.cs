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
           int passedSeconds = GetPassedSeconds();
           _outputValue = GetOutputValueCalculator()(passedSeconds);
           //double delta = (IsBurnerOn ? 1 : -1)
           //               *Math.Exp(Math.Sqrt(GetPassedSeconds())/100);
           //if (_outputValue > 120 || delta > 0) _outputValue += delta;
           //startTime = DateTime.Now;
        }

           private OutputValueCalculatorDelegate GetOutputValueCalculator()
        {
            return GetTemperature ?? DefaultOutputValueCalculator;
        }

        /// <summary>
        /// Делегат для расчёта выходного значения концентрации
        /// </summary>
        public OutputValueCalculatorDelegate GetTemperature { get; set; }
        
        /// <summary>
        /// Расчёт выходного значения температуры
        /// </summary>
        
        /// <param name="GetPassedSeconds">Количество прошедших секунд</param>
        /// <returns></returns>
        public delegate double OutputValueCalculatorDelegate(int passedSeconds);

        /// <summary>
        /// Метод для расчёта выходного значения концентрации по умолчанию (если не указан делегат)
        /// </summary>

        private double DefaultOutputValueCalculator(int passedSeconds)
        {
            return (Math.Sin(0.002 * passedSeconds) * 100 + 100);
        }
    }
}
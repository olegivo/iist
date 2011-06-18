using System;

namespace EmulationClient.Emulation
{
    /// <summary>
    /// 
    /// </summary>
    public class GasConcentration : CPBase
    {
        /// <summary>
        /// 
        /// </summary>
        public GasConcentration()
        {
            startTime = DateTime.Now;
        }

        private readonly DateTime startTime;

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
        /// Входной параметр: Температура перед рукавным фильтром 
        /// </summary>
        public  double Temperature { get; set; }


        /// <summary>
        /// Входной параметр: Количеством оборотов дымососа  
        /// </summary>
        public  double Speed { get; set; }

        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public override void Refresh()
        {
            RefreshTemperature();
            int passedSeconds = GetPassedSeconds();
            OutputValue = Math.Abs(Math.Sin(0.005 * passedSeconds)) * 500 + 3500 + Temperature + Speed;
        }

        private void RefreshTemperature()
        {
            ;
        }

    }
}
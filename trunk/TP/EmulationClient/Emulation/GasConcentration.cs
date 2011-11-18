using System;

namespace EmulationClient.Emulation
{
    /// <summary>
    /// Концентрация газа
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

        private double _temperature;
        private double _speed;

        /// <summary>
        /// Входной параметр: Температура перед рукавным фильтром
        /// </summary>
        public double Temperature
        {
            get { return GetTemperature != null ? GetTemperature() : _temperature; }
            set { _temperature = value; }
        }

        /// <summary>
        /// Входной параметр: Количество оборотов дымососа  
        /// </summary>
        public double Speed
        {
            get { return GetSpeed != null ? GetSpeed() : _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public GetDoubleValueDelegate GetTemperature { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GetDoubleValueDelegate GetSpeed { get; set; }

        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public override void Refresh()
        {
            int passedSeconds = GetPassedSeconds();
            double temperature = Temperature;
            double speed = Speed;
            _outputValue = Math.Abs(Math.Sin(0.005 * passedSeconds)) * 50 + 3500
                + (temperature > 150 ? (30 * temperature - 2500) : 0)
                + (-33 * speed + 1500);
        }

    }
}
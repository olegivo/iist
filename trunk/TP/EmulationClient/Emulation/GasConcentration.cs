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
        /// Делегат для расчёта выходного значения концентрации
        /// </summary>
        public OutputValueCalculatorDelegate GetConcentration { get; set; }

        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public override void Refresh()
        {
            int passedSeconds = GetPassedSeconds();
            double temperature = Temperature;
            double speed = Speed;
            _outputValue = GetOutputValueCalculator()(speed, temperature, passedSeconds);
        }

        /// <summary>
        /// Получить делегат для расчёта выходного значения концентрации
        /// </summary>
        /// <returns></returns>
        private OutputValueCalculatorDelegate GetOutputValueCalculator()
        {
            return GetConcentration ?? DefaultOutputValueCalculator;
        }

        /// <summary>
        /// Расчёт выходного значения концентрации
        /// </summary>
        /// <param name="speed">Текущая скорость</param>
        /// <param name="temperature">Текущая температура</param>
        /// <param name="passedSeconds">Количество прошедших секунд</param>
        /// <returns></returns>
        public delegate double OutputValueCalculatorDelegate(double speed, double temperature, int passedSeconds);

        /// <summary>
        /// Метод для расчёта выходного значения концентрации по умолчанию (если не указан делегат)
        /// </summary>
        /// <param name="speed"></param>
        /// <param name="temperature"></param>
        /// <param name="passedSeconds"></param>
        /// <returns></returns>
        private double DefaultOutputValueCalculator(double speed, double temperature, int passedSeconds)
        {
            return Math.Abs(Math.Sin(0.005 * passedSeconds)) * 50 + 3500
                   + (temperature > 150 ? (30 * temperature - 4500) : 0)
                   + (-31.25 * speed + 1250);
        }
    }
}
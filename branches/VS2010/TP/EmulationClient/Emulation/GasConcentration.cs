using System;

namespace EmulationClient.Emulation
{
    /// <summary>
    /// ������������ ����
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
        /// ������� ��������: ����������� ����� �������� ��������
        /// </summary>
        public double Temperature
        {
            get { return GetTemperature != null ? GetTemperature() : _temperature; }
            set { _temperature = value; }
        }

        /// <summary>
        /// ������� ��������: ���������� �������� ��������  
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
        /// ������� ��� ������� ��������� �������� ������������
        /// </summary>
        public OutputValueCalculatorDelegate GetConcentration { get; set; }

        /// <summary>
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        public override void Refresh()
        {
            int passedSeconds = GetPassedSeconds();
            double temperature = Temperature;
            double speed = Speed;
            _outputValue = GetOutputValueCalculator()(speed, temperature, passedSeconds);
        }

        /// <summary>
        /// �������� ������� ��� ������� ��������� �������� ������������
        /// </summary>
        /// <returns></returns>
        private OutputValueCalculatorDelegate GetOutputValueCalculator()
        {
            return GetConcentration ?? DefaultOutputValueCalculator;
        }

        /// <summary>
        /// ������ ��������� �������� ������������
        /// </summary>
        /// <param name="speed">������� ��������</param>
        /// <param name="temperature">������� �����������</param>
        /// <param name="passedSeconds">���������� ��������� ������</param>
        /// <returns></returns>
        public delegate double OutputValueCalculatorDelegate(double speed, double temperature, int passedSeconds);

        /// <summary>
        /// ����� ��� ������� ��������� �������� ������������ �� ��������� (���� �� ������ �������)
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
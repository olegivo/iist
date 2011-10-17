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
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        public override void Refresh()
        {
            int passedSeconds = GetPassedSeconds();
            _outputValue = Math.Abs(Math.Sin(0.005 * passedSeconds)) * 500 + 3500 + Temperature + Speed;
        }

    }
}
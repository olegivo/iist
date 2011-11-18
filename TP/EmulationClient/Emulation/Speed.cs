﻿namespace EmulationClient.Emulation
{
    /// <summary>
    /// Количество оборотов дымососа
    /// </summary>
    public class Speed: CPBase
    {
        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public override void Refresh()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public void SetSpeedValue(double value)
        {
            _outputValue = value;
        }
    }
}

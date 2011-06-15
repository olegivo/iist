using System;

namespace EmulationClient.Emulation
{
    public class GasConcentration : IControlledParameter
    {
        /// <summary>
        /// Значение контролируемого параметра
        /// </summary>
        public double OutputValue
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Входной параметр: Температура перед рукавным фильтром 
        /// </summary>
        public double Temperature;

        /// <summary>
        /// Входной параметр: Количеством оборотов дымососа  
        /// </summary>
        public double Speed;

        /// <summary>
        /// Входной параметр: Горелка Вкл.\Выкл.
        /// </summary>
        public bool Burner;


        /// <summary>
        /// Концентрация выбрасываемого газа СО 
        /// </summary>
        public double CCO;

        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public void Refresh()
        {
            throw new NotImplementedException();
        }

        //todo: добавить входные параметры
    }
}
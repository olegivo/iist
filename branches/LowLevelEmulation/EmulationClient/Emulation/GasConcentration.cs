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
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public void Refresh()
        {
            throw new NotImplementedException();
        }

        //todo: добавить входные параметры
    }
}
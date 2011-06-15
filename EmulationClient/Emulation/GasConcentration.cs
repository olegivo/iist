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
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Входной параметр: Температура перед рукавным фильтром 
        /// </summary>
        public double Temperature;
        
            //if (Burner = true);
            //{
            //Temperature = Math.Exp(1/2*DateTime.Now.Second);
            //}
            //else          
            //{
            //Temperature = Math.Exp(-1/2*DateTime.Now.Second);
            //}
        

        /// <summary>
        /// Входной параметр: Количеством оборотов дымососа  
        /// </summary>
        public double Speed;


        /// <summary>
        /// Входной параметр: Горелка Вкл.\Выкл.
        /// </summary>
        public bool Burner;
       
        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public void Refresh()
        {
            DateTime now = DateTime.Now; //todo:зафиксировать время в переменной.
           int second = now.Second;
           OutputValue = Math.Abs(Math.Sin(0.005*second))* 500 + 3500 + Temperature + Speed;

            throw new NotImplementedException();
        }

    }
}
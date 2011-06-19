namespace EmulationClient.Emulation
{
    public abstract class CPBase
    {
        public double outputvalue;
        /// <summary>
        /// Значение контролируемого параметра
        /// </summary>
        public double OutputValue
        {
            get
            {
                Refresh();
                return  outputvalue;
            }
            set { outputvalue = value; }
        }

        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public abstract void Refresh();
    }
}
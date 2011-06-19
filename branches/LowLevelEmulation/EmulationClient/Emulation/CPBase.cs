namespace EmulationClient.Emulation
{
    public abstract class CPBase
    {
        /// <summary>
        /// Значение контролируемого параметра
        /// </summary>
        public double OutputValue
        {
            get
        {
            Refresh();
            return 0d;
        }
            set {  }
        }

        /// <summary>
        /// Обновить значение (используется функция пересчёта входных параметров в выходной)
        /// </summary>
        public abstract void Refresh();
    }
}
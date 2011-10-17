namespace Oleg_ivo.Plc.FieldBus
{
    /// <summary>
    /// Параметры транспорта Modbus
    /// </summary>
    public class ModbusAccessorTimeouts
    {
        /// <summary>
        /// 
        /// </summary>
        public int WaitToRetryMilliseconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Retries { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ReadTimeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int WriteTimeout { get; set; }
    }
}
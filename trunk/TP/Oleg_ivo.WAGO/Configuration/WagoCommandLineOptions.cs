namespace Oleg_ivo.WAGO.Configuration
{
    public class WagoCommandLineOptions
    {
        /// <summary>
        /// Название конфигурации DMIS
        /// </summary>
        public string ConfigName { get; set; }

        public bool AutoRegister { get; set; }
        
        public bool AutoRegisterAllChannels { get; set; }
    }
}
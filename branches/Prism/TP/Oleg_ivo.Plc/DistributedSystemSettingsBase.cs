using System.Collections.Generic;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.Plc
{
    ///<summary>
    /// Настройки распределённой системы
    ///</summary>
    public class DistributedSystemSettingsBase : IDistributedSystemSettings
    {
        private Dictionary<FieldBusType, FieldBusLoadOptions> _fieldBusLoadOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"></see> class.
        /// </summary>
        public DistributedSystemSettingsBase()
        {
            InitLoadOptions();
        }

        ///<summary>
        ///
        ///</summary>
        protected virtual void InitLoadOptions()
        {
            _fieldBusLoadOptions = new Dictionary<FieldBusType, FieldBusLoadOptions>();
        }

        /// <summary>
        /// Опции загрузки полевых шин
        /// </summary>
        public Dictionary<FieldBusType, FieldBusLoadOptions> FieldBusLoadOptions
        {
            get { return _fieldBusLoadOptions; }
        }
    }
}
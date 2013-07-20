using Autofac;

namespace Oleg_ivo.Plc
{
    ///<summary>
    /// Распределённая информационно-измерительная система (фасад)
    ///</summary>
    public abstract class DistributedMeasurementInformationSystemBase : IDistributedMeasurementInformationSystem
    {
        #region fields

        ///<summary>
        ///
        ///</summary>
        protected static DistributedMeasurementInformationSystemBase _instance;
        private IPlcManager plcManager;
        private IDistributedSystemSettings settings;
        protected IComponentContext Context;

        #endregion

        #region properties

        ///<summary>
        /// Диспетчер ПЛК системы
        ///</summary>
        public IPlcManager PlcManager
        {
            get { return plcManager; }
        }

        #endregion

        #region constructors

        ///<summary>
        /// Настройки распределённой системы
        ///</summary>
        public IDistributedSystemSettings Settings
        {
            get { return settings; }
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DistributedMeasurementInformationSystemBase" />.
        /// </summary>
        /// <param name="context"></param>
        protected DistributedMeasurementInformationSystemBase(IComponentContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract IDistributedSystemSettings CreateSettings();

        ///<summary>
        /// Построить конфигурацию системы
        ///</summary>
        public virtual void BuildSystemConfiguration()
        {
            plcManager = Context.Resolve<IPlcManager>();
            settings = CreateSettings();
        }

        #endregion

        ///<summary>
        ///
        ///</summary>
        public abstract void ShowConfiguration();
    }
}
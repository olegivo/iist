using Autofac;

namespace Oleg_ivo.Plc
{
    ///<summary>
    /// ������������� �������������-������������� ������� (�����)
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
        /// ��������� ��� �������
        ///</summary>
        public IPlcManager PlcManager
        {
            get { return plcManager; }
        }

        #endregion

        #region constructors

        #region Singleton

        ///<summary>
        /// ��������� ������������� �������
        ///</summary>
        public IDistributedSystemSettings Settings
        {
            get { return settings; }
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="DistributedMeasurementInformationSystemBase" />.
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
        /// ��������� ������������ �������
        ///</summary>
        public virtual void BuildSystemConfiguration()
        {
            plcManager = Context.Resolve<IPlcManager>();
            settings = CreateSettings();
        }

        #endregion

        #endregion

        ///<summary>
        ///
        ///</summary>
        public abstract void ShowConfiguration();
    }
}
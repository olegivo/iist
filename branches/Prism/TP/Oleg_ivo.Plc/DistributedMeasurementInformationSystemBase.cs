namespace Oleg_ivo.Plc
{
    ///<summary>
    /// ������������� �������������-������������� ������� (�����)
    ///</summary>
    public class DistributedMeasurementInformationSystemBase
    {
        #region fields

        ///<summary>
        ///
        ///</summary>
        protected static DistributedMeasurementInformationSystemBase _instance;
        private readonly PlcManagerBase _plcManager;
        private readonly DistributedSystemSettingsBase _settings;

        #endregion

        #region properties

        ///<summary>
        /// ��������� ��� �������
        ///</summary>
        public PlcManagerBase PlcManagerBase
        {
            get { return _plcManager; }
        }

        #endregion

        #region constructors

        #region Singleton


        ///<summary>
        /// ������������ ���������
        ///</summary>
        public static DistributedMeasurementInformationSystemBase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DistributedMeasurementInformationSystemBase();
                }
                return _instance;
            }
        }

        ///<summary>
        /// ��������� ������������� �������
        ///</summary>
        public DistributedSystemSettingsBase Settings
        {
            get { return _settings; }
        }

        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="DistributedMeasurementInformationSystemBase" />.
        /// </summary>
        protected DistributedMeasurementInformationSystemBase()
        {
            _plcManager = CreatePlcManager();
            _settings = CreateSettings();
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        protected virtual DistributedSystemSettingsBase CreateSettings()
        {
            return new DistributedSystemSettingsBase();
        }

        ///<summary>
        ///
        ///</summary>
        ///<returns></returns>
        protected virtual PlcManagerBase CreatePlcManager()
        {
            return new PlcManagerBase();
        }

        ///<summary>
        /// ��������� ������������ �������
        ///</summary>
        public virtual void BuildSystemConfiguration()
        {
        }

        #endregion

        #endregion

        ///<summary>
        ///
        ///</summary>
        public virtual void ShowConfiguration()
        {
            throw new System.NotImplementedException();
        }
    }
}
using System.Configuration;
using System.Diagnostics;

namespace Oleg_ivo.WAGO.Configuration
{
    ///<summary>
    ///
    ///</summary>
    [DebuggerDisplay("ComputeCurrentConfiguration={ComputeCurrentConfiguration}, LoadSavedConfiguration={LoadSavedConfiguration}")]
    public class LevelLoadOptionsConfigElement1 : ConfigurationElement//MeasurementSystemLevelLoadOptions
    {

        #region constructors
        ///<summary>
        ///
        ///</summary>
        public LevelLoadOptionsConfigElement1()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loadSavedConfiguration"></param>
        /// <param name="computeCurrentConfiguration"></param>
        public LevelLoadOptionsConfigElement1(bool loadSavedConfiguration,
                                             bool computeCurrentConfiguration)
        {
            LoadSavedConfiguration = loadSavedConfiguration;
            ComputeCurrentConfiguration = computeCurrentConfiguration;
        }

        #endregion

        /// <summary>
        /// ��������� ������� ������������
        /// </summary>
        [ConfigurationProperty("ComputeCurrentConfiguration", DefaultValue = "true", IsRequired = false)]
        public bool ComputeCurrentConfiguration
        {
            get { return (bool)this["ComputeCurrentConfiguration"]; }
            set { this["ComputeCurrentConfiguration"] = value; }
        }

        /// <summary>
        /// ��������� ���������� ������������
        /// </summary>
        [ConfigurationProperty("LoadSavedConfiguration", DefaultValue = "true", IsRequired = false)]
        public bool LoadSavedConfiguration
        {
            get { return (bool)this["LoadSavedConfiguration"]; }
            set { this["LoadSavedConfiguration"] = value; }
        }

        /// <summary>
        /// ������������� �����-���� ������� ���������������� ������������
        /// </summary>
        public bool IsNeedComputeOrLoad
        {
            get { return ComputeCurrentConfiguration || LoadSavedConfiguration; }
        }
    }
}
using System.Collections.Generic;
using System.Configuration;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Configuration
{
    ///<summary>
    ///
    ///</summary>
    public class ConfigurationManager
    {
        #region Singleton

        private static ConfigurationManager _instance;
        private Dictionary<FieldBusType, FieldBusLoadOptions> _fieldBusLoadOptions = new Dictionary<FieldBusType, FieldBusLoadOptions>();

        ///<summary>
        /// ≈динственный экземпл€р
        ///</summary>
        public static ConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigurationManager();
                }
                return _instance;
            }
        }

        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="ConfigurationManager" />.
        /// </summary>
        protected ConfigurationManager()
        {
        }

        #endregion

        /// <summary>
        /// ќпции загрузки полевых шин
        /// </summary>
        public Dictionary<FieldBusType, FieldBusLoadOptions> FieldBusLoadOptions
        {
            get { return _fieldBusLoadOptions; }
        }


        ///<summary>
        ///
        ///</summary>
        ///<param name="s"></param>
        public void LoadConfig(string s)
        {
            System.Configuration.Configuration config =
                System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string sectionGroupName = "DistributedSystemSettings";
            string sectionName = "LoadOptions";
            LoadOptionsSection section = null;
            //section = (LoadOptionsSection)
            //          System.Configuration.ConfigurationManager.GetSection(sectionName);

            ConfigurationSectionGroup sectionGroup = config.GetSectionGroup(sectionGroupName);
            if (sectionGroup==null)
            {
                sectionGroup = new ConfigurationSectionGroup();
                //config..Sections.Add(sectionGroupName, sectionGroup);
            }
            // Create a configuration section and save it
            // to the configuration file.
            section = sectionGroup.Sections[sectionName] as LoadOptionsSection;
            if (section == null)
            {
                section = new LoadOptionsSection();
                config.Sections.Add(sectionName, section);
            }
            else
            {
                section.MyChildSection2.Add(new LoadOptionsConfigElement());
            }

            config.Save();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NLog;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Configuration
{
    ///<summary>
    ///
    ///</summary>
    public class ConfigurationManager
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private string currentConfigName;

        public ConfigurationManager(WagoCommandLineOptions commandLineOptions)
        {
            CommandLineOptions = commandLineOptions;
        }

        /// <summary>
        /// Параметры командной строки
        /// </summary>
        public WagoCommandLineOptions CommandLineOptions { get; private set; }

        /// <summary>
        /// Название текущей конфигурации
        /// </summary>
        public string CurrentConfigName
        {
            get
            {
                return currentConfigName ?? CommandLineOptions.ConfigName;
            }
            set { currentConfigName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDistributedSystemSettings LoadConfig()
        {
            return LoadConfig(CurrentConfigName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configName"></param>
        public IDistributedSystemSettings LoadConfig(string configName)
        {
            Log.Debug("Загрузка настроек системы '{0}' из App.Config", configName);
            var config = GetConfig();
            var section = GetDMISConfigurationSection(config);
            if (string.IsNullOrEmpty(configName) || section.DMISConfigurations[configName] == null)
            {
                Log.Debug("Настройки системы '{0}' не найдены. Всего настроек: {1}",
                          configName,
                          section.DMISConfigurations.Count);
                configName = section.DMISConfigurations[0].Name;
                Log.Debug("Будут использованы первые найденные настройки ('{0}')", configName);
                CurrentConfigName = configName;
            }
            var distributedSystemSettings = LoadConfig(configName, section);
            config.Save();
            return distributedSystemSettings;
        }

        private static System.Configuration.Configuration GetConfig()
        {
            return System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }

        private DMISConfigurationSection GetDMISConfigurationSection(System.Configuration.Configuration config)
        {
            const string sectionName = "dMISConfigurationSection";
            var section = config.Sections[sectionName] as DMISConfigurationSection;
            if (section == null)
            {
                section = new DMISConfigurationSection();
                config.Sections.Add(sectionName, section);
            }
            return section;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="section"></param>
        private IDistributedSystemSettings LoadConfig(string configName, DMISConfigurationSection section)
        {
            var dmisConfig = section.DMISConfigurations[configName];
            if (dmisConfig == null)
            {
                Log.Debug("Конфигурация с указанным именем в секции отсутствует и будет добавлена");
                dmisConfig = new DMISConfigurationElement { Name = configName, IsEmulationMode = true };
                section.DMISConfigurations.Add(dmisConfig);
            }

            foreach (var fieldBusType in Enum.GetNames(typeof(FieldBusType)).Where(fbt=>fbt!=FieldBusType.Unknown.ToString()))
            {
                var loadOptions = dmisConfig.LoadOptions[fieldBusType];
                if (loadOptions == null)
                {
                    Log.Debug("Опции загрузки для полевой шины {0} отсутствуют и будут добавлены", fieldBusType);
                    loadOptions = new LoadOptionsConfigurationElement
                        {
                            FieldBusType = fieldBusType,
                            FieldNodesLevel =
                                new LevelLoadOptionsConfigElement { ComputeCurrentConfiguration = false, LoadSavedConfiguration = true },
                            PhysicalChannelsLevel =
                                new LevelLoadOptionsConfigElement { ComputeCurrentConfiguration = false, LoadSavedConfiguration = true },
                            LogicalChannelsLevel =
                                new LevelLoadOptionsConfigElement { ComputeCurrentConfiguration = false, LoadSavedConfiguration = true }
                        };
                    dmisConfig.LoadOptions.Add(loadOptions);
                }
            }

            Log.Debug("Создание экземпляра IDistributedSystemSettings");
            return dmisConfig.CreateDMISSettings();
        }

    }

    #region Partial configuration classes

    public partial class LevelLoadOptionsConfigElement
    {
        public MeasurementSystemLevelLoadOptions CreateLevelLoadOptions()
        {
            return new MeasurementSystemLevelLoadOptions
                {
                    ComputeCurrentConfiguration = ComputeCurrentConfiguration,
                    LoadSavedConfiguration = LoadSavedConfiguration
                };
        }
    }

    public partial class LoadOptionsConfigurationElement
    {
        public FieldBusLoadOptions CreateLoadOptions()
        {
            var fieldBusType = (FieldBusType)Enum.Parse(typeof(FieldBusType), FieldBusType, true);
            return new FieldBusLoadOptions(fieldBusType)
                {
                    FieldNodesLevel = FieldNodesLevel.CreateLevelLoadOptions(),
                    PhysicalChannelsLevel = PhysicalChannelsLevel.CreateLevelLoadOptions(),
                    LogicalChannelsLevel = LogicalChannelsLevel.CreateLevelLoadOptions(),
                };
        }
    }

    public partial class LoadOptionsConfigurationElementCollection
    {
        public Dictionary<FieldBusType, FieldBusLoadOptions> CreateFieldBusLoadOptions()
        {
            return this.OfType<LoadOptionsConfigurationElement>().ToList()
                       .Select(lo => lo.CreateLoadOptions())
                       .ToDictionary(lo => lo.FieldBusType, lo => lo);
        }
    }

    public partial class DMISConfigurationElement
    {
        public IDistributedSystemSettings CreateDMISSettings()
        {
            return new DistributedSystemSettings
                {
                    Name = Name,
                    IsEmulationMode = IsEmulationMode,
                    FieldBusLoadOptions = LoadOptions.CreateFieldBusLoadOptions()
                };

        }
    }

    #endregion

}
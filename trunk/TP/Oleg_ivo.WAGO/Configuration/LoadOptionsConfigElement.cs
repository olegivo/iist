using System;
using System.Configuration;
using Oleg_ivo.Plc;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Configuration
{
    ///<summary>
    ///
    ///</summary>
    public class LoadOptionsConfigElement : ConfigurationElement//MeasurementSystemLevelLoadOptions
    {

        #region constructors
        ///<summary>
        ///
        ///</summary>
        public LoadOptionsConfigElement()
        {
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="a1"></param>
        ///<param name="a2"></param>
        public LoadOptionsConfigElement(String a1, String a2)
        {
            MyChildAttribute1 = a1;
            MyChildAttribute2 = a2;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="name"></param>
        public LoadOptionsConfigElement(string name)
        {
            Name = name;
        }

        #endregion


        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("FieldBusType", DefaultValue = "Unknown", IsRequired = false)]
        //[StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
            public FieldBusType FieldBusType
        {
            get
            { return (FieldBusType)this["FieldBusType"]; }
            set
            { this["FieldBusType"] = value; }
        }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("myChildAttrib1", DefaultValue = "Zippy", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String MyChildAttribute1
        {
            get
            { return (String)this["myChildAttrib1"]; }
            set
            { this["myChildAttrib1"] = value; }
        }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("myChildAttrib2", DefaultValue = "Michael Zawondy", IsRequired = false)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String MyChildAttribute2
        {
            get
            { return (String)this["myChildAttrib2"]; }
            set
            { this["myChildAttrib2"] = value; }
        }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("Name", DefaultValue = "Microsoft",
                    IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        /// <summary>
        /// Вычислить текущую конфигурацию
        /// </summary>
        [ConfigurationProperty("ComputeCurrentConfiguration", DefaultValue = "true", IsRequired = false)]
        public bool ComputeCurrentConfiguration
        {
            get { return (bool) this["ComputeCurrentConfiguration"]; }
            set { this["ComputeCurrentConfiguration"] = value; }
        }

        /// <summary>
        /// Загрузить сохранённую конфигурацию
        /// </summary>
        [ConfigurationProperty("LoadSavedConfiguration", DefaultValue = "true", IsRequired = false)]
        public bool LoadSavedConfiguration
        {
            get { return (bool)this["LoadSavedConfiguration"]; }
            set { this["LoadSavedConfiguration"] = value; }
        }

        /// <summary>
        /// Необходимость каким-либо образом инициализировать конфигурацию
        /// </summary>
        public bool IsNeedComputeOrLoad
        {
            get { return ComputeCurrentConfiguration || LoadSavedConfiguration; }
        }
    }
}
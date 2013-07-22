using System;
using System.Configuration;
using System.Diagnostics;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Configuration
{
    ///<summary>
    ///
    ///</summary>
    [DebuggerDisplay("FieldBusType={FieldBusType}")]
    public class LoadOptionsConfigElement : ConfigurationElement//MeasurementSystemLevelLoadOptions
    {

        #region constructors
        ///<summary>
        ///
        ///</summary>
        public LoadOptionsConfigElement()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldBusType"></param>
        public LoadOptionsConfigElement(FieldBusType fieldBusType):this()
        {
            FieldBusType = fieldBusType;
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusType"></param>
        public LoadOptionsConfigElement(string fieldBusType):this()
        {
            var o = Enum.Parse(typeof (FieldBusType), fieldBusType);
            FieldBusType = (FieldBusType) o;
        }

        #endregion

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("FieldBusType", DefaultValue = "Unknown", IsRequired = true, IsKey = true)]
        public FieldBusType FieldBusType
        {
            get { return (FieldBusType)this["FieldBusType"]; }
            set { this["FieldBusType"] = value; }
        }

        [ConfigurationProperty("FieldNodesLevel")]
        public LevelLoadOptionsConfigElement FieldNodesLevel
        {
            get { return (LevelLoadOptionsConfigElement) this["FieldNodesLevel"]; }
            set { this["FieldNodesLevel"] = value; }
        }

        [ConfigurationProperty("PhysicalChannelsLevel")]
        public LevelLoadOptionsConfigElement PhysicalChannelsLevel
        {
            get { return (LevelLoadOptionsConfigElement)this["PhysicalChannelsLevel"]; }
            set { this["PhysicalChannelsLevel"] = value; }
        }

        [ConfigurationProperty("LogicalChannelsLevel")]
        public LevelLoadOptionsConfigElement LogicalChannelsLevel
        {
            get { return (LevelLoadOptionsConfigElement)this["LogicalChannelsLevel"]; }
            set { this["LogicalChannelsLevel"] = value; }
        }
    }
}
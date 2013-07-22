using System;
using System.Configuration;

namespace Oleg_ivo.WAGO.Configuration
{
    ///<summary>
    ///
    ///</summary>
    public class DistributedSystemSettingsElement : ConfigurationElement
    {
        ///<summary>
        ///
        ///</summary>
        public DistributedSystemSettingsElement() { }

        ///<summary>
        ///
        ///</summary>
        ///<param name="name"></param>
        public DistributedSystemSettingsElement(String name) { Name = name; }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("Name", DefaultValue = "Default", IsRequired = true, IsKey = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String Name
        {
            get { return (String)this["Name"]; }
            set { this["Name"] = value; }
        }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("IsEmulationMode", DefaultValue = "false", IsRequired = false)]
        public bool IsEmulationMode
        {
            get { return (bool)this["IsEmulationMode"]; }
            set { this["IsEmulationMode"] = value; }
        }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("LoadOptions", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(LoadOptionsCollection),
            AddItemName = "addLoadOptions",
            ClearItemsName = "clearLoadOptions",
            RemoveItemName = "RemoveLoadOptions")]
        public LoadOptionsCollection LoadOptions
        {
            get { return (LoadOptionsCollection)this["LoadOptions"]; }
            set { this["LoadOptions"] = value; }
        }
    }
}
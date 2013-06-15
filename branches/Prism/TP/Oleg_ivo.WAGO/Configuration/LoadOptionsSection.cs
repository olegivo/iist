using System;
using System.Configuration;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Configuration
{
    ///<summary>
    ///
    ///</summary>
    public class LoadOptionsSection : ConfigurationSection
    {
        ///<summary>
        ///
        ///</summary>
        public LoadOptionsSection()
        {
        }

        ///<summary>
        ///
        ///</summary>
        ///<param name="attributeValue"></param>
        public LoadOptionsSection(String attributeValue)
        {
            Name = attributeValue;
        }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("Name", DefaultValue = "Clowns", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String Name
        {
            get
            { return (String)this["Name"]; }
            set
            { this["Name"] = value; }
        }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("myChildSection")]
        public MyChildConfigElement MyChildSection
        {
            get
            { return (MyChildConfigElement)this["myChildSection"]; }
            set
            { this["myChildSection"] = value; }
        }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("LoadOptions", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(LoadOptionsCollection),
            AddItemName = "addLoadOptions",
            ClearItemsName = "clearLoadOptions",
            RemoveItemName = "RemoveLoadOptions")]
        public LoadOptionsCollection MyChildSection2
        {
            get { return (LoadOptionsCollection)this["LoadOptions"]; }
            set { this["LoadOptions"] = value; }
        }


    }
}

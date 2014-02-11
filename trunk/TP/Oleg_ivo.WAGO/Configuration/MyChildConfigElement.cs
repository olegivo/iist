using System;
using System.Configuration;
using Oleg_ivo.Plc.FieldBus;

namespace Oleg_ivo.WAGO.Configuration
{
    ///<summary>
    ///
    ///</summary>
    public class MyChildConfigElement : ConfigurationElement//MeasurementSystemLevelLoadOptions
    {
        ///<summary>
        ///
        ///</summary>
        public MyChildConfigElement()
        {
        }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("FieldBusType", DefaultValue = "Unknown", IsRequired = true)]
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
        ///<param name="a1"></param>
        ///<param name="a2"></param>
        public MyChildConfigElement(String a1, String a2)
        {
            MyChildAttribute1 = a1;
            MyChildAttribute2 = a2;
        }

        ///<summary>
        ///
        ///</summary>
        [ConfigurationProperty("myChildAttrib1", DefaultValue = "Zippy", IsRequired = true)]
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
        [ConfigurationProperty("myChildAttrib2", DefaultValue = "Michael Zawondy", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\", MinLength = 1, MaxLength = 60)]
        public String MyChildAttribute2
        {
            get
            { return (String)this["myChildAttrib2"]; }
            set
            { this["myChildAttrib2"] = value; }
        }
    }
}
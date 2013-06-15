using System;
using System.ComponentModel;

namespace Oleg_ivo.WAGO.Factory
{
    internal class DataSetTypeConverter : TypeConverter
    {
        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
        /// </summary>
        /// <returns>
        /// true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        /// <param name="sourceType">A <see cref="T:System.Type"></see> that represents the type you want to convert from. </param>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return true;
            //return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Returns whether this converter can convert the object to the specified type, using the specified context.
        /// </summary>
        /// <returns>
        /// true if this converter can perform the conversion; otherwise, false.
        /// </returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        /// <param name="destinationType">A <see cref="T:System.Type"></see> that represents the type you want to convert to. </param>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return true;
            //return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Returns whether the given value object is valid for this type and for the specified context.
        /// </summary>
        /// <returns>
        /// true if the specified value is valid for this object; otherwise, false.
        /// </returns>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"></see> that provides a format context. </param>
        /// <param name="value">The <see cref="T:System.Object"></see> to test for validity. </param>
        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return true;
            //return base.IsValid(context, value);
        }
    }
}
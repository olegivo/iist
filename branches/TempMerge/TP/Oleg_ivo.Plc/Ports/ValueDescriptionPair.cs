using System;

namespace Oleg_ivo.Plc.Ports
{
    [Serializable]
    public class ValueDescriptionPair
    {
        /// <summary>
        /// Пара значение - описание
        /// </summary>
        /// <param name="value"></param>
        /// <param name="description"></param>
        public ValueDescriptionPair(object value, string description)
        {
            _value = value;
            _description = description;
        }

        private object _value;
        private string _description;

        /// <summary>
        /// Значение
        /// </summary>
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return Description;
        }
    }
}
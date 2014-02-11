namespace Oleg_ivo.WAGO.Meta
{
    ///<summary>
    /// Описание ПЛК WAGO
    ///</summary>
    public class WagoPlcMeta : WagoMeta
    {
        #region fields
        private ushort _model;

        #endregion

        #region properties
        /// <summary>
        /// Модель
        /// </summary>
        public ushort Model
        {
            get { return _model; }
            set { _model = value; }
        }

        #endregion

        #region constructors

        ///<summary>
        ///
        ///</summary>
        ///<param name="model"></param>
        public WagoPlcMeta(ushort model)
        {
            _model = model;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("Контроллер WAGO {0}", Model>0 ? string.Format("750-{0}", Model) : "");
        }
    }
}
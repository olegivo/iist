namespace Oleg_ivo.WAGO.Meta
{
    ///<summary>
    /// Контейнер для мета-описания WAGO
    ///</summary>
    public interface IWagoMetaContainer
    {
        ///<summary>
        /// Мета-описание
        ///</summary>
        WagoMeta Meta { get; set; }
    }
}
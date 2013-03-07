namespace Oleg_ivo.Plc.Common
{
    ///<summary>
    /// Однозначно идентифицируемый объект
    ///</summary>
    public interface IIdentified
    {
        ///<summary>
        /// Идентификатор
        ///</summary>
        int Id { get; set; }
    }
}
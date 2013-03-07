namespace Oleg_ivo.WAGO.Controls
{
    ///<summary>
    ///
    ///</summary>
    public interface IDbEditor
    {
        ///<summary>
        /// Сохранить
        ///</summary>
        void Save();

        ///<summary>
        /// Заполнить
        ///</summary>
        ///<param name="editValue"></param>
        void Fill(object editValue);
    }
}
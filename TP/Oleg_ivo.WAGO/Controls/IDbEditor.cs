namespace Oleg_ivo.WAGO.Controls
{
    ///<summary>
    ///
    ///</summary>
    public interface IDbEditor
    {
        ///<summary>
        /// ���������
        ///</summary>
        void Save();

        ///<summary>
        /// ���������
        ///</summary>
        ///<param name="editValue"></param>
        void Fill(object editValue);
    }
}
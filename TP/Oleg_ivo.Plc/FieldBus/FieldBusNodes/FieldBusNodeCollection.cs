using System.Collections.Generic;

namespace Oleg_ivo.Plc.FieldBus.FieldBusNodes
{
    ///<summary>
    /// ��������� ����� ������� ����
    ///</summary>
    public class FieldBusNodeCollection : List<FieldBusNode>//TODO:inline?
    {
        #region fields

        #endregion

        #region properties

        #endregion

        #region constructors

        public FieldBusNodeCollection()
        {
        }

        public FieldBusNodeCollection(IEnumerable<FieldBusNode> collection) : base(collection)
        {
        }

        #endregion
    }
}
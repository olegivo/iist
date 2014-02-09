using Oleg_ivo.Plc.FieldBus.FieldBusNodes;

namespace Oleg_ivo.Plc.Devices.Contollers
{
    ///<summary>
    /// ���
    ///</summary>
    public abstract class PLC
    {
        #region fields
        private readonly FieldBusNode _fieldBusNode;

        #endregion

        #region properties

        ///<summary>
        /// ����� ��� �� ����
        ///</summary>
        public FieldBusNode FieldBusNode
        {
            get { return _fieldBusNode; }
        }

        #endregion

        #region constructors
        ///<summary>
        ///
        ///</summary>
        ///<param name="fieldBusNode"></param>
        protected PLC(FieldBusNode fieldBusNode)
        {
            _fieldBusNode = fieldBusNode;
        }

        #endregion

        /// <summary>
        /// ��������� ���������� ������ (+ ������ �����-������)
        /// </summary>
        public abstract void BuildPhysicalChannels();

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("���������� [����� ��� {0}:{1}]", FieldBusNode.AddressPart1, FieldBusNode.AddressPart2);
        }
    }
}
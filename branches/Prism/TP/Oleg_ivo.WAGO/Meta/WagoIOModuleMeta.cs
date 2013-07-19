using Oleg_ivo.Plc.Factory;
using Oleg_ivo.WAGO.Devices;

namespace Oleg_ivo.WAGO.Meta
{
    ///<summary>
    /// �������� ������ �����-������ WAGO
    ///</summary>
    public class WagoIOModuleMeta : WagoMeta
    {

        #region fields
        private readonly bool _isAnalog;
        private readonly bool _isDiscrete;
        private readonly bool _isInput;
        private readonly bool _isOutput;
        private readonly ushort _size;

        #endregion

        #region properties

        ///<summary>
        ///
        ///</summary>
// ReSharper disable MemberCanBePrivate.Global
        public ushort Register { get; private set; }
// ReSharper restore MemberCanBePrivate.Global

        ///<summary>
        /// �����������
        ///</summary>
        public ushort Size
        {
            get { return _size; }
        }

        /// <summary>
        /// ������ ����������
        /// </summary>
        public bool IsAnalog
        {
            get { return _isAnalog; }
        }

        /// <summary>
        /// ������ ����������
        /// </summary>
        public bool IsDiscrete
        {
            get { return _isDiscrete; }
        }

        /// <summary>
        /// ������ �����
        /// </summary>
        public bool IsInput
        {
            get { return _isInput; }
        }

        /// <summary>
        /// ������ ������
        /// </summary>
        public bool IsOutput
        {
            get { return _isOutput; }
        }

        ///<summary>
        /// ����� �� ���� ��� ������
        ///</summary>
        public int WriteAddress { get; set; }

        ///<summary>
        /// ����� �� ���� ��� ������
        ///</summary>
        public ushort ReadAddress { get; set; }

        #endregion

        #region constructors

        ///<summary>
        /// �������� ������ �����-������ WAGO
        ///</summary>
        ///<param name="isAnalog"></param>
        ///<param name="isDiscrete"></param>
        ///<param name="isInput"></param>
        ///<param name="isOutput"></param>
        ///<param name="size"></param>
        ///<param name="register"></param>
        ///<param name="address"></param>
        public WagoIOModuleMeta(bool isAnalog, bool isDiscrete, bool isInput, bool isOutput, ushort size, ushort register, int address)
        {
            _isAnalog = isAnalog;
            WriteAddress = address;
            _isOutput = isOutput;
            _isInput = isInput;
            _isDiscrete = isDiscrete;
            _size = size;
            Register = register;
        }

        #endregion

        ///<summary>
        /// ������� ������ �� ��������
        ///</summary>
        ///<returns></returns>
        public WagoIOModule CreateModule(ILogicalChannelsFactory logicalChannelFactory)
        {
            WagoIOModule module = new WagoIOModule(logicalChannelFactory) {Meta = this};
            return module;
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
            string ad = "";
            if (IsAnalog)
                ad += "����������";
            if (IsDiscrete)
                ad += "����������";

            string io = "";
            if (IsInput)
            {
                io += "�����";
                if (IsOutput)
                    io += "-";
            }
            if (IsOutput)
                io += "������";

            if (Size>0)
            {
                io += string.Format(". ������ - {0} {1}",
                    IsDiscrete && !IsAnalog ? Size : (ushort)(Size / 16), 
                    IsAnalog ? "���������" : IsDiscrete ? "���" : "");
            }

            string model = Register < 1000 ? string.Format(" [{0}]", Register) : "";
            return string.Format("WAGO {2}: {0} ������ {1}", ad, io, model);
        }

    }
}
using Oleg_ivo.Plc.Factory;
using Oleg_ivo.WAGO.Devices;

namespace Oleg_ivo.WAGO.Meta
{
    ///<summary>
    /// Описание модуля ввода-вывода WAGO
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
        /// Разрядность
        ///</summary>
        public ushort Size
        {
            get { return _size; }
        }

        /// <summary>
        /// Модуль аналоговый
        /// </summary>
        public bool IsAnalog
        {
            get { return _isAnalog; }
        }

        /// <summary>
        /// Модуль дискретный
        /// </summary>
        public bool IsDiscrete
        {
            get { return _isDiscrete; }
        }

        /// <summary>
        /// Модуль ввода
        /// </summary>
        public bool IsInput
        {
            get { return _isInput; }
        }

        /// <summary>
        /// Модуль вывода
        /// </summary>
        public bool IsOutput
        {
            get { return _isOutput; }
        }

        ///<summary>
        /// Адрес на шине для записи
        ///</summary>
        public int WriteAddress { get; set; }

        ///<summary>
        /// Адрес на шине для чтения
        ///</summary>
        public ushort ReadAddress { get; set; }

        #endregion

        #region constructors

        ///<summary>
        /// Описание модуля ввода-вывода WAGO
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
        /// Создать модуль по описанию
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
                ad += "аналоговый";
            if (IsDiscrete)
                ad += "дискретный";

            string io = "";
            if (IsInput)
            {
                io += "ввода";
                if (IsOutput)
                    io += "-";
            }
            if (IsOutput)
                io += "вывода";

            if (Size>0)
            {
                io += string.Format(". Размер - {0} {1}",
                    IsDiscrete && !IsAnalog ? Size : (ushort)(Size / 16), 
                    IsAnalog ? "регистров" : IsDiscrete ? "бит" : "");
            }

            string model = Register < 1000 ? string.Format(" [{0}]", Register) : "";
            return string.Format("WAGO {2}: {0} модуль {1}", ad, io, model);
        }

    }
}
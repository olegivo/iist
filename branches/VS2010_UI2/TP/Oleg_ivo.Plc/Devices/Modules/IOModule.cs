using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.Plc.Devices.Modules
{
    ///<summary>
    /// Модуль ввода-вывода
    ///</summary>
    public abstract class IOModule : IIOModule
    {
        private PhysicalChannel _physicalChannel;

        #region fields

        #endregion

        #region properties

        ///<summary>
        ///
        ///</summary>
        public PhysicalChannel PhysicalChannel
        {
            get { return _physicalChannel; }
            protected internal set
            {
// ReSharper disable RedundantCheckBeforeAssignment
                if (_physicalChannel != value) _physicalChannel = value;
// ReSharper restore RedundantCheckBeforeAssignment
            }
        }

        ///<summary>
        /// Разрядность модуля
        ///</summary>
        public abstract ushort Size { get; }

        ///<summary>
        /// Адрес на шине для записи
        ///</summary>
        public abstract int WriteAddress { get; set; }

        ///<summary>
        /// Адрес на шине для чтения
        ///</summary>
        public abstract int ReadAddress { get; set; }

        /// <summary>
        /// Модуль аналоговый
        /// </summary>
        public abstract bool IsAnalog { get; }

        /// <summary>
        /// Модуль дискретный
        /// </summary>
        public abstract bool IsDiscrete { get; }

        ///<summary>
        /// Модуль ввода
        ///</summary>
        public abstract bool IsInput { get; }

        /// <summary>
        /// Модуль вывода
        /// </summary>
        public abstract bool IsOutput { get; }

        #endregion

        #region constructors

        #endregion

        #region methods
        /// <summary>
        /// Построить логические каналы по умолчанию для данного физического канала
        /// </summary>
        public abstract LogicalChannelCollection BuildDefaultLogicalChannels();

        /// <summary>
        /// Предикат для поиска модуля ввода-вывода
        /// адресу чтения (<see cref="ReadAddress"/>)
        /// и адресу записи (<see cref="WriteAddress"/>)
        /// </summary>
        /// <param name="ioModule">Образец для сравнения</param>
        /// <param name="withAddresses">Учитывать адреса</param>
        /// <param name="withTypes">Учитывать типы модулей (дискретный/аналоговый, входной/выходной)</param>
        /// <param name="withSize">Учитывать размер модуля ()</param>
        /// <returns></returns>
        public bool EqualsPredicate(IOModule ioModule, bool withAddresses, bool withTypes, bool withSize)
        {
            return ioModule != null

                   && (Equals(ioModule)
                       ||
                       (
                           (!withAddresses
                            || (ioModule.ReadAddress == ReadAddress
                                && ioModule.WriteAddress == WriteAddress
                               )
                           )
                           && (!withTypes
                               || (ioModule.IsAnalog == IsAnalog
                                   && ioModule.IsDiscrete == IsDiscrete
                                   && ioModule.IsInput == IsInput
                                   && ioModule.IsOutput == IsOutput
                                  )
                              )
                           && (!withSize
                               || (ioModule.Size == Size)
                              )
                       ));
        }

        #endregion
    }
}
using Oleg_ivo.Plc.Channels;

namespace Oleg_ivo.Plc.Devices.Modules
{
    ///<summary>
    /// ������ �����-������
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
        /// ����������� ������
        ///</summary>
        public abstract ushort Size { get; }

        ///<summary>
        /// ����� �� ���� ��� ������
        ///</summary>
        public abstract int WriteAddress { get; set; }

        ///<summary>
        /// ����� �� ���� ��� ������
        ///</summary>
        public abstract int ReadAddress { get; set; }

        /// <summary>
        /// ������ ����������
        /// </summary>
        public abstract bool IsAnalog { get; }

        /// <summary>
        /// ������ ����������
        /// </summary>
        public abstract bool IsDiscrete { get; }

        ///<summary>
        /// ������ �����
        ///</summary>
        public abstract bool IsInput { get; }

        /// <summary>
        /// ������ ������
        /// </summary>
        public abstract bool IsOutput { get; }

        #endregion

        #region constructors

        #endregion

        #region methods
        /// <summary>
        /// ��������� ���������� ������ �� ��������� ��� ������� ����������� ������
        /// </summary>
        public abstract LogicalChannelCollection BuildDefaultLogicalChannels();

        /// <summary>
        /// �������� ��� ������ ������ �����-������
        /// ������ ������ (<see cref="ReadAddress"/>)
        /// � ������ ������ (<see cref="WriteAddress"/>)
        /// </summary>
        /// <param name="ioModule">������� ��� ���������</param>
        /// <param name="withAddresses">��������� ������</param>
        /// <param name="withTypes">��������� ���� ������� (����������/����������, �������/��������)</param>
        /// <param name="withSize">��������� ������ ������ ()</param>
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
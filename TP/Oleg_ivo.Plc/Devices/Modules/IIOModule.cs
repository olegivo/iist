namespace Oleg_ivo.Plc.Devices.Modules
{
    ///<summary>
    /// ��������� ������ �����-������
    ///</summary>
    public interface IIOModule
    {
        ///<summary>
        /// �����������
        ///</summary>
        ushort Size { get; }

        /// <summary>
        /// ������ ����������
        /// </summary>
        bool IsAnalog { get; }

        /// <summary>
        /// ������ ����������
        /// </summary>
        bool IsDiscrete { get; }

        /// <summary>
        /// ������ �����
        /// </summary>
        bool IsInput { get; }

        /// <summary>
        /// ������ ������
        /// </summary>
        bool IsOutput { get; }
    }
}
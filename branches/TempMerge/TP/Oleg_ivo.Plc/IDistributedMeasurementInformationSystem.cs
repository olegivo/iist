namespace Oleg_ivo.Plc
{
    public interface IDistributedMeasurementInformationSystem
    {
        ///<summary>
        /// ��������� ��� �������
        ///</summary>
        IPlcManager PlcManager { get; }

        ///<summary>
        /// ��������� ������������� �������
        ///</summary>
        IDistributedSystemSettings Settings { get; }

        ///<summary>
        /// ��������� ������������ �������
        ///</summary>
        void BuildSystemConfiguration();

        ///<summary>
        ///
        ///</summary>
        void ShowConfiguration();
    }
}
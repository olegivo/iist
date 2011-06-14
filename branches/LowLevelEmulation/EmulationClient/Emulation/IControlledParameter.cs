namespace EmulationClient.Emulation
{
    /// <summary>
    /// �������������� ��������
    /// </summary>
    public interface IControlledParameter
    {
        /// <summary>
        /// �������� ��������������� ���������
        /// </summary>
        double OutputValue { get; }

        /// <summary>
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        void Refresh();
    }
}
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
        double GetOutputValue();

        /// <summary>
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        void Refresh();
    }
}
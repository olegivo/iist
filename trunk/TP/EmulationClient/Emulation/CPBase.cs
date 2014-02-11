namespace EmulationClient.Emulation
{
    /// <summary>
    /// ������� ���������� ��������������� ���������
    /// </summary>
    public abstract class CPBase : IControlledParameter
    {
        /// <summary>
        /// �������� �������� ��������������� ���������. ������������ ��� ��������� � ������
        /// </summary>
        protected double _outputValue;

        /// <summary>
        /// �������� ���������� �������� ��������������� ���������
        /// </summary>
        public double GetOutputValue()
        {
            Refresh();
            return _outputValue;
        }

        /// <summary>
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        public abstract void Refresh();
    }
}
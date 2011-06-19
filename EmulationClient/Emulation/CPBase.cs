namespace EmulationClient.Emulation
{
    public abstract class CPBase
    {
        /// <summary>
        /// �������� ��������������� ���������
        /// </summary>
        public double OutputValue { get; set; }

       /// <summary>
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        public abstract void Refresh();
    }
}
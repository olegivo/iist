namespace EmulationClient.Emulation
{
    public abstract class CPBase
    {
        /// <summary>
        /// �������� ��������������� ���������
        /// </summary>
        public double OutputValue
        {
            get
        {
            Refresh();
            return 0d;
        }
            set {  }
        }

        /// <summary>
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        public abstract void Refresh();
    }
}
namespace EmulationClient.Emulation
{
    public abstract class CPBase
    {
        public double outputvalue;
        /// <summary>
        /// �������� ��������������� ���������
        /// </summary>
        public double OutputValue
        {
            get
            {
                Refresh();
                return  outputvalue;
            }
            set { outputvalue = value; }
        }

        /// <summary>
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        public abstract void Refresh();
    }
}
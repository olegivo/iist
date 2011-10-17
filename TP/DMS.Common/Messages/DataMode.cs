namespace DMS.Common.Messages
{
    /// <summary>
    /// ����� ������
    /// </summary>
    public enum DataMode
    {
        /// <summary>
        /// ����������
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// ����� ������ (�� ����� �������������� ��������� � ����� ���������������)
        /// </summary>
        Read = 1,

        /// <summary>
        /// ����� ������ (�� ����� ��������������� ��������� � ����� ��������������)
        /// </summary>
        Write = 2
    }
}
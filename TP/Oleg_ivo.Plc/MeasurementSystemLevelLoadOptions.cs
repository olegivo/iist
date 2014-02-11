namespace Oleg_ivo.Plc
{
    /// <summary>
    /// ����� �������� ������ ������������
    /// </summary>
    public class MeasurementSystemLevelLoadOptions
    {
        /// <summary>
        /// ��������� ������� ������������
        /// </summary>
        public bool ComputeCurrentConfiguration { get; set; }

        /// <summary>
        /// ��������� ���������� ������������
        /// </summary>
        public bool LoadSavedConfiguration { get; set; }

        /// <summary>
        /// ������������� �����-���� ������� ���������������� ������������
        /// </summary>
        public bool IsNeedComputeOrLoad
        {
            get { return ComputeCurrentConfiguration || LoadSavedConfiguration; }
        }
    }
}
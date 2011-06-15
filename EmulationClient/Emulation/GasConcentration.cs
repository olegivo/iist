using System;

namespace EmulationClient.Emulation
{
    public class GasConcentration : IControlledParameter
    {
        /// <summary>
        /// �������� ��������������� ���������
        /// </summary>
        public double OutputValue
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// ������� ��������: ����������� ����� �������� �������� 
        /// </summary>
        double Temperature();

        /// <summary>
        /// ������� ��������: ����������� �������� ��������  
        /// </summary>
        double Speed();

        /// <summary>
        /// ������� ��������: ������� ���.\����.
        /// </summary>
        bool Burner();


        /// <summary>
        /// ������������ �������������� ���� �� 
        /// </summary>
        double CCO();

        /// <summary>
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        public void Refresh()
        {
            throw new NotImplementedException();
        }

        //todo: �������� ������� ���������
    }
}
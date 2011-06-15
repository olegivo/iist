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
        public double Temperature;

        /// <summary>
        /// ������� ��������: ����������� �������� ��������  
        /// </summary>
        public double Speed;

        /// <summary>
        /// ������� ��������: ������� ���.\����.
        /// </summary>
        public bool Burner;


        /// <summary>
        /// ������������ �������������� ���� �� 
        /// </summary>
        public double CCO;

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
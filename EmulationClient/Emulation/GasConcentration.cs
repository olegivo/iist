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
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        public void Refresh()
        {
            throw new NotImplementedException();
        }

        //todo: �������� ������� ���������
    }
}
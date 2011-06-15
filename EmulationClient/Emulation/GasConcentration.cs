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
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// ������� ��������: ����������� ����� �������� �������� 
        /// </summary>
        public double Temperature;
        
            //if (Burner = true);
            //{
            //Temperature = Math.Exp(1/2*DateTime.Now.Second);
            //}
            //else          
            //{
            //Temperature = Math.Exp(-1/2*DateTime.Now.Second);
            //}
        

        /// <summary>
        /// ������� ��������: ����������� �������� ��������  
        /// </summary>
        public double Speed;


        /// <summary>
        /// ������� ��������: ������� ���.\����.
        /// </summary>
        public bool Burner;
       
        /// <summary>
        /// �������� �������� (������������ ������� ��������� ������� ���������� � ��������)
        /// </summary>
        public void Refresh()
        {
            DateTime now = DateTime.Now; //todo:������������� ����� � ����������.
           int second = now.Second;
           OutputValue = Math.Abs(Math.Sin(0.005*second))* 500 + 3500 + Temperature + Speed;

            throw new NotImplementedException();
        }

    }
}
using System;
using System.IO;
using System.Linq;

namespace Oleg_ivo.Plc.Channels
{
    /// <summary>
    /// �������, �������������� ������� ������������� ��� ��������
    /// </summary>
    public class Polynom
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly DtsPolynom PowerCoefficients = new DtsPolynom();

        /// <summary>
        /// �������� �������� �������� �� ���������
        /// </summary>
        /// <param name="argument">��������</param>
        /// <returns></returns>
        public double GetValue(double argument)
        {
            return PowerCoefficients.PolynomCoefficients.Sum(polynomCoefficient => polynomCoefficient.Coefficient * Math.Pow(argument, polynomCoefficient.Power));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="polynom"></param>
        /// <returns></returns>
        public static string SerializePolynom(Polynom polynom)
        {
/*
            // ������� �����������
            XmlSerializer sr = new XmlSerializer(typeof(Polynom));

            // ������� writer, � ������� ����� ����������� ������������
            StringBuilder sb = new StringBuilder();
            StringWriter w = new StringWriter(sb, System.Globalization.CultureInfo.InvariantCulture);

            // �����������
            sr.Serialize(w, polynom);
*/

            // �������� ������ Xml
            string xml;
//            xml = sb.ToString();
            xml = polynom.PowerCoefficients.GetXml();
            Console.WriteLine(xml);
            
            byte[] bytes = xml.Select(c => (byte)c).ToArray();
            //TODO: System.Compression?
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static Polynom DeSerializePolynom(string xml)
        {
            // ������� reader
            char[] chars = Convert.FromBase64String(xml).Select(b => (char)b).ToArray();
            string s = new string(chars);
            Console.WriteLine(s);

            StringReader reader = new StringReader(s);

            // ������������� 
            Polynom clone = new Polynom();
            clone.PowerCoefficients.ReadXml(reader);

            return clone;
        }

    }
}
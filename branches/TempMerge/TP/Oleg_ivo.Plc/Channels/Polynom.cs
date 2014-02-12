using System;
using System.Data;
using System.IO;
using System.Linq;
using Oleg_ivo.Base.Extensions;

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
            return
                PowerCoefficients.PolynomCoefficients.AsEnumerable()
                    .Sum(
                        polynomCoefficient =>
                            polynomCoefficient.Coefficient*Math.Pow(argument, polynomCoefficient.Power));
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return "y = " + PowerCoefficients.PolynomCoefficients.AsEnumerable()
                .Select(
                    polynomCoefficient =>
                        string.Format("{0}*x^{1}", polynomCoefficient.Coefficient, polynomCoefficient.Power))
                .JoinToString(" + ");
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
            //Log.Debug(xml);
            
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
            //Log.Debug(s);

            StringReader reader = new StringReader(s);

            // ������������� 
            Polynom clone = new Polynom();
            clone.PowerCoefficients.ReadXml(reader);

            return clone;
        }

    }
}
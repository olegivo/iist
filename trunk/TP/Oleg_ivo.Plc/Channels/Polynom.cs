using System;
using System.IO;
using System.Linq;

namespace Oleg_ivo.Plc.Channels
{
    /// <summary>
    /// Полином, представленный словарём коэффициентов при степенях
    /// </summary>
    public class Polynom
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly DtsPolynom PowerCoefficients = new DtsPolynom();

        /// <summary>
        /// Получить значение полинома от аргумента
        /// </summary>
        /// <param name="argument">Аргумент</param>
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
            // создаем сериалайзер
            XmlSerializer sr = new XmlSerializer(typeof(Polynom));

            // создаем writer, в который будет происходить сериализация
            StringBuilder sb = new StringBuilder();
            StringWriter w = new StringWriter(sb, System.Globalization.CultureInfo.InvariantCulture);

            // сериализуем
            sr.Serialize(w, polynom);
*/

            // получаем строку Xml
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
            // создаем reader
            char[] chars = Convert.FromBase64String(xml).Select(b => (char)b).ToArray();
            string s = new string(chars);
            Console.WriteLine(s);

            StringReader reader = new StringReader(s);

            // десериализуем 
            Polynom clone = new Polynom();
            clone.PowerCoefficients.ReadXml(reader);

            return clone;
        }

    }
}
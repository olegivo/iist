using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Oleg_ivo.Base.Extensions;

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

        public Dictionary<short, double> Dictionary { get; set; }

        /// <summary>
        /// Получить значение полинома от аргумента
        /// </summary>
        /// <param name="argument">Аргумент</param>
        /// <returns></returns>
        public double GetValue(double argument)
        {
            if (Dictionary != null)
                return
                    Dictionary
                        .Sum(
                            polynomCoefficient =>
                                polynomCoefficient.Value*Math.Pow(argument, polynomCoefficient.Key));
            return
                PowerCoefficients.PolynomCoefficients.AsEnumerable()
                    .Sum(
                        polynomCoefficient =>
                            polynomCoefficient.Coefficient * Math.Pow(argument, polynomCoefficient.Power));
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
            if (Dictionary != null)
                return "y = " + Dictionary.OrderBy(item => item.Key)
                    .Select(
                        polynomCoefficient =>
                            string.Format("{0}*x^{1}", polynomCoefficient.Value, polynomCoefficient.Key))
                    .JoinToString(" + ");
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
            if (polynom.Dictionary != null)
            {
                var x =
                    new XDocument(new XElement("DtsPolynom",
                        polynom.Dictionary.OrderBy(item => item.Key)
                            .Select(
                                polynomCoefficient =>
                                    new XElement("PolynomCoefficients", new XElement("Power", polynomCoefficient.Key),
                                        new XElement("Coefficient", polynomCoefficient.Value.ToString(CultureInfo.GetCultureInfo("en")))))));
                xml = x.ToString();
            }
            else
            {
                xml = polynom.PowerCoefficients.GetXml();
            }

            //            xml = sb.ToString();
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
            // создаем reader
            char[] chars = Convert.FromBase64String(xml).Select(b => (char)b).ToArray();
            string s = new string(chars);
            //Log.Debug(s);

            // десериализуем 
            var clone = new Polynom();
            using (var reader = new StringReader(s))
            {
                clone.PowerCoefficients.ReadXml(reader);
            }
            
            //var xDocument = XDocument.Parse(s);
            //clone.Dictionary =
            //    xDocument.Root.Elements()
            //        .ToDictionary(element => short.Parse(element.Element("Power").Value),
            //            element => double.Parse(element.Element("Coefficient").Value, CultureInfo.InvariantCulture));

            clone.Dictionary = clone.PowerCoefficients.PolynomCoefficients.AsEnumerable()
                .ToDictionary(item => item.Power, item => item.Coefficient);

            return clone;
        }

    }
}
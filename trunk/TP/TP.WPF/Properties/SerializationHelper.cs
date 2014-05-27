using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace TP.WPF.Properties
{
    public static class SerializationHelper
    {
        public static string SerializeToString<T>(this T obj)//where T:ISerializable
        {
            var stringBuilder = new StringBuilder();
            var serializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter(stringBuilder))
            {
                serializer.Serialize(stringWriter, obj);
            }
            return stringBuilder.ToString();
        }

        public static T Deserialize<T>(this string serialization)//where T:ISerializable
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(serialization))
                return (T) serializer.Deserialize(stringReader);
        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Serializer
{
    public static class XmlSerialize
    {
        public static string Serialize<T>(T obj)
        {
            XmlSerializer x = new XmlSerializer(obj.GetType());
            StringBuilder xmlString = new StringBuilder();
            TextWriter write = new StringWriter(xmlString);
            x.Serialize(write, obj);
            return xmlString.ToString();
        }

        public static T Deserialize<T>(string Xml)
        {
            T result;
            var x = new XmlSerializer(typeof(T));

            using (TextReader reader = new StringReader(Xml))
            {
                result = (T)x.Deserialize(reader);
            }
            return result;
        }
    }
}

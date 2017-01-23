namespace CdrParser.Util
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.Linq;

    public class Helper
    {
        private static Helper _instance = null;

        private Helper()
        {
        }

        public static Helper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Helper();
                }
                return _instance;
            }
        }

        public T DeserializeFromXML<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader stream = new StringReader(xml);
            T obj = (T)serializer.Deserialize(stream);
            stream.Close();
            return obj;
        }

        public string SerializeToXML<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter stream = new StringWriter())
            {
                serializer.Serialize(stream, obj);
                stream.Close();
                return stream.ToString();
            }
        }
    }
}

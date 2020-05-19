using System;
using System.IO;
using System.Xml.Serialization;

namespace Bridge.Utilities
{
    public class Serializer
    {
        public static void SerializeToFile(Object obj, String strFile)
        {
            TextWriter writer = new StreamWriter( strFile );
            new XmlSerializer(obj.GetType()).Serialize(writer, obj);
            writer.Close();
        }

        public static Object DeserializeFile(String strFile,Type type)
        {
            Object obj = null;

            if (File.Exists(strFile))
            {
                TextReader reader = new StreamReader(strFile);

                obj = new XmlSerializer(type).Deserialize(reader);
                reader.Close();
            }

            return obj;
        }

    }
}

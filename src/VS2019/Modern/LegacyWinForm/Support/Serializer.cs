using System;
using System.IO;
using System.Xml.Serialization;

namespace LegacyWinForm.Support
{
    public class Serializer : ISerializer
    {
        public bool SerializeToFile(XmlSerializer serializer, Object obj, string fileName)
        {
            CreateDirectoryIfNotExist(fileName);

            using (StreamWriter strWriter = new StreamWriter(fileName))
            {
                serializer.Serialize(strWriter, obj);
            }
            return true; 

        }

        public Object DeserializeFile(XmlSerializer serializer, string fileName)
        {
            Object obj = null;

            CreateDirectoryIfNotExist(fileName);
            if (!File.Exists(fileName))
                return null;
            else
            {
                using (StreamReader strReader = new StreamReader(fileName))
                {
                    obj = serializer.Deserialize(strReader);
                }
            }
            return obj;
        }

        public void CreateDirectoryIfNotExist(string fileName)
        {
            string directoryName = Path.GetDirectoryName(fileName);
            if ((directoryName.Length > 0) && (!Directory.Exists(directoryName)))
            {
                Directory.CreateDirectory(directoryName);
            }
        }
    }
}

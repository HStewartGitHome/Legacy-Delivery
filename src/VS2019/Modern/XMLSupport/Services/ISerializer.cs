using System.Xml.Serialization;

namespace XMLSupport.Services
{
    public interface ISerializer
    {
        void CreateDirectoryIfNotExist(string fileName);
        object DeserializeFile(XmlSerializer serializer, string fileName);
        bool SerializeToFile(XmlSerializer serializer, object obj, string fileName);
    }
}
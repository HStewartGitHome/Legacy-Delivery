
using DeliverySupport.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using XMLSupport.Models;
using XMLSupport.Services;

namespace GetServiceXML
{
    public class Process
    {
        private ILogger<Process> _logger;
        private IXMLClient _XMLClient;

        public Process(ILogger<Process> logger)
        {
            _logger = logger;

            ServiceProvider serviceProvider = MyServiceFactory.GetServiceProvider();
            _XMLClient = serviceProvider.GetService<XMLClient>();
        }

        public bool ProcessXML(string type, string fileName)
        {
            _logger.LogInformation("Processing XML Type={type} with FileName={fileName}", type, fileName);
            if (type.CompareTo("GETITEMLIST") == 0)
                return ProcessGetItemList(fileName);
            else if (type.CompareTo("GELOCALITEMLIST") == 0)
            {
                _logger.LogInformation("Return true, local only");
                return true;
            }

            else if (type.CompareTo("SEEDITEMLIST") == 0)
                return ProcessSeedItemList(fileName);
            else if (type.CompareTo("GETMESSAGE") == 0)
                return ProcessGetMessage(fileName);
            else
            {
                _logger.LogWarning("Method {methodtype} not defined", type);
                return false;
            }
        }

        public bool ProcessGetItemList(string fileName)
        {
            bool result = false;

            _logger.LogInformation("GetItemList");

            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<XMLSupport.Models.Item>));


            List<XMLSupport.Models.Item> items = _XMLClient.GetXMLItems();

            result = serializer.SerializeToFile(xmlSerializer, items, fileName);
            if (result == true)
                _logger.LogInformation("Successfully transfered to {filename}", fileName);
            else
                _logger.LogInformation("Failed to transfered to {filename}", fileName);

            return result;
        }


        public bool ProcessSeedItemList(string fileName)
        {
            _logger.LogInformation("SeedItemList");

            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Item>));

            List<Item> items = (List<Item>)serializer.DeserializeFile(xmlSerializer, fileName);
            return _XMLClient.UpdateFromXML(items);
        }

        public bool ProcessGetMessage(string fileName)
        {
            bool result = false;
            _logger.LogInformation("GetMessage");
            string location = "";
            DateTime time;

            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Message));

            // deserialize the input to get location and time
            Message messageInput = (Message)serializer.DeserializeFile(xmlSerializer, fileName);
            time = DateTime.Parse(messageInput.Time);
            location = messageInput.Location;

            _logger.LogInformation("Time={messsagetime} which is Time of {time}", messageInput.Time, time);
            _logger.LogInformation("Location= {location}", location);

            Message message = _XMLClient.GetMessageXML(time, location);

            if (message == null)
            {
                _logger.LogWarning("Message is null");
                File.Delete(fileName);
            }
            else
            {
                _logger.LogInformation("Message with ToName {messagetoname} and FromName {messagefromname} and time {messagetime}",
                                        message.ToName, message.FromName, message.Time);

                result = serializer.SerializeToFile(xmlSerializer, message, fileName);
                if (result == true)
                    _logger.LogInformation("Successfully transfered to {filename} ", fileName);
                else
                    _logger.LogWarning("Failed to transfered to {filename}", fileName);
            }

            return result;
        }

    }
}

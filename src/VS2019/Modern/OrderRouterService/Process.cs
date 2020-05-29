
using DeliverySupport.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMLSupport.Models;
using XMLSupport.Services;

namespace OrderRouterService
{
    public class Process : IProcess
    {
        private ILogger<Process> _logger;
        private string dirRouter = "";
        private IXMLClient _XMLClient;


        public Process(ILogger<Process> logger)
        {
            string dirOutgoing, dirErrors; ;

            _logger = logger;
            ServiceProvider serviceProvider = MyServiceFactory.GetServiceProvider();
            _XMLClient = serviceProvider.GetService<XMLClient>();
            IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
            dirRouter = configuration.GetValue<string>("AppSettings:RouterDir");
            dirOutgoing = dirRouter + @"\Outgoing";
            dirErrors = dirRouter + @"\Errors";
            _logger.LogInformation("Router Directory=" + dirRouter);
            _logger.LogInformation("Router Outgoing Directory=" + dirOutgoing);
            _logger.LogInformation("Router Errors Directory=" + dirErrors);
            if (!Directory.Exists(dirOutgoing))
                Directory.CreateDirectory(dirOutgoing);
            if (!Directory.Exists(dirErrors))
                Directory.CreateDirectory(dirErrors);
        }

        public async Task<bool> ProcessNextFile()
        {
            bool result = false;
            _logger.LogInformation("Processing Next file");

            string file = GetNextFile();
            if ((file != null) && (file.Length > 0))
            {
                result = ProcessFile(file);
                if (result)
                {
                    _logger.LogInformation("File {filename} is processed, moving to outgoing", file);
                    File.Move(file, $"{dirRouter}\\Outgoing\\{Path.GetFileName(file)}");
                }
                else
                {
                    _logger.LogInformation("File {filename} failed to  processed, moving to Errors", file);
                    File.Move(file, $"{dirRouter}\\Errors\\{Path.GetFileName(file)}");
                }
            }
            else
                _logger.LogInformation("file is empty, done with files found");


            result = true;
            await Task.WhenAll();
            return result;

        }

        private bool ProcessFile(string file)
        {
            bool result = false;

            try
            { 
                var serializer = new Serializer();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Context));

                Context contextReturn = (Context)serializer.DeserializeFile(xmlSerializer, file);
                if (contextReturn != null)
                {
                    if (contextReturn.RequestType == 1)
                        result = ProcessOrderContext(contextReturn);
                    else
                        result = ProcessMessageContext(contextReturn);
                    if (result)
                        _logger.LogInformation("Context has been processed");
                    else
                        _logger.LogWarning("Context failed to be processed");
                }
            }
            catch( Exception e )
            {
                _logger.LogError("Exception trying to process context", e);
            }

            return result;
        }

        private bool ProcessMessageContext(Context contextReturn)
        {
            _logger.LogInformation("Message processing");
            Message message = contextReturn.MessageObj;
            string time = contextReturn.Time;
            string location = contextReturn.Location;

            _XMLClient.IsTestObject = false;
            bool result = _XMLClient.CreateMessageFromXML(message, time, location);

            return true;
        }

        private bool ProcessOrderContext(Context contextReturn)
        {
            _logger.LogInformation("Order processing");
            Order order = contextReturn.InOrder;
            string time = contextReturn.Time;
            string location = contextReturn.Location;

            _XMLClient.IsTestObject = false;
            bool result = _XMLClient.CreateOrderFromXML(order, time, location);

            return true;
        }

        private string GetNextFile()
        {
            string result = "";

            var files = Directory.GetFiles(dirRouter, "*.xml");

            if (files.Length > 0)
                result = files[0];

            return result;
        }
    }
}

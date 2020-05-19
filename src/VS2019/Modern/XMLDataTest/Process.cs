using DeliverySupport.Data;
using DeliverySupport.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using XMLSupport.Models;
using XMLSupport.Services;

// This class was originally in unit testing logic but made as seperate command line application
// so that unit testing logic does not alter the database.

namespace XMLDataTest
{
    public class Process
    {
        private readonly ILogger _logger = null;
        private IConfiguration _configuration = null;
        private IXMLClient _XMLClient;

        public Process(ILogger<Process> logger)
        {
            _logger = logger;


            string settingsFile = "xmldatatest.json";
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(settingsFile, false, true)
                .Build();

            ServiceProvider serviceProvider = MyServiceFactory.GetServiceProvider();
            _XMLClient = serviceProvider.GetService<XMLClient>();
        }


        public void ProcessAllTests()
        {
            _logger.LogInformation("Starting XMLDataTest");
            TestUpdateFromXML();
            TestUpdateFromXMLWithGetXMLItems();
            TestCreateMessageFromXML();
            TestCreateOrderFromXML();
            TestCreateOrderFromXML2();
            TestGetMessageXML();
            TestSeedItemsSimData();
            TestUpdateDefaultItems();
            _logger.LogInformation("Finished XMLDataTest");
        }

        public void TestUpdateFromXML()
        {
            _logger.LogInformation("Testing UpdateFromXML");

            var dataFactory = new XMLDataFactory();
            List<XMLSupport.Models.Item> items = dataFactory.MakeXMLItemList();
            XMLSupport.Models.Item item = dataFactory.MakeXMLItem(9001, "Test Item 1", 2.99, 1, "Test1.png", 1000);
            items.Add(item);
            item = dataFactory.MakeXMLItem(9002, "Test Item 2", 2.99, 1, "Test2.png", 1000);
            items.Add(item);


            _XMLClient.IsTestObject = true;
            bool result = _XMLClient.UpdateFromXML(items);
            if (result == true)
                _logger.LogInformation("successfully ran UpdateFromXML without GetXMLItems");
            else
                _logger.LogWarning("failed to run UpdateFromXML without GetXMLItems");

            // clean up test objects inserted
            result = _XMLClient.CleanUpTestItems();
            if (result == true)
                _logger.LogInformation("Successfully clean up test items");
            else
                _logger.LogWarning("Failed to clean up test items");
        }

        public void TestUpdateFromXMLWithGetXMLItems()
        {
            _logger.LogInformation("Testing UpdateFromXML with GetXMLItems");

            var dataFactory = new XMLDataFactory();
            List<XMLSupport.Models.Item> items = dataFactory.MakeXMLItemList();
            XMLSupport.Models.Item item = dataFactory.MakeXMLItem(9001, "Test Item 1", 2.99, 1, "Test1.png", 1000);
            items.Add(item);
            item = dataFactory.MakeXMLItem(9002, "Test Item 2", 2.99, 1, "Test2.png", 1000);
            items.Add(item);

            _XMLClient.IsTestObject = true;
            bool result = _XMLClient.UpdateFromXML(items);
            if (result == true)
                _logger.LogInformation("successfully ran UpdateFromXML with GetXMLItems");
            else
                _logger.LogWarning("failed to run UpdateFromXML with GetXMLItems");
            items = _XMLClient.GetXMLItems();
            _logger.LogInformation("item count is {count}", items.Count);

            // clean up test objects inserted
            result = _XMLClient.CleanUpTestItems();
            if (result == true)
                _logger.LogInformation("Successfully clean up test items");
            else
                _logger.LogWarning("Failed to clean up test items");
        }


        public void TestCreateMessageFromXML()
        {
            _logger.LogInformation("Testing CreateMessageFromXML");

            var dataFactory = new XMLDataFactory();
            XMLSupport.Models.Message message = dataFactory.MakeXMLMessage(2, "Testing", "Unit Test", "This is from Unit Testing");
            string time = DateTime.Now.ToString();
            string location = "TestMethod5";

            _XMLClient.IsTestObject = true;
            bool result = _XMLClient.CreateMessageFromXML(message, time, location);
            if (result == true)
                _logger.LogInformation("successfully ran CreateMessageFromXML");
            else
                _logger.LogWarning("failed to run CreateMessageFromXML");


            // clean up test objects inserted
            result = _XMLClient.CleanUpTestMessages();
            if (result == true)
                _logger.LogInformation("Successfully clean up test items");
            else
                _logger.LogWarning("Failed to clean up test items");
        }


        public void TestCreateOrderFromXML()
        {
            _logger.LogInformation("Testing CreateOrderFromXML");

            string fileName = "c:\\src\\data\\testorder.xml";
            var dataFactory = new XMLDataFactory();
            XMLSupport.Models.Order order = dataFactory.MakeXMLOrder(11001, 1);
            Customer customer = dataFactory.MakeXMLCustomer(1002, "John Doe", "1111 Parkway", "My City", "NC", "28111");
            order.CustomerObj = customer;

            List<XMLSupport.Models.Item> items = dataFactory.MakeXMLItemList();
            XMLSupport.Models.Item item = dataFactory.MakeXMLItem(9001, "Test Item 1", 2.99, 1, "Test1.png", 1000);
            items.Add(item);
            item = dataFactory.MakeXMLItem(9002, "Test Item 2", 2.99, 1, "Test2.png", 1000);
            items.Add(item);
            order.Items = items;

            order.BeforeTax = 5.98;
            order.Tax = 0.17;
            order.Shipping = 0.00;
            order.Tip = 2.00;
            order.Total = 8.15;

            string time = DateTime.Now.ToString();
            string location = "TestMethod7";

            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XMLSupport.Models.Order));
            bool result = serializer.SerializeToFile(xmlSerializer, order, fileName);
            if (result == true)
                _logger.LogInformation("successfully serialize to file");
            else
                _logger.LogWarning("failed to serialize to file");

            _XMLClient.IsTestObject = true;
            result = _XMLClient.CreateOrderFromXML(order, time, location);
            if (result == true)
                _logger.LogInformation("successfully ran CreateOrderFromXML");
            else
                _logger.LogWarning("failed to run CreateOrderFromXML");


            // clean up test objects inserted
            result = _XMLClient.CleanUpTestOrders();
            if (result == true)
                _logger.LogInformation("Successfully clean up test items");
            else
                _logger.LogWarning("Failed to clean up test items");
        }


        public void TestCreateOrderFromXML2()
        {
            _logger.LogInformation("Testing CreateOrderFromXML 2nd Version");

            string time = DateTime.Now.ToString();
            string location = "TestMethod9";

            string fileName = "c:\\src\\data\\testcontext2.xml";
            string fileOrder = "c:\\src\\data\\testorder2.xml";

            var dataFactory = new XMLDataFactory();
            Context context = dataFactory.MakeXMLContext();
            context.Location = location;
            context.Method = "UnitMethod9";
            context.Time = time;
            context.RequestType = 3;

            XMLSupport.Models.Order order = dataFactory.MakeXMLOrder(11001, 1);
            Customer customer = dataFactory.MakeXMLCustomer(1002, "John Doe", "1111 Parkway", "My City", "NC", "28111");
            order.CustomerObj = customer;

            List<XMLSupport.Models.Item> items = dataFactory.MakeXMLItemList();
            XMLSupport.Models.Item item = dataFactory.MakeXMLItem(9001, "Test Item 1", 2.99, 1, "Test1.png", 1000);
            items.Add(item);
            item = dataFactory.MakeXMLItem(9002, "Test Item 2", 2.99, 1, "Test2.png", 1000);
            items.Add(item);
            order.Items = items;
            context.InOrder = order;

            var serializer = new Serializer();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Context));
            bool result = serializer.SerializeToFile(xmlSerializer, context, fileName);
            if (result == true)
                _logger.LogInformation("successfully serialize context to file");
            else
                _logger.LogWarning("failed to serialize context to file");

            XmlSerializer xmlSerializer2 = new XmlSerializer(typeof(XMLSupport.Models.Order));
            result = serializer.SerializeToFile(xmlSerializer2, order, fileOrder);
            if (result == true)
                _logger.LogInformation("successfully serialize order to file");
            else
                _logger.LogWarning("failed to serialize order to file");

            _XMLClient.IsTestObject = true;
            result = _XMLClient.CreateOrderFromXML(context.InOrder, time, location);
            if (result == true)
                _logger.LogInformation("successfully ran CreateOrderFromXML");
            else
                _logger.LogWarning("failed Logto run CreateOrderFromXML");


            // clean up test objects inserted
            result = _XMLClient.CleanUpTestOrders();
            if (result == true)
                _logger.LogInformation("Successfully clean up test items");
            else
                _logger.LogWarning("Failed to clean up test items");
        }


        public void TestGetMessageXML()
        {
            _logger.LogInformation("Testing GetMessageXML");

            DateTime searchTime = DateTime.Now;
            string location = "TestMessage10";
            string newLocation = "TestMessage10New";
            string fileName = "c:\\src\\data\\getmessage.xml";
            string fileName2 = "c:\\src\\data\\getmessage2.xml";


            Thread.Sleep(3000);

            var dataFactory = new XMLDataFactory();
            XMLSupport.Models.Message message = dataFactory.MakeXMLMessage(2, "Testing", "Unit Test", "This is from unit testing");
            DateTime testTime = DateTime.Now;
            string time = testTime.ToString();
            message.Time = time;
            message.Location = newLocation;

            _XMLClient.IsTestObject = true;
            bool result = _XMLClient.CreateMessageFromXML(message, time, newLocation);
            if (result == true)
                _logger.LogInformation("successfully ran CreateMessageFromXML");
            else
                _logger.LogWarning("failed to run CreateMessageFromXML");

            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XMLSupport.Models.Message));

            XMLSupport.Models.Message messageOutput = _XMLClient.GetMessageXML(searchTime, location);
            if (messageOutput == null)
                _logger.LogWarning("Message return is null");
            else
            {
                _logger.LogInformation("MessageType is {messagetype} and expected {messagetype2}", messageOutput.MessageType, message.MessageType);
                _logger.LogInformation("ToName is {toname} and expected {toname2}", messageOutput.ToName, message.ToName);
                _logger.LogInformation("ToName is {fromname} and expected {fromname2}", messageOutput.FromName, message.FromName);
                _logger.LogInformation("Location is {location} and expected {location2}", messageOutput.Location, message.Location);

                result = serializer.SerializeToFile(xmlSerializer, messageOutput, fileName);
                if (result == true)
                    _logger.LogInformation("successfully serialize message output to file");
                else
                    _logger.LogWarning("failed to serialize message output to file");
            }

            // now search for same time as message receieve above
            searchTime = testTime;
            messageOutput = _XMLClient.GetMessageXML(searchTime, location);
            if (messageOutput == null)
                _logger.LogInformation("Message is null and success from GetMessageXML");
            else
            {
                _logger.LogWarning("MessageType is {messagetype} and got {messagetype2}", messageOutput.MessageType, message.MessageType);
                _logger.LogInformation("ToName is {toname} and got {toname2}", messageOutput.ToName, message.ToName);
                _logger.LogInformation("ToName is {fromname} and got {fromname2}", messageOutput.FromName, message.FromName);
                _logger.LogWarning("Location is {location} and got {location2}", messageOutput.Location, message.Location);

                result = serializer.SerializeToFile(xmlSerializer, messageOutput, fileName2);
                if (result == true)
                    _logger.LogInformation("successfully serialize message output to file");
                else
                    _logger.LogWarning("failed to serialize message output to file");

            }

            // clean up test objects inserted
            result = _XMLClient.CleanUpTestMessages();
            if (result == true)
                _logger.LogInformation("Successfully clean up test items");
            else
                _logger.LogWarning("Failed to clean up test items");
        }

        public void TestSeedItemsSimData()
        {
            string inputFile = @"C:\src\data\itemlist.xml";
            string outputFile = @"C:\src\data\itemsseed.txt";
            string line, str;

            _logger.LogInformation("Testing TestSeedItemsSimData");
            _logger.LogInformation("Loading Item Seed data from {itemlist}", inputFile);

            var serializer = new Serializer();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Item>));

            List<Item> items = (List<Item>)serializer.DeserializeFile(xmlSerializer, inputFile);

            List<string> lines = new List<string>();

            str = "\"";

            foreach (Item item in items)
            {
                line = "Item = new ItemModel();";
                lines.Add(line);
                line = "Item.Id = Id++;";
                lines.Add(line);
                line = "Item.ItemNum = " + item.ItemNum.ToString() + ";";
                lines.Add(line);
                line = @"Item.Description = " + str + item.Description.TrimEnd() + str + ";";
                lines.Add(line);
                line = "Item.Amount = " + item.Amount.ToString() + "M;";
                lines.Add(line);
                line = "Item.DefaultQuantity = " + item.Quantity.ToString() + ";";
                lines.Add(line);
                line = "Item.ImageFileName = " + str + item.ImageFileName + str + ";";
                lines.Add(line);
                line = "Item.CategoryNum = " + item.Category.ToString() + ";";
                lines.Add(line);
                line = "Items.Add(Item);";
                lines.Add(line);
                line = "";
                lines.Add(line);
            }


            _logger.LogInformation("Writing source lines to {output}", outputFile);
            System.IO.File.WriteAllLines(outputFile, lines);

            _logger.LogInformation("Finish Testing TestSeedItemsSimData");
        }

        public void TestUpdateDefaultItems()
        {
            _logger.LogInformation("Testing Update with Default Items");

            ServiceProvider serviceProvider = MyServiceFactory.GetServiceProvider();
            var itemDataService = serviceProvider.GetService<IItemDataService>();

            var itemList = DeliverySupport.Data.Sim.InitializeItems.CreateDefaultItems();

            Task.Run(async () =>
            {
                await itemDataService.CreateOrUpdateList(itemList);
            }).Wait();
        }
    }
}


using DeliverySupport.Data;
using DeliverySupport.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XMLSupport.Models;

namespace XMLSupport.Services
{
    public class XMLClient : IXMLClient
    {
        private IXMLDataFactory dataFactory = null;
        private IConfiguration _configuration;
        private ILogger<XMLClient> _logger = null;
        IItemDataService _itemDataService = null;
        IMessageDataService _messageDataService = null;
        IOrderDataService _orderDataService = null;

        public XMLClient(ILogger<XMLClient> logger)
        {
            _logger = logger;

            _logger.LogInformation("Started logging in XMLClient");

            dataFactory = new XMLDataFactory();

            // we will have to switch this if not SqlServer
            ServiceProvider serviceProvider = MyServiceFactory.GetServiceProvider();
            _itemDataService = serviceProvider.GetService<IItemDataService>();
            _messageDataService = serviceProvider.GetService<IMessageDataService>();
            _orderDataService = serviceProvider.GetService<IOrderDataService>();
            _configuration = serviceProvider.GetService<IConfiguration>();


        }

        public bool IsTestObject { get; set; }
        public bool UpdateFromXML(List<Item> XMLItems)
        {

            List<DeliverySupport.Models.IItemModel> items = new List<DeliverySupport.Models.IItemModel>();

            foreach (Item xmlItem in XMLItems)
            {
                DeliverySupport.Models.IItemModel item = dataFactory.MakeItemFromXMLItem(xmlItem);
                item.IsTestObject = IsTestObject;
                items.Add(item);
            }

            // wait for task - may need better method but not use often
            Task.Run(async () =>
           {
               await _itemDataService.CreateOrUpdateList(items);
           }).Wait();

            return true;
        }

        public List<Item> GetXMLItems()
        {

            List<Item> xmlItems = new List<Item>();
            List<DeliverySupport.Models.IItemModel> items = null;


            // need to wait for task - beed find better method
            Task.Run(async () =>
            {
                items = await _itemDataService.GetItemsAsync();
            }).Wait();


            foreach (DeliverySupport.Models.IItemModel item in items)
            {
                Item xmlItem = dataFactory.MakeXMLItem(item);
                xmlItems.Add(xmlItem);
            }

            return xmlItems;
        }

        public bool CreateMessageFromXML(Message XMLMessage, string Time, string Location)
        {
            DeliverySupport.Models.IMessageModel message = dataFactory.MakeMessageFromXMLMessage(XMLMessage);
            if (Time.Length > 0)
                message.TimeCreated = DateTime.Parse(Time);
            message.LocationCreated = Location;
            message.IsTestObject = IsTestObject;

            // need to find a better way of doing this
            Task.Run(async () =>
           {
               await _messageDataService.Create(message);
           }).Wait();


            return true;
        }

        public bool CreateOrderFromXML(Order XMLOrder, string Time, string Location)
        {
            int OrderNum = XMLOrder.OrderNum;

            // transfer order
            DeliverySupport.Models.IOrderModel order = dataFactory.MakeOrderFrmXMLOrder(XMLOrder);
            if (Time.Length > 0)
                order.TimeCreated = DateTime.Parse(Time);
            order.LocationCreated = Location;
            order.IsTestObject = IsTestObject;

            // transfer Customer

            List<DeliverySupport.Models.IOrderCustomerModel> customers = new List<DeliverySupport.Models.IOrderCustomerModel>();
            if (XMLOrder.CustomerObj != null)
            {
                DeliverySupport.Models.IOrderCustomerModel customer = dataFactory.MakeOrderCustomerFromXMLCustomer(XMLOrder.CustomerObj);
                customer.OrderNum = OrderNum;
                customers.Add(customer);
            }
            order.Customers = customers;

            // transfer item(s)
            List<DeliverySupport.Models.IOrderItemModel> items = new List<DeliverySupport.Models.IOrderItemModel>();
            foreach (Models.Item xmlItem in XMLOrder.Items)
            {
                DeliverySupport.Models.IOrderItemModel item = dataFactory.MakeOrderItemFromXMLItem(xmlItem);
                item.OrderNum = OrderNum;
                items.Add(item);
            }
            order.OrderItems = items;


            // need to find a better way of doing this
            Task.Run(async () =>
            {
                await _orderDataService.Create(order);
            }).Wait();

            return true;
        }


        public Models.Message GetMessageXML(DateTime Time, string Location)
        {
            Models.Message XMLMessage = null;

            DeliverySupport.Models.IMessageModel message = null;

            //_logger.LogInformation($"Checking for message with Time { Time}  and Location { Location}");


            // need to find a better way of doing this
            Task.Run(async () =>
            {
                message = await _messageDataService.GetByTimeLocation(Time, Location);
            }).Wait();


            if (message != null)
            {
                XMLMessage = dataFactory.MakeXMLMessage(message);
                //  _logger.LogInformation($"Receieve Msg with Name {XMLMessage.Name} and Time of {XMLMessage.Time}");
            }

            return XMLMessage;
        }

        public bool CleanUpTestItems()
        {
            bool result = false;

            Task.Run(async () =>
            {
                await _itemDataService.DeleteTestItems();
                result = true;
            }).Wait();

            return result;

        }

        public bool CleanUpTestOrders()
        {
            bool result = false;


            Task.Run(async () =>
            {
                await _orderDataService.DeleteTestOrders();
                result = true;
            }).Wait();

            return result;

        }

        public bool CleanUpTestMessages()
        {
            bool result = false;
            Task.Run(async () =>
            {
                await _messageDataService.DeleteTestMessages();
                result = true;
            }).Wait();

            return result;

        }





    }
}

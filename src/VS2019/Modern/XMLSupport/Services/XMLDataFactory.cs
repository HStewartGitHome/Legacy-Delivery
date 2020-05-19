using System.Collections.Generic;
using XMLSupport.Models;

namespace XMLSupport.Services
{
    public class XMLDataFactory : IXMLDataFactory
    {
        // to xml methods 
        public Message MakeXMLMessage(DeliverySupport.Models.IMessageModel message)
        {
            Message XMLMessage = new Message();
            XMLMessage.ToName = message.ToName;
            XMLMessage.FromName = message.FromName;
            XMLMessage.MessageText = message.MessageText;
            XMLMessage.Location = message.LocationCreated;
            XMLMessage.Time = message.TimeCreated.ToString();
            XMLMessage.MessageType = 0;

            return XMLMessage;
        }


        public Item MakeXMLItem(DeliverySupport.Models.IItemModel item)
        {
            Item xmlItem = new Item();
            xmlItem.ItemNum = item.ItemNum;
            xmlItem.Description = item.Description;
            xmlItem.Amount = (double)item.Amount;
            xmlItem.Quantity = (int)item.DefaultQuantity;
            xmlItem.ImageFileName = item.ImageFileName;
            xmlItem.Category = (int)item.CategoryNum;

            return xmlItem;
        }

        // from xml methods
        public DeliverySupport.Models.IOrderItemModel MakeOrderItemFromXMLItem(Item xmlItem)
        {
            DeliverySupport.Models.IOrderItemModel item = new DeliverySupport.Models.OrderItemModel();
            item.ItemNum = xmlItem.ItemNum;
            item.Quantity = xmlItem.Quantity;
            item.CategoryNum = xmlItem.Category;
            return item;
        }

        public DeliverySupport.Models.IItemModel MakeItemFromXMLItem(Item xmlItem)
        {
            DeliverySupport.Models.IItemModel item = new DeliverySupport.Models.ItemModel();
            item.ItemNum = xmlItem.ItemNum;
            item.Description = xmlItem.Description;
            item.Amount = (decimal)xmlItem.Amount;
            item.DefaultQuantity = xmlItem.Quantity;
            item.CategoryNum = xmlItem.Category;
            item.ImageFileName = xmlItem.ImageFileName;
            return item;
        }




        public DeliverySupport.Models.IOrderCustomerModel MakeOrderCustomerFromXMLCustomer(Customer xmlCustomer)
        {
            DeliverySupport.Models.IOrderCustomerModel customer = new DeliverySupport.Models.OrderCustomerModel();
            customer.CustomerNum = xmlCustomer.CustomerNum;
            customer.CustomerName = xmlCustomer.Name;
            customer.CustomerAddress = xmlCustomer.Address;
            customer.CustomerCity = xmlCustomer.City;
            customer.CustomerState = xmlCustomer.State;
            customer.CustomerZip = xmlCustomer.Zip;

            return customer;

        }


        public DeliverySupport.Models.IOrderModel MakeOrderFrmXMLOrder(Order xmlOrder)
        {
            DeliverySupport.Models.IOrderModel order = new DeliverySupport.Models.OrderModel();
            order.OrderNum = xmlOrder.OrderNum;
            order.BeforeTax = (decimal)xmlOrder.BeforeTax;
            order.Tax = (decimal)xmlOrder.Tax;
            order.Shipping = (decimal)xmlOrder.Shipping;
            order.Tip = (decimal)xmlOrder.Tip;
            order.Total = (decimal)xmlOrder.Total;

            return order;
        }

        public DeliverySupport.Models.IMessageModel MakeMessageFromXMLMessage(Message xmlMessage)
        {
            DeliverySupport.Models.IMessageModel message = new DeliverySupport.Models.MessageModel();

            message.ToName = xmlMessage.ToName;
            message.FromName = xmlMessage.FromName;
            message.FromName = "LEGACY";
            message.MessageText = xmlMessage.MessageText;

            return message;
        }

        public Item MakeXMLItem(int itemNum, string description, double amount, int quantity, string imageFileName, int category)
        {
            Item item = new Item();
            item.ItemNum = itemNum;
            item.Description = description;
            item.Amount = amount;
            item.Quantity = quantity;
            item.ImageFileName = imageFileName;
            item.Category = category;
            return item;
        }

        public Message MakeXMLMessage(int messageType, string toName, string fromName, string messageText)
        {
            Message message = new Message();
            message.MessageType = messageType;
            message.ToName = toName;
            message.FromName = fromName;
            message.MessageText = messageText;

            return message;

        }

        public Order MakeXMLOrder(int orderNum, int destination)
        {
            Order order = new Order();
            order.OrderNum = orderNum;
            order.Destination = destination;
            return order;
        }

        public Customer MakeXMLCustomer(int customerNum, string name, string address, string city, string state, string zip)
        {
            Customer customer = new Customer();
            customer.CustomerNum = customerNum;
            customer.Name = name;
            customer.Address = address;
            customer.City = city;
            customer.State = state;
            customer.Zip = zip;

            return customer;
        }

        public List<Item> MakeXMLItemList()
        {
            List<Item> list = new List<Item>();
            return list;
        }

        public Context MakeXMLContext()
        {
            Context context = new Context();
            return context;
        }


    }
}

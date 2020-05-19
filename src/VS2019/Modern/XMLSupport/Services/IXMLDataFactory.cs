using DeliverySupport.Models;
using System.Collections.Generic;
using XMLSupport.Models;

namespace XMLSupport.Services
{
    public interface IXMLDataFactory
    {
        DeliverySupport.Models.IItemModel MakeItemFromXMLItem(Models.Item xmlItem);
        DeliverySupport.Models.IMessageModel MakeMessageFromXMLMessage(Models.Message xmlMessage);
        IOrderCustomerModel MakeOrderCustomerFromXMLCustomer(Customer xmlCustomer);
        DeliverySupport.Models.IOrderModel MakeOrderFrmXMLOrder(Models.Order xmlOrder);
        IOrderItemModel MakeOrderItemFromXMLItem(Models.Item xmlItem);
        Context MakeXMLContext();
        Customer MakeXMLCustomer(int customerNum, string name, string address, string city, string state, string zip);
        Models.Item MakeXMLItem(int itemNum, string description, double amount, int quantity, string imageFileName, int category);
        Models.Item MakeXMLItem(DeliverySupport.Models.IItemModel item);
        List<Models.Item> MakeXMLItemList();
        Models.Message MakeXMLMessage(int messageType, string toName, string fromName, string messageText);
        Models.Message MakeXMLMessage(DeliverySupport.Models.IMessageModel message);
        Models.Order MakeXMLOrder(int orderNum, int destination);
    }
}
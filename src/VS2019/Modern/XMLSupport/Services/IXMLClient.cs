using System;
using System.Collections.Generic;
using XMLSupport.Models;

namespace XMLSupport.Services
{
    public interface IXMLClient
    {
        bool IsTestObject { get; set; }

        bool CleanUpTestItems();
        bool CleanUpTestMessages();
        bool CleanUpTestOrders();
        bool CreateMessageFromXML(Message XMLMessage, string Time, string Location);
        bool CreateOrderFromXML(Order XMLOrder, string Time, string Location);
        Message GetMessageXML(DateTime Time, string Location);
        List<Item> GetXMLItems();
        bool UpdateFromXML(List<Item> XMLItems);
    }
}
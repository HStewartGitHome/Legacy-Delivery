using DeliverySupport.Models;
using System;

namespace Delivery.Models
{
    public class DisplayMessageModel : IMessageModel
    {
        public int Id { get; set; }
        public string ToName { get; set; }
        public string FromName { get; set; }
        public string MessageText { get; set; }
        public DateTime TimeCreated { get; set; }
        public string LocationCreated { get; set; }
        public bool IsTestObject { get; set; }
    }
}

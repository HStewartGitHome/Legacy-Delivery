using System;

namespace DeliverySupport.Models
{
    public class MessageModel : IMessageModel
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

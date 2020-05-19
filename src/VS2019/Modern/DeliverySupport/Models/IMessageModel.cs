using System;

namespace DeliverySupport.Models
{
    public interface IMessageModel
    {
        int Id { get; set; }
        string ToName { get; set; }
        string FromName { get; set; }
        string MessageText { get; set; }
        string LocationCreated { get; set; }
        DateTime TimeCreated { get; set; }
        bool IsTestObject { get; set; }
    }
}
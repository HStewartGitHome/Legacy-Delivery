using System;
using System.Collections.Generic;

namespace DeliverySupport.Models
{
    public interface IOrderModel
    {
        decimal BeforeTax { get; set; }
        List<IOrderCustomerModel> Customers { get; set; }
        int Id { get; set; }
        bool IsTestObject { get; set; }
        string LocationCreated { get; set; }
        List<IOrderItemModel> OrderItems { get; set; }
        int OrderNum { get; set; }
        decimal Shipping { get; set; }
        decimal Tax { get; set; }
        DateTime TimeCreated { get; set; }
        decimal Tip { get; set; }
        decimal Total { get; set; }
    }
}
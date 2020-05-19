using System;
using System.Collections.Generic;

namespace DeliverySupport.Models
{
    public class OrderModel : IOrderModel
    {
        public int Id { get; set; }
        public int OrderNum { get; set; }
        public DateTime TimeCreated { get; set; }
        public string LocationCreated { get; set; }
        public decimal BeforeTax { get; set; }
        public decimal Tax { get; set; }
        public decimal Shipping { get; set; }
        public decimal Tip { get; set; }
        public decimal Total { get; set; }
        public List<IOrderItemModel> OrderItems { get; set; }
        public List<IOrderCustomerModel> Customers { get; set; }
        public bool IsTestObject { get; set; }
    }
}

using DeliverySupport.Data;
using DeliverySupport.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DeliverySupport.Services
{
    public class OrderCart : IOrderCart
    {
        public int OrderNum { get; set; }
        public decimal Total { get; set; }
        public decimal BeforeTax { get; set; }
        public decimal Tax { get; set; }
        public int TaxPercent { get; set; }
        public IOrderModel Order { get; set; }
        private ILogger<OrderCart> _logger = null;
        private IDataFactory Factory = new DataFactory();


        public OrderCart(ILogger<OrderCart> logger)
        {
            _logger = logger;
            Order = null;
            TaxPercent = 6;
            RefreshVariables();
        }

        public void RefreshVariables()
        {
            if (Order == null)
            {
                OrderNum = 0;
                BeforeTax = 0;
                Tax = 0;
                Total = 0;
            }
            else
            {
                OrderNum = Order.OrderNum;
                BeforeTax = 0;

                foreach (IOrderItemModel Item in Order.OrderItems)
                {
                    IExtendedOrderItemModel extItem = (IExtendedOrderItemModel)Item;
                    BeforeTax += extItem.Amount * Item.Quantity;
                }

                Tax = (BeforeTax * TaxPercent) / 100;
                Total = BeforeTax + Tax;

                Order.BeforeTax = BeforeTax;
                Order.Tax = Tax;
                Order.Total = Total;
            }
        }

        public void NewOrderCart(string LocationCreated, int MaxOrderNum)
        {
            OrderNum = MaxOrderNum;

            Order = Factory.MakeNewOrder(OrderNum, LocationCreated);
            RefreshVariables();
        }

        public void AddItemToCart(int ItemNum,
                                  decimal Amount,
                                  int Quantity,
                                  int CategoryNum,
                                  string Description,
                                  string CategoryDescription,
                                  string ImageFileName)
        {
            if (Order != null)
            {
                IOrderItemModel FoundItem = FindItem(ItemNum);
                if (FoundItem == null)
                {
                    IOrderItemModel item = Factory.MakeNewExtendedOrderItem(ItemNum, Amount, Quantity, Order.OrderNum, CategoryNum, Description, CategoryDescription, ImageFileName);
                    Order.OrderItems.Add(item);
                }
                else
                {
                    IOrderItemModel WorkItem = FoundItem;
                    Order.OrderItems.Remove(FoundItem);
                    WorkItem.Quantity += Quantity;
                    Order.OrderItems.Add(WorkItem);
                }
                RefreshVariables();
            }
        }

        public IOrderItemModel FindItem(int ItemNum)
        {
            IOrderItemModel FoundItem = null;

            foreach (IOrderItemModel item in Order.OrderItems)
            {
                if (item.ItemNum == ItemNum)
                    FoundItem = item;
            }

            return FoundItem;
        }

        public void AddCustomer(IOrderCustomerModel Customer)
        {
            if (Order != null)
            {
                Order.Customers = new List<IOrderCustomerModel>();
                Customer.CustomerNum = Order.OrderNum;
                Customer.OrderNum = Order.OrderNum;
                Order.Customers.Add(Customer);
            }
        }


    }
}

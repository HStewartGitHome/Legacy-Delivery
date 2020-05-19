using DeliverySupport.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySupport.Data.Sim
{
    public class OrderSimDataService : IOrderDataService
    {
        private readonly ILogger<IOrderDataService> _logger;
        private List<IOrderModel> _orders = new List<IOrderModel>();
        private List<IItemModel> _items = new List<IItemModel>();
        private List<IItemCategoryModel> _itemCategories = new List<IItemCategoryModel>();
        private int _nextId = 1;
        private bool _isDataInitialized { get; set; }
        public object Factory { get; private set; }

        public OrderSimDataService(ILogger<IOrderDataService> logger)
        {
            _logger = logger;
            _isDataInitialized = false;
        }

        // this was original method - but may never be used
        public async Task CreateHeaderOnly(IOrderModel order)
        {
            await Create(order);

        }

        public async Task Create(IOrderModel order)
        {
            await Task.Run(() =>
            {
                order.Id = _nextId++;
                _orders.Add(order);
            });

        }

        public async Task<List<IOrderModel>> GetOrdersAsync()
        {
            return await Task.FromResult(_orders);

        }

        public async Task<IOrderModel> GetOrderAsync(int id)
        {
            return await Task.FromResult(_orders.Where(x => x.Id == id).FirstOrDefault());

        }

        public async Task DeleteTestOrders()
        {
            await Task.Run(() => { _orders.Remove(_orders.Where(x => x.IsTestObject == true).FirstOrDefault()); });

        }

        public async Task DeleteOrder(int id)
        {
            await Task.Run(() => { _orders.Remove(_orders.Where(x => x.Id == id).FirstOrDefault()); });

        }

        public async Task<int> GetMaxOrderNum()
        {
            int Value = 0;

            foreach (IOrderModel WorkOrder in _orders)
            {
                if (WorkOrder.OrderNum > Value)
                    Value = WorkOrder.OrderNum;
            }
            await Task.Delay(0);

            return Value;
        }

        public async Task<List<IOrderItemViewModel>> GetOrderItemViewForOrderNum(int OrderNum)
        {
            List<IOrderItemViewModel> items = new List<IOrderItemViewModel>();

            if (_isDataInitialized == false)
                InitializeData();

            foreach (IOrderModel Order in _orders)
            {
                if (Order.OrderNum == OrderNum)
                {
                    foreach (IOrderItemModel item in Order.OrderItems)
                    {
                        IOrderItemViewModel itemView = new OrderItemViewModel();
                        itemView.OrderNum = OrderNum;
                        itemView.ItemNum = item.ItemNum;
                        itemView.Quantity = item.Quantity;
                        itemView.CategoryNum = item.CategoryNum;

                        foreach (IItemCategoryModel catModel in _itemCategories)
                        {
                            if (item.CategoryNum == catModel.CategoryNum)
                                itemView.CategoryDescription = catModel.CategoryDescription;
                        }
                        items.Add(itemView);

                    }
                }
            }

            await Task.Delay(0);
            return items;


        }

        public async Task DeleteOrdersBeforeTime(DateTime time)
        {
            await Task.Run(() => { _orders.Remove(_orders.Where(x => x.TimeCreated < time).FirstOrDefault()); });

        }

        internal void InitializeData()
        {
            _items = InitializeItems.CreateDefaultItems();

            _nextId = InitializeItems.NextID;

            DataFactory Factory = new DataFactory();
            _itemCategories = Factory.CreateDefaultItemCategoryList();
            _isDataInitialized = true;

        }

    }
}

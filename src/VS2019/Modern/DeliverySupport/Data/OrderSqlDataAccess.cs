
using Dapper;
using DeliverySupport.DataAccess;
using DeliverySupport.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverySupport.Data
{
    public class OrderSqlDataService : IOrderDataService
    {
        private readonly ISqlDataAccess _dataAccess;
        private readonly ILogger<IOrderDataService> _logger;

        public OrderSqlDataService(ISqlDataAccess dataAccess,
                                  ILogger<IOrderDataService> logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }

        // this was original method - but may never be used
        public async Task CreateHeaderOnly(IOrderModel order)
        {
            var p = new
            {
                order.OrderNum,
                order.TimeCreated,
                order.LocationCreated,
                order.BeforeTax,
                order.Tax,
                order.Shipping,
                order.Tip,
                order.Total,
                order.IsTestObject
            };

            await _dataAccess.SaveData("dbo.spOrder_Create", p, "SQLDB");

        }

        public async Task Create(IOrderModel order)
        {

            int CustomerNum = 0;
            string CustomerName = "";
            string CustomerAddress = "";
            string CustomerCity = "";
            string CustomerState = "";
            string CustomerZip = "";

            if (order.Customers.Count > 0)
            {
                IOrderCustomerModel customer = order.Customers[0];
                CustomerNum = customer.CustomerNum;
                CustomerName = customer.CustomerName;
                CustomerAddress = customer.CustomerAddress;
                CustomerCity = customer.CustomerCity;
                CustomerState = customer.CustomerState;
                CustomerZip = customer.CustomerZip;
            }


            var p = new DynamicParameters();
            p.Add("OrderNum", order.OrderNum);
            p.Add("TimeCreated", order.TimeCreated);
            p.Add("LocationCreated", order.LocationCreated);
            p.Add("BeforeTax", order.BeforeTax);
            p.Add("Tax", order.Tax);
            p.Add("Shipping", order.Shipping);
            p.Add("Tip", order.Tip);
            p.Add("Total", order.Total);
            p.Add("IsTestObject", order.IsTestObject);
            p.Add("CustomerNum", CustomerNum);
            p.Add("CustomerName", CustomerName);
            p.Add("CustomerAddress", CustomerAddress);
            p.Add("CustomerCity", CustomerCity);
            p.Add("CustomerState", CustomerState);
            p.Add("CustomerZip", CustomerZip);
            p.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _dataAccess.SaveData("dbo.spOrder_CreateCust", p, "SQLDB");

            var OrderId = p.Get<int>("Id");
            //_logger.LogInformation("Save Order Return ID of {OrderId} for OrderNum of {OrderNum}", OrderId, order.OrderNum);

            // loop though items 
            foreach (IOrderItemModel item in order.OrderItems)
            {
                var pi = new DynamicParameters();
                pi.Add("ItemNum", item.ItemNum);
                pi.Add("Quantity", item.Quantity);
                pi.Add("CategoryNum", item.CategoryNum);
                pi.Add("OrderNum", item.OrderNum);
                pi.Add("OrderId", OrderId);
                pi.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                ;
                await _dataAccess.SaveData("dbo.spOrderItem_Create", pi, "SQLDB");

                var Id = pi.Get<int>("Id");
                //_logger.LogInformation("Save Item Return ID of {Id} for ItemNum of {ItemNum}", Id, item.ItemNum);
            }
        }

        public async Task<List<IOrderModel>> GetOrdersAsync()
        {
            var orders = await _dataAccess.LoadData<OrderModel, dynamic>("dbo.spOrders_Get",
                                                                          new { },
                                                                        "SQLDB");
            return orders.ToList<IOrderModel>();
        }

        public async Task<IOrderModel> GetOrderAsync(int id)
        {
            var orders = await _dataAccess.LoadData<OrderModel, dynamic>("dbo.spOrders_GetOne",
                                                                         new { Id = id },
                                                                         "SQLDB");
            return orders.FirstOrDefault();
        }

        public async Task DeleteTestOrders()
        {
            await _dataAccess.SaveData("dbo.spOrders_DeleteTest", new { }, "SQLDB");
        }

        public async Task DeleteOrder(int id)
        {
            await _dataAccess.SaveData("dbo.spOrders_Delete", new { Id = id }, "SQLDB");
        }

        public async Task<int> GetMaxOrderNum()
        {
            int Value = 0;
            int Count = 0;

            try
            {
                var pi = new DynamicParameters();
                pi.Add("MaxOrderNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
                pi.Add("Count", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _dataAccess.LoadData<int, dynamic>("dbo.spOrders_MaxOrderNum",
                                                             pi,
                                                         "SQLDB");

                Count = pi.Get<int>("Count");
                if (Count > 0)
                    Value = pi.Get<int>("MaxOrderNum");
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception getting MaxOrderNum", ex);
            }

            return Value;
        }

        public async Task<List<IOrderItemViewModel>> GetOrderItemViewForOrderNum(int OrderNum)
        {
            List<IOrderItemViewModel> items = new List<IOrderItemViewModel>();

            try
            {
                var p = new
                {
                    OrderNum
                };
                items = await _dataAccess.LoadData<IOrderItemViewModel, dynamic>("spOrderItem_GetForOrderNum",
                                                                              p,
                                                                            "SQLDB");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception getting orderitem view for OrderNum = {ordernum}, OrderNum");
            }
            return items.ToList<IOrderItemViewModel>();

        }

        public async Task DeleteOrdersBeforeTime(DateTime time)
        {
            await _dataAccess.SaveData("dbo.spOrders_DeleteBeforeDate", new { Time = time }, "SQLDB");
        }

    }
}

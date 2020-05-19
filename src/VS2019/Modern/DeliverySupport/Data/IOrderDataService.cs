using DeliverySupport.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliverySupport.Data
{
    public interface IOrderDataService
    {
        Task Create(IOrderModel order);
        Task<IOrderModel> GetOrderAsync(int id);
        Task<List<IOrderModel>> GetOrdersAsync();
        Task DeleteTestOrders();
        Task<int> GetMaxOrderNum();
        Task<List<IOrderItemViewModel>> GetOrderItemViewForOrderNum(int OrderNum);
        Task DeleteOrdersBeforeTime(DateTime time);

        // old method
        Task CreateHeaderOnly(IOrderModel order);

    }
}
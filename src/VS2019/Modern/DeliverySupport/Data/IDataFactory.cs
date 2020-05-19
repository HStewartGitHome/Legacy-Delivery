using DeliverySupport.Models;
using System.Collections.Generic;

namespace DeliverySupport.Data
{
    public interface IDataFactory
    {
        List<IItemCategoryModel> CreateDefaultItemCategoryList();
        IItemCategoryModel MakeItemCategory(int CategoryNum, string Description);
        List<IItemCategoryModel> NewItemCategoryList();
        IOrderModel MakeNewOrder(int OrderNum,
                                 string LocationCreated);
        IOrderItemModel MakeNewOrderItem(int ItemNum, decimal Amount, int Quantity, int OrderNum);
        IExtendedOrderItemModel MakeNewExtendedOrderItem(int ItemNum,
                                                         decimal Amount,
                                                         int Quantity,
                                                         int OrderNum,
                                                         int CategoryNum,
                                                         string Description,
                                                         string CategoryDescription,
                                                         string ImageFileName);
        List<IItemModel> MakeItemList();
    }
}
using DeliverySupport.Models;
using System;
using System.Collections.Generic;

namespace DeliverySupport.Data
{
    public class DataFactory : IDataFactory
    {
        public IItemCategoryModel MakeItemCategory(int CategoryNum, string Description)
        {
            IItemCategoryModel itemCategory = new ItemCategoryModel();
            itemCategory.CategoryNum = CategoryNum;
            itemCategory.CategoryDescription = Description;

            return itemCategory;
        }

        public List<IItemCategoryModel> NewItemCategoryList()
        {
            List<IItemCategoryModel> itemCategoryList = new List<IItemCategoryModel>();
            return itemCategoryList;

        }

        public List<IItemCategoryModel> CreateDefaultItemCategoryList()
        {
            List<IItemCategoryModel> itemCategoryList = NewItemCategoryList();
            IItemCategoryModel cat = MakeItemCategory((int)CategoryType.Meat, "Meat");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Fruit, "Fruit");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Vegatable, "Vegatable");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.FrozenMeat, "Frozen Meat");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.FrozenFruit, "Frozen Fruit");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.FrozenVegatable, "Frozen Vegatable");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Rice, "Rice");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Bread, "Bread");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Nuts, "Nuts");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Chocolate, "Chocolate");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Spreads, "Spreads");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Breakfast, "Breakfast");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Snack, "Snack");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Drink, "Drink");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Health, "Health");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.Misc, "Misc");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.ISAGenix, "ISAGenix");
            itemCategoryList.Add(cat);
            cat = MakeItemCategory((int)CategoryType.TestCategory, "Test Category");
            itemCategoryList.Add(cat);

            return itemCategoryList;

        }

        public IOrderModel MakeNewOrder(int OrderNum,
                                         string LocationCreated)
        {
            IOrderModel orderModel = new OrderModel();
            orderModel.OrderNum = OrderNum;
            orderModel.LocationCreated = LocationCreated;
            orderModel.Shipping = 0;
            orderModel.Tax = 0;
            orderModel.Tip = 0;
            orderModel.Total = 0;
            orderModel.TimeCreated = DateTime.Now;
            orderModel.BeforeTax = 0;
            orderModel.IsTestObject = false;
            orderModel.OrderItems = new List<IOrderItemModel>();
            orderModel.Customers = new List<IOrderCustomerModel>();
            IOrderCustomerModel customer = new OrderCustomerModel();
            orderModel.Customers.Add(customer);


            return orderModel;
        }

        public IOrderItemModel MakeNewOrderItem(int ItemNum, decimal Amount, int Quantity, int OrderNum)
        {
            IOrderItemModel Item = new OrderItemModel();
            Item.ItemNum = ItemNum;
            Item.Quantity = Quantity;
            Item.OrderNum = OrderNum;

            return Item;
        }

        public IExtendedOrderItemModel MakeNewExtendedOrderItem(int ItemNum,
                                                                decimal Amount,
                                                                int Quantity,
                                                                int OrderNum,
                                                                int CategoryNum,
                                                                string Description,
                                                                string CategoryDescription,
                                                                string ImageFileName)
        {
            IExtendedOrderItemModel Item = new ExtendedOrderItemModel();
            Item.ItemNum = ItemNum;
            Item.Quantity = Quantity;
            Item.OrderNum = OrderNum;
            Item.Amount = Amount;
            Item.Description = Description;
            Item.CategoryDescription = CategoryDescription;
            Item.ImageFileName = ImageFileName;

            return Item;
        }

        public List<IItemModel> MakeItemList()
        {
            List<IItemModel> itemList = new List<IItemModel>();
            return itemList;
        }

    }
}

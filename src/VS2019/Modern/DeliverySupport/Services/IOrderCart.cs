using DeliverySupport.Models;

namespace DeliverySupport.Services
{
    public interface IOrderCart
    {
        IOrderModel Order { get; set; }
        int OrderNum { get; set; }
        decimal Total { get; set; }

        void AddCustomer(IOrderCustomerModel Customer);
        void AddItemToCart(int ItemNum, decimal Amount, int Quantity, int CategoryNum, string Description, string CategoryDescription, string ImageFileName);
        void NewOrderCart(string LocationCreated, int MaxOrderNum);
        void RefreshVariables();
    }
}
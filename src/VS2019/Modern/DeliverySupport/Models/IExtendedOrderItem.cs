namespace DeliverySupport.Models
{
    public interface IExtendedOrderItemModel : IOrderItemModel
    {
        decimal Amount { get; set; }
        string CategoryDescription { get; set; }
        string Description { get; set; }
        string ImageFileName { get; set; }
    }
}
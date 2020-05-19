namespace DeliverySupport.Models
{
    public interface IOrderItemViewModel
    {
        decimal Amount { get; set; }
        string CategoryDescription { get; set; }
        int CategoryNum { get; set; }
        string Description { get; set; }
        string ImageFileName { get; set; }
        int ItemNum { get; set; }
        int OrderNum { get; set; }
        int Quantity { get; set; }
    }
}
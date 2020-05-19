namespace DeliverySupport.Models
{
    public interface IOrderItemModel
    {
        int Id { get; set; }
        int ItemNum { get; set; }
        int OrderNum { get; set; }
        int Quantity { get; set; }
        int CategoryNum { get; set; }
    }
}
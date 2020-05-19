namespace DeliverySupport.Models
{
    public class OrderItemModel : IOrderItemModel
    {
        public int Id { get; set; }
        public int ItemNum { get; set; }
        public int Quantity { get; set; }
        public int OrderNum { get; set; }
        public int CategoryNum { get; set; }
    }
}

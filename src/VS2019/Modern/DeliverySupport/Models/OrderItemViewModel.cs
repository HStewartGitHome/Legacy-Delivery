namespace DeliverySupport.Models
{
    public class OrderItemViewModel : IOrderItemViewModel
    {
        public int ItemNum { get; set; }
        public int Quantity { get; set; }
        public int OrderNum { get; set; }
        public int CategoryNum { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string CategoryDescription { get; set; }
        public string ImageFileName { get; set; }
    }
}

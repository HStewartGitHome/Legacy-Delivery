namespace DeliverySupport.Models
{
    public class ExtendedOrderItemModel : IExtendedOrderItemModel
    {
        public int Id { get; set; }
        public int ItemNum { get; set; }
        public int OrderNum { get; set; }
        public int Quantity { get; set; }
        public int CategoryNum { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string CategoryDescription { get; set; }
        public string ImageFileName { get; set; }
        public decimal MyAmount { get; set; }
    }
}

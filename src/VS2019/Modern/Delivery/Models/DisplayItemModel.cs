using DeliverySupport.Models;

namespace Delivery.Models
{
    public class DisplayItemModel : IItemModel
    {
        public int Id { get; set; }
        public int ItemNum { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int DefaultQuantity { get; set; }
        public string ImageFileName { get; set; }
        public bool IsTestObject { get; set; }
        public int CategoryNum { get; set; }
        public string CategoryDescription { get; set; }
    }
}

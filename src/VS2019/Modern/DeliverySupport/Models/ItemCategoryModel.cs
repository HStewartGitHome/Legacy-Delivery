namespace DeliverySupport.Models
{

    public class ItemCategoryModel : IItemCategoryModel
    {
        public int Id { get; set; }
        public int CategoryNum { get; set; }
        public string CategoryDescription { get; set; }
    }
}

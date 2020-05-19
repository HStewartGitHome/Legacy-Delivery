namespace DeliverySupport.Models
{
    public interface IItemCategoryModel
    {
        int CategoryNum { get; set; }
        string CategoryDescription { get; set; }
        int Id { get; set; }
    }
}
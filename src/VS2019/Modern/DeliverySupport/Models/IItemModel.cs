namespace DeliverySupport.Models
{
    public interface IItemModel
    {
        decimal Amount { get; set; }
        string Description { get; set; }
        int Id { get; set; }
        string ImageFileName { get; set; }
        bool IsTestObject { get; set; }
        int ItemNum { get; set; }
        int DefaultQuantity { get; set; }
        string CategoryDescription { get; set; }
        int CategoryNum { get; set; }
    }
}
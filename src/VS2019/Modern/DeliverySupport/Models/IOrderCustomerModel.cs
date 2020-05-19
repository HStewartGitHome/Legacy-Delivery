namespace DeliverySupport.Models
{
    public interface IOrderCustomerModel
    {
        string CustomerAddress { get; set; }
        string CustomerCity { get; set; }
        int CustomerNum { get; set; }
        int Id { get; set; }
        string CustomerName { get; set; }
        int OrderNum { get; set; }
        string CustomerState { get; set; }
        string CustomerZip { get; set; }
    }
}
namespace DeliverySupport.Models
{
    public class OrderCustomerModel : IOrderCustomerModel
    {
        public int Id { get; set; }
        public int CustomerNum { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZip { get; set; }
        public int OrderNum { get; set; }
    }
}

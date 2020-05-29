using System.Collections.Generic;

namespace LegacyWinForm.Models
{
    public class Order
    {
        public int OrderNum { get; set; }
        public int Destination { get; set; }
        public double BeforeTax { get; set; }
        public double Tax { get; set; }
        public double Shipping { get; set; }
        public double Tip { get; set; }
        public double Total { get; set; }
        public List<Item> Items { get; set; }
        public Customer CustomerObj { get; set; }
    }
}

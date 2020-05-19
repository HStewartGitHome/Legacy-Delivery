using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge.DataModel
{
    public class Item 
    {
        public int ItemNum { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public string ImageFileName { get; set; }
        public int Category { get; set; }
    }
}

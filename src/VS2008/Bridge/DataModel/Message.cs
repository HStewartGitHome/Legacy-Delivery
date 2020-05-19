using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bridge.DataModel
{
    public class Message
    {
        public string ToName { get; set; }
        public string FromName { get; set; }
        public string MessageText { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
    }
}

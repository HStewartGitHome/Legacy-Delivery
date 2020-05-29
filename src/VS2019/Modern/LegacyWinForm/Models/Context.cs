namespace LegacyWinForm.Models
{
    public class Context
    {

        public string Method { get; set; }
        public int RequestType { get; set; }
        public string ID { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        public Order InOrder { get; set; }
        public Message MessageObj { get; set; }
    }
}

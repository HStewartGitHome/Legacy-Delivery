namespace LegacyWinForm.Models
{
    public class Message
    {
        public int MessageType { get; set; }
        public string ToName { get; set; }
        public string FromName { get; set; }
        public string MessageText { get; set; }
        public string Time { get; set; }
        public string Location { get; set; }
    }
}

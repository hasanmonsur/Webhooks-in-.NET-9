namespace WebhookServer.Models
{
    public class OrderEvent
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}

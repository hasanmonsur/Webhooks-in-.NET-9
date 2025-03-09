using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebhookClient.Models
{
    public class OrderEvent
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}

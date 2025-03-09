using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebhookServer.Hubs;
using WebhookServer.Models;

namespace WebhookServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {

        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<WebhookController> _logger;

        public WebhookController(IHubContext<NotificationHub> hubContext, ILogger<WebhookController> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        [HttpPost("order")]
        public async Task<IActionResult> ReceiveOrderEvent([FromBody] OrderEvent orderEvent)
        {
            if (orderEvent == null || orderEvent.OrderId <= 0 || string.IsNullOrEmpty(orderEvent.Status))
            {
                return BadRequest("Invalid order event data.");
            }

            orderEvent.Timestamp = DateTime.UtcNow;
            _logger.LogInformation("Received webhook event: OrderId={OrderId}, Status={Status}",
                orderEvent.OrderId, orderEvent.Status);

            // Broadcast to all subscribed clients
            await _hubContext.Clients.Group("OrderUpdates")
                .SendAsync("ReceiveOrderUpdate", orderEvent);

            return Ok(new { Message = "Webhook event processed successfully." });
        }

    }
}

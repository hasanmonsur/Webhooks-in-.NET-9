using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace WebhookServer.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SubscribeToOrderUpdates()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "OrderUpdates");
        }

        public async Task UnsubscribeFromOrderUpdates()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "OrderUpdates");
        }
    }
}

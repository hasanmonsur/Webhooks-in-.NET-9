// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using WebhookClient.Models;

Console.WriteLine("Hello, World!");
var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:8800/notificationHub")
            .WithAutomaticReconnect()
            .Build();

// Handle incoming updates
connection.On<OrderEvent>("ReceiveOrderUpdate", (orderEvent) =>
{
    Console.WriteLine($"Order Update: OrderId={orderEvent.OrderId}, Status={orderEvent.Status}, Timestamp={orderEvent.Timestamp}");
});

try
{
    await connection.StartAsync();
    Console.WriteLine("Connected to the server.");

    // Subscribe to updates
    await connection.InvokeAsync("SubscribeToOrderUpdates");
    Console.WriteLine("Subscribed to order updates.");

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();

    // Cleanup
    await connection.InvokeAsync("UnsubscribeFromOrderUpdates");
    await connection.StopAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
using MassTransit;
using Shared.Contracts.Events;

namespace Consumer.Worker.Consumers;

public class NotificationRequestedConsumer : IConsumer<NotificationRequested>
{
    public Task Consume(ConsumeContext<NotificationRequested> context)
    {
        var msg = context.Message;

        Console.WriteLine("✅ Consumed NotificationRequested:");
        Console.WriteLine($"Id: {msg.NotificationId}");
        Console.WriteLine($"Channel: {msg.Channel}");
        Console.WriteLine($"To: {msg.To}");
        Console.WriteLine($"Message: {msg.Message}");
        Console.WriteLine($"At: {msg.CreatedAtUtc:O}");

        // Here you’d call provider (SMS/Email/Push) in real system
        return Task.CompletedTask;
    }
}
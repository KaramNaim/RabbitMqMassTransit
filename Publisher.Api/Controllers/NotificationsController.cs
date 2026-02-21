using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Events;

namespace Publisher.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public NotificationsController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestNotification([FromBody] RequestNotificationDto dto)
    {
        var evt = new NotificationRequested(
            NotificationId: Guid.NewGuid(),
            Channel: dto.Channel,
            To: dto.To,
            Message: dto.Message,
            CreatedAtUtc: DateTime.UtcNow
        );

        Console.WriteLine($"event: {evt}");

        await _publishEndpoint.Publish(evt);

        return Ok(new { evt.NotificationId, Status = "Published" });
    }

    [HttpPost("stress")]
    public async Task<IActionResult> StressTest([FromQuery] int count = 10000)
    {
        var tasks = new List<Task>();

        for (int i = 0; i < count; i++)
        {
            var evt = new NotificationRequested(
                Guid.NewGuid(),
                "sms",
                "+96171867409",
                $"Stress message {i}",
                DateTime.UtcNow
            );

            tasks.Add(_publishEndpoint.Publish(evt));
        }

        await Task.WhenAll(tasks);

        return Ok($"Published {count} messages");
    }
}

public record RequestNotificationDto(string Channel, string To, string Message);
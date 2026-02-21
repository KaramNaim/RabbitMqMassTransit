namespace Shared.Contracts.Events;

public record NotificationRequested(
    Guid NotificationId,
    string Channel,   // sms/email/push...
    string To,
    string Message,
    DateTime CreatedAtUtc
);

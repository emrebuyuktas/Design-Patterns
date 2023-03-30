using MediatR;
using ObserverPattern.Events;

namespace ObserverPattern.EventHandlers;

public class WriteConsoleUserEventHandler: INotificationHandler<UserCreatedEvent>
{
    private readonly ILogger<WriteConsoleUserEventHandler> _logger;

    public WriteConsoleUserEventHandler(ILogger<WriteConsoleUserEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"User {notification.User.UserName} created");
        return Task.CompletedTask;
    }
}
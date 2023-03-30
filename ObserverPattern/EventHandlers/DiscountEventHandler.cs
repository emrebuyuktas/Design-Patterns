using MediatR;
using ObserverPattern.Events;
using ObserverPattern.Models;

namespace ObserverPattern.EventHandlers;

public class DiscountEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly AppIdenitytDbContext _context;
    private readonly ILogger<DiscountEventHandler> _logger;
    
    public DiscountEventHandler(AppIdenitytDbContext context, ILogger<DiscountEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _context.Discounts.AddAsync(new Models.Discount { Rate = 10, UserId = notification.User.Id });
        await _context.SaveChangesAsync();
        _logger.LogInformation("Discount created");
    }
}
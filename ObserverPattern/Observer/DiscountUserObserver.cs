using ObserverPattern.Models;

namespace ObserverPattern.Observer;

public class DiscountUserObserver : IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public DiscountUserObserver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void UserCreate(AppUser user)
    {
        var logger = _serviceProvider.GetService<ILogger<DiscountUserObserver>>();
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppIdenitytDbContext>();
        context.Discounts.Add(new Discount
        {
            Rate = 10,
            UserId = user.Id
        });
        context.SaveChanges();
        logger.LogInformation("Discount created");
    }
}
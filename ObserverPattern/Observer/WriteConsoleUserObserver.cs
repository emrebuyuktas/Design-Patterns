using ObserverPattern.Models;

namespace ObserverPattern.Observer;

public class WriteConsoleUserObserver : IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public WriteConsoleUserObserver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void UserCreate(AppUser user)
    {
        var logger = _serviceProvider.GetService<ILogger<WriteConsoleUserObserver>>();
        
        logger.LogInformation($"User {user.UserName} created");
    }
}
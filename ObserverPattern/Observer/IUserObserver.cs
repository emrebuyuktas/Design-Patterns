using ObserverPattern.Models;

namespace ObserverPattern.Observer;

public interface IUserObserver
{
    void UserCreate(AppUser user);
}
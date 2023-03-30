using ObserverPattern.Models;

namespace ObserverPattern.Observer;

public class UserObserverSubject
{
    private readonly List<IUserObserver> _observers;

    public UserObserverSubject()
    {
        _observers = new List<IUserObserver>();
    }
    public void RegisterObserver(IUserObserver userObserver)
    {
        _observers.Add(userObserver);
    }

    public void RemoveObserver(IUserObserver userObserver)
    {
        _observers.Remove(userObserver);
    }

    public void NotifyObservers(AppUser appUser)
    {
        _observers.ForEach(x =>
        {
            x.UserCreate(appUser);
        });
    }
}
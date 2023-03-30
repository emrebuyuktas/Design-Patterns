using MediatR;
using ObserverPattern.Models;

namespace ObserverPattern.Events;

public class UserCreatedEvent : INotification
{
    public AppUser User { get; set; }  
}
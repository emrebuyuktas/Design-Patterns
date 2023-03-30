using System.Net;
using System.Net.Mail;
using MediatR;
using ObserverPattern.Events;

namespace ObserverPattern.EventHandlers;

public class EmailEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly ILogger<EmailEventHandler> _logger;

    public EmailEventHandler(ILogger<EmailEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var mailMessage = new MailMessage();

        var smptClient = new SmtpClient("srvm11.trwww.com");

        mailMessage.From = new MailAddress("deneme@kariyersistem.com");

        mailMessage.To.Add(new MailAddress(notification.User.Email));

        mailMessage.Subject = "Sitemize hoş geldiniz.";

        mailMessage.Body = "<p>Sitemizin genel kuralları : bıdı bıdı....</p>";

        mailMessage.IsBodyHtml = true;
        smptClient.Port = 587;
        smptClient.Credentials = new NetworkCredential("deneme@kariyersistem.com", "Password12*");

        smptClient.Send(mailMessage);
        _logger.LogInformation($"Email was send to user :{notification.User.UserName}");

        return Task.CompletedTask;
    }
}
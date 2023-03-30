using System.Net;
using System.Net.Mail;
using ObserverPattern.Models;

namespace ObserverPattern.Observer;

public class EmailUserObserver :IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public EmailUserObserver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void UserCreate(AppUser user)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<EmailUserObserver>>();

        var mailMessage = new MailMessage();

        var smptClient = new SmtpClient("ssl://smtp.gmail.com");

        mailMessage.From = new MailAddress("a");

        mailMessage.To.Add(new MailAddress(user.Email));

        mailMessage.Subject = "Welcome.";

        mailMessage.Body = "<p>Privacy Policy</p>";

        mailMessage.IsBodyHtml = true;
        smptClient.Port = 465;
        smptClient.Credentials = new NetworkCredential("a", @"a");

        smptClient.Send(mailMessage);
        logger.LogInformation($"Email was send to user :{user.UserName}");
    }
}
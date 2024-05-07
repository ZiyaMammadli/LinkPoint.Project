using LinkPoint.Business.Services.Interfaces;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace LinkPoint.Business.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }
    public void SendEmail(string To, string Subject, string HtmlBody)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailSettings:From").Value));
        email.To.Add(MailboxAddress.Parse(To));
        email.Subject = Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = HtmlBody };

        using var smtp = new SmtpClient();
        smtp.Connect(_config.GetSection("EmailSettings:Host").Value, 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.GetSection("EmailSettings:From").Value, _config.GetSection("EmailSettings:Password").Value);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}

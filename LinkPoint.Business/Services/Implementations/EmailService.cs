using Azure.Core;
using LinkPoint.Business.Services.Interfaces;
using System.Net.Mail;

namespace LinkPoint.Business.Services.Implementations;

public class EmailService : IEmailService
{
    public void SendEmail(string To, string Subject, string HtmlBody)
    {
    }
}

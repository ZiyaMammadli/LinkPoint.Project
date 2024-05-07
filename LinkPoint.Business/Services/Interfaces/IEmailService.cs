namespace LinkPoint.Business.Services.Interfaces;

public interface IEmailService
{
    public void SendEmail(string To,string Subject,string HtmlBody);
}

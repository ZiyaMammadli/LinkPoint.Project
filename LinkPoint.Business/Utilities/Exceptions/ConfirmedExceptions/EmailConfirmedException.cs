namespace LinkPoint.Business.Utilities.Exceptions.ConfirmedExceptions;

public class EmailConfirmedException:Exception
{
    public int StatusCode { get; set; }
    public EmailConfirmedException() { }
    public EmailConfirmedException(string message):base(message) { }
    public EmailConfirmedException(int statusCode,string message):base(message)
    {

        StatusCode = statusCode;

    }
}

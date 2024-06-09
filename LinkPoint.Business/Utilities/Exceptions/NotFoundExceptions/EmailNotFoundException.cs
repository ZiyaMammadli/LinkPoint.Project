namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class EmailNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public EmailNotFoundException() { }
    public EmailNotFoundException(string message) : base(message) { }
    public EmailNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class MessageNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public MessageNotFoundException() { }
    public MessageNotFoundException(string message) : base(message) { }
    public MessageNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

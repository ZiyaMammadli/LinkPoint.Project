namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class ConversationNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public ConversationNotFoundException() { }
    public ConversationNotFoundException(string message) : base(message) { }
    public ConversationNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

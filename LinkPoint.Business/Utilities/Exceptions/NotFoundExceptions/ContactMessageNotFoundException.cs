namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class ContactMessageNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public ContactMessageNotFoundException() { }
    public ContactMessageNotFoundException(string message) : base(message) { }
    public ContactMessageNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

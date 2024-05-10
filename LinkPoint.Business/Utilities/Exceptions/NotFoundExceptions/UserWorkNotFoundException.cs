namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class UserWorkNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public UserWorkNotFoundException() { }
    public UserWorkNotFoundException(string message) : base(message) { }
    public UserWorkNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

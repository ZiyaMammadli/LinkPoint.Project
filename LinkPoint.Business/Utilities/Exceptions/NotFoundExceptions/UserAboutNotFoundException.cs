namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class UserAboutNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public UserAboutNotFoundException() { }
    public UserAboutNotFoundException(string message) : base(message) { }
    public UserAboutNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

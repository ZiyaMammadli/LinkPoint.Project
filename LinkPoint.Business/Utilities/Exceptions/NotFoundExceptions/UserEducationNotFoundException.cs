namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class UserEducationNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public UserEducationNotFoundException() { }
    public UserEducationNotFoundException(string message) : base(message) { }
    public UserEducationNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

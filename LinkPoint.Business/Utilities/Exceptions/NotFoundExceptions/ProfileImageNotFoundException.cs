namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class ProfileImageNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public ProfileImageNotFoundException() { }
    public ProfileImageNotFoundException(string message) : base(message) { }
    public ProfileImageNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

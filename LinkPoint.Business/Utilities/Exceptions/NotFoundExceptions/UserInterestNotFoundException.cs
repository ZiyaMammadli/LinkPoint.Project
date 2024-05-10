namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class UserInterestNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public UserInterestNotFoundException() { }
    public UserInterestNotFoundException(string message) : base(message) { }
    public UserInterestNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

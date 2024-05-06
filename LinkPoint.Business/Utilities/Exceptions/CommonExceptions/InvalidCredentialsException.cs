namespace LinkPoint.Business.Utilities.Exceptions.CommonExceptions;

public class InvalidCredentialsException:Exception
{
    public int StatusCode { get; set; }
    public InvalidCredentialsException() { }
    public InvalidCredentialsException(string message):base(message) { }
    public InvalidCredentialsException(int statusCode,string message):base(message)
    {
        StatusCode = statusCode;
    }
}

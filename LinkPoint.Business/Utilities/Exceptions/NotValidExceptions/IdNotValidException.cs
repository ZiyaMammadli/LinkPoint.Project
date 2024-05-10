namespace LinkPoint.Business.Utilities.Exceptions.NotValidExceptions;

public class IdNotValidException:Exception
{
    public int StatusCode { get; set; }
    public IdNotValidException() { }
    public IdNotValidException(string message) : base(message) { }
    public IdNotValidException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

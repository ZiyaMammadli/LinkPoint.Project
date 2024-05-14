namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class PostNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public PostNotFoundException() { }
    public PostNotFoundException(string message) : base(message) { }
    public PostNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

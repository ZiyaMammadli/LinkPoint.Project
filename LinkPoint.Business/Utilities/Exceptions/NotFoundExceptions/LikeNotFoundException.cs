namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class LikeNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public LikeNotFoundException() { }
    public LikeNotFoundException(string message) : base(message) { }
    public LikeNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

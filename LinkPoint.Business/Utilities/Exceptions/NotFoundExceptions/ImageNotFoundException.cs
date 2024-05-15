namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class ImageNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public ImageNotFoundException() { }
    public ImageNotFoundException(string message) : base(message) { }
    public ImageNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

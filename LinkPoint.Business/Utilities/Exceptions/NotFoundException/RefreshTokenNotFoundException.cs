namespace LinkPoint.Business.Utilities.Exceptions.NotFoundException;

public class RefreshTokenNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public RefreshTokenNotFoundException() { }
    public RefreshTokenNotFoundException(string message):base(message) { }

    public RefreshTokenNotFoundException(int statusCode,string message):base(message)
    {
        StatusCode = statusCode;
    }
}

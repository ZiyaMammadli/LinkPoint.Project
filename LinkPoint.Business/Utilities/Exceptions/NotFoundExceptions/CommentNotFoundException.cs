namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class CommentNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public CommentNotFoundException() { }
    public CommentNotFoundException(string message) : base(message) { }
    public CommentNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

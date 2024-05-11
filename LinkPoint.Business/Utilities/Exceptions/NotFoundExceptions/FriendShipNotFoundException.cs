namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class FriendShipNotFoundException : Exception
{
    public int StatusCode { get; set; }
    public FriendShipNotFoundException() { }
    public FriendShipNotFoundException(string message) : base(message) { }
    public FriendShipNotFoundException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}

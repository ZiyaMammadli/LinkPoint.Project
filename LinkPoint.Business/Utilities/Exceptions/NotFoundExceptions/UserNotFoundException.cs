namespace LinkPoint.Business.Utilities.Exceptions.NotFoundExceptions;

public class UserNotFoundException:Exception
{
    public int StatusCode { get; set; }
    public UserNotFoundException() { }
    public UserNotFoundException(string message):base(message) { }  
    public UserNotFoundException(int statusCode,string message):base(message) 
    {
        StatusCode = statusCode;
    }   
}

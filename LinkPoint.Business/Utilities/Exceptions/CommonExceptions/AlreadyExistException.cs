namespace LinkPoint.Business.Utilities.Exceptions.CommonExceptions;

public class AlreadyExistException:Exception
{
    public int StatusCode { get; set; }
    public AlreadyExistException() { }
    public AlreadyExistException(string message):base(message) { }
    public AlreadyExistException(int statusCode,string message):base(message) 
    {
        StatusCode = statusCode;
    }

}

namespace LinkPoint.Business.Utilities.Exceptions.CommonExceptions;

public class ValueNullException:Exception
{
    public ValueNullException() { }
    public ValueNullException(string message):base(message) { }
}

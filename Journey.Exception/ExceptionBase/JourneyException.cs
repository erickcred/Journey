namespace Journey.Exception.ExceptionBase;

public class JourneyException : SystemException
{
  public string Message { get; set; }

  public JourneyException(string message) => Message = message;
}

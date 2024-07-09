using System.Net;

namespace Journey.Exception.ExceptionBase;

public class ErrorOnValidationException : JourneyException
{
  public ErrorOnValidationException(string message) : base(message) { }

  public override HttpStatusCode GetStatusCode()
  {
    return HttpStatusCode.BadRequest;
  }
}

using System.Net;

namespace Journey.Exception.ExceptionBase;

public class ErrorOnValidationException : JourneyException
{
  private readonly IList<string> _errors;

  public ErrorOnValidationException(IList<string> messages) : base(string.Empty)
  {
    _errors = messages;
  }

  public override HttpStatusCode GetStatusCode()
  {
    return HttpStatusCode.BadRequest;
  }

  public override IList<string> GetErrorMessages()
  {
    return _errors;
  }
}

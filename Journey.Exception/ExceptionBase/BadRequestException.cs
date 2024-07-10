using System.Net;

namespace Journey.Exception.ExceptionBase;

public class BadRequestException : JourneyException
{
  public BadRequestException(string message) : base(message) { }

  public override HttpStatusCode GetStatusCode()
  {
    return HttpStatusCode.BadRequest;
  }

  public override IList<string> GetErrorMessages()
  {
    return [ Message ];
  }
}

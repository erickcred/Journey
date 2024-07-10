using Journey.Communication.Responses;
using Journey.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Journey.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    if (context.Exception is JourneyException)
    {
      var journeyException = (JourneyException)context.Exception;

      context.HttpContext.Response.StatusCode = (int)journeyException.GetStatusCode();

      var responseJson = new ResponseErrorJson(journeyException.GetErrorMessages());
      
      context.Result = new ObjectResult(responseJson);
    }
    else
    {
      context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

      var responseJson = new ResponseErrorJson(new List<string> { "Error desconhecido!" });
      
      context.Result = new ObjectResult(responseJson);
    }
  }
}
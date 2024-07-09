using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
  [HttpPost]
  public IActionResult Register([FromBody] RequestRegisterTripJson request)
  {
    var responseTrip = ForResponseTripJson(request);
    return Created(string.Empty, request);
  }

  #region Convetions ForResponses
  private ResponseTripJson ForResponseTripJson(RequestRegisterTripJson request)
  {
    var response = new ResponseTripJson
    {
      Id = Guid.NewGuid(),
      Name = request.Name,
      StartDate = request.StartDate,
      EndDate = request.EndDate,
    };
    return response;
  }
  #endregion
}

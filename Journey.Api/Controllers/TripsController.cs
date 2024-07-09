using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
  private readonly RegisterTripUseCase _resgisterTripUseCase;
  private readonly GetAllTripUseCase _GetAllTripUseCase;


  public TripsController(RegisterTripUseCase resgisterTripUseCase, GetAllTripUseCase getallTripUseCase)
  {
    _resgisterTripUseCase = resgisterTripUseCase;
    _GetAllTripUseCase = getallTripUseCase;
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    var trips = _GetAllTripUseCase.Execute();
    return Ok(trips);
  }

  [HttpPost]
  public IActionResult Register([FromBody] RequestRegisterTripJson request)
  {
    try
    {
      var response = _resgisterTripUseCase.Execute(request);

      return Created(string.Empty, response);
    }
    catch (JourneyException ex)
    {
      return BadRequest(ex.Message);
    }
    catch
    {
      return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido!");  
    }
  }
}

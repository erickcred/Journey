using Journey.Application.UseCases.Trips.Get;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
  private readonly RegisterTripUseCase _resgisterTripUseCase;
  private readonly GetAllTripUseCase _getAllTripUseCase;
  private readonly GetTripByIdUseCase _getTripUseCase;


  public TripsController(RegisterTripUseCase resgisterTripUseCase, GetAllTripUseCase getallTripUseCase, GetTripByIdUseCase getTripUseCase)
  {
    _resgisterTripUseCase = resgisterTripUseCase;
    _getAllTripUseCase = getallTripUseCase;
    _getTripUseCase = getTripUseCase;
  }

  [HttpGet]
  [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
  public IActionResult GetAll()
  {
    var trips = _getAllTripUseCase.Execute();
    return Ok(trips);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
  public IActionResult GetById([FromRoute] Guid id)
  {
    var trip = _getTripUseCase.Execute(id);
    return Ok(trip);
  }

  [HttpPost]
  [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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

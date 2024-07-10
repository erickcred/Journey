using Journey.Application.UseCases.Trips.Delete;
using Journey.Application.UseCases.Trips.Get;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
  private readonly RegisterTripUseCase _resgisterTripUseCase;
  private readonly GetAllTripUseCase _getAllTripUseCase;
  private readonly GetTripByIdUseCase _getTripUseCase;
  private readonly DeleteTripUseCase _deleteTripUseCase;


  public TripsController(RegisterTripUseCase resgisterTripUseCase, GetAllTripUseCase getallTripUseCase, GetTripByIdUseCase getTripUseCase, DeleteTripUseCase deleteTripUseCase)
  {
    _resgisterTripUseCase = resgisterTripUseCase;
    _getAllTripUseCase = getallTripUseCase;
    _getTripUseCase = getTripUseCase;
    _deleteTripUseCase = deleteTripUseCase;
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
  [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
  public IActionResult GetById([FromRoute] Guid id)
  {
    var trip = _getTripUseCase.Execute(id);
    return Ok(trip);
  }

  [HttpPost]
  [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
  public IActionResult Register([FromBody] RequestRegisterTripJson request)
  {
    var response = _resgisterTripUseCase.Execute(request);
    return Created(string.Empty, response);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
  public IActionResult Delete([FromRoute] Guid id)
  {
    var response = _deleteTripUseCase.Excecute(id);
    return Ok(response);
  }
}

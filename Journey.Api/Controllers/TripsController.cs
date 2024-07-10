using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Delete;
using Journey.Application.UseCases.Activities.Register;
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
  private readonly RegisterActivityForTripUseCase _registerActivityForTripUseCase;
  private readonly CompleteActivityForTripUseCase _completeActivityForTripUseCase;
  private readonly DeleteActivityForTripUseCase _deleteActivityForTripUseCase;


  public TripsController(
    RegisterTripUseCase resgisterTripUseCase,
    GetAllTripUseCase getallTripUseCase,
    GetTripByIdUseCase getTripUseCase,
    DeleteTripUseCase deleteTripUseCase,
    RegisterActivityForTripUseCase registerActivityForTripUseCase,
    CompleteActivityForTripUseCase completeActivityForTripUseCase,
    DeleteActivityForTripUseCase deleteActivityForTripUseCase)
  {
    _resgisterTripUseCase = resgisterTripUseCase;
    _getAllTripUseCase = getallTripUseCase;
    _getTripUseCase = getTripUseCase;
    _deleteTripUseCase = deleteTripUseCase;
    _registerActivityForTripUseCase = registerActivityForTripUseCase;
    _completeActivityForTripUseCase = completeActivityForTripUseCase;
    _deleteActivityForTripUseCase = deleteActivityForTripUseCase;
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
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public IActionResult GetById([FromRoute] Guid id)
  {
    var trip = _getTripUseCase.Execute(id);
    return Ok(trip);
  }

  [HttpPost]
  [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  public IActionResult Register([FromBody] RequestRegisterTripJson request)
  {
    var response = _resgisterTripUseCase.Execute(request);
    return Created(string.Empty, response);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public IActionResult Delete([FromRoute] Guid id)
  {
    var response = _deleteTripUseCase.Excecute(id);
    return Ok(response);
  }

  [HttpPost("{tripId}/activity")]
  [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public IActionResult RegisterActivity([FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson request)
  {
    var response = _registerActivityForTripUseCase.Execute(tripId, request);
    return Created(string.Empty, response);
  }

  [HttpPut("{tripId}/activity/{activityId}/complete")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public IActionResult CompliteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
  {
    _completeActivityForTripUseCase.Execute(tripId, activityId);
    return NoContent();
  }

  [HttpDelete("{tripId}/activity/{activityId}/delete")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
  public IActionResult DelteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
  {
    _deleteActivityForTripUseCase.Execute(tripId, activityId);
    return NoContent();
  }
}

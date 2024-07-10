using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases.Trips.Register;

public class RegisterTripUseCase
{
  private readonly JourneyContext _journeyContext;

  public RegisterTripUseCase(JourneyContext journeyContext)
  {
    _journeyContext = journeyContext;
  }

  public ResponseShortTripJson Execute(RequestRegisterTripJson request)
  {
    Validate(request);
    var result = RequestForEntity(request);
    var response = _journeyContext.Trips.Add(result);
    _journeyContext.SaveChanges();

    return EntityForResponse(response.Entity);
  }

  private void Validate(RequestRegisterTripJson request)
  {

    var validator = new RegisterTripValidator();
    var result = validator.Validate(request);
    if (result.IsValid == false)
    {
      var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }


  #region Conversões
  private Trip RequestForEntity(RequestRegisterTripJson request)
  {
    return new Trip { Name = request.Name, StartDate = request.StartDate, EndDate = request.EndDate };
  }

  private ResponseShortTripJson EntityForResponse(Trip entity)
  {
    return new ResponseShortTripJson
    {
      Id = entity.Id,
      Name = entity.Name,
      StartDate = entity.StartDate,
      EndDate = entity.EndDate
    };
  }
  #endregion
}

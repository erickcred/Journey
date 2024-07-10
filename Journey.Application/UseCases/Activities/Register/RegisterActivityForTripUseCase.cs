using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Journey.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Register;

public class RegisterActivityForTripUseCase
{
  private readonly JourneyContext _journeyContext;

  public RegisterActivityForTripUseCase(JourneyContext journeyContext)
  {
    _journeyContext = journeyContext;
  }

  public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
  {
    var trip = _journeyContext
      .Trips
      .AsNoTracking()
      .FirstOrDefault(t => t.Id.Equals(tripId));

    Validate(trip, request);

    var activity = new Activity
    {
      Name = request.Name,
      Date = request.Date,
      TripId = trip.Id,
      Status = ActivityStatus.Pending
    };

    _journeyContext.Activities.Add(activity);
    _journeyContext.SaveChanges();
    return new ResponseActivityJson
    {
      Id = activity.Id,
      Name = activity.Name,
      Date = activity.Date,
      Status = (Communication.Enums.ActivityStatus)activity.Status
    };
  }

  private void Validate(Trip? trip, RequestRegisterActivityJson request)
  {
    if (trip is null)
      throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);

    var validator = new RegisterActivityValidator();
    var result = validator.Validate(request);

    if ((request.Date >= trip.StartDate && request.Date <= trip.EndDate) == false)
    {
      result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATA_VISITA_INVALIDA));
    }

    if (result.IsValid == false)
    {
      var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
      throw new ErrorOnValidationException(errorMessages);
    }
  }
}

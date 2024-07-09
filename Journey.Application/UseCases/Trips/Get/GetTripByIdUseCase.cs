using Journey.Communication.Responses;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Get;

public class GetTripByIdUseCase
{
  private readonly JourneyContext _journeyContext;

  public GetTripByIdUseCase(JourneyContext journeyContext)
  {
    _journeyContext = journeyContext;
  }

  public ResponseTripJson Execute(Guid tripId)
  {
    var trip = _journeyContext
      .Trips
      .Include(a => a.Activities)
      .AsNoTracking()
      .FirstOrDefault(t => t.Id.Equals(tripId));

    return new ResponseTripJson
    {
      Id = trip.Id,
      Name = trip.Name,
      StartDate = trip.StartDate,
      EndDate = trip.EndDate,
      Activities = trip.Activities.Select(a => new ResponseActivityJson
      {
        Id = a.Id,
        Name = a.Name,
        Date = a.Date,
        Status = (Communication.Enums.ActivityStatus)a.Status
      }).ToList()
    };
  }
}

using Journey.Communication.Responses;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetAll;

public class GetAllTripUseCase
{
  private readonly JourneyContext _journeyContext;

  public GetAllTripUseCase(JourneyContext journeyContext)
  {
    _journeyContext = journeyContext;
  }

  public ResponseTripsJson Execute()
  {
    var trips = _journeyContext.Trips.AsNoTracking().ToList();
    return new ResponseTripsJson
    {
      Trips = trips.Select(t => new ResponseShortTripJson
      {
        Id = t.Id,
        Name = t.Name,
        StartDate = t.StartDate,
        EndDate = t.EndDate,
      }).ToList()
    };
  }

}

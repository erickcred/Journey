using Journey.Communication.Responses;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
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
    try
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
    catch (JourneyException ex)
    {
      throw new JourneyException(ex.Message);
    }
  }

}

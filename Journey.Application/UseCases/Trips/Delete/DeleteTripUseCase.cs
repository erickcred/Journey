using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Delete;

public class DeleteTripUseCase
{
  private readonly JourneyContext _journeyContext;

  public DeleteTripUseCase(JourneyContext journeyContext)
  {
    _journeyContext = journeyContext;
  }

  public ResponseTripJson Excecute(Guid tripId)
  {
    using var transaction = _journeyContext.Database.BeginTransaction();
    try
    {
      var trip = _journeyContext
        .Trips
        .Include(a => a.Activities)
        .FirstOrDefault(t => t.Id.Equals(tripId));

      if (trip is null)
        throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);

      _journeyContext.Trips.Remove(trip);
      _journeyContext.SaveChanges();
      transaction.Commit();

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
    catch (NotFoundException ex)
    {
      transaction.Rollback();
      throw new NotFoundException(ex.Message);
    }
    catch (ErrorOnValidationException ex)
    {
      transaction.Rollback();
      throw new ErrorOnValidationException(ex.Message);
    }
    catch
    {
      transaction.Rollback();
      throw new System.Exception();
    }
  }
}

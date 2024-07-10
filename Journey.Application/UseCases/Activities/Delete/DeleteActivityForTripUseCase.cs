using Journey.Exception;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Activities.Delete;

public class DeleteActivityForTripUseCase
{
  private readonly JourneyContext _journeyContext;

  public DeleteActivityForTripUseCase(JourneyContext journeyContext)
  {
    _journeyContext = journeyContext;
  }

  public void Execute(Guid tripId, Guid activityId)
  {
    var activity = _journeyContext
      .Activities
      .FirstOrDefault(a => a.Id.Equals(activityId) && a.TripId.Equals(tripId));

    if (activity is null)
      throw new NotFoundException(ResourceErrorMessages.ATIVIDADE_NAO_ENCONTRADA);

    _journeyContext.Remove(activity);
    _journeyContext.SaveChanges();
  }
}

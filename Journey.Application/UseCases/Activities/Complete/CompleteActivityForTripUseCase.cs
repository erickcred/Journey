using Journey.Exception;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases.Activities.Complete;

public class CompleteActivityForTripUseCase
{
  private readonly JourneyContext _journeyContext;

  public CompleteActivityForTripUseCase(JourneyContext journeyContext)
  {
    _journeyContext = journeyContext;
  }

  public void Execute(Guid tripId, Guid activityId)
  {
    var activity = _journeyContext
      .Activities
      .FirstOrDefault(a => a.Id.Equals(activityId) && a.TripId.Equals(tripId));

    if (activity is null)
      throw new NotFoundException(ResourceErrorMessages.VIAGEM_NAO_ENCONTRADA);

    activity.Status = ActivityStatus.Done;
    _journeyContext.Activities.Update(activity);
    _journeyContext.SaveChanges();
  }
}

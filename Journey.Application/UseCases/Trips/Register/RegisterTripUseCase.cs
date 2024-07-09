using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
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
    using var transaction = _journeyContext.Database.BeginTransaction();
    try
    { 
      Validate(request);
      var result = RequestForEntity(request);
      var response = _journeyContext.Trips.Add(result);
      _journeyContext.SaveChanges();

      transaction.Commit();
      return EntityForResponse(response.Entity);
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

  private void Validate(RequestRegisterTripJson request)
  {
    if (string.IsNullOrWhiteSpace(request.Name))
      throw new ErrorOnValidationException(ResourceErrorMessages.NOME_VAZIO);

    if (request.StartDate.Date < DateTime.UtcNow.Date)
      throw new ErrorOnValidationException(ResourceErrorMessages.DATA_MENOR_QUE_ATUAL);

    if (request.EndDate.Date < request.StartDate.Date)
      throw new ErrorOnValidationException(ResourceErrorMessages.DATA_FIM_MENOR_QUE_INICIO);
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

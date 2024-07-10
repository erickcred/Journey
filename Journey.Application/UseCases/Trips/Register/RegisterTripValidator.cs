using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Trips.Register;

public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
{
  public RegisterTripValidator()
  {
    RuleFor(request => request.Name)
      .NotEmpty()
      .WithMessage(ResourceErrorMessages.NOME_VAZIO);

    RuleFor(request => request.StartDate.Date)
      .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
      .WithMessage(ResourceErrorMessages.DATA_MENOR_QUE_ATUAL);

    RuleFor(request => request)
      .Must(request => request.EndDate.Date >= request.StartDate.Date)
      .WithMessage(ResourceErrorMessages.DATA_FIM_MENOR_QUE_INICIO);
  }
}

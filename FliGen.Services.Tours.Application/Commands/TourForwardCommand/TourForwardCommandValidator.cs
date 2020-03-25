using FluentValidation;

namespace FliGen.Services.Tours.Application.Commands.TourForwardCommand
{
    public sealed class TourForwardCommandValidator : AbstractValidator<TourForwardCommand>
    {
        public TourForwardCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на изменение статуса тура");
        }
    }
}

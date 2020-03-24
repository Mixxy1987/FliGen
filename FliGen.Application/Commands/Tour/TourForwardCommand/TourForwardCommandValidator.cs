using FluentValidation;

namespace FliGen.Application.Commands.Tour.TourForwardCommand
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

using FluentValidation;

namespace FliGen.Services.Tours.Application.Commands.TourCancelCommand
{
    public sealed class TourCancelCommandValidator : AbstractValidator<TourCancelCommand>
    {
        public TourCancelCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на отмену тура");
        }
    }
}

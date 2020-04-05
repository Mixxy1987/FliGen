using FluentValidation;

namespace FliGen.Services.Tours.Application.Commands.TourForward
{
    public sealed class TourForwardValidator : AbstractValidator<TourForward>
    {
        public TourForwardValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на изменение статуса тура");
        }
    }
}

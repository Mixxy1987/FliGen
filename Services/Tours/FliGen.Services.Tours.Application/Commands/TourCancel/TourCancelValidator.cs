using FluentValidation;

namespace FliGen.Services.Tours.Application.Commands.TourCancel
{
    public sealed class TourCancelValidator : AbstractValidator<TourCancel>
    {
        public TourCancelValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на отмену тура");
        }
    }
}

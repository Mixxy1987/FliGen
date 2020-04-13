using FluentValidation;

namespace FliGen.Services.Tours.Application.Commands.TourBack
{
    public sealed class TourBackValidator : AbstractValidator<TourBack>
    {
        public TourBackValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на откат тура");
        }
    }
}

using FluentValidation;

namespace FliGen.Application.Commands.Tour.AddTour
{
    public sealed class AddTourCommandValidator : AbstractValidator<AddTourCommand>
    {
        public AddTourCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на создание тура");
        }
    }
}

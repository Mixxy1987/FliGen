using System;
using FluentValidation;

namespace FliGen.Services.Tours.Application.Commands.PlayerRegisterOnTour
{
    public sealed class PlayerRegisterOnTourValidator : AbstractValidator<PlayerRegisterOnTour>
    {
        public PlayerRegisterOnTourValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на регистрацию");
            RuleFor(c => c.RegistrationDate)
                .Must(x => DateTime.TryParse(x, out _))
                .WithMessage("Invalid datetime format for registration date");
        }
    }
}

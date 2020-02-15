using FluentValidation;

namespace FliGen.Application.Commands.League.CreateLeague
{
    public sealed class CreateLeagueCommandValidator : AbstractValidator<CreateLeagueCommand>
    {
        public CreateLeagueCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на создание лиги");
        }
    }
}

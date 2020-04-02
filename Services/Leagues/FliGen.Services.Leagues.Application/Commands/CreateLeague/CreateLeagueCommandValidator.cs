using FluentValidation;

namespace FliGen.Services.Leagues.Application.Commands.CreateLeague
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

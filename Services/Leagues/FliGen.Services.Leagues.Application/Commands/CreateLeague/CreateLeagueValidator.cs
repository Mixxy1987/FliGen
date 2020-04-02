using FluentValidation;

namespace FliGen.Services.Leagues.Application.Commands.CreateLeague
{
    public sealed class CreateLeagueValidator : AbstractValidator<CreateLeague>
    {
        public CreateLeagueValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на создание лиги");
        }
    }
}

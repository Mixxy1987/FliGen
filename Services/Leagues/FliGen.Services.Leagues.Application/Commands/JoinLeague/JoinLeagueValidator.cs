using FluentValidation;

namespace FliGen.Services.Leagues.Application.Commands.JoinLeague
{
    public sealed class JoinLeagueValidator : AbstractValidator<JoinLeague>
    {
        public JoinLeagueValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда вступление в лигу");
        }
    }
}

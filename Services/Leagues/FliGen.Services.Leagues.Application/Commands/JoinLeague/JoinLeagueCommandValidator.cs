using FluentValidation;

namespace FliGen.Services.Leagues.Application.Commands.JoinLeague
{
    public sealed class JoinLeagueCommandValidator : AbstractValidator<JoinLeagueCommand>
    {
        public JoinLeagueCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда вступление в лигу");
        }
    }
}

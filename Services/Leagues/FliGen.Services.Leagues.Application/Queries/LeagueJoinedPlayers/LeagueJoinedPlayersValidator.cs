using FluentValidation;

namespace FliGen.Services.Leagues.Application.Queries.LeagueJoinedPlayers
{
    public sealed class LeagueJoinedPlayersValidator : AbstractValidator<LeagueJoinedPlayers>
    {
        public LeagueJoinedPlayersValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задан запрос на получения списка игроков");

            RuleFor(c => c.LeagueId)
                .NotEmpty()
                .WithMessage("Не задана id лиги");
        }
    }
}

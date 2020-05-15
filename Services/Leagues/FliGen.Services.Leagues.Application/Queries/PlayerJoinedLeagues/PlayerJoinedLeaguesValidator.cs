using FluentValidation;

namespace FliGen.Services.Leagues.Application.Queries.PlayerJoinedLeagues
{
    public sealed class PlayerJoinedLeaguesValidator : AbstractValidator<PlayerJoinedLeagues>
    {
        public PlayerJoinedLeaguesValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задан запрос на получения списка лиг для игрока");
        }
    }
}

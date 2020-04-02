using FluentValidation;

namespace FliGen.Services.Players.Application.Queries.Players
{
    public sealed class PlayersQueryValidator : AbstractValidator<PlayersQuery>
    {
        public PlayersQueryValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана запрос на получение списка игроков");

            RuleFor(c => c.QueryType)
                .IsInEnum()
                .WithMessage("Невалидное значение QueryType");
        }
    }
}

using FluentValidation;

namespace FliGen.Services.Leagues.Application.Queries.Leagues
{
    public sealed class LeaguesQueryValidator : AbstractValidator<LeaguesQuery>
    {
        public LeaguesQueryValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на создание лиги");
        }
    }
}

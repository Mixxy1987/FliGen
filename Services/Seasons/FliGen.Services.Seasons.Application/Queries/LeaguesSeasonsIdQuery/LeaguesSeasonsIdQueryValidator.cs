using FluentValidation;

namespace FliGen.Services.Seasons.Application.Queries.LeaguesSeasonsIdQuery
{
    public sealed class LeaguesSeasonsIdQueryValidator : AbstractValidator<LeaguesSeasonsIdQuery>
    {
        public LeaguesSeasonsIdQueryValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задан запрос на получение информации о сезонах");
        }
    }
}

using FluentValidation;

namespace FliGen.Services.Tours.Application.Queries.SeasonStats
{
    public sealed class SeasonStatsQueryValidator : AbstractValidator<SeasonStatsQuery>
    {
        public SeasonStatsQueryValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задан запрос на получение статистики о сезонах");
        }
    }
}

using FluentValidation;

namespace FliGen.Services.Seasons.Application.Queries.Seasons
{
    public sealed class SeasonsQueryValidator : AbstractValidator<SeasonsQuery>
    {
        public SeasonsQueryValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задан запрос на получение информации о сезонах");

        }
    }
}

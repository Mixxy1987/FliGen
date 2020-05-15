using FluentValidation;

namespace FliGen.Services.Seasons.Application.Queries.LeaguesIdBySeasonsId
{
    public sealed class LeaguesIdBySeasonsIdQueryValidator : AbstractValidator<LeaguesIdBySeasonsIdQuery>
    {
        public LeaguesIdBySeasonsIdQueryValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задан запрос на получение информации о сезонах");

            RuleFor(c => c.SeasonsId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Не заданы id сезонов");
        }
    }
}

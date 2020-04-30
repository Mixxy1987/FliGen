using FluentValidation;

namespace FliGen.Services.Leagues.Application.Queries.LeagueInformation
{
    public sealed class LeagueInformationValidator : AbstractValidator<LeagueInformationQuery>
    {
        public LeagueInformationValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана запрос на получение информации о лиге");
        }
    }
}

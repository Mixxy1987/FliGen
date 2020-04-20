using FluentValidation;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeague
{
    public sealed class UpdateLeagueValidator : AbstractValidator<UpdateLeague>
    {
        public UpdateLeagueValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на обновление лиги");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Не задано имя лиги");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("Не задано описание лиги");

            RuleFor(c => c.LeagueType)
                .NotEmpty()
                .WithMessage("Не задан тип лиги");
        }
    }
}

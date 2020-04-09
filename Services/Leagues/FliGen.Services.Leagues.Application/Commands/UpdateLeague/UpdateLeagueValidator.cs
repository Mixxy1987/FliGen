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
        }
    }
}

using FluentValidation;

namespace FliGen.Services.Leagues.Application.Commands.UpdateLeague
{
    public sealed class UpdateLeagueCommandValidator : AbstractValidator<UpdateLeagueCommand>
    {
        public UpdateLeagueCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на обновление лиги");
        }
    }
}

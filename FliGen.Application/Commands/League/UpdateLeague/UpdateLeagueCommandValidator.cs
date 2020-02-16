using FluentValidation;

namespace FliGen.Application.Commands.League.UpdateLeague
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

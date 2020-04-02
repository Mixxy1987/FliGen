using FluentValidation;

namespace FliGen.Services.Leagues.Application.Commands.DeleteLeague
{
    public sealed class DeleteLeagueCommandValidator : AbstractValidator<DeleteLeagueCommand>
    {
        public DeleteLeagueCommandValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на удаление лиги");
        }
    }
}

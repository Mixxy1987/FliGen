using FluentValidation;

namespace FliGen.Application.Commands.League.DeleteLeague
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

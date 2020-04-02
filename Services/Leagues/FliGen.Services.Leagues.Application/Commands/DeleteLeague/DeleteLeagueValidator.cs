using FluentValidation;

namespace FliGen.Services.Leagues.Application.Commands.DeleteLeague
{
    public sealed class DeleteLeagueValidator : AbstractValidator<DeleteLeague>
    {
        public DeleteLeagueValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на удаление лиги");
        }
    }
}

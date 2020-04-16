using FluentValidation;

namespace FliGen.Services.Players.Application.Commands.DeletePlayer
{
    public sealed class DeletePlayerValidator : AbstractValidator<DeletePlayer>
    {
        public DeletePlayerValidator()
        {
            RuleFor(c => c)
                .NotEmpty()
                .WithMessage("Не задана команда на удаление игрока");
        }
    }
}
